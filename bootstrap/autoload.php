<?php

require_once dirname(__DIR__) . '/vendor/autoload.php';

$config = Northwoods\Config\ConfigFactory::make([
    'directory' => dirname(__DIR__) . '/config',
]);

(new josegonzalez\Dotenv\Loader(dirname(__DIR__) . '/.env'))->parse()->putenv();

spl_autoload_register(function ($name) {
    include dirname(__DIR__) . '/_inc/cla_'.strtolower($name).'.php';
});
