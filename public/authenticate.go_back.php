<?php
$err = '정보를 제대로 입력했나요?';
if (DEBUG)
{
    $err = implode("\n" . '자세한 오류: ', [ $err, $details ]);
}
?>
<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <script>
        'use strict';
        alert(`<?=$err?>`);
        history.back();
    </script>
    <style>
        html {
            background: #fff;
        }
        body {
            position: absolute;
            top: 50%; left: 50%;
            transform: translate(-50%, -50%);
            white-space: pre;
            text-align: center;
        }
    </style>
<body><?=$err?></body>
</html>