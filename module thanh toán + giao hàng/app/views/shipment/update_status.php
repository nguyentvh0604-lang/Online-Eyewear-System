<?php include __DIR__ . '/../layouts/header.php'; ?>

<h2>Cập nhật trạng thái vận đơn</h2>

<?php if (!empty($shipment)): ?>

    <form action="index.php?route=shipment/update-status" method="POST">

        <input type="hidden" name="shipment_id" value="<?= $shipment['id'] ?>">

        <p><strong>Order ID:</strong> <?= $shipment['order_id'] ?></p>

        <p><strong>Địa chỉ:</strong> <?= htmlspecialchars($shipment['shipping_address']) ?></p>

        <label>Tracking Number:</label><br>
        <input type="text" name="tracking_number"
               value="<?= htmlspecialchars($shipment['tracking_number'] ?? '') ?>">
        <br><br>

        <label>Trạng thái:</label><br>
        <select name="status">
            <option value="pending" <?= $shipment['status']=='pending'?'selected':'' ?>>pending</option>
            <option value="packing" <?= $shipment['status']=='packing'?'selected':'' ?>>packing</option>
            <option value="shipping" <?= $shipment['status']=='shipping'?'selected':'' ?>>shipping</option>
            <option value="delivered" <?= $shipment['status']=='delivered'?'selected':'' ?>>delivered</option>
            <option value="cancelled" <?= $shipment['status']=='cancelled'?'selected':'' ?>>cancelled</option>
        </select>

        <br><br>

        <button type="submit">Cập nhật</button>
    </form>

<?php else: ?>
    <p>Không tìm thấy vận đơn</p>
<?php endif; ?>

<br>
<a href="index.php?route=shipment/tracking">← Quay lại</a>

<?php include __DIR__ . '/../layouts/footer.php'; ?>