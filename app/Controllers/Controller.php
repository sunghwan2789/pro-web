<?php
namespace App\Controllers;

use Psr\Container\ContainerInterface;

class Controller
{
    protected $container;

    public function __construct(ContainerInterface $container)
    {
        $this->container = $container;
    }
}
