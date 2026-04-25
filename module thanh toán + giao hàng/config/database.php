<?php

class Database
{
    private static $host = "localhost";
    private static $dbName = "ecommerce_module";
    private static $username = "root";
    private static $password = "";
    private static $conn = null;

    public static function connect()
    {
        if (self::$conn === null) {
            try {
                self::$conn = new PDO(
                    "mysql:host=" . self::$host . ";dbname=" . self::$dbName . ";charset=utf8",
                    self::$username,
                    self::$password
                );
                self::$conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
            } catch (PDOException $e) {
                die("Kết nối DB thất bại: " . $e->getMessage());
            }
        }

        return self::$conn;
    }
}