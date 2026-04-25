<?php

require_once __DIR__ . '/../../config/database.php';

class ShipmentRepository
{
    private $conn;

    public function __construct()
    {
        $this->conn = Database::connect();
    }

    public function create($orderId, $shippingAddress, $trackingNumber = null, $status = 'pending')
    {
        $sql = "INSERT INTO shipments (order_id, shipping_address, tracking_number, status)
                VALUES (:order_id, :shipping_address, :tracking_number, :status)";
        $stmt = $this->conn->prepare($sql);

        return $stmt->execute([
            ':order_id' => $orderId,
            ':shipping_address' => $shippingAddress,
            ':tracking_number' => $trackingNumber,
            ':status' => $status
        ]);
    }

    public function getAll()
    {
        $stmt = $this->conn->query("SELECT * FROM shipments ORDER BY id DESC");
        return $stmt->fetchAll(PDO::FETCH_ASSOC);
    }

    public function findById($id)
    {
        $stmt = $this->conn->prepare("SELECT * FROM shipments WHERE id = :id");
        $stmt->execute([':id' => $id]);
        return $stmt->fetch(PDO::FETCH_ASSOC);
    }

    public function updateStatus($id, $status)
    {
        $stmt = $this->conn->prepare("UPDATE shipments SET status = :status WHERE id = :id");
        return $stmt->execute([
            ':id' => $id,
            ':status' => $status
        ]);
    }

    public function updateTrackingNumber($id, $trackingNumber)
    {
        $stmt = $this->conn->prepare("UPDATE shipments SET tracking_number = :tracking_number WHERE id = :id");
        return $stmt->execute([
            ':id' => $id,
            ':tracking_number' => $trackingNumber
        ]);
    }

    public function delete($id)
    {
        $stmt = $this->conn->prepare("DELETE FROM shipments WHERE id = :id");
        return $stmt->execute([':id' => $id]);
    }
}