<?php

require_once __DIR__ . '/../app/controllers/PaymentController.php';
require_once __DIR__ . '/../app/controllers/ShipmentController.php';
require_once __DIR__ . '/../app/controllers/ReturnRequestController.php';
require_once __DIR__ . '/../app/controllers/OperationsController.php';

$route = $_GET['route'] ?? 'payment/checkout';

switch ($route) {
    case 'payment/checkout':
        (new PaymentController())->checkout();
        break;

    case 'payment/process':
        (new PaymentController())->processCheckout();
        break;

    case 'payment/confirm':
        (new PaymentController())->confirm();
        break;

    case 'shipment/tracking':
        (new ShipmentController())->tracking();
        break;

    case 'shipment/update-status':
        (new ShipmentController())->updateStatus();
        break;

    case 'return/create':
        (new ReturnRequestController())->create();
        break;

    case 'return/store':
        (new ReturnRequestController())->store();
        break;

    case 'return/list':
        (new ReturnRequestController())->list();
        break;

    case 'operations/dashboard':
        (new OperationsController())->dashboard();
        break;

    default:
        echo "404 - Không tìm thấy trang";
        break;
}