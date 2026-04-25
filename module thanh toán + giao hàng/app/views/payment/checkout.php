<?php include __DIR__ . '/../layouts/header.php'; ?>

<h2>Trang thanh toán</h2>

<form action="index.php?route=payment/process" method="POST">
    <label>Mã đơn hàng:</label><br>
    <input type="number" name="order_id" required><br><br>

    <label>Số tiền:</label><br>
    <input type="number" step="0.01" name="amount" required><br><br>

    <button type="submit">Thanh toán COD</button>
</form>

<?php include __DIR__ . '/../layouts/footer.php'; ?>