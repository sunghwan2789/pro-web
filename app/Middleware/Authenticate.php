<?php
namespace App\Middleware;

use Psr\Container\ContainerInterface;
use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;

/**
 * 로그인 상태 검사
 *
 * 세션 쿠키를 확인해서 로그인한 유저만 요청을 허용한다.
 * 필요하면 로그인 페이지로 리다이렉션을 진행한다.
 */
class Authenticate
{
    /** @var \Psr\Container\ContainerInterface */
    protected $container;

    /** @var \Slim\Router */
    protected $router;

    public function __construct(ContainerInterface $container)
    {
        $this->container = $container;
        $this->router = $container['router'];
    }

    public function __invoke(Request $request, Response $response, callable $next)
    {
        // 이미 로그인 상태이면 통과
        if ($this->isValidUser($request)) {
            return $next($request, $response);
        }

        $loginUrl = $this->router->pathFor('login');

        $whitelist = [
            $loginUrl,
            $this->router->pathFor('session.store'),
            $this->router->pathFor('session.register'),
        ];

        // 로그인 페이지 접속 중이면 통과
        if (in_array($request->getUri()->getPath(), $whitelist)) {
            return $next($request, $response);
        }

        // 손님은 로그인 페이지로 안내
        return $response
            ->withStatus(303)
            ->withHeader('Location', $loginUrl . '?return=' . \rawurlencode($request->getRequestTarget()));
    }

    /**
     * 세션 상태를 확인하여 로그인한 유저인지 검사한다.
     *
     * @return bool
     */
    private function isValidUser(Request $request)
    {
        $isValid = false;
        // 세션 쿠키 존재유무 확인
        if (isset($_COOKIE[session_name()])) {
            // 로그인했는지 확인
            session_start();
            $isValid = isset($_SESSION['uid']);
            session_write_close();
        }
        return $isValid;
    }
}
