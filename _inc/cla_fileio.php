<?php
class FileIO
{
    public static function scandir($target) {
        foreach(scandir($target) as $subtarget) {
            if($subtarget == '.' || $subtarget == '..') continue;
            if(is_file($target.'/'.$subtarget)) {
                $list[] = $target.'/'.$subtarget;
                continue;
            }
            foreach(static::scandir($target.'/'.$subtarget) as $subtarget) $list[] = $subtarget;
        }
        return $list;
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

    public static function put() {
        set_time_limit(0);

    }
    public static function mime($path) {
        $finfo = new finfo(FILEINFO_MIME_TYPE);
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
    public static function surf($url, $method = 'GET', $param = '', $cookie = '') {
        $method = strtoupper($method);
        $imfile = strpos($method, 'FILE') !== false;
        if($imfile) $fp = fopen($param, 'wb+');
        $ch = curl_init();

        curl_setopt($ch, CURLOPT_URL, $url);
        curl_setopt($ch, CURLOPT_USERAGENT, '');
        curl_setopt($ch, CURLOPT_COOKIE, $cookie);
        if(strpos($method, 'POST') !== false) {
            curl_setopt($ch, CURLOPT_POST, true);
            curl_setopt($ch, CURLOPT_POSTFIELDS, $param);
        }
        curl_setopt($ch, CURLOPT_CONNECTTIMEOUT, 10);
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
        if($imfile) curl_setopt($ch, CURLOPT_FILE, $fp);
        else if(strpos($method, 'NOHEAD') === false) curl_setopt($ch, CURLOPT_HEADER, true);

        $data = curl_exec($ch);
        curl_close($ch);
        if($imfile) fclose($fp);

        if(!$data) die('* Internet Connect Failed!');
        return $data;
    }
}
