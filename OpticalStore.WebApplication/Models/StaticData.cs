namespace OpticalStore.WebApplication.Models
{
    // ─── Domain models (tĩnh, không cần EF Core) ───────────────────────────

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class ProductVariant
    {
        public int VariantId { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string FrameMaterial { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Sku { get; set; } = string.Empty;
        public int Stock { get; set; }  // Tồn kho
    }

    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProductType { get; set; } = string.Empty; // frame | lens | service
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Category? Category { get; set; }
        public List<ProductVariant> Variants { get; set; } = new();

        // Giá thấp nhất trong các variant
        public decimal MinPrice => Variants.Any() ? Variants.Min(v => v.Price) : 0;

        // Tổng tồn kho
        public int TotalStock => Variants.Sum(v => v.Stock);

        // Trạng thái tồn kho
        public string StockStatus => TotalStock == 0 ? "Hết hàng"
                                   : TotalStock <= 5  ? "Còn ít"
                                                      : "Còn hàng";
    }

    // ─── Static data store ─────────────────────────────────────────────────

    public static class StaticData
    {
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
                ImageUrl = "https://placehold.co/400x300/0d6efd/white?text=Ray-Ban+RB5154",
                Category = Categories[0],
                Variants = new()
                {
                    new() { VariantId = 101, Color = "Đen", Size = "M", FrameMaterial = "Acetate", Price = 2_850_000, Sku = "RB5154-BLK-M", Stock = 12 },
                    new() { VariantId = 102, Color = "Nâu", Size = "M", FrameMaterial = "Acetate", Price = 2_850_000, Sku = "RB5154-BRN-M", Stock = 8 },
                    new() { VariantId = 103, Color = "Vàng", Size = "L", FrameMaterial = "Acetate", Price = 3_100_000, Sku = "RB5154-GLD-L", Stock = 4 },
                }
            },
            new Product
            {
                ProductId = 2, CategoryId = 1,
                Name = "Kính cận Oakley OX8046",
                ProductType = "frame",
                Description = "Gọng titanium siêu nhẹ chỉ 18g, công nghệ Three Point Fit giữ kính ổn định. Lý tưởng cho người hoạt động nhiều.",
                ImageUrl = "https://placehold.co/400x300/198754/white?text=Oakley+OX8046",
                Category = Categories[0],
                Variants = new()
                {
                    new() { VariantId = 201, Color = "Bạc", Size = "S", FrameMaterial = "Titanium", Price = 4_200_000, Sku = "OX8046-SLV-S", Stock = 5 },
                    new() { VariantId = 202, Color = "Xám", Size = "M", FrameMaterial = "Titanium", Price = 4_200_000, Sku = "OX8046-GRY-M", Stock = 3 },
                }
            },
            new Product
            {
                ProductId = 3, CategoryId = 2,
                Name = "Kính mát Gucci GG0061S",
                ProductType = "frame",
                Description = "Kính mát thời trang dáng cat-eye biểu tượng của Gucci, tròng phân cực UV400, phù hợp đi biển và dạo phố.",
                ImageUrl = "https://placehold.co/400x300/dc3545/white?text=Gucci+GG0061S",
                Category = Categories[1],
                Variants = new()
                {
                    new() { VariantId = 301, Color = "Đen/Vàng", Size = "M", FrameMaterial = "Acetate", Price = 8_500_000, Sku = "GG0061S-BG-M", Stock = 6 },
                    new() { VariantId = 302, Color = "Đồi mồi", Size = "M", FrameMaterial = "Acetate", Price = 8_500_000, Sku = "GG0061S-TRT-M", Stock = 2 },
                }
            },
            new Product
            {
                ProductId = 4, CategoryId = 2,
                Name = "Kính mát Maui Jim MJ780",
                ProductType = "frame",
                Description = "Tròng kính PolarizedPlus2 loại bỏ hoàn toàn chói lóa, màu sắc sống động hơn 20% so với kính thường. Lý tưởng cho hoạt động ngoài trời.",
                ImageUrl = "https://placehold.co/400x300/0dcaf0/333?text=Maui+Jim+MJ780",
                Category = Categories[1],
                Variants = new()
                {
                    new() { VariantId = 401, Color = "Xanh dương", Size = "L", FrameMaterial = "Nylon", Price = 5_600_000, Sku = "MJ780-BLU-L", Stock = 9 },
                    new() { VariantId = 402, Color = "Đen", Size = "L", FrameMaterial = "Nylon", Price = 5_600_000, Sku = "MJ780-BLK-L", Stock = 7 },
                    new() { VariantId = 403, Color = "Xám", Size = "M", FrameMaterial = "Nylon", Price = 5_200_000, Sku = "MJ780-GRY-M", Stock = 0 },
                }
            },
            new Product
            {
                ProductId = 5, CategoryId = 3,
                Name = "Kính thể thao Oakley Radar EV",
                ProductType = "frame",
                Description = "Thiết kế khí động học tối ưu tầm nhìn ngoại vi, tròng Prizm™ tăng độ tương phản. Được các VĐV Olympic tin dùng.",
                ImageUrl = "https://placehold.co/400x300/ffc107/333?text=Oakley+Radar+EV",
                Category = Categories[2],
                Variants = new()
                {
                    new() { VariantId = 501, Color = "Trắng/Xanh lá", Size = "L", FrameMaterial = "O Matter", Price = 6_800_000, Sku = "RADAREV-WGR-L", Stock = 4 },
                    new() { VariantId = 502, Color = "Đen/Vàng", Size = "L", FrameMaterial = "O Matter", Price = 6_800_000, Sku = "RADAREV-BYL-L", Stock = 4 },
                }
            },
            new Product
            {
                ProductId = 6, CategoryId = 3,
                Name = "Kính thể thao Nike Vision EV0925",
                ProductType = "frame",
                Description = "Gọng kính linh hoạt, đệm mũi mềm chống trơn trượt, phù hợp chạy bộ, đạp xe và các môn thể thao tốc độ cao.",
                ImageUrl = "https://placehold.co/400x300/6c757d/white?text=Nike+EV0925",
                Category = Categories[2],
                Variants = new()
                {
                    new() { VariantId = 601, Color = "Đen", Size = "M", FrameMaterial = "Grilamid", Price = 3_400_000, Sku = "EV0925-BLK-M", Stock = 0 },
                    new() { VariantId = 602, Color = "Xanh navy", Size = "M", FrameMaterial = "Grilamid", Price = 3_400_000, Sku = "EV0925-NVY-M", Stock = 1 },
                }
            },
            new Product
            {
                ProductId = 7, CategoryId = 4,
                Name = "Gọng kính Titanium Silhouette 5515",
                ProductType = "frame",
                Description = "Gọng không vành (rimless) nổi tiếng thế giới, trọng lượng chỉ 1.8g, cảm giác như không đeo kính. Được ưa chuộng bởi người bận rộn.",
                ImageUrl = "https://placehold.co/400x300/6610f2/white?text=Silhouette+5515",
                Category = Categories[3],
                Variants = new()
                {
                    new() { VariantId = 701, Color = "Vàng hồng", Size = "S", FrameMaterial = "Titanium", Price = 7_200_000, Sku = "SIL5515-RGD-S", Stock = 3 },
                    new() { VariantId = 702, Color = "Bạc", Size = "M", FrameMaterial = "Titanium", Price = 7_200_000, Sku = "SIL5515-SLV-M", Stock = 5 },
                }
            },
            new Product
            {
                ProductId = 8, CategoryId = 4,
                Name = "Gọng kính Lindberg Strip 9700",
                ProductType = "frame",
                Description = "Thiết kế Đan Mạch tối giản, gọng acetate kết hợp càng titanium. Có thể cá nhân hóa màu sắc và kích thước theo yêu cầu.",
                ImageUrl = "https://placehold.co/400x300/20c997/white?text=Lindberg+9700",
                Category = Categories[3],
                Variants = new()
                {
                    new() { VariantId = 801, Color = "Trong suốt", Size = "S", FrameMaterial = "Acetate + Titanium", Price = 9_500_000, Sku = "LBG9700-CLR-S", Stock = 2 },
                    new() { VariantId = 802, Color = "Nâu đồi mồi", Size = "M", FrameMaterial = "Acetate + Titanium", Price = 9_500_000, Sku = "LBG9700-TRT-M", Stock = 6 },
                    new() { VariantId = 803, Color = "Xanh lam", Size = "L", FrameMaterial = "Acetate + Titanium", Price = 9_900_000, Sku = "LBG9700-BLU-L", Stock = 0 },
                }
            },
        };

        // ─── NV2: Lọc theo danh mục ──────────────────────────────────────
        public static List<Product> GetByCategory(int? categoryId)
        {
            if (categoryId == null || categoryId == 0)
                return Products.Where(p => p.IsActive).ToList();
            return Products.Where(p => p.IsActive && p.CategoryId == categoryId).ToList();
        }

        // ─── NV3: Tìm kiếm sản phẩm ─────────────────────────────────────
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

        // ─── NV3: Kiểm tra tồn kho theo SKU ─────────────────────────────
        public static ProductVariant? GetVariantBySku(string sku)
            => Products.SelectMany(p => p.Variants)
                       .FirstOrDefault(v => v.Sku.Equals(sku, StringComparison.OrdinalIgnoreCase));

        // ─── NV4: Lấy chi tiết sản phẩm ─────────────────────────────────
        public static Product? GetById(int id)
            => Products.FirstOrDefault(p => p.ProductId == id);
    }
}
