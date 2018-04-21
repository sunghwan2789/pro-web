<?php
namespace App\Middleware;

use Psr\Container\ContainerInterface;
use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;

class Authenticate
{
    protected $container;
    public function __construct(ContainerInterface $container)
    {
        $this->container = $container;
    }

    public function __invoke(Request $req, Response $res, callable $next)
    {
        if ($req->getUri()->getPath() === $this->container->router->pathFor('auth-login')) {
            $req->withCookieParams(session_name());
        }
        return $next($req, $res);
    }
}
