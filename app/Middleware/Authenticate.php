<?php
namespace App\Middleware;

use \Slim\Container as ContainerInterface;
use \Psr\Http\Message\ServerRequestInterface as Request;
use \Psr\Http\Message\ResponseInterface as Response;

class Authenticate
{
    protected $container;
    public function __construct(ContainerInterface $container)
    {
        $this->container = $container;
    }

    public function __invoke(Request $req, Response $res, callable $next)
    {
        $res->getBody()->write('ffff');
        $res = $next($req, $res);
        return $res;
    }
}
