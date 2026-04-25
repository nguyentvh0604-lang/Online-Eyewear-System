<?php include __DIR__ . '/../layouts/header.php'; ?>

<h2>Tạo yêu cầu đổi trả</h2>

<form action="index.php?route=return/store" method="POST">
    <label>Mã đơn hàng:</label><br>
    <input type="number" name="order_id" required><br><br>

    <label>Lý do đổi trả:</label><br>
    <textarea name="reason" rows="5" cols="40" required></textarea><br><br>

    <button type="submit">Gửi yêu cầu</button>
</form>

<?php include __DIR__ . '/../layouts/footer.php'; ?>