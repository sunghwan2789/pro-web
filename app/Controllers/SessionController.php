<?php
namespace App\Controllers;

use Psr\Container\ContainerInterface;
use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;

class SessionController
{
    /** @var \Psr\Container\ContainerInterface */
    protected $container;

    /** @var \Slim\Views\PhpRenderer */
    protected $view;

    public function __construct(ContainerInterface $container)
    {
        $this->container = $container;
        $this->view = $container['view'];
    }

    public function login(Request $request, Response $response)
    {
        $this->view->render($response, 'login.phtml');
        return $response;
    }

    public function signin(Request $request, Rseponse $response)
    {
        return $response;
    }

    public function register(Request $request, Rseponse $response)
    {
        return $response;
    }

    public function logout(Request $request, Rseponse $response)
    {
        return $response;
    }
}
