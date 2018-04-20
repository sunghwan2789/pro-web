<?php
header('Content-Type: text/plain; charset=UTF-8');

//$SOURCES = __DIR__ . '/_data/sources/';

$source = $config->get('storage.sources') . '/' . implode('_', [ $_GET['tid'], $_GET['uid'], $_GET['fid'] ]) . '.c';

if (stripos(realpath($source), realpath($config->get('storage.sources'))) !== 0)
{
    exit;
}

$fp = fopen($source, 'rb');
if ($fp === false)
{
    exit;
}
fseek($fp, 3);

while (!feof($fp))
{
    echo fread($fp, 4096);
    flush();
}
fclose($fp);
?>