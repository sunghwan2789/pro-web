<?php
namespace App\Controllers;

use Psr\Container\ContainerInterface;
use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;

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

    /** GET /tasks */
    public function index(Request $request, Response $response)
    {
        $todayStr = date('Y-m-d');

        $query = $this->db->prepare(
            'SELECT idx, title, date FROM pro_tasks'
            . ' WHERE date BETWEEN :start AND :end OR date BETWEEN :start AND :end'
            // order by endDate desc
            . ' ORDER BY date DESC'
        );
        $query->bindParam(':start', $todayStr);
        $query->bindParam(':end', $todayStr);
        $query->execute();
        $tasks = $query->fetchAll();

        $this->view->render($response, 'tasks/index.phtml', [
            'tasks' => $tasks,
        ]);
        return $response;
    }
}
