<?php
namespace App\Controllers;

use Psr\Container\ContainerInterface;
use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;
use DateTime;

class TasksController
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

    /** GET /{task_id} */
    public function show(Request $request, Response $response, array $args)
    {
        $taskId = $args['task_id'];

        $query = $this->db->prepare('SELECT * FROM pro_tasks WHERE idx = ?');
        $query->bindValue(1, $taskId);
        $query->execute();
        $task = $query->fetch();

        if ($task === false) {
            throw new \Exception('not found');
        }

        $this->view->render($response, 'tasks/show.phtml', [
            'task' => $task,
        ]);
        return $response;
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

        $this->view->render($response, 'tasks/new.phtml');
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
            'INSERT INTO pro_tasks (`start`, `end`, title, content, `in`, `out`) ' .
            'VALUES (?, ?, ?, ?, ?, ?)'
        );
        $query->bindValue(1, $_POST['start']);
        $query->bindValue(2, $_POST['end']);
        $query->bindValue(3, $_POST['title']);
        $query->bindValue(4, $_POST['content']);
        $query->bindValue(5, $_POST['in']);
        $query->bindValue(6, $_POST['out']);
        $query->execute();

        $taskId = $this->db->lastInsertId();

        return $response
            ->withStatus(303)
            ->withHeader('Location', $this->router->pathFor('task.show', [
                'task_id' => $taskId,
            ]));
    }

    /** GET / */
    public function index(Request $request, Response $response)
    {
        $todayStr = date('Y-m-d');

        $query = $this->db->prepare(
            'SELECT idx, title, `start`, `end` FROM pro_tasks'
            . ' WHERE end >= :end'
            // order by endDate desc
            . ' ORDER BY end ASC'
        );
        $query->bindParam(':end', $todayStr);
        $query->execute();
        $tasks = $query->fetchAll();

        $this->view->render($response, 'tasks/index.phtml', [
            'tasks' => $tasks,
        ]);
        return $response;
    }

    /** GET /archive[/{year}[/{month}]] */
    public function list(Request $request, Response $response, array $args)
    {
        $startDate = (new DateTime())->setTimestamp(0);
        $endDate = new DateTime();
        if (!empty($args['year'])) {
            $startDate->setDate($args['year'], 1, 1);
            $endDate = (clone $startDate)->modify('+1 year');
            if (!empty($args['month'])) {
                $startDate->setDate($args['year'], $args['month'], 1);
                $endDate = (clone $startDate)->modify('+1 month')->modify('-1 day');
            }
        }
        $startDateStr = $startDate->format('Y-m-d');
        $endDateStr = $endDate->format('Y-m-d');

        $query = $this->db->prepare(
            'SELECT idx, title, start, end FROM pro_tasks'
            . ' WHERE start BETWEEN :start AND :end OR end BETWEEN :start AND :end'
            // order by endDate desc
            . ' ORDER BY start DESC'
        );
        $query->bindParam(':start', $startDateStr);
        $query->bindParam(':end', $endDateStr);
        $query->execute();
        $tasks = $query->fetchAll();

        $this->view->render($response, 'tasks/archive.phtml', [
            'displayYear' => $args['year'],
            'displayMonth' => $args['month'],
            'shouldDisplayMonthFilter' => !empty($args['year']),
            'tasks' => $tasks,
        ]);
        return $response;
    }
}
