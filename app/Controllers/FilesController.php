<?php
namespace App\Controllers;

use Psr\Container\ContainerInterface;
use Psr\Http\Message\ServerRequestInterface as Request;
use Psr\Http\Message\ResponseInterface as Response;
use Slim\Http\Stream;

class FilesController
{
    /** @var \Psr\Container\ContainerInterface */
    protected $container;

    /** @var \Northwoods\Config\ConfigInterface */
    protected $config;

    /** @var \PDO */
    protected $db;

    /** @var \Slim\Views\PhpRenderer */
    protected $view;

    /** @var \Slim\Router */
    protected $router;

    public function __construct(ContainerInterface $container)
    {
        $this->container = $container;
        $this->config = $container['config'];
        $this->db = $container['db'];
    }

    /** GET /files/{file_id} */
    public function download(Request $request, Response $response, array $args)
    {
        $fileId = $args['file_id'];

        $query = $this->db->prepare('SELECT md5, name FROM pro_activity_attach WHERE md5 = ?');
        $query->bindValue(1, $fileId);
        $query->execute();
        $file = $query->fetch();
        if ($file === false) {
            throw new \Exception('not found');
        }

        $basePath = $this->config->get('storage.attaches');
        $path = $basePath . "/{$file['md5']}";
        if (stripos(realpath($path), realpath($basePath)) !== 0) {
            throw new \Exception('LFI guard');
        }

        static::get($path, $file['name']);
    }

    public static function filesize($target) {
        $size = ((file_exists($target)) ? sprintf('%.0f', filesize($target)) : sprintf('%.0f', $target))*1024;
        $units = array('b', 'B', 'KB', 'MB', 'GB', 'TB');
        for($i = 0; $size >= 1024 && $i < 4; $i++) $size /= 1024;
        return round($size, 2).$units[$i];
    }
    public static function stripFileName($name) {
        return preg_replace('/[\\\\\/:*?"<>|]/', '-', $name);
    }
    public static function mime($path) {
        $finfo = new \finfo(FILEINFO_MIME_TYPE);
        return $finfo->file($path);
    }
    public static function get($path, $name = '', $mimeType = '', $expires = 0, $streaming = false)
    {
        // http://w-shadow.com/blog/2007/08/12/how-to-force-file-download-with-php/
        set_time_limit(0);
        ini_set('zlib.output_compression', 'Off');

        $fp = @fopen($path, 'rb');
        if ($fp === false)
        {
            echo '* File not found or inaccessible!';
            exit;
        }

        @ob_end_clean();

        if ($expires > 0)
        {
            header('Expires: ' . gmdate('D, d M Y H:i:s', time() + $expires) . ' GMT');
            header('Cache-Control: max-age=' . $expires);
        }
        else
        {
            header('Pragma: no-cache');
            header('Expires: Thu, 01 Jan 1970 00:00:00 GMT');
            header('Cache-control: no-store');
        }

        // https://tools.ietf.org/html/rfc6266
        $name = self::stripFileName(empty($name) ? basename($path) : $name);
        header('Content-Type: ' . (empty($mimeType) ? self::mime($path) : $mimeType));
        header('Content-Disposition: ' . ($streaming ? 'inline' : 'attachment') . '; ' .
            'filename="' . $name . '"; ' .
            'filename*=UTF-8\'\'' . rawurlencode($name));

        // https://tools.ietf.org/html/rfc7233
        $size = fstat($fp)['size'];
        header('Accept-Ranges: bytes');
        if (isset($_SERVER['HTTP_RANGE']))
        {
            list($rangeUnit, $ranges) = explode('=', $_SERVER['HTTP_RANGE'], 2);
            if ($rangeUnit == 'bytes')
            {
                list($range, $extra_ranges) = explode(',', $ranges, 2);
            }
            else
            {
                $range = '';
            }
            list($start, $end) = explode('-', $range);

            if (empty($start) && !empty($end))
            {
                $start = intval($end) ? max($size - intval($end), 0) : 0;
                $end = $size - 1;
            }
            else
            {
                $end = empty($end) ? $size - 1 : min(intval($end), $size - 1);
                $start = empty($start) || intval($start) > $end ? 0 : intval($start);
            }

            // header('HTTP/1.1 206 Partial Content');
            http_response_code(206);
            header('Content-Range: bytes ' . $start . '-' . $end . '/' . $size);

            $size = $end - $start + 1;
            fseek($fp, $start);
        }
        header('Content-Length: ' . $size);
        flush();

        $send = 0;
        while (!feof($fp) && $send < $size)
        {
            if (isset($_GET['t']))
            {
                $buffer = fread($fp, min($size - $send, 1024 * 1024 * abs(intval($_GET['t']))));
                echo $buffer;
                flush();
                sleep(1);
                $send += strlen($buffer);
            }
            else
            {
                $buffer = fread($fp, min($size - $send, 1024 * 1024 * 4));
                echo $buffer;
                flush();
                $send += strlen($buffer);
            }
        }

        fclose($fp);
    }
}
