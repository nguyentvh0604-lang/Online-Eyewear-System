<?php

require_once __DIR__ . '/../../config/database.php';

class PaymentRepository
{
    private $conn;

    public function __construct()
    {
        $this->conn = Database::connect();
    }

    public function create($orderId, $paymentMethod, $amount, $status = 'pending')
    {
        $sql = "INSERT INTO payments (order_id, payment_method, amount, status)
                VALUES (:order_id, :payment_method, :amount, :status)";
        $stmt = $this->conn->prepare($sql);
        return $stmt->execute([
            ':order_id' => $orderId,
            ':payment_method' => $paymentMethod,
            ':amount' => $amount,
            ':status' => $status
        ]);
    }

    public function getAll()
    {
        $stmt = $this->conn->query("SELECT * FROM payments ORDER BY id DESC");
        return $stmt->fetchAll(PDO::FETCH_ASSOC);
    }

    public function findById($id)
    {
        $stmt = $this->conn->prepare("SELECT * FROM payments WHERE id = :id");
        $stmt->execute([':id' => $id]);
        return $stmt->fetch(PDO::FETCH_ASSOC);
    }

    public function updateStatus($id, $status)
    {
        $stmt = $this->conn->prepare("UPDATE payments SET status = :status WHERE id = :id");
        return $stmt->execute([
            ':id' => $id,
            ':status' => $status
        ]);
    }

    public function delete($id)
    {
        $stmt = $this->conn->prepare("DELETE FROM payments WHERE id = :id");
        return $stmt->execute([':id' => $id]);
    }
}