<?php

require_once __DIR__ . '/../../config/database.php';

class ReturnRequestRepository
{
    private $conn;

    public function __construct()
    {
        $this->conn = Database::connect();
    }

    public function create($orderId, $reason, $status = 'pending')
    {
        $sql = "INSERT INTO return_requests (order_id, reason, status)
                VALUES (:order_id, :reason, :status)";
        $stmt = $this->conn->prepare($sql);

        return $stmt->execute([
            ':order_id' => $orderId,
            ':reason' => $reason,
            ':status' => $status
        ]);
    }

    public function getAll()
    {
        $stmt = $this->conn->query("SELECT * FROM return_requests ORDER BY id DESC");
        return $stmt->fetchAll(PDO::FETCH_ASSOC);
    }

    public function updateStatus($id, $status)
    {
        $stmt = $this->conn->prepare("UPDATE return_requests SET status = :status WHERE id = :id");
        return $stmt->execute([
            ':id' => $id,
            ':status' => $status
        ]);
    }

    public function delete($id)
    {
        $stmt = $this->conn->prepare("DELETE FROM return_requests WHERE id = :id");
        return $stmt->execute([':id' => $id]);
    }
}