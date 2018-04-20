<?php

// require_once '../_inc/cla_pdb.php';

$query = PDB::$conn->prepare('SELECT authority FROM pro_members WHERE id = ?');
$query->bindValue(1, $_SESSION['uid']);
$query->execute();
if ($query->fetchColumn() > 0)
{
    echo '권한 없음';
    exit;
}

// 최근 1년간 참여 횟수로 정렬됨
$query = PDB::$conn->prepare(
    'SELECT a.id, a.gen, a.name FROM pro_members a ' .
    'LEFT JOIN (' .
        'SELECT c.uid, COUNT(*) AS attends ' .
        'FROM pro_activities b ' .
        'LEFT JOIN pro_activity_attend c ON (b.idx = c.aid) ' .
        'WHERE b.end > DATE_SUB(CURDATE(), INTERVAL 2 MONTH) ' .
        'GROUP BY c.uid' .
    ') d ON (a.id = d.uid) '.
    'ORDER BY a.authority ASC, d.attends DESC, a.gen DESC, a.id ASC'
);
$query->execute();
$members = $query->fetchAll(PDB::FETCH_ASSOC);

?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title>프로 활동일지 작성</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/earlyaccess/notosanskr.css">
    <style>
        body {
            margin: 3em .5em 0 .5em;
            line-height: 1.6;
        }
        body, input, button, textarea, select {
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
        span {
            float: left;
        }
        header button {
            vertical-align: middle;
        }
        label {
            display: block;
            margin-bottom: .5em;
        }
        textarea {
            vertical-align: top;
            display: block;
            width: 100%;
            box-sizing: border-box;
        }
        textarea[name=content] {
            height: 300px;
        }
        .span {
            margin-top: -.5em;
            display: block;
            width: 100%;
            margin-bottom: 1em;
        }
        li {
            cursor: pointer;
        }
        li:hover {
            background: #f5f5f5;
        }
        li:hover::after {
            content: '\2717';
            float: right;
        }
    </style>
<body>
    <form action="submit.php" method="POST">
        <header><span>프로 활동일지 작성</span><button type="submit">&#10004;</button></header>
        <label>모임장소: <input name="place" type="text" placeholder="M502"></label>
        <label>시작: <input name="start" type="text" placeholder="2015-04-13 15:00" pattern="\d{4}-\d{2}-\d{2} \d{2}:\d{2}" required></label>
        <label>종료: <input name="end" type="text" placeholder="2015-04-13 16:00" pattern="\d{4}-\d{2}-\d{2} \d{2}:\d{2}" required></label>
        <ul><li>누르면 지워집니다.</ul><!-- ul > li > input[name=attend[]] -->
        <label>참여 회원: <select><option></option><?php
            foreach ($members as $member)
            {
                echo '<option value="', $member['id'], '">',
                     $member['id'], ' ', $member['gen'], '기 ', $member['name'];
            }
        ?></select></label>
        <label>목적: <textarea name="purpose" rows="1" required></textarea></label><button class="span">늘이기</button>
        <label>내용: <textarea name="content"></textarea></label><button class="span">늘이기</button>
        <label>파일 첨부: <input name="attach[]" type="file" multiple></label>
    </form>
    <script src="bower_components/jquery/dist/jquery.min.js"></script>
    <script>
        $('select').on('change', function(e) {
            e.preventDefault();
            var option = $(this.selectedOptions[0]);
            if (!option.val().length)
            {
                return;
            }
            $(this).val('');
            option.hide().prop('tab-index', -1);

            $('ul').append(
                $('<li>').text(option.text())
                    .append($('<input>', { name: 'attend[]', value: option.val(), type: 'hidden' })));
        });
        $(document).on('click', 'li', function(e) {
            e.preventDefault();
            $('option[value=' + $('input', this).val() + ']').show().removeProp('tab-index');
            $(this).remove();
        });
        $('[name=purpose]').on('keypress', function(e) {
            if (e.keyCode == 13) // ENTER
            {
                e.preventDefault();
            }
        });
        $('.span').on('click', function(e) {
            e.preventDefault();
            var target = $('textarea', $(this).prev());
            target.height(target.height() + 150);
        });
        $('[name=start]').on('input propertychange', function() {
            $('[name=end]').val($(this).val());
        });
        $('form').on('submit', function(e) {
            e.preventDefault();
            if (!confirm('활동 정보를 제대로 기입했나요?'))
            {
                return;
            }
            $.ajax({
                type: 'POST',
                url: this.action,
                data: new FormData(this),
                processData: false,
                contentType: false,
            }).done(function()
            {
                window.location.replace('./');
            })
        });
    </script>
</html>