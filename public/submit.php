<?php

// require_once '../_inc/cla_pdb.php';

// define('UPLOAD_PATH', '_data/attaches/');

switch (substr($_SERVER['HTTP_REFERER'], strrpos($_SERVER['HTTP_REFERER'], '/') + 1))
{
    case 'task.php':
    {
        // $query = PDB::$conn->prepare('SELECT 1 FROM pro_members WHERE id = ?');
        // $query->bindValue(1, $_POST['uid']);
        // $query->execute();
        // if (!$query->fetchColumn())
        // {
        //     echo 1;
        //     exit;
        // }

        $query = PDB::$conn->prepare('SELECT 1 FROM pro_tasks WHERE idx = ?');
        $query->bindValue(1, $_POST['id']);
        $query->execute();
        if (!$query->fetchColumn())
        {
            echo 2;
            exit;
        }

        $tmpb = implode('', [ $config->get('storage.sources') . '/', $_POST['id'], '_', $_SESSION['uid'], '_' ]);
        for ($i = 1; file_exists($tmp = implode('', [ $tmpb, $i, '.c' ])); $i++);
        $params = [ 'id'=> $_POST['id'], 'uid'=> $_SESSION['uid'], 'fid'=> $i ];

        $fp = fopen($tmp, 'wb');
        if ($fp === false)
        {
            echo 2;
            exit;
        }
        fwrite($fp, pack('C', 0xEF));
        fwrite($fp, pack('C', 0xBB));
        fwrite($fp, pack('C', 0xBF));
        $params['size'] = fwrite($fp, $_POST['source']);
        fclose($fp);

        $val = null;
        exec(implode(' ', [ dirname(__DIR__) . '/bin/validate.bat', escapeshellarg($tmp) ]), $val, $val);
        $val = !($params['compile'] = $val == 0) * 2;

        $query = PDB::$conn->prepare('INSERT INTO pro_submit (tid, uid, fid, compile, size) ' .
            'VALUES (:id, :uid, :fid, :compile, :size) ' .
            'ON DUPLICATE KEY UPDATE date = NOW(), compile = :compile, size = :size');
        echo !$query->execute($params) ? $val ? $val : 3 : $val;
        exit;
    }
    case 'activity.php':
    {
        $query = PDB::$conn->prepare('SELECT authority FROM pro_members WHERE id = ?');
        $query->bindValue(1, $_SESSION['uid']);
        $query->execute();
        if ($query->fetchColumn() > 0)
        {
            echo '권한 없음';
            exit;
        }

        $query = PDB::$conn->prepare(
            'INSERT INTO pro_activities (start, end, purpose, content, place, uid) ' .
            'VALUES (?, ?, ?, ?, ?, ?)'
        );
        $query->bindValue(1, $_POST['start']);
        $query->bindValue(2, $_POST['end']);
        $query->bindValue(3, $_POST['purpose']);
        $query->bindValue(4, $_POST['content']);
        $query->bindValue(5, $_POST['place']);
        $query->bindValue(6, $_SESSION['uid']);
        $query->execute();

        $activityId = PDB::$conn->lastInsertId();

        $query = str_repeat('(' . $activityId . ', ?),', count($_POST['attend']));
        $query = PDB::$conn->prepare(
            'INSERT INTO pro_activity_attend (aid, uid) ' .
            'VALUES ' . substr($query, 0, strlen($query) - 1)
        );
        $query->execute($_POST['attend']);

        for ($i = 0; $i < count($_FILES['attach']['name']); $i++)
        {
            $path = $_FILES['attach']['tmp_name'][$i];
            $filename = $_FILES['attach']['name'][$i];
            if (!file_exists($path))
            {
                continue;
            }

            $fileId = md5_file($path);
            if (!@move_uploaded_file($path, $config->get('storage.attaches') . '/' . $fileId))
            {
                continue;
            }

            $query = PDB::$conn->prepare(
                'INSERT INTO pro_activity_attach (aid, md5, name) ' .
                'VALUES (:aid, :md5, :name)'
            );
            $params = [];
            $params['aid'] = $activityId;
            $params['md5'] = $fileId;
            $params['name'] = $filename;
            $query->execute($params);

            @unlink($path);
        }

        exit;
    }
}

header('Location: ' . $_SERVER['HTTP_REFERER']);

?>