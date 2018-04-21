<?php

require dirname(__DIR__) . '/vendor/autoload.php';

(new josegonzalez\Dotenv\Loader(dirname(__DIR__) . '/.env'))->parse()->putenv();
$config = Northwoods\Config\ConfigFactory::make([
    'directory' => dirname(__DIR__) . '/config' . '/',
]);


$app = new Slim\App($config->get('slim'));


$container = $app->getContainer();

$container['config'] = function ($c) use ($config) {
    return $config;
};

$container['db'] = function ($c) {
    $pdo = new PDO(getenv('DB_DSN'), getenv('DB_USERNAME'), getenv('DB_PASSWORD'));
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $pdo->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);
    return $pdo;
};

$container['view'] = function ($c) {
    return new Slim\Views\PhpRenderer(
        dirname(__DIR__) . '/resources/views/',
        [
            'router' => $c->router,
            'request' => $c->request,
            'response' => $c->response,
        ]
    );
};


$app->add(App\Middleware\CheckValidUser::class);
$app->add(App\Middleware\Authenticate::class);


$app->get('/', App\Controllers\HomeController::class . ':index');
$app->group('/auth', function () {
    $this->post('/login', App\Controllers\AuthController::class . ':login')->setName('auth-login');
});


$app->run();
