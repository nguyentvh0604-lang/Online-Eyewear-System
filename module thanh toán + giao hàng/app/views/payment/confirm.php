<?php include __DIR__ . '/../layouts/header.php'; ?>

<h2>Xác nhận đơn hàng</h2>
<p>Đơn hàng đã được tạo thanh toán COD thành công.</p>
<p>Hãy nhập ID payment để xác nhận đã thanh toán.</p>

<form action="index.php?route=payment/confirm" method="POST">
    <label>Payment ID:</label><br>
    <input type="number" name="payment_id" required><br><br>
    <button type="submit">Xác nhận thanh toán</button>
</form>

<?php include __DIR__ . '/../layouts/footer.php'; ?>