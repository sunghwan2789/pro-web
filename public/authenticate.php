<?php
require_once dirname(__DIR__) . '/bootstrap/autoload.php';

define('DEBUG', 1);

/*
 * Generate a secure hash for a given password. The cost is passed
 * to the blowfish algorithm. Check the PHP manual page for crypt to
 * find more information about this setting.
 */
function generate_hash($password, $cost=11){
    /* To generate the salt, first generate enough random bytes. Because
     * base64 returns one character for each 6 bits, the we should generate
     * at least 22*6/8=16.5 bytes, so we generate 17. Then we get the first
     * 22 base64 characters
     */
    // $salt=substr(base64_encode(openssl_random_pseudo_bytes(17)),0,22);
    $salt=substr(base64_encode(random_bytes(17)),0,22);
    /* As blowfish takes a salt with the alphabet ./A-Za-z0-9 we have to
     * replace any '+' in the base64 string with '.'. We don't have to do
     * anything about the '=', as this only occurs when the b64 string is
     * padded, which is always after the first 22 characters.
     */
    $salt=str_replace("+",".",$salt);
    /* Next, create a string that will be passed to crypt, containing all
     * of the settings, separated by dollar signs
     */
    $param='$'.implode('$',array(
        "2y", //select the most secure version of blowfish (>=PHP 5.3.7)
        str_pad($cost,2,"0",STR_PAD_LEFT), //add the cost in two digits
        $salt //add the salt
    ));

    //now do the actual hashing
    return crypt($password,$param);
}

function encrypt($str)
{
    return sha1('y#.fij/|lP&!79.Txcf]' . sha1($str . 'y#.fij/|lP&!79.Txcf]') . $str);
}
function parse_dir($uri)
{
    return preg_replace('/\/[^\/]*$/', '/', explode('?', $uri)[0]);
}
function go_back($details = 'unknown')
{
    include 'authenticate.go_back.php';
}

if (preg_match('/\/authenticate\.php$/', $_SERVER['SCRIPT_NAME']))
{
    sleep(1); // slow down bruteforce

    try
    {
        if ($_GET['action'] === 'logout')
        {
            if (isset($_COOKIE[session_name()]))
            {
                session_start();
                $_SESSION = [];
                setcookie(session_name(), null, -1,
                    parse_dir($_SERVER['REQUEST_URI']), $_SERVER['HTTP_HOST'],
                    false, true);
                session_destroy();
            }
            header('Location: ' . parse_dir($_SERVER['HTTP_REFERER']));
            exit;
        }

        if (isset($_COOKIE[session_name()]))
        {
            session_start();
            if (isset($_SESSION['uid']))
            {
                throw new Exception('already logged in');
            }
            session_write_close();
        }

        if ($_POST['action'] === 'initialize')
        {
            if ($_POST['pw'] !== $_POST['pwc'])
            {
                throw new Exception('pw does not equal to pwc');
            }

            $query = PDB::$conn->prepare('SELECT 1 FROM pro_members WHERE id = ? AND name = ? AND contact = ? AND pw IS NULL');
            $query->bindValue(1, $_POST['id']);
            $query->bindValue(2, $_POST['name']);
            $query->bindValue(3, preg_replace('/[^0-9]/', '', strval($_POST['tel'])));
            $query->execute();
            if (!$query->fetchColumn())
            {
                throw new Exception('authentication failed or pw is already set');
            }

            $query = PDB::$conn->prepare('UPDATE pro_members SET pw = ? WHERE id = ?');
            $query->bindValue(1, generate_hash(encrypt(strval($_POST['pw']))));
            $query->bindValue(2, $_POST['id']);
            $query->execute();
            header('Location: '. strval($_POST['return']));

            $query = PDB::$conn->prepare('INSERT INTO pro_member_log SET uid = ?, `text` = ?');
            $query->bindValue(1, $_POST['id']);
            $query->bindValue(2, 'INITIALIZE');
            $query->execute();
            exit;
        }

        $query = PDB::$conn->prepare('SELECT pw FROM pro_members WHERE id = ?');
        $query->bindValue(1, $_POST['id']);
        $query->execute();
        $pw = $query->fetchColumn();
        if (hash_equals($pw, crypt(encrypt(strval($_POST['pw'])), $pw)))
        {
            session_set_cookie_params(60 * 60 * 24 * 7 * 4 * ($_POST['keep'] === 'on'),
                parse_dir($_SERVER['REQUEST_URI']), $_SERVER['HTTP_HOST'],
                false, true);
            session_start();
            $_SESSION['uid'] = $_POST['id'];
            header('Location: ' . strval($_POST['return']));

            $query = PDB::$conn->prepare('INSERT INTO pro_member_log SET uid = ?, `text` = ?');
            $query->bindValue(1, $_POST['id']);
            $query->bindValue(2, 'LOGIN');
            $query->execute();
            exit;
        }

        $query = PDB::$conn->prepare('SELECT pw FROM pro_members WHERE id = ?');
        $query->bindValue(1, $_POST['id']);
        $query->execute();
        $pw = $query->fetchColumn();
        if ($pw === false)
        {
            throw new Exception('id does not exist');
        }
        if (!empty($pw))
        {
            throw new Exception('pw is wrong');
        }

        include 'authenticate.initForm.php';
        exit;
    }
    catch (Exception $e)
    {
        go_back($e->getMessage());
        exit;
    }
}

if (isset($_COOKIE[session_name()]))
{
    session_start();
    if (isset($_SESSION['uid']))
    {
        goto AUTHENTICATED;
    }
}
include 'authenticate.loginForm.php';
exit;

AUTHENTICATED:
?>