<?php

require_once __DIR__ . '/../services/ReturnRequestService.php';

class ReturnRequestController
{
    private $returnRequestService;

    public function __construct()
    {
        $this->returnRequestService = new ReturnRequestService();
    }

    public function create()
    {
        include __DIR__ . '/../views/return_request/create.php';
    }

    public function store()
    {
        $orderId = $_POST['order_id'] ?? null;
        $reason = $_POST['reason'] ?? null;

        if (!$orderId || !$reason) {
            echo "Thiếu dữ liệu đổi trả";
            return;
        }

        $this->returnRequestService->createReturnRequest($orderId, $reason);
        header("Location: index.php?route=return/list");
        exit;
    }

    public function list()
    {
        $requests = $this->returnRequestService->getAllReturnRequests();
        include __DIR__ . '/../views/return_request/list.php';
    }
}