<?php
namespace App\Controllers;

use Psr\Container\ContainerInterface;
use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;

class ActivitiesController
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

    /** GET /new */
    public function new(Request $request, Response $response)
    {
        $query = $this->db->prepare('SELECT authority FROM pro_members WHERE id = ?');
        $query->bindValue(1, $_SESSION['uid']);
        $query->execute();
        if ($query->fetchColumn() > 0)
        {
            echo '권한 없음';
            return $response;
        }

        // 최근 1년간 참여 횟수로 정렬됨
        $query = $this->db->prepare(
            'SELECT a.id, a.gen, a.name FROM pro_members a '
            . 'LEFT JOIN ('
                . 'SELECT c.uid, COUNT(*) AS attends '
                . 'FROM pro_activities b '
                . 'LEFT JOIN pro_activity_attend c ON (b.idx = c.aid) '
                . 'WHERE b.end > DATE_SUB(CURDATE(), INTERVAL 2 MONTH) '
                . 'GROUP BY c.uid'
            . ') d ON (a.id = d.uid) '
            . 'ORDER BY a.authority ASC, d.attends DESC, a.gen DESC, a.id ASC'
        );
        $query->execute();
        $members = $query->fetchAll();

        $this->view->render($response, 'activities/new.phtml', [
            'members' => $members,
        ]);
        return $response;
    }
}
