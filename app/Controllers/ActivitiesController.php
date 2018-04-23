<?php
namespace App\Controllers;

use Psr\Container\ContainerInterface;
use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;

class ActivitiesController
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

    /** GET /new */
    public function new(Request $request, Response $response)
    {
        $query = $this->db->prepare('SELECT authority FROM pro_members WHERE id = ?');
        $query->bindValue(1, $_SESSION['uid']);
        $query->execute();
        if ($query->fetchColumn() > 0)
        {
            echo '권한 없음';
            return $response;
        }

        // 최근 1년간 참여 횟수로 정렬됨
        $query = $this->db->prepare(
            'SELECT a.id, a.gen, a.name FROM pro_members a '
            . 'LEFT JOIN ('
                . 'SELECT c.uid, COUNT(*) AS attends '
                . 'FROM pro_activities b '
                . 'LEFT JOIN pro_activity_attend c ON (b.idx = c.aid) '
                . 'WHERE b.end > DATE_SUB(CURDATE(), INTERVAL 2 MONTH) '
                . 'GROUP BY c.uid'
            . ') d ON (a.id = d.uid) '
            . 'ORDER BY a.authority ASC, d.attends DESC, a.gen DESC, a.id ASC'
        );
        $query->execute();
        $members = $query->fetchAll();

        $this->view->render($response, 'activities/new.phtml', [
            'members' => $members,
        ]);
        return $response;
    }

    /** POST /new */
    public function store(Request $request, Response $response)
    {
        $query = $this->db->prepare('SELECT authority FROM pro_members WHERE id = ?');
        $query->bindValue(1, $_SESSION['uid']);
        $query->execute();
        if ($query->fetchColumn() > 0)
        {
            echo '권한 없음';
            return $response;
        }

        $query = $this->db->prepare(
            'INSERT INTO pro_activities (start, end, purpose, content, place, uid) ' .
            'VALUES (?, ?, ?, ?, ?, ?)'
        );
        $query->bindValue(1, $_POST['start']);
        $query->bindValue(2, $_POST['end']);
        $query->bindValue(3, $_POST['purpose']);
        $query->bindValue(4, $_POST['content']);
        $query->bindValue(5, $_POST['place']);
        $query->bindValue(6, $_SESSION['uid']);
        $query->execute();

        $activityId = $this->db->lastInsertId();

        $query = str_repeat('(' . $activityId . ', ?),', count($_POST['attend']));
        $query = $this->db->prepare(
            'INSERT INTO pro_activity_attend (aid, uid) ' .
            'VALUES ' . substr($query, 0, strlen($query) - 1)
        );
        $query->execute($_POST['attend']);

        for ($i = 0; $i < count($_FILES['attach']['name']); $i++)
        {
            $path = $_FILES['attach']['tmp_name'][$i];
            $filename = $_FILES['attach']['name'][$i];
            if (!file_exists($path))
            {
                continue;
            }

            $fileId = md5_file($path);
            if (!@move_uploaded_file($path, $this->config->get('storage.attaches') . '/' . $fileId))
            {
                continue;
            }

            $query = $this->db->prepare(
                'INSERT INTO pro_activity_attach (aid, md5, name) ' .
                'VALUES (:aid, :md5, :name)'
            );
            $params = [];
            $params['aid'] = $activityId;
            $params['md5'] = $fileId;
            $params['name'] = $filename;
            $query->execute($params);

            @unlink($path);
        }
        return $response
            ->withStatus(303)
            ->withHeader('Location', $this->router->pathFor('activity.show', [
                'activityId' => $activityId,
            ]));
    }
}
