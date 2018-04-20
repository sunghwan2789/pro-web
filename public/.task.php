<?php

require_once '../_inc/cla_pdb.php';

$C_BASE = <<<EOP
#include &lt;stdio.h&gt;

int main()
{
    return 0;
}
EOP;

$query = PDB::$conn->prepare('SELECT * FROM pro_tasks WHERE block = 0 ORDER BY date DESC LIMIT 0, 1');
$query->execute();
$task = $query->fetch(PDB::FETCH_ASSOC);

?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
    <title>프로 과제 제출</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/earlyaccess/notosanskr.css">
    <style>
        body {
            margin: 3em .5em 0 .5em;
            line-height: 1.6;
        }
        body, input, button, textarea {
            font-size: 16px;
            font-family: 'Noto Sans KR', 'Malgun Gothic', sans-serif;
        }
        pre, textarea, code {
            font-family: Consolas, Monaco, monospace;
            tab-size: 4;
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
            text-align: right;
            box-sizing: border-box;
            white-space: nowrap;
        }
            header label {
                float: left;
            }
            header button {
                vertical-align: middle;
            }
        h2 time {
            display: inline-block;
            font-size: initial;
            font-weight: initial;
        }
        #desc {
            text-align: justify;
        }
        aside {
            overflow: hidden;
        }
        @media (min-width: 640px), (orientation: landscape) and (min-width: 480px) {
            aside div {
                width: calc(50% - .25em);
                float: left;
            }
            aside div:last-child {
                float: right;
            }
        }
        pre, textarea {
            line-height: 1.2;
            background: #f9f9f9;
            border: 1px solid #a9a9a9;
            padding: .5em;
            overflow-x: scroll;
        }
        textarea {
            display: block;
            width: 100%;
            height: 300px;
            box-sizing: border-box;
        }
        .CodeMirror {
            height: auto;
        }
        #span {
            display: block;
            width: 100%;
        }
        #result {
            position: fixed;
            top: 0;
            left: 0;
            background: rgba(0, 0, 0, .6);
            width: 100%;
            height: 100%;
            color: #ccc;
            text-align: right;
            padding: .5em;
            box-sizing: border-box;
            display: none;
        }
        body.submitting #result {
            display: block;
        }
            #result div {
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                text-align: left;
                white-space: nowrap;
            }
            #result label {
                display: block;
            }
            #result :checked + span {
                font-weight: bold;
                color: #6c0;
            }
            #result :disabled + span {
                color: #c00;
            }
            #result div > span {
                font-size: 3em;
                font-weight: bold;
                display: none;
            }
            #result.end div > span {
                display: block;
            }
            #result.end #close {
                display: none;
            }
            #show {
                display: none;
            }
            #result.end #show {
                display: inline-block;
            }
    </style>
<body>
    <form action="submit.php" method="POST">
        <header>
            <label>학번: <input name="uid" type="text" pattern="[0-9]{8}" value="<?=$_SESSION['uid']?>" readonly></label>
            <button type="submit">&#10004;</button>
        </header>

        <h1>문제</h1>
            <h2><?=htmlspecialchars($task['title'])?><time><?=$task['date']?></time></h2>
            <div id="desc"><?=$task['content']?></div>
            <aside>
                <div><h3>예시 입력</h3><pre><?=htmlspecialchars($task['in'])?></pre></div>
                <div><h3>예시 출력</h3><pre><?=htmlspecialchars($task['out'])?></pre></div>
            </aside>
            <input name="id" type="hidden" value="<?=$task['idx']?>">

        <h1>소스코드</h1>
            <button id="load">파일에서 불러오기</button><input id="loader" type="file" accept=".c,.cpp,.cc,.txt" style="display:none">
            <button id="reset">지우기</button>
            <textarea name="source" wrap="off" required><?=$C_BASE?></textarea>
            <button id="span">늘이기</button>
    </form>
    <form id="result">
        <button id="close">&#10006;</button><button id="show">&#8801;</button>
        <div>
            <label><input type="checkbox"><span>프로 회원확인</span></label>
            <label><input type="checkbox"><span>소스코드 컴파일</span></label>
            <label><input type="checkbox"><span>제출</span></label>
            <span>&#9786;참 잘했어요.</span>
        </div>
    </form>
    <script src="bower_components/jquery/dist/jquery.min.js"></script>
    <script>
    /* Source Editor: Utilities */
        $('#load').on('click', function(e) {
            e.preventDefault();
            $('#loader').val('').click();
        });
        var reader;
        $('#loader').on('change', function() {
            $('textarea').prop('disabled', true);
            if (reader)
            {
                reader.abort();
            }
            reader = new FileReader();
            reader.onload = function() {
                $('textarea').val(this.result).prop('disabled', false);
            };
            reader.readAsText(this.files[0]);
        });
        $('#reset').on('click', function(e) {
            e.preventDefault();
            $('textarea').val('');
        });
        $('#span').on('click', function(e) {
            e.preventDefault();
            $('textarea').height($('textarea').height() + 150);
        });
    /* Source Editor: Temporary Save */
        var saveTimer;
        $('textarea').on('input propertychange', function(e) {
            clearTimeout(saveTimer);
            saveTimer = setTimeout(function() {
                    localStorage['pro_submit'] = $('textarea').val();
                }, 3000);
        });
        if (localStorage['pro_submit'])
        {
            $('textarea').val(localStorage['pro_submit']);
        }
    /* Source Editor: Editing */
        $('textarea').on('keydown', function(e) {
            if (e.altKey || e.ctrlKey || e.metaKey)
            {
                return;
            }
            switch (e.keyCode)
            {
                case 9: var t = '    '; // Tab
                case 13: // Enter
                {
                    e.preventDefault();
                    var v = this.value,
                        p = this.selectionStart,
                        s = t === undefined ? [ '\n', (v.substring(v.lastIndexOf('\n', p - (v[p] == '\n')) + 1, p).match(/^([ \u00A0\t]*)/) || [])[1] ].join('') : t;
                    if (document.queryCommandSupported('insertText'))
                    {
                        document.execCommand('insertText', false, s);
                    }
                    else
                    {
                        this.value = [ v.substring(0, p), v.substring(this.selectionEnd) ].join(s);
                        this.selectionEnd = this.selectionStart = p + s.length;
                    }
                    break;
                }
            }
        });
    /* Submit */
        var xhr;
        $('form').on('submit', function(e) {
            e.preventDefault();
            $('#result')[0].reset();
            $('#result input').prop('disabled', false);
            $('body').addClass('submitting');
            $('textarea').val(function() {
                return $(this).val()
                        .replace(/\u00A0/g, ' ')
                        .replace(/\t/g, '    ')
                        .replace(/[ \u00A0\t]+([\r\n])/g, '$1')
                        .replace(/\r\n/g, '\n').replace(/\r/g, '\n')
            });
            xhr = $.ajax({
                    type: 'POST',
                    url: this.action,
                    data: $(this).serialize(),
                }).done(function(err) {
                    var checks = $('#result input'),
                        l = err == 0 ? 3 : err;
                    while (l--)
                    {
                        $(checks[l]).prop('checked', true);
                    }
                    if (err != 0)
                    {
                        $(checks[err - 1]).prop('disabled', true);
                    }
                    else
                    {
                        localStorage.removeItem('pro_submit');
                        $('#result').addClass('end');
                    }
                }).fail(function() {
                    $('#result input').prop('disabled', true);
                });
        });
        $('#result input').on('click', function(e) {
            e.preventDefault();
        });
        $('#close').on('click', function(e) {
            e.preventDefault();
            if (xhr)
            {
                xhr.abort();
            }
            $('body').removeClass('submitting');
        });
        $('#show').on('click', function(e) {
            e.preventDefault();
            window.location.replace('status.php?tid=' + <?=$task['idx']?>);
        });
    </script>
</html>
