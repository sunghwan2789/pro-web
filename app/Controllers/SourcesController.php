<?php
namespace App\Controllers;

use Psr\Container\ContainerInterface;
use Slim\Http\Request;
use Psr\Http\Message\ResponseInterface as Response;
use Slim\Http\Stream;

class SourcesController
{
    /** @var \Psr\Container\ContainerInterface */
    protected $container;

    /** @var \Northwoods\Config\ConfigInterface */
    protected $config;

    /** @var \PDO */
    protected $db;

    /** @var \Slim\Views\PhpRenderer */
    protected $view;

    /** @var \Slim\Router */
    protected $router;

    public function __construct(ContainerInterface $container)
    {
        $this->container = $container;
        $this->config = $container['config'];
        $this->db = $container['db'];
        $this->view = $container['view'];
        $this->router = $container['router'];
    }

    /** GET /new/{task_id} */
    public function new(Request $request, Response $response, array $args)
    {
        $taskId = $args['task_id'];

        $query = $this->db->prepare('SELECT * FROM pro_tasks WHERE idx = ?');
        $query->bindValue(1, $taskId);
        $query->execute();
        $task = $query->fetch();

        if ($task === false) {
            throw new \Exception('not found');
        }

        $this->view->render($response, 'sources/new.phtml', [
            'task' => $task,
        ]);
        return $response;
    }

    /** POST /new/{task_id} */
    public function store(Request $request, Response $response, array $args)
    {
        $taskId = $args['task_id'];

        $query = $this->db->prepare('SELECT * FROM pro_tasks WHERE idx = ?');
        $query->bindValue(1, $taskId);
        $query->execute();
        $task = $query->fetch();

        if ($task === false) {
            throw new \Exception('not found');
        }

        $tmpb = implode('', [ $this->config->get('storage.sources') . '/', $taskId, '_', $_SESSION['uid'], '_' ]);
        for ($i = 1; file_exists($sourcePath = implode('', [ $tmpb, $i, '.c' ])); $i++) {}
        $params = [ 'id'=> $taskId, 'uid'=> $_SESSION['uid'], 'fid'=> $i ];

        $fp = fopen($sourcePath, 'wb');
        if ($fp === false) {
            throw new \Exception('cannot open file');
        }
        fwrite($fp, pack('C', 0xEF));
        fwrite($fp, pack('C', 0xBB));
        fwrite($fp, pack('C', 0xBF));
        $params['size'] = fwrite($fp, $_POST['source']);
        fclose($fp);

        $SUCCESS = 0;
        $COMPILE_ERROR = 1;
        $RUNTIME_ERROR = 2;
        $PARTIAL_SUCCESS = 3;

        $errorPath = tempnam(sys_get_temp_dir(), 'PRO_TEST');
        $objPath = $errorPath . '.obj';
        $exePath = $errorPath . '.exe';
        exec(implode(' ', [
            __DIR__ . '/../../bin/compile.bat',
            escapeshellarg($sourcePath),
            escapeshellarg($exePath),
            escapeshellarg($objPath),
            escapeshellarg($errorPath),
        ]), $output, $exitCode);

        $params['error'] = implode("\n", array_slice(explode("\n", file_get_contents($errorPath)), 1));
        $params['compile'] = 1;
        $params['status'] = $PARTIAL_SUCCESS;
        $params['score'] = 0;
        $this->insertOrUpdate($params);

        if ($exitCode != 0) {
            $params['compile'] = 0;
            $params['status'] = $COMPILE_ERROR;
            goto SUBMIT;
        }

        // 프로그램 채점
        $query = $this->db->prepare(
            'SELECT `score`, `input`, `output`
            FROM pro_task_tests WHERE `task_id` = ?
            ORDER BY `score` ASC'
        );
        $query->bindValue(1, $taskId);
        $query->execute();
        $inputPath = tempnam(sys_get_temp_dir(), 'PRE');
        foreach ($query->fetchAll() as $row) {
            file_put_contents($inputPath, $row['input']);

            $output = [];
            exec(implode(' ' , [
                escapeshellcmd($exePath),
                '<' . escapeshellarg($inputPath),
            ]), $output, $exitCode);

            if ($exitCode != 0) {
                $params['status'] = $RUNTIME_ERROR;
                goto SUBMIT;
            }

            // 우측 공백 제거
            $expectedOutput = array_map('rtrim', explode("\n", $row['output']));
            $output = array_map('rtrim', $output);

            for ($i = 0; $i < count($expectedOutput); $i++) {
                if ($expectedOutput[$i] !== $output[$i]) {
                    goto SUBMIT;
                }
            }
            // 마지막 공백 줄이 포함되었으면 판단 후 패스
            if ($i != count($output)) {
                if ($i + 1 == count($output) && empty($output[$i])) {
                    // noop
                } else {
                    goto SUBMIT;
                }
            }

            $params['score'] = $row['score'];
            $this->insertOrUpdate($params);
        }

        $params['status'] = $SUCCESS;
SUBMIT:
        $this->insertOrUpdate($params);

        // cleanup
        @unlink($inputPath);
        @unlink($errorPath);
        @unlink($objPath);
        @unlink($exePath);

        return $response
            ->withStatus(303)
            ->withHeader('Location',
                $this->router->pathFor('source.search') . '?task_id=' . $taskId);
    }

    private function insertOrUpdate($params)
    {
        $query = $this->db->prepare(
            'INSERT INTO pro_submit (tid, uid, fid, compile, size, `status`, `error`, `score`)
            VALUES (:id, :uid, :fid, :compile, :size, :status, :error, :score)
            ON DUPLICATE KEY UPDATE `status` = :status, `score` = :score'
        );
        $query->execute($params);
    }

    /** GET /{source_id} */
    public function show(Request $request, Response $response, array $args)
    {
        $sourceId = $args['source_id'];

        $query = $this->db->prepare(
            'SELECT s.source_id, s.uid, s.tid, s.fid, s.compile, s.size, s.date, '
            . ' m.gen, m.name, t.title FROM pro_submit s '
            . ' LEFT JOIN pro_members m ON (m.id = s.uid) '
            . ' LEFT JOIN pro_tasks t ON (t.idx = s.tid) '
            . ' WHERE source_id = ?');
        $query->bindValue(1, $sourceId);
        $query->execute();
        $source = $query->fetch();

        if ($source === false) {
            throw new \Exception('not found');
        }

        $stream = $this->getSourceStream($source);
        // BOM 마크 건너뛰기
        $stream->seek(3);
        $this->view->render($response, 'sources/show.phtml', [
            'source' => $source,
            'stream' => $stream,
        ]);
        return $response;
    }

    private function getSourceStream($source): Stream
    {
        $basePath = $this->config->get('storage.sources');
        $path = $basePath . "/{$source['tid']}_{$source['uid']}_{$source['fid']}.c";
        if (stripos(realpath($path), realpath($basePath)) !== 0) {
            throw new \Exception('LFI guard');
        }
        return new Stream(fopen($path, 'r'));
    }

    /** GET /{source_id}/raw */
    public function raw(Request $request, Response $response, array $args)
    {
        $sourceId = $args['source_id'];

        $query = $this->db->prepare('SELECT * FROM pro_submit WHERE source_id = ?');
        $query->bindValue(1, $sourceId);
        $query->execute();
        $source = $query->fetch();

        if ($source === false) {
            throw new \Exception('not found');
        }

        return $response
            ->withHeader('Content-Type', 'text/plain; charset=UTF-8')
            ->withBody($this->getSourceStream($source));
    }

    /**
     * GET /search
     *
     * 쿼리 문자열 목록:
     * * task_id: 과제로 필터
     * * user_id: 학번으로 필터
     * * is_compiled: 컴파일 가능 여부로 필터
     * * review_status: 관리자 확인 여부로 필터
     */
    // TODO: review_status 추가하기
    public function search(Request $request, Response $response)
    {
        $taskId = $request->getQueryParam('task_id');
        $userId = $request->getQueryParam('user_id');
        $isCompiled = $request->getQueryParam('is_compiled');
        $reviewStatus = $request->getQueryParam('review_status');

        $sql = 'SELECT submit.source_id, submit.uid, submit.fid, submit.compile, submit.size, submit.date, '
            . 'member.gen, member.name, submit.tid, task.title FROM pro_submit submit '
            . 'LEFT JOIN pro_members member ON (submit.uid = member.id) '
            . ' LEFT JOIN pro_tasks task ON (submit.tid = task.idx) ';
        $conds = [];
        $params = [];
        if (!is_null($taskId)) {
            $conds[] = 'submit.tid = ?';
            $params[] = $taskId;
        }
        if (!is_null($userId)) {
            $conds[] = 'submit.uid = ?';
            $params[] = $userId;
        }
        if (count($conds)) {
            $sql .= ' WHERE ' . \implode(' AND ', $conds);
        }
        $sql .= ' ORDER BY submit.source_id DESC';

        $query = $this->db->prepare($sql);
        $query->execute($params);
        $sources = $query->fetchAll();

        $this->view->render($response, 'sources/search.phtml', [
            'sources' => $sources,
        ]);
        return $response;
    }
}
