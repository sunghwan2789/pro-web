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
            'config' => $c->config,
            'basePath' => getenv('APP_BASE_PATH'),
        ]
    );
};


$app->get('/', App\Controllers\HomeController::class . ':index')->setName('root');
$app->get('/login', App\Controllers\SessionController::class . ':login')->setName('login');
$app->post('/login', App\Controllers\SessionController::class . ':store')->setName('session.store');
$app->post('/register', App\Controllers\SessionController::class . ':register')->setName('session.register');
$app->post('/logout', App\Controllers\SessionController::class . ':destroy')->setName('logout');
$app->group('/activities', function () {
    $this->get('/new', App\Controllers\ActivitiesController::class . ':new')->setName('activity.new');
    $this->post('/new', App\Controllers\ActivitiesController::class . ':store')->setName('activity.store');
    $this->get('/archive[/{year}[/{month}]]', App\Controllers\ActivitiesController::class . ':list')->setName('activity.list');
    $this->get('/{activity_id}', App\Controllers\ActivitiesController::class . ':show')->setName('activity.show');
    $this->get('/{activity_id}/edit', App\Controllers\ActivitiesController::class . ':edit');
    $this->post('/{activity_id}/put', App\Controllers\ActivitiesController::class . ':update');
    $this->post('/{activity_id}/delete', App\Controllers\ActivitiesController::class . ':destroy');
});
$app->get('/files/{file_id}', App\Controllers\FilesController::class . ':download')->setName('file.download');
$app->group('/tasks', function () {
    $this->get('', App\Controllers\TasksController::class . ':index')->setName('task.index');
    $this->get('/new', App\Controllers\TasksController::class . ':new')->setName('task.new');
    $this->post('/new', App\Controllers\TasksController::class . ':store')->setName('task.store');
    $this->get('/archive[/{year}[/{month}]]', App\Controllers\TasksController::class . ':list')->setName('task.list');
    $this->get('/{task_id}', App\Controllers\TasksController::class . ':show')->setName('task.show');
    $this->get('/{task_id}/edit', App\Controllers\TasksController::class . ':edit');
    $this->post('/{task_id}/put', App\Controllers\TasksController::class . ':update');
    $this->post('/{task_id}/delete', App\Controllers\TasksController::class . ':destroy');
});
$app->group('/sources', function () {
    $this->get('', App\Controllers\SourcesController::class . ':index')->setName('source.index');
    $this->get('/new/{task_id}', App\Controllers\SourcesController::class . ':new')->setName('source.new');
    $this->post('/new/{task_id}', App\Controllers\SourcesController::class . ':store')->setName('source.store');
    $this->get('/search', App\Controllers\SourcesController::class . ':search')->setName('source.search');
    $this->get('/{source_id}', App\Controllers\SourcesController::class . ':show')->setName('source.show');
    $this->get('/{source_id}/raw', App\Controllers\SourcesController::class .':raw')->setName('source.raw');
    $this->post('/{source_id}/vote', App\Controllers\SourcesController::class . ':voteup');
    $this->post('/{source_id}/vote/delete', App\Controllers\SourcesController::class . ':votedown');
});
$app->post('/markdown/render', function ($req, $res) use ($app) {
    $app->getContainer()->view->render($res, 'markdown/preview.phtml', [
        'content' => $req->getParsedBodyParam('content'),
    ]);
    return $res;
});


$app->add(App\Middleware\Authenticate::class);


$app->run();
