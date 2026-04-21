-- 1. TẠO DATABASE
CREATE DATABASE opticalstore CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE opticalstore;

-- 2. TẠO CÁC BẢNG (Theo đúng thứ tự để không lỗi khóa ngoại)

-- Bảng 1: users
CREATE TABLE users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    full_name VARCHAR(150) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    phone VARCHAR(20),
    password_hash VARCHAR(255) NOT NULL,
    role ENUM('customer', 'sales_staff', 'operations_staff', 'manager', 'admin') NOT NULL DEFAULT 'customer',
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Bảng 2: categories
CREATE TABLE categories (
    category_id INT AUTO_INCREMENT PRIMARY KEY,
    parent_category_id INT DEFAULT NULL,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    FOREIGN KEY (parent_category_id) REFERENCES categories(category_id) ON DELETE SET NULL
);

-- Bảng 3: products
CREATE TABLE products (
    product_id INT AUTO_INCREMENT PRIMARY KEY,
    category_id INT NOT NULL,
    name VARCHAR(200) NOT NULL,
    product_type ENUM('frame', 'lens', 'service') NOT NULL,
    description TEXT,
    image_2d_url VARCHAR(500),
    image_3d_url VARCHAR(500),
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (category_id) REFERENCES categories(category_id) ON DELETE RESTRICT
);

-- Bảng 4: product_variants
CREATE TABLE product_variants (
    variant_id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    color VARCHAR(50),
    size VARCHAR(50),
    frame_material VARCHAR(100),
    price DECIMAL(12,2) NOT NULL,
    sku VARCHAR(100) NOT NULL UNIQUE,
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    FOREIGN KEY (product_id) REFERENCES products(product_id) ON DELETE CASCADE
);

-- Bảng 5: inventory
CREATE TABLE inventory (
    inventory_id INT AUTO_INCREMENT PRIMARY KEY,
    variant_id INT NOT NULL UNIQUE,
    quantity INT NOT NULL DEFAULT 0,
    reserved_qty INT NOT NULL DEFAULT 0,
    last_updated DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (variant_id) REFERENCES product_variants(variant_id) ON DELETE CASCADE
);

-- Bảng 6: cart
CREATE TABLE cart (
    cart_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    variant_id INT NOT NULL,
    quantity INT NOT NULL DEFAULT 1,
    added_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UNIQUE KEY uq_cart_user_variant (user_id, variant_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (variant_id) REFERENCES product_variants(variant_id) ON DELETE CASCADE
);

-- Bảng 7: prescriptions
CREATE TABLE prescriptions (
    prescription_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    od_sphere DECIMAL(5,2),
    od_cylinder DECIMAL(5,2),
    od_axis SMALLINT,
    os_sphere DECIMAL(5,2),
    os_cylinder DECIMAL(5,2),
    os_axis SMALLINT,
    pd DECIMAL(5,2),
    note TEXT,
    verified_by INT DEFAULT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE RESTRICT,
    FOREIGN KEY (verified_by) REFERENCES users(user_id) ON DELETE SET NULL
);

-- Bảng 8: promotions
CREATE TABLE promotions (
    promotion_id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    discount_type ENUM('percent', 'fixed') NOT NULL,
    discount_value DECIMAL(12,2) NOT NULL,
    min_order_value DECIMAL(12,2) DEFAULT 0,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    is_active TINYINT(1) NOT NULL DEFAULT 1,
    created_by INT NOT NULL,
    FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE RESTRICT
);

-- Bảng 9: policies
CREATE TABLE policies (
    policy_id INT AUTO_INCREMENT PRIMARY KEY,
    policy_type ENUM('purchase', 'return', 'warranty') NOT NULL,
    title VARCHAR(300) NOT NULL,
    content TEXT,
    effective_date DATE NOT NULL,
    created_by INT NOT NULL,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (created_by) REFERENCES users(user_id) ON DELETE RESTRICT
);

-- Bảng 10: orders
CREATE TABLE orders (
    order_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    prescription_id INT DEFAULT NULL,
    promotion_id INT DEFAULT NULL,
    order_type ENUM('standard', 'pre_order', 'prescription') NOT NULL DEFAULT 'standard',
    status ENUM('pending', 'confirmed', 'processing', 'shipping', 'completed', 'cancelled') NOT NULL DEFAULT 'pending',
    total_amount DECIMAL(14,2) NOT NULL DEFAULT 0,
    discount_amount DECIMAL(14,2) NOT NULL DEFAULT 0,
    final_amount DECIMAL(14,2) NOT NULL DEFAULT 0,
    shipping_address TEXT,
    note TEXT,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE RESTRICT,
    FOREIGN KEY (prescription_id) REFERENCES prescriptions(prescription_id) ON DELETE SET NULL,
    FOREIGN KEY (promotion_id) REFERENCES promotions(promotion_id) ON DELETE SET NULL
);

-- Bảng 11: order_items
CREATE TABLE order_items (
    order_item_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    variant_id INT NOT NULL,
    quantity INT NOT NULL DEFAULT 1,
    unit_price DECIMAL(12,2) NOT NULL,
    subtotal DECIMAL(14,2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(order_id) ON DELETE CASCADE,
    FOREIGN KEY (variant_id) REFERENCES product_variants(variant_id) ON DELETE RESTRICT
);

-- Bảng 12: payments
CREATE TABLE payments (
    payment_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL UNIQUE,
    method ENUM('cod', 'bank_transfer') NOT NULL DEFAULT 'cod',
    status ENUM('pending', 'paid', 'refunded', 'failed') NOT NULL DEFAULT 'pending',
    amount DECIMAL(14,2) NOT NULL,
    transaction_ref VARCHAR(200),
    paid_at DATETIME DEFAULT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(order_id) ON DELETE CASCADE
);

-- Bảng 13: shipments
CREATE TABLE shipments (
    shipment_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL UNIQUE,
    handled_by INT DEFAULT NULL,
    carrier VARCHAR(100),
    tracking_number VARCHAR(200),
    status ENUM('preparing', 'in_transit', 'delivered', 'failed') NOT NULL DEFAULT 'preparing',
    shipped_at DATETIME DEFAULT NULL,
    delivered_at DATETIME DEFAULT NULL,
    note TEXT,
    FOREIGN KEY (order_id) REFERENCES orders(order_id) ON DELETE CASCADE,
    FOREIGN KEY (handled_by) REFERENCES users(user_id) ON DELETE SET NULL
);

-- Bảng 14: return_requests
CREATE TABLE return_requests (
    return_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    user_id INT NOT NULL,
    policy_id INT DEFAULT NULL,
    handled_by INT DEFAULT NULL,
    reason TEXT,
    request_type ENUM('return', 'exchange', 'warranty') NOT NULL,
    status ENUM('pending', 'approved', 'rejected', 'completed') NOT NULL DEFAULT 'pending',
    refund_amount DECIMAL(14,2) DEFAULT 0,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    resolved_at DATETIME DEFAULT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(order_id) ON DELETE RESTRICT,
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE RESTRICT,
    FOREIGN KEY (policy_id) REFERENCES policies(policy_id) ON DELETE SET NULL,
    FOREIGN KEY (handled_by) REFERENCES users(user_id) ON DELETE SET NULL
);

-- 3. TẠO INDEXES ĐỂ TỐI ƯU TRUY VẤN
CREATE INDEX idx_products_category ON products(category_id);
CREATE INDEX idx_products_active ON products(is_active);
CREATE INDEX idx_orders_user ON orders(user_id);
CREATE INDEX idx_orders_status ON orders(status);
CREATE INDEX idx_cart_user ON cart(user_id);

-- Admin user (password: Admin@123)
INSERT INTO users (full_name, email, phone, password_hash, role) VALUES
('Admin System', 'admin@opticalstore.vn', '0901234567',
 '$2a$11$xxx...', 'admin');
 
-- Danh mục gốc
INSERT INTO categories (name, description) VALUES
('Gọng kính', 'Các loại gọng kính'),
('Tròng kính', 'Các loại tròng kính'),
('Dịch vụ', 'Dịch vụ làm kính');
 
-- Sản phẩm mẫu
INSERT INTO products (category_id, name, product_type, description, is_active) VALUES
(1, 'Gọng Ray-Ban Classic', 'frame', 'Gọng kim loại cao cấp', 1),
(2, 'Tròng cận Essilor', 'lens', 'Tròng chống tia UV', 1);
 
-- Biến thể sản phẩm
INSERT INTO product_variants (product_id, color, size, frame_material, price, sku) VALUES
(1, 'Đen', 'M', 'Titan', 850000, 'RB-001-BLK-M'),
(1, 'Vàng', 'L', 'Titan', 950000, 'RB-001-GLD-L');
 
-- Tồn kho
INSERT INTO inventory (variant_id, quantity, reserved_qty) VALUES
(1, 50, 0), (2, 30, 0);
