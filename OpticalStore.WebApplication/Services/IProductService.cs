using OpticalStore.WebApplication.Models;

namespace OpticalStore.WebApplication.Services
{
    /// <summary>
    /// NV3 - Interface Service: định nghĩa logic nghiệp vụ tìm kiếm, lọc, kiểm tra tồn kho
    /// </summary>
    public interface IProductService
    {
        // Lấy danh sách sản phẩm (có thể tìm kiếm + lọc danh mục kết hợp)
        List<Product> GetProducts(string? keyword, int? categoryId);

        // Lấy chi tiết 1 sản phẩm
        Product? GetProductDetail(int id);

        // Lấy danh sách danh mục (dùng cho dropdown lọc)
        List<Category> GetCategories();

        // NV3: Kiểm tra tồn kho theo SKU — trả về thông tin variant + trạng thái
        StockCheckResult CheckStock(string sku);
    }

    /// <summary>
    /// Kết quả kiểm tra tồn kho (NV3)
    /// </summary>
    public class StockCheckResult
    {
        public bool Found { get; set; }          // Có tìm thấy SKU hay không
        public string Sku { get; set; } = "";
        public int Stock { get; set; }           // Số lượng tồn
        public string Status { get; set; } = ""; // Còn hàng / Còn ít / Hết hàng
        public string Color { get; set; } = "";
        public string Size { get; set; } = "";
        public decimal Price { get; set; }
    }
}
