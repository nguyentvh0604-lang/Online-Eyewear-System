<?php

require_once __DIR__ . '/../repositories/ShipmentRepository.php';

class ShipmentService
{
    private $shipmentRepository;

    public function __construct()
    {
        $this->shipmentRepository = new ShipmentRepository();
    }

    public function createShipment($orderId, $shippingAddress, $trackingNumber = null)
    {
        return $this->shipmentRepository->create($orderId, $shippingAddress, $trackingNumber, 'pending');
    }

    public function updateShipmentStatus($shipmentId, $status)
    {
        return $this->shipmentRepository->updateStatus($shipmentId, $status);
    }

    public function getAllShipments()
    {
        return $this->shipmentRepository->getAll();
    }
}
