<?php
namespace App\Controllers;

use Psr\Container\ContainerInterface;
use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;

class ActivityController
{
    /** @var \Psr\Container\ContainerInterface */
    protected $container;

    /** @var \PDO */
    protected $db;

    /** @var \Slim\Views\PhpRenderer */
    protected $view;

    /** @var \Slim\Router */
    protected $router;

    public function __construct(ContainerInterface $container)
    {
        $this->container = $container;
        $this->db = $container['db'];
        $this->view = $container['view'];
        $this->router = $container['router'];
    }

    /** GET /new */
    public function new(Request $request, Response $response)
    {
        $get = $request->getQueryParams();
        $this->view->render($response, 'session/login.phtml', [
            'registered' => isset($get['registered']),
            'return' => strval($get['return']),
        ]);
        return $response;
    }
}
