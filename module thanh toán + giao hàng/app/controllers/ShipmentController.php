<?php

require_once __DIR__ . '/../services/ShipmentService.php';

class ShipmentController
{
    private $shipmentService;

    public function __construct()
    {
        $this->shipmentService = new ShipmentService();
    }

    public function tracking()
    {
        $shipments = $this->shipmentService->getAllShipments();
        include __DIR__ . '/../views/shipment/tracking.php';
    }

    public function updateStatus()
    {
        $shipmentId = $_POST['shipment_id'] ?? null;
        $status = $_POST['status'] ?? null;

        if (!$shipmentId || !$status) {
            echo "Thiếu dữ liệu cập nhật";
            return;
        }

        $this->shipmentService->updateShipmentStatus($shipmentId, $status);
        header("Location: index.php?route=shipment/tracking");
        exit;
    }
}