<?php

// require_once '../_inc/cla_fileio.php';

// define('UPLOAD_PATH', '_data/attaches/');

$id = strval($_GET['id']);
if (preg_match('/\.\.[\\\\\/]|:/', $id) || !file_exists($config->get('storage.attaches') . '/' . $id))
{
    exit;
}

FileIO::get($config->get('storage.attaches') . '/' . $id, $_GET['name'], 'application/octet-stream');

?>