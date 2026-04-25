<?php

require_once __DIR__ . '/../services/ShipmentService.php';
require_once __DIR__ . '/../services/ReturnRequestService.php';
require_once __DIR__ . '/../services/PaymentService.php';

class OperationsController
{
    private $shipmentService;
    private $returnRequestService;
    private $paymentService;

    public function __construct()
    {
        $this->shipmentService = new ShipmentService();
        $this->returnRequestService = new ReturnRequestService();
        $this->paymentService = new PaymentService();
    }

    public function dashboard()
    {
        $shipments = $this->shipmentService->getAllShipments();
        $returns = $this->returnRequestService->getAllReturnRequests();
        $payments = $this->paymentService->getAllPayments();

        include __DIR__ . '/../views/operations/dashboard.php';
    }
}