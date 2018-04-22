<?php
namespace App\Controllers;

use Psr\Container\ContainerInterface;
use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;

class SessionController
{
    /** @var \Psr\Container\ContainerInterface */
    protected $container;

    /** @var \PDO */
    protected $db;

    /** @var \Slim\Views\PhpRenderer */
    protected $view;

    /** @var \Slim\Router */
    protected $router;

    public function __construct(ContainerInterface $container)
    {
        $this->container = $container;
        $this->db = $container['db'];
        $this->view = $container['view'];
        $this->router = $container['router'];
    }

    public function login(Request $request, Response $response)
    {
        if (isset($_COOKIE[session_name()]))
        {
            session_start();
            if (isset($_SESSION['uid']))
            {
                throw new \Exception('already logged in');
            }
            session_write_close();
        }

        $params = $request->getQueryParams();
        $this->view->render($response, 'session/login.phtml', [
            'registered' => isset($params['registered']),
            'return' => strval($params['return']),
        ]);
        return $response;
    }

    public function store(Request $request, Response $response)
    {
        if (isset($_COOKIE[session_name()]))
        {
            session_start();
            if (isset($_SESSION['uid']))
            {
                throw new \Exception('already logged in');
            }
            session_write_close();
        }

        $uri = $request->getUri();
        $body = $request->getParsedBody();
        // TODO: 에러 처리
        if (is_null($body)) {
            return $response;
        }

        $query = $this->db->prepare('SELECT pw FROM pro_members WHERE id = ?');
        $query->bindValue(1, $body['id']);
        $query->execute();
        $pw = $query->fetchColumn();

        if ($pw === false) {
            throw new \Exception('id does not exist');
        }
        if (is_null($pw)) {
            $this->view->render($response, 'session/register.phtml', [
                'return' => strval($body['return']),
                'id' => strval($body['id']),
            ]);
            return $response;
        }
        if (!hash_equals($pw, crypt(static::encrypt(strval($body['pw'])), $pw))) {
            throw new \Exception('pw is wrong');
        }

        session_set_cookie_params(
            60 * 60 * 24 * 7 * 4 * ($body['keep'] === 'on'),
            '/',
            $uri->getHost(),
            false,
            true);
        session_start();
        $_SESSION['uid'] = $body['id'];

        $query = $this->db->prepare('INSERT INTO pro_member_log SET uid = ?, `text` = ?');
        $query->bindValue(1, $body['id']);
        $query->bindValue(2, 'LOGIN');
        $query->execute();

        return $response
            ->withStatus(303)
            ->withHeader('Location', strval($body['return']));
    }

    public function register(Request $request, Response $response)
    {
        if (isset($_COOKIE[session_name()]))
        {
            session_start();
            if (isset($_SESSION['uid']))
            {
                throw new \Exception('already logged in');
            }
            session_write_close();
        }

        if ($_POST['pw'] !== $_POST['pwc']) {
            throw new \Exception('pw does not equal to pwc');
        }

        $query = $this->db->prepare('SELECT 1 FROM pro_members WHERE id = ? AND name = ? AND contact = ? AND pw IS NULL');
        $query->bindValue(1, $_POST['id']);
        $query->bindValue(2, $_POST['name']);
        $query->bindValue(3, \preg_replace('/\D/', '', strval($_POST['tel'])));
        $query->execute();
        if (!$query->fetchColumn()) {
            throw new \Exception('authentication failed or pw is already set');
        }

        $query = $this->db->prepare('UPDATE pro_members SET pw = ? WHERE id = ?');
        $query->bindValue(1, static::generate_hash(static::encrypt(strval($_POST['pw']))));
        $query->bindValue(2, $_POST['id']);
        $query->execute();

        $query = $this->db->prepare('INSERT INTO pro_member_log SET uid = ?, `text` = ?');
        $query->bindValue(1, $_POST['id']);
        $query->bindValue(2, 'INITIALIZE');
        $query->execute();

        return $response
            ->withStatus(303)
            ->withHeader('Location', $this->router->pathFor('login') . '?registered=1&return=' . \rawurlencode(strval($_POST['return'])));
    }

    public function destroy(Request $request, Response $response)
    {
        if (isset($_COOKIE[session_name()]))
        {
            session_start();
            $_SESSION = [];
            setcookie(session_name(), null, -1,
                '/', $request->getUri()->getHost(),
                false, true);
            session_destroy();
        }
        $this->view->render($response, 'session/logout.phtml');
        return $response;
    }

    private static function encrypt(string $val)
    {
        return sha1('y#.fij/|lP&!79.Txcf]' . sha1($val . 'y#.fij/|lP&!79.Txcf]') . $val);
    }

    /*
     * Generate a secure hash for a given password. The cost is passed
     * to the blowfish algorithm. Check the PHP manual page for crypt to
     * find more information about this setting.
     */
    private static function generate_hash($password, $cost=11)
    {
        /* To generate the salt, first generate enough random bytes. Because
         * base64 returns one character for each 6 bits, the we should generate
         * at least 22*6/8=16.5 bytes, so we generate 17. Then we get the first
         * 22 base64 characters
         */
        // $salt=substr(base64_encode(openssl_random_pseudo_bytes(17)),0,22);
        $salt=substr(base64_encode(random_bytes(17)),0,22);
        /* As blowfish takes a salt with the alphabet ./A-Za-z0-9 we have to
         * replace any '+' in the base64 string with '.'. We don't have to do
         * anything about the '=', as this only occurs when the b64 string is
         * padded, which is always after the first 22 characters.
         */
        $salt=str_replace("+",".",$salt);
        /* Next, create a string that will be passed to crypt, containing all
         * of the settings, separated by dollar signs
         */
        $param='$'.implode('$',array(
            "2y", //select the most secure version of blowfish (>=PHP 5.3.7)
            str_pad($cost,2,"0",STR_PAD_LEFT), //add the cost in two digits
            $salt //add the salt
        ));

        //now do the actual hashing
        return crypt($password,$param);
    }
}
