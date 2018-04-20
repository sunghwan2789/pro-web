<?php

// require_once '../_inc/cla_pdb.php';

$query = PDB::$conn->prepare(
    'SELECT a.idx, a.start, a.end, a.purpose, a.content, a.place, c.gen, c.name ' .
    'FROM pro_activities a ' .
    'LEFT JOIN pro_activity_attend b ON (a.idx = b.aid) ' .
    'LEFT JOIN pro_members c ON (b.uid = c.id) ' .
    'ORDER BY a.idx DESC'
);
$query->execute();
$activitiesAttends = $query->fetchAll(PDB::FETCH_ASSOC);

?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title>프로 활동일지</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/earlyaccess/notosanskr.css">
    <style>
        body {
            margin: 3em .5em 0 .5em;
            line-height: 1.6;
            font-size: 16px;
            font-family: 'Noto Sans KR', 'Malgun Gothic', sans-serif;
        }
        body > header {
            position: fixed;
            top: 0;
            left: 0;
            background: #efefef;
            border-bottom: 1px solid #ccc;
            line-height: 2.5em;
            width: 100%;
            padding: 0 .5em;
            box-sizing: border-box;
            white-space: nowrap;
            font-weight: bold;
        }
        header a {
            float: right;
            text-decoration: none;
        }
    </style>
<body>
    <header>프로 활동일지 <a href="task.php">과제</a></header>
<?php


$p = 0;
foreach ($activitiesAttends as $i)
{
    if ($p != $i['idx'])
    {
        if ($p)
        {
?>

    </ul>
    <hr>
<?php
        }
        $p = $i['idx'];
        $start = strtotime($i['start']);
        $end = strtotime($i['end']);
?>
    <h2><?=htmlspecialchars($i['purpose'])?></h2>
    @<?=$i['place']?> #<time><?=date('Y-m-d H:i', $start)?></time> ~ <?=date((date('d', $start) != date('d', $end) ? 'Y-m-d ' : '') . 'H:i', $end)?>

    <p><?=nl2br(htmlspecialchars($i['content']))?>


    <h3>첨부 파일</h3>
    <ul>
<?php
        $query = PDB::$conn->prepare('SELECT md5, name FROM pro_activity_attach WHERE aid = ?');
        $query->execute([ $i['idx'] ]);
        foreach ($query->fetchAll(PDB::FETCH_ASSOC) as $j)
        {
            echo '<li><a href="download.php?id=', $j['md5'], '&amp;name=', urlencode($j['name']), '">',
                 htmlspecialchars($j['name']), '</a>';
        }
?>

    </ul>

    <h3>참여 회원</h3>
    <ul>
<?php
    }
    echo '<li>', $i['gen'], '기 ', htmlspecialchars($i['name']);
}
if ($p)
{
?>

    </ul>
<?php
}


?>
</html>