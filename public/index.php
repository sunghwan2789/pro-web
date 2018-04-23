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
$app->get('/login', App\Controllers\SessionController::class . ':login')->setName('login');
$app->post('/login', App\Controllers\SessionController::class . ':store')->setName('session.store');
$app->post('/register', App\Controllers\SessionController::class . ':register')->setName('session.register');
$app->get('/logout', App\Controllers\SessionController::class . ':destroy')->setName('logout');
$app->group('/activities', function () {
    $this->get('/new', App\Controllers\ActivitiesController::class . ':new')->setName('activity.new');
    $this->post('/new', App\Controllers\ActivitiesController::class . ':store')->setName('activity.store');
    $this->get('/archive[/{year}[/{month}[/{date}]]]', App\Controllers\ActivitiesController::class . ':list')->setName('activity.list');
    $this->get('/{activityId}', App\Controllers\ActivitiesController::class . ':show')->setName('activity.show');
    $this->get('/{activityId}/edit', App\Controllers\ActivitiesController::class . ':edit');
    $this->post('/{activityId}/put', App\Controllers\ActivitiesController::class . ':update');
    $this->post('/{activityId}/delete', App\Controllers\ActivitiesController::class . ':destroy');
});
$app->group('/tasks', function () {
    $this->get('', App\Controllers\TasksController::class . ':index')->setName('task.index');
    $this->get('/new', App\Controllers\TasksController::class . ':new')->setName('task.new');
    $this->post('/new', App\Controllers\TasksController::class . ':store')->setName('task.store');
    $this->get('/{taskId}', App\Controllers\TasksController::class . ':show')->setName('task.show');
    $this->get('/{taskId}/edit', App\Controllers\TasksController::class . ':edit');
    $this->post('/{taskId}/put', App\Controllers\TasksController::class . ':update');
    $this->post('/{taskId}/delete', App\Controllers\TasksController::class . ':destroy');
});
$app->group('/sources', function () {
    $this->get('', App\Controllers\SourcesController::class . ':index')->setName('source.index');
    $this->get('/new[/{taskId}]', App\Controllers\SourcesController::class . ':new');
    $this->post('/new[/{taskId}]', App\Controllers\SourcesController::class . ':store');
    $this->get('/{sourceId}', App\Controllers\SourcesController::class . ':show');
    $this->post('/{sourceId}/vote', App\Controllers\SourcesController::class . ':voteup');
    $this->post('/{sourceId}/vote/delete', App\Controllers\SourcesController::class . ':votedown');
    $this->get('/search', App\Controllers\SourcesController::class . ':search');
});


$app->run();
