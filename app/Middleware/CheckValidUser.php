<?php
namespace App\Middleware;

use Psr\Container\ContainerInterface;
use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;

class CheckValidUser
{
    protected $container;
    public function __construct(ContainerInterface $container)
    {
        $this->container = $container;
    }

    public function __invoke(Request $req, Response $res, callable $next)
    {
        if (isset($req->getCookieParams()[session_name()])) {
            session_start();
            if (isset($_SESSION['uid'])) {
                return $next($req, $res);
            }
        }

        $this->container->view->render($res, 'login.phtml', ['initiated'=>true]);
        return $res;
    }
}
