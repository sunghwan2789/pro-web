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


$middleware = [
    App\Middleware\Authenticate::class,
];
foreach (array_reverse($middleware) as $mw) {
    $app->add($mw);
}


$app->get('/', App\Controllers\HomeController::class . ':index');
$app->group('/session', function () {
    $this->get('/login', App\Controllers\SessionController::class . ':login')->setName('session.login');
    $this->post('/signin', App\Controllers\SessionController::class . ':signin')->setName('session.signin');
    $this->post('/register', App\Controllers\SessionController::class . ':register')->setName('session.register');
    $this->get('/logout', App\Controllers\SessionController::class . ':logout')->setName('session.logout');
});
$app->group('/activity', function () {
    $this->get('/', App\Controllers\ActivityController::class . ':index');
    $this->get('/new', App\Controllers\ActivityController::class . ':new')->setName('activity.new');
    $this->post('/post', App\Controllers\ActivityController::class . ':post')->setName('activity.post');
    $this->get('/{activityNumber}', App\Controllers\ActivityController::class . ':show');
    $this->post('/{activityNumber}/edit', App\Controllers\ActivityController::class . ':edit');
    $this->post('/{activityNumber}/delete', App\Controllers\ActivityController::class . ':delete');
});


$app->run();
