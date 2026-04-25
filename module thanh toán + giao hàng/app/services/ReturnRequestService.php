<?php

require_once __DIR__ . '/../repositories/ReturnRequestRepository.php';

class ReturnRequestService
{
    private $returnRequestRepository;

    public function __construct()
    {
        $this->returnRequestRepository = new ReturnRequestRepository();
    }

    public function createReturnRequest($orderId, $reason)
    {
        return $this->returnRequestRepository->create($orderId, $reason, 'pending');
    }

    public function getAllReturnRequests()
    {
        return $this->returnRequestRepository->getAll();
    }

    public function updateStatus($id, $status)
    {
        return $this->returnRequestRepository->updateStatus($id, $status);
    }
}