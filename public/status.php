<?php

// require_once '../_inc/cla_pdb.php';

$query = PDB::$conn->prepare('SELECT idx, title, date FROM pro_tasks WHERE idx = ?');
$query->execute([ $_GET['tid'] ]);
$task = $query->fetch(PDB::FETCH_ASSOC);

$query = PDB::$conn->prepare('SELECT submit.uid, submit.fid, submit.compile, submit.size, submit.date, ' .
    'member.gen, member.name FROM pro_submit submit ' .
    'LEFT JOIN pro_members member ON (submit.uid = member.id) ' .
    'WHERE submit.tid = ? ORDER BY submit.date DESC');
$query->execute([ $_GET['tid'] ]);
$submits = $query->fetchAll(PDB::FETCH_ASSOC);

?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title>프로 과제 채점</title>
    <link rel="stylesheet" href="bower_components/codemirror/lib/codemirror.css">
    <link rel="stylesheet" href="bower_components/codemirror/theme/mdn-like.css">
    <link rel="stylesheet" href="https://fonts.googleapis.com/earlyaccess/notosanskr.css">
    <style>
        body {
            margin: 3em .5em 0 .5em;
            line-height: 1.6;
            font-size: 16px;
            font-family: 'Noto Sans KR', 'Malgun Gothic', sans-serif;
        }
        header {
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
            text-align: right;
        }
            header span {
                float: left;
            }
            header time {
                float: right;
                font-size: initial;
                font-weight: initial;
            }
        #dest {
            position: fixed;
            right: 0;
            top: 40px;
            bottom: 0;
            width: calc(100% - 400px);
        }
        #dest .CodeMirror {
            height: 100%;
        }
    </style>
<body>
    <header><span><?=htmlspecialchars($task['title'])?></span><button>&#8606;</button></header>
    <table>
        <thead><tr><th>기수<th>이름<th>컴파일<th>코드 길이<th>제출한 시간
        <tbody>
<?php
        foreach ($submits as $submit)
        {?>
            <tr>
                <td><?=$submit['gen']?>기
                <td><?=$submit['name']?>

                <td><?=[ 'x', 'o' ][$submit['compile']]?>

                <td><a href="source.php?<?=implode('&amp;', [
                        'tid=' . $task['idx'],
                        'uid=' . $submit['uid'],
                        'fid=' . $submit['fid'],
                    ])?>"><?=$submit['size']?>B</a>
                <td><?=$submit['date']?>

<?php
        }?>
    </table>
    <div id="dest"></div>
    <script src="bower_components/jquery/dist/jquery.min.js"></script>
    <script src="bower_components/codemirror/lib/codemirror.js"></script>
    <script src="bower_components/codemirror/mode/clike/clike.js"></script>
    <script>
        var xhr;
        $('a').on('click', function(e) {
            e.preventDefault();
            if (xhr)
            {
                xhr.abort();
            }
            xhr = $.ajax({
                    url: this,
                }).done(function(src) {
                    CodeMirror($('#dest').empty()[0], {
                        value: src,
                        indentUnit: 4,
                        mode: 'clike',
                        readOnly: true,
                        lineNumbers: true,
                        theme: 'mdn-like',
                        lineWrapping: true
                    });
                });
        });
        $('button').on('click', function(e) {
            e.preventDefault();
            window.location.href = 'task.php';
        })
    </script>
</html>