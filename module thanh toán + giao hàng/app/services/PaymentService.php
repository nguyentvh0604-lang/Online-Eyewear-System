<?php

require_once __DIR__ . '/../repositories/PaymentRepository.php';

class PaymentService
{
    private $paymentRepository;

    public function __construct()
    {
        $this->paymentRepository = new PaymentRepository();
    }

    public function processCOD($orderId, $amount)
    {
        return $this->paymentRepository->create($orderId, 'COD', $amount, 'unpaid');
    }

    public function confirmPayment($paymentId)
    {
        return $this->paymentRepository->updateStatus($paymentId, 'paid');
    }

    public function getAllPayments()
    {
        return $this->paymentRepository->getAll();
    }
}