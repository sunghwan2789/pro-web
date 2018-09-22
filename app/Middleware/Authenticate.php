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
        if ($this->isAuthenticated($request)) {
            // 로그인 유저가 로그인 시도하면 차단
            if ($this->isAuthenticating($request)) {
                // throw new \Exception('already logged in');
                return $response
                    ->withStatus(303)
                    ->withHeader('Location',
                        $request->getQueryParams()['return']);
            }
            return $next($request, $response);
        }

        // 로그인 페이지 접속 중이면 통과
        if ($this->isAuthenticating($request)) {
            return $next($request, $response);
        }

        // 손님은 로그인 페이지로 안내
        return $response
            ->withStatus(303)
            ->withHeader('Location',
                $this->router->pathFor('login')
                . '?return=' . \rawurlencode($request->getRequestTarget()));
    }

    /**
     * 루트를 검사하여 로그인 시도인지 확인한다.
     */
    private function isAuthenticating(Request $request): bool
    {
        /** @var \Slim\Route */
        $route = $request->getAttribute('route');

        $whitelist = [
            'login',
            'session.store',
            'session.register',
        ];

        return $route && in_array($route->getName(), $whitelist);
    }

    /**
     * 세션을 검사하여 로그인 상태인지 확인한다.
     */
    private function isAuthenticated(Request $request): bool
    {
        $isAuthenticated = false;
        // 세션 쿠키 존재유무 확인
        session_name(getenv('APP_SESSION_NAME'));
        if (isset($_COOKIE[session_name()])) {
            // 로그인했는지 확인
            session_start(['cookie_path' => getenv('APP_BASE_PATH')]);
            $isAuthenticated = isset($_SESSION['uid']);
            session_write_close();
        }
        return $isAuthenticated;
    }
}
