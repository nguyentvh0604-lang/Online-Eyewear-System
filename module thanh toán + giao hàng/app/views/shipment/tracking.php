<?php include __DIR__ . '/../layouts/header.php'; ?>

<h2>Danh sách vận đơn</h2>

<table border="1" cellpadding="8" cellspacing="0">
    <tr>
        <th>ID</th>
        <th>Order ID</th>
        <th>Địa chỉ</th>
        <th>Tracking</th>
        <th>Trạng thái</th>
        <th>Hành động</th>
    </tr>

    <?php if (!empty($shipments)): ?>
        <?php foreach ($shipments as $s): ?>
            <tr>
                <td><?= $s['id'] ?></td>
                <td><?= $s['order_id'] ?></td>
                <td><?= htmlspecialchars($s['shipping_address']) ?></td>
                <td><?= htmlspecialchars($s['tracking_number'] ?? '') ?></td>
                <td><?= htmlspecialchars($s['status']) ?></td>

                <td>
                    <!-- Form update nhanh -->
                    <form action="index.php?route=shipment/update-status" method="POST" style="display:inline;">
                        <input type="hidden" name="shipment_id" value="<?= $s['id'] ?>">

                        <select name="status">
                            <option value="pending" <?= $s['status']=='pending'?'selected':'' ?>>pending</option>
                            <option value="packing" <?= $s['status']=='packing'?'selected':'' ?>>packing</option>
                            <option value="shipping" <?= $s['status']=='shipping'?'selected':'' ?>>shipping</option>
                            <option value="delivered" <?= $s['status']=='delivered'?'selected':'' ?>>delivered</option>
                            <option value="cancelled" <?= $s['status']=='cancelled'?'selected':'' ?>>cancelled</option>
                        </select>

                        <button type="submit">Update</button>
                    </form>

                    |
                    <a href="index.php?route=shipment/update-form&id=<?= $s['id'] ?>">
                        Chi tiết
                    </a>
                </td>
            </tr>
        <?php endforeach; ?>
    <?php else: ?>
        <tr>
            <td colspan="6">Không có dữ liệu</td>
        </tr>
    <?php endif; ?>
</table>

<br><br>

<a href="index.php?route=operations/dashboard">← Quay về Dashboard</a>

<?php include __DIR__ . '/../layouts/footer.php'; ?>