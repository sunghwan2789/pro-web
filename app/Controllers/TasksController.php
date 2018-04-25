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
            'SELECT idx, title, date FROM pro_tasks'
            . ' WHERE date BETWEEN :start AND :end OR date BETWEEN :start AND :end'
            // order by endDate desc
            . ' ORDER BY date DESC'
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
