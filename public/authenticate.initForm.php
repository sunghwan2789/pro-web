<!DOCTYPE html>
<html lang="ko">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title>프로 계정 설정</title>
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
        h3 {
            margin-top: .5em;
            font-size: 1.2em;
        }
        input[readonly] {
            background: #eee;
        }
        input.wrong {
            box-shadow: 0 0 1em #f30;
            transition: initial;
        }
    </style>
<body>
    <img src="_data/logo-pro-2.png" alt="프로 로고">
    <aside>
        <button>&#10005;</button>
        <p>누리집을 이용하려면 계정에 비밀번호를 설정해야 합니다. 본인 확인을 위해 필요한 정보를 모두 입력하고 계정 설정을 완료하세요.
        <p>* 본인 확인이 안 되면 관리자에게 문의 바랍니다.
    </aside>
    <form action="authenticate.php" method="POST">
        <input name="action" type="hidden" value="initialize">
        <input name="return" type="hidden" value="<?=htmlentities(strval($_POST['return']))?>">
        <h3>비밀번호 설정</h3>
        <input name="id" type="text" placeholder="아이디" value="<?=htmlentities(strval($_POST['id']))?>" required readonly>
        <input name="pw" type="password" placeholder="비밀번호" required>
        <input name="pwc" type="password" placeholder="비밀번호 확인" required>
        <h3>본인 확인</h3>
        <input name="name" type="text" placeholder="성명" required>
        <input name="tel" type="text" placeholder="휴대폰 번호" required>
        <label><input type="checkbox" required> 위 정보는 모두 본인이 입력하였음.</label>
        <button>계정 설정 완료</button>
    </form>
    <script>
        document.querySelector('body').classList.add('js');
        document.querySelector('aside button').addEventListener('click', function() {
            this.parentNode.remove();
        });

        var pw = document.querySelector('input[name=pw]'),
            pwc = document.querySelector('input[name=pwc]');
        function pwConfirmed()
        {
            return pw.value === pwc.value;
        }
        function pwConfirm()
        {
            pwc.classList[['add', 'remove'][pwConfirmed() | 0]]('wrong');
        }
        pw.addEventListener('input', pwConfirm);
        pwc.addEventListener('input', pwConfirm);
        document.querySelector('form').addEventListener('submit', function(e) {
            if (!pwConfirmed())
            {
                e.preventDefault();
                alert('비밀번호를 확인하세요.');
            }
        });
    </script>
</html>