# 👓 OpticalStore - Hệ thống bán kính mắt trực tuyến (Web tĩnh - Demo giao diện)

## 📌 Tổng quan

**OpticalStore** là một ứng dụng web **tĩnh** mô phỏng giao diện và luồng nghiệp vụ của hệ thống bán kính mắt trực tuyến.

> ⚠️ Dự án chỉ dừng ở mức **mockup / demo luồng** – dữ liệu (sản phẩm, giỏ hàng, đơn hàng) được lưu tạm trên `localStorage` hoặc nhúng trực tiếp trong mã nguồn. Không có xử lý thanh toán thực tế, không lưu trữ dữ liệu lâu dài.

## 🚀 Các tính năng mô phỏng (Demo)

### 👤 Luồng khách hàng

- Đăng nhập / đăng ký (giả lập, kiểm tra tài khoản mẫu)
- Duyệt danh mục sản phẩm: gọng kính, tròng kính, combo
- Xem chi tiết sản phẩm (hình ảnh, giá, màu sắc, kích thước)
- Thêm sản phẩm vào giỏ hàng
- Quản lý giỏ hàng (tăng/giảm số lượng, xóa)
- Đặt hàng với 3 loại:
  - Mua kính có sẵn
  - Đặt trước (pre-order)
  - Làm kính theo đơn (nhập thông số mắt)
- Xem lịch sử đơn hàng mẫu (dữ liệu cố định)
- Mô phỏng thanh toán COD (chỉ hiển thị thông báo)
- Gửi yêu cầu đổi trả (form gửi đi – không lưu thực tế)

### 🧑‍💼 Luồng nhân viên bán hàng (chế độ xem demo)

- Xem danh sách đơn hàng mô phỏng
- Cập nhật trạng thái đơn hàng (demo)
- Xử lý đơn kính (prescription)

### 📦 Luồng nhân viên vận hành

- Quản lý tồn kho (mock)
- Tạo vận đơn (demo)

### 📊 Luồng quản lý / Admin

- Xem báo cáo doanh thu (biểu đồ đơn giản từ mock data)
- Quản lý sản phẩm (thêm/sửa/xóa trong giao diện – dữ liệu không lưu lâu dài)

## 🧱 Công nghệ sử dụng

| Thành phần      | Công nghệ                                    |
| --------------- | -------------------------------------------- |
| **Giao diện**   | HTML5, CSS3, Bootstrap 5                     |
| **Tương tác**   | JavaScript (thuần)                           |
| **Lưu trữ tạm** | `localStorage`, sessionStorage, hoặc mảng JS |
| **Biểu đồ**     | Chart.js (tuỳ chọn)                          |
| **Icons**       | FontAwesome / Bootstrap Icons                |
