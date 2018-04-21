<?php
namespace App\Controllers;

use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;

class HomeController extends Controller
{
    public function index(Request $req, Response $res, array $args)
    {
        $this->container->view->render($res, 'home.phtml');
        return $res;
    }
}
