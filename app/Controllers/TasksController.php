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

        $query = $this->db->prepare('SELECT * FROM pro_tasks WHERE id = ?');
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
}
