<?php include __DIR__ . '/../layouts/header.php'; ?>

<h2>Dashboard cho Operations Staff</h2>

<h3>1. Thanh toán</h3>
<table border="1" cellpadding="8" cellspacing="0">
    <tr>
        <th>ID</th>
        <th>Order ID</th>
        <th>Phương thức</th>
        <th>Số tiền</th>
        <th>Trạng thái</th>
    </tr>
    <?php foreach ($payments as $payment): ?>
        <tr>
            <td><?= $payment['id'] ?></td>
            <td><?= $payment['order_id'] ?></td>
            <td><?= htmlspecialchars($payment['payment_method']) ?></td>
            <td><?= $payment['amount'] ?></td>
            <td><?= htmlspecialchars($payment['status']) ?></td>
        </tr>
    <?php endforeach; ?>
</table>

<br>

<h3>2. Vận đơn</h3>
<table border="1" cellpadding="8" cellspacing="0">
    <tr>
        <th>ID</th>
        <th>Order ID</th>
        <th>Tracking</th>
        <th>Trạng thái</th>
    </tr>
    <?php foreach ($shipments as $shipment): ?>
        <tr>
            <td><?= $shipment['id'] ?></td>
            <td><?= $shipment['order_id'] ?></td>
            <td><?= htmlspecialchars($shipment['tracking_number'] ?? '') ?></td>
            <td><?= htmlspecialchars($shipment['status']) ?></td>
        </tr>
    <?php endforeach; ?>
</table>

<br>

<h3>3. Yêu cầu đổi trả</h3>
<table border="1" cellpadding="8" cellspacing="0">
    <tr>
        <th>ID</th>
        <th>Order ID</th>
        <th>Lý do</th>
        <th>Trạng thái</th>
    </tr>
    <?php foreach ($returns as $return): ?>
        <tr>
            <td><?= $return['id'] ?></td>
            <td><?= $return['order_id'] ?></td>
            <td><?= htmlspecialchars($return['reason']) ?></td>
            <td><?= htmlspecialchars($return['status']) ?></td>
        </tr>
    <?php endforeach; ?>
</table>

<?php include __DIR__ . '/../layouts/footer.php'; ?>