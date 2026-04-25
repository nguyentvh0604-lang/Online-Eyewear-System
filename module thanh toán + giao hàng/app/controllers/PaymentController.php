<?php

require_once __DIR__ . '/../services/PaymentService.php';

class PaymentController
{
    private $paymentService;

    public function __construct()
    {
        $this->paymentService = new PaymentService();
    }

    public function checkout()
    {
        include __DIR__ . '/../views/payment/checkout.php';
    }

    public function processCheckout()
    {
        $orderId = $_POST['order_id'] ?? null;
        $amount = $_POST['amount'] ?? null;

        if (!$orderId || !$amount) {
            echo "Thiếu dữ liệu thanh toán";
            return;
        }

        $this->paymentService->processCOD($orderId, $amount);
        include __DIR__ . '/../views/payment/confirm.php';
    }

    public function confirm()
    {
        $paymentId = $_POST['payment_id'] ?? null;

        if (!$paymentId) {
            echo "Thiếu payment_id";
            return;
        }

        $this->paymentService->confirmPayment($paymentId);
        include __DIR__ . '/../views/payment/result.php';
    }
}