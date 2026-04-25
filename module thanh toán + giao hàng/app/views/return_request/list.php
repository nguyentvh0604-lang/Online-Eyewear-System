<?php include __DIR__ . '/../layouts/header.php'; ?>

<h2>Danh sách yêu cầu đổi trả</h2>

<table border="1" cellpadding="8" cellspacing="0">
    <tr>
        <th>ID</th>
        <th>Order ID</th>
        <th>Lý do</th>
        <th>Trạng thái</th>
        <th>Ngày tạo</th>
    </tr>
    <?php foreach ($requests as $request): ?>
        <tr>
            <td><?= $request['id'] ?></td>
            <td><?= $request['order_id'] ?></td>
            <td><?= htmlspecialchars($request['reason']) ?></td>
            <td><?= htmlspecialchars($request['status']) ?></td>
            <td><?= $request['created_at'] ?></td>
        </tr>
    <?php endforeach; ?>
</table>

<?php include __DIR__ . '/../layouts/footer.php'; ?>