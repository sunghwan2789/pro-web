<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title>프로 로그인</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/earlyaccess/notosanskr.css">
    <style>
        *,
        *::before,
        *::after {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }
        button::-moz-focus-inner {
            border: 0;
            padding: 0;
        }
        html {
            background: #fff;
        }
        body {
            position: absolute;
            top: 50%; left: 50%;
            transform: translate(-50%,-50%);
            font-family: 'Noto Sans KR', sans-serif;
            font-size: 14px;
            line-height: 1.6;
            color: #333;
            width: 300px;
            margin-bottom: 40px;
        }
        input,
        button,
        select,
        textarea {
            font-family: inherit;
            font-size: inherit;
            line-height: inherit;
        }
    </style>
    <style>
        img,
        aside,
        form > * {
            display: block;
            width: 100%;
        }
        input,
        button {
            height: 32px;
            vertical-align: middle;
        }
        button {
            cursor: pointer;
        }
        aside {
            border: 2px solid #f7d200;
            background: #f2fc35;
            margin-bottom: .5em;
            text-align: justify;
        }
            aside button {
                float: right;
                width: 32px;
                border: 0;
                padding: 0 0 2px 2px;
                background: #f7d200;
                display: none;
            }
        body.js aside {
            position: fixed;
            box-shadow: .2em .4em .4em rgba(0, 0, 0, .5);
        }
            body.js aside button {
                display: block;
            }
        input {
            margin-bottom: -1px;
        }
        input[type=checkbox] {
            margin-top: 1px;
        }
    </style>
<body>
    <img src="_data/logo-pro-2.png" alt="프로 로고">
<?php
if (preg_match('/\/authenticate\.php$/', $_SERVER['HTTP_REFERER']))
{
?>
    <aside>
        <button>&#10005;</button>
        <p>계정 설정을 완료했습니다.
        <p>다시 로그인하세요.
    </aside>
<?php
}
?>
    <form action="authenticate.php" method="POST">
        <input name="return" type="hidden" value="<?=htmlentities($_SERVER['REQUEST_URI'])?>">
        <input name="id" type="text" placeholder="아이디">
        <input name="pw" type="password" placeholder="비밀번호">
        <label><input name="keep" type="checkbox"> 로그인 상태 유지</label>
        <button>로그인</button>
    </form>
    <script>
        document.querySelector('body').classList.add('js');
        document.querySelector('aside button').addEventListener('click', function() {
            this.parentNode.remove();
        });
    </script>
</html>