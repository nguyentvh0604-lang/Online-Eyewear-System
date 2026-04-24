using OpticalStore.WebApplication.Models;
using OpticalStore.WebApplication.Repositories;

namespace OpticalStore.WebApplication.Services
{
    /// <summary>
    /// NV3 - ProductService: xử lý logic nghiệp vụ, gọi Repository bên dưới
    /// Luồng: Controller → Service → Repository → StaticData
    /// </summary>
    public class ProductService : IProductService
    {
        // Dependency Injection: Service nhận Repository qua constructor
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        // ── Lấy danh sách sản phẩm với logic tìm kiếm + lọc ─────────────
        public List<Product> GetProducts(string? keyword, int? categoryId)
        {
            // Nếu không có điều kiện → lấy tất cả
            if (string.IsNullOrWhiteSpace(keyword) && (categoryId == null || categoryId == 0))
                return _repo.GetAll();

            // Có điều kiện → dùng Search (NV3)
            return _repo.Search(keyword, categoryId);
        }

        // ── Lấy chi tiết sản phẩm theo ID ────────────────────────────────
        public Product? GetProductDetail(int id)
            => _repo.GetById(id);

        // ── Lấy danh sách danh mục ────────────────────────────────────────
        public List<Category> GetCategories()
            => _repo.GetAllCategories();

        // ── NV3: Kiểm tra tồn kho theo SKU ───────────────────────────────
        public StockCheckResult CheckStock(string sku)
        {
            var variant = _repo.GetVariantBySku(sku.Trim());

            // SKU không tồn tại
            if (variant == null)
                return new StockCheckResult { Found = false, Sku = sku };

            // Xác định trạng thái tồn kho
            var status = variant.Stock == 0 ? "Hết hàng"
                       : variant.Stock <= 5  ? "Còn ít"
                                             : "Còn hàng";

            return new StockCheckResult
            {
                Found  = true,
                Sku    = variant.Sku,
                Stock  = variant.Stock,
                Status = status,
                Color  = variant.Color,
                Size   = variant.Size,
                Price  = variant.Price,
            };
        }
    }
}
