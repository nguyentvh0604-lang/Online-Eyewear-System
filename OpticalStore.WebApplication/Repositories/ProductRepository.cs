using OpticalStore.WebApplication.Models;

namespace OpticalStore.WebApplication.Repositories
{
    /// <summary>
    /// NV2 - ProductRepository: triển khai IProductRepository dùng dữ liệu hardcode (không có DB)
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        // ── Lấy toàn bộ sản phẩm đang hoạt động ─────────────────────────
        public List<Product> GetAll()
            => StaticData.Products
                         .Where(p => p.IsActive)
                         .ToList();

        // ── Lấy 1 sản phẩm theo ID ───────────────────────────────────────
        public Product? GetById(int id)
            => StaticData.GetById(id);

        // ── NV2: Lọc sản phẩm theo danh mục ─────────────────────────────
        public List<Product> GetByCategory(int categoryId)
            => StaticData.GetByCategory(categoryId);

        // ── NV2+3: Tìm kiếm theo từ khóa, kết hợp lọc danh mục ─────────
        public List<Product> Search(string? keyword, int? categoryId = null)
            => StaticData.Search(keyword, categoryId);

        // ── Lấy toàn bộ danh mục ────────────────────────────────────────
        public List<Category> GetAllCategories()
            => StaticData.Categories;

        // ── NV2: Kiểm tra tồn kho theo mã SKU ───────────────────────────
        public ProductVariant? GetVariantBySku(string sku)
            => StaticData.GetVariantBySku(sku);

        // ── Lấy tất cả variant của một sản phẩm ─────────────────────────
        public List<ProductVariant> GetVariantsByProductId(int productId)
        {
            var product = StaticData.GetById(productId);
            return product?.Variants ?? new List<ProductVariant>();
        }
    }
}
