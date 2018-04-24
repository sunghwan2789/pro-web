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
        for ($i = 1; file_exists($tmp = implode('', [ $tmpb, $i, '.c' ])); $i++);
        $params = [ 'id'=> $taskId, 'uid'=> $_SESSION['uid'], 'fid'=> $i ];

        $fp = fopen($tmp, 'wb');
        if ($fp === false)
        {
            throw new \Exception('cannot open file');
        }
        fwrite($fp, pack('C', 0xEF));
        fwrite($fp, pack('C', 0xBB));
        fwrite($fp, pack('C', 0xBF));
        $params['size'] = fwrite($fp, $_POST['source']);
        fclose($fp);

        $val = null;
        exec(implode(' ', [ dirname(dirname(__DIR__)) . '/bin/validate.bat', escapeshellarg($tmp) ]), $val, $val);
        $val = !($params['compile'] = $val == 0) * 2;

        $query = $this->db->prepare('INSERT INTO pro_submit (tid, uid, fid, compile, size) ' .
            'VALUES (:id, :uid, :fid, :compile, :size) ' .
            'ON DUPLICATE KEY UPDATE date = NOW(), compile = :compile, size = :size');
        /*echo*/ !$query->execute($params) ? $val ? $val : 3 : $val;

        return $response
            ->withStatus(303)
            ->withHeader('Location',
                $this->router->pathFor('source.search') . '?task_id=' . $taskId);
    }

    /** GET /{source_id} */
    public function show(Request $request, Response $response, array $args)
    {
        $sourceId = $args['source_id'];

        $query = $this->db->prepare('SELECT * FROM pro_submit WHERE source_id = ?');
        $query->bindValue(1, $sourceId);
        $query->execute();
        $source = $query->fetch();

        if ($source === false) {
            throw new \Exception('not found');
        }

        $this->view->render($response, 'sources/show.phtml', [
            'source' => $source,
            'stream' => $this->getSourceStream($source),
        ]);
        return $response;
    }

    private function getSourceStream($source)
    {
        $basePath = $this->config->get('storage.sources');
        $path = $basePath . "/{$source['tid']}_{$source['uid']}_{$source['fid']}.c";
        if (stripos(realpath($path), realpath($basePath) !== 0)) {
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
            . 'member.gen, member.name, submit.tid FROM pro_submit submit '
            . 'LEFT JOIN pro_members member ON (submit.uid = member.id) ';
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
