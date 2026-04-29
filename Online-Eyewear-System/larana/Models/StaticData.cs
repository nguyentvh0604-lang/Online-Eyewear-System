using System.Text.Json;

namespace OpticalStore.WebApplication.Models
{
    // ─── Domain models ─────────────────────────────────────────────────────────

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ProductVariant
    {
        public int VariantId { get; set; }
        public int ProductId { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string FrameMaterial { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Sku { get; set; } = string.Empty;
        public int Stock { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Category? Category { get; set; }
        public List<ProductVariant> Variants { get; set; } = new();

        public decimal MinPrice => Variants.Any() ? Variants.Min(v => v.Price) : 0;
        public int TotalStock => Variants.Sum(v => v.Stock);
        public string StockStatus => TotalStock == 0 ? "Hết hàng"
                                   : TotalStock <= 5  ? "Còn ít"
                                                      : "Còn hàng";
    }

    public class AppUser
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // plain-text for demo
        public string Role { get; set; } = "customer";       // customer | admin
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class CartItem
    {
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public int VariantId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;

        // Navigation (resolved at runtime)
        public ProductVariant? Variant { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int VariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal => UnitPrice * Quantity;

        // Navigation
        public ProductVariant? Variant { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "pending";           // pending | shipping | completed | cancelled
        public string OrderType { get; set; } = "standard";       // standard | pre_order | prescription
        public string ShippingAddress { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public decimal FinalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }

    // ─── Static Data Store ─────────────────────────────────────────────────────

    public static class StaticData
    {
        // ── Seed Data ──────────────────────────────────────────────────────

        public static List<Category> Categories { get; } = new()
        {
            new() { CategoryId = 1, Name = "Kính cận" },
            new() { CategoryId = 2, Name = "Kính mát" },
            new() { CategoryId = 3, Name = "Kính thể thao" },
            new() { CategoryId = 4, Name = "Gọng kính" },
        };

        public static List<Product> Products { get; } = new()
        {
            new Product
            {
                ProductId = 1, CategoryId = 1,
                Name = "Kính cận Ray-Ban RB5154",
                ProductType = "frame",
                Description = "Gọng kính cổ điển dáng clubmaster, chất liệu acetate cao cấp, phù hợp mặt oval và trái tim. Thiết kế vượt thời gian, sang trọng.",
                ImageUrl = "/images/images1.jpg",
                Category = Categories[0],
                Variants = new()
                {
                    new() { VariantId = 101, ProductId = 1, Color = "Đen", Size = "M", FrameMaterial = "Acetate", Price = 2_850_000, Sku = "RB5154-BLK-M", Stock = 12 },
                    new() { VariantId = 102, ProductId = 1, Color = "Nâu", Size = "M", FrameMaterial = "Acetate", Price = 2_850_000, Sku = "RB5154-BRN-M", Stock = 8 },
                    new() { VariantId = 103, ProductId = 1, Color = "Vàng", Size = "L", FrameMaterial = "Acetate", Price = 3_100_000, Sku = "RB5154-GLD-L", Stock = 4 },
                }
            },
            new Product
            {
                ProductId = 2, CategoryId = 1,
                Name = "Kính cận Oakley OX8046",
                ProductType = "frame",
                Description = "Gọng titanium siêu nhẹ chỉ 18g, công nghệ Three Point Fit giữ kính ổn định. Lý tưởng cho người hoạt động nhiều.",
                ImageUrl = "/images/images2.jpg",
                Category = Categories[0],
                Variants = new()
                {
                    new() { VariantId = 201, ProductId = 2, Color = "Bạc", Size = "S", FrameMaterial = "Titanium", Price = 4_200_000, Sku = "OX8046-SLV-S", Stock = 5 },
                    new() { VariantId = 202, ProductId = 2, Color = "Xám", Size = "M", FrameMaterial = "Titanium", Price = 4_200_000, Sku = "OX8046-GRY-M", Stock = 3 },
                }
            },
            new Product
            {
                ProductId = 3, CategoryId = 2,
                Name = "Kính mát Gucci GG0061S",
                ProductType = "frame",
                Description = "Kính mát thời trang dáng cat-eye biểu tượng của Gucci, tròng phân cực UV400, phù hợp đi biển và dạo phố.",
                ImageUrl = "/images/images3.jpg",
                Category = Categories[1],
                Variants = new()
                {
                    new() { VariantId = 301, ProductId = 3, Color = "Đen/Vàng", Size = "M", FrameMaterial = "Acetate", Price = 8_500_000, Sku = "GG0061S-BG-M", Stock = 6 },
                    new() { VariantId = 302, ProductId = 3, Color = "Đồi mồi", Size = "M", FrameMaterial = "Acetate", Price = 8_500_000, Sku = "GG0061S-TRT-M", Stock = 2 },
                }
            },
            new Product
            {
                ProductId = 4, CategoryId = 2,
                Name = "Kính mát Maui Jim MJ780",
                ProductType = "frame",
                Description = "Tròng kính PolarizedPlus2 loại bỏ hoàn toàn chói lóa, màu sắc sống động hơn 20% so với kính thường. Lý tưởng cho hoạt động ngoài trời.",
                ImageUrl = "/images/images4.jpg",
                Category = Categories[1],
                Variants = new()
                {
                    new() { VariantId = 401, ProductId = 4, Color = "Xanh dương", Size = "L", FrameMaterial = "Nylon", Price = 5_600_000, Sku = "MJ780-BLU-L", Stock = 9 },
                    new() { VariantId = 402, ProductId = 4, Color = "Đen", Size = "L", FrameMaterial = "Nylon", Price = 5_600_000, Sku = "MJ780-BLK-L", Stock = 7 },
                    new() { VariantId = 403, ProductId = 4, Color = "Xám", Size = "M", FrameMaterial = "Nylon", Price = 5_200_000, Sku = "MJ780-GRY-M", Stock = 0 },
                }
            },
            new Product
            {
                ProductId = 5, CategoryId = 3,
                Name = "Kính thể thao Oakley Radar EV",
                ProductType = "frame",
                Description = "Thiết kế khí động học tối ưu tầm nhìn ngoại vi, tròng Prizm™ tăng độ tương phản. Được các VĐV Olympic tin dùng.",
                ImageUrl = "/images/images5.jpg",
                Category = Categories[2],
                Variants = new()
                {
                    new() { VariantId = 501, ProductId = 5, Color = "Trắng/Xanh lá", Size = "L", FrameMaterial = "O Matter", Price = 6_800_000, Sku = "RADAREV-WGR-L", Stock = 4 },
                    new() { VariantId = 502, ProductId = 5, Color = "Đen/Vàng", Size = "L", FrameMaterial = "O Matter", Price = 6_800_000, Sku = "RADAREV-BYL-L", Stock = 4 },
                }
            },
            new Product
            {
                ProductId = 6, CategoryId = 3,
                Name = "Kính thể thao Nike Vision EV0925",
                ProductType = "frame",
                Description = "Gọng kính linh hoạt, đệm mũi mềm chống trơn trượt, phù hợp chạy bộ, đạp xe và các môn thể thao tốc độ cao.",
                ImageUrl = "/images/images6.jpg",
                Category = Categories[2],
                Variants = new()
                {
                    new() { VariantId = 601, ProductId = 6, Color = "Đen", Size = "M", FrameMaterial = "Grilamid", Price = 3_400_000, Sku = "EV0925-BLK-M", Stock = 0 },
                    new() { VariantId = 602, ProductId = 6, Color = "Xanh navy", Size = "M", FrameMaterial = "Grilamid", Price = 3_400_000, Sku = "EV0925-NVY-M", Stock = 1 },
                }
            },
            new Product
            {
                ProductId = 7, CategoryId = 4,
                Name = "Gọng kính Titanium Silhouette 5515",
                ProductType = "frame",
                Description = "Gọng không vành (rimless) nổi tiếng thế giới, trọng lượng chỉ 1.8g, cảm giác như không đeo kính.",
                ImageUrl = "/images/images7.jpg",
                Category = Categories[3],
                Variants = new()
                {
                    new() { VariantId = 701, ProductId = 7, Color = "Vàng hồng", Size = "S", FrameMaterial = "Titanium", Price = 7_200_000, Sku = "SIL5515-RGD-S", Stock = 3 },
                    new() { VariantId = 702, ProductId = 7, Color = "Bạc", Size = "M", FrameMaterial = "Titanium", Price = 7_200_000, Sku = "SIL5515-SLV-M", Stock = 5 },
                }
            },
            new Product
            {
                ProductId = 8, CategoryId = 4,
                Name = "Gọng kính Lindberg Strip 9700",
                ProductType = "frame",
                Description = "Thiết kế Đan Mạch tối giản, gọng acetate kết hợp càng titanium. Có thể cá nhân hóa màu sắc và kích thước theo yêu cầu.",
                ImageUrl = "/images/images8.jpg",
                Category = Categories[3],
                Variants = new()
                {
                    new() { VariantId = 801, ProductId = 8, Color = "Trong suốt", Size = "S", FrameMaterial = "Acetate + Titanium", Price = 9_500_000, Sku = "LBG9700-CLR-S", Stock = 2 },
                    new() { VariantId = 802, ProductId = 8, Color = "Nâu đồi mồi", Size = "M", FrameMaterial = "Acetate + Titanium", Price = 9_500_000, Sku = "LBG9700-TRT-M", Stock = 6 },
                    new() { VariantId = 803, ProductId = 8, Color = "Xanh lam", Size = "L", FrameMaterial = "Acetate + Titanium", Price = 9_900_000, Sku = "LBG9700-BLU-L", Stock = 0 },
                }
            },
        };

        public static List<AppUser> Users { get; } = new()
        {
            new() { UserId = 1, FullName = "Admin Larana",   Email = "admin@larana.vn",    Password = "123",    Role = "admin",    Phone = "0901234567", Address = "123 Lê Lợi, Q1, TP.HCM" },
            new() { UserId = 2, FullName = "Trần Võ Hoài Nguyên",  Email = "hn@example.com",      Password = "123", Role = "customer", Phone = "0912345678", Address = "456 Nguyễn Huệ, Q1, TP.HCM" },
            new() { UserId = 3, FullName = "Trần Võ Hoài Nguyên",  Email = "nguyen@example.com",    Password = "123", Role = "customer", Phone = "0923456789", Address = "11 đường số 7 Khu phố 4, TP. Thủ Đức" },
        };

        // ── Runtime mutable stores (InMemory) ─────────────────────────────

        private static List<CartItem> _cartItems = new();
        private static List<Order> _orders = new();
        private static int _cartItemIdCounter = 1;
        private static int _orderIdCounter = 1;

        // ── Product helpers ────────────────────────────────────────────────

        public static List<Product> GetByCategory(int? categoryId)
        {
            if (categoryId == null || categoryId == 0)
                return Products.Where(p => p.IsActive).ToList();
            return Products.Where(p => p.IsActive && p.CategoryId == categoryId).ToList();
        }

        public static List<Product> Search(string? keyword, int? categoryId = null)
        {
            var query = Products.Where(p => p.IsActive).AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(p =>
                    p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    p.Category!.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            if (categoryId.HasValue && categoryId > 0)
                query = query.Where(p => p.CategoryId == categoryId);
            return query.ToList();
        }

        public static Product? GetById(int id) => Products.FirstOrDefault(p => p.ProductId == id);

        public static ProductVariant? GetVariantBySku(string sku)
            => Products.SelectMany(p => p.Variants)
                       .FirstOrDefault(v => v.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));

        public static ProductVariant? GetVariantById(int variantId)
            => Products.SelectMany(p => p.Variants)
                       .FirstOrDefault(v => v.VariantId == variantId);

        // ── Auth helpers ───────────────────────────────────────────────────

        public static AppUser? Login(string email, string password)
            => Users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

        public static bool Register(string fullName, string email, string password, string phone)
        {
            if (Users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                return false;
            var newId = Users.Max(u => u.UserId) + 1;
            Users.Add(new AppUser
            {
                UserId = newId,
                FullName = fullName,
                Email = email,
                Password = password,
                Phone = phone,
                Role = "customer"
            });
            return true;
        }

        public static AppUser? GetUserById(int id) => Users.FirstOrDefault(u => u.UserId == id);

        // ── Cart helpers ───────────────────────────────────────────────────

        public static List<CartItem> GetCart(int userId)
        {
            var items = _cartItems.Where(c => c.UserId == userId).ToList();
            // Attach navigation
            foreach (var item in items)
                item.Variant = GetVariantById(item.VariantId);
            return items;
        }

        public static void AddToCart(int userId, int variantId, int quantity = 1)
        {
            var existing = _cartItems.FirstOrDefault(c => c.UserId == userId && c.VariantId == variantId);
            if (existing != null)
                existing.Quantity += quantity;
            else
                _cartItems.Add(new CartItem
                {
                    CartItemId = _cartItemIdCounter++,
                    UserId = userId,
                    VariantId = variantId,
                    Quantity = quantity,
                    AddedAt = DateTime.Now
                });
        }

        public static void UpdateCartItem(int cartItemId, int quantity)
        {
            var item = _cartItems.FirstOrDefault(c => c.CartItemId == cartItemId);
            if (item != null) item.Quantity = Math.Max(1, quantity);
        }

        public static void RemoveCartItem(int cartItemId)
            => _cartItems.RemoveAll(c => c.CartItemId == cartItemId);

        public static void ClearCart(int userId)
            => _cartItems.RemoveAll(c => c.UserId == userId);

        public static int GetCartCount(int userId)
            => _cartItems.Where(c => c.UserId == userId).Sum(c => c.Quantity);

        // ── Order helpers ──────────────────────────────────────────────────

        public static Order PlaceOrder(int userId, string shippingAddress, string note, string orderType)
        {
            var cartItems = GetCart(userId);
            if (!cartItems.Any()) throw new InvalidOperationException("Giỏ hàng trống.");

            var orderItems = cartItems.Select(c =>
            {
                var variant = GetVariantById(c.VariantId);
                return new OrderItem
                {
                    OrderItemId = 0,
                    VariantId = c.VariantId,
                    Quantity = c.Quantity,
                    UnitPrice = variant?.Price ?? 0,
                    Variant = variant
                };
            }).ToList();

            var total = orderItems.Sum(oi => oi.Subtotal);

            var order = new Order
            {
                OrderId = _orderIdCounter++,
                UserId = userId,
                CreatedAt = DateTime.Now,
                Status = "pending",
                OrderType = orderType,
                ShippingAddress = shippingAddress,
                Note = note,
                FinalAmount = total,
                OrderItems = orderItems
            };

            _orders.Add(order);
            ClearCart(userId);
            return order;
        }

        public static List<Order> GetOrdersByUser(int userId)
            => _orders.Where(o => o.UserId == userId)
                      .OrderByDescending(o => o.CreatedAt)
                      .ToList();

        public static Order? GetOrderById(int orderId)
            => _orders.FirstOrDefault(o => o.OrderId == orderId);

        public static List<Order> GetAllOrders()
            => _orders.OrderByDescending(o => o.CreatedAt).ToList();

        public static void UpdateOrderStatus(int orderId, string status)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null) order.Status = status;
        }
    }
}
