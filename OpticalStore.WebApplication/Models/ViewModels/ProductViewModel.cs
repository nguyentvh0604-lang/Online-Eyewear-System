using OpticalStore.WebApplication.Models;
using OpticalStore.WebApplication.Services;

namespace OpticalStore.WebApplication.Models.ViewModels
{
    /// <summary>
    /// NV4 - ViewModel cho trang danh sách sản phẩm
    /// Gom tất cả dữ liệu View cần vào 1 object → View không phải gọi logic trực tiếp
    /// </summary>
    public class ProductListViewModel
    {
        // ── Dữ liệu hiển thị ─────────────────────────────────────────────
        public List<Product> Products { get; set; } = new();       // Danh sách kết quả
        public List<Category> Categories { get; set; } = new();    // Dropdown lọc danh mục

        // ── Trạng thái bộ lọc hiện tại (để giữ giá trị form) ─────────────
        public string? Keyword { get; set; }        // Từ khóa đang tìm
        public int? SelectedCategoryId { get; set; } // Danh mục đang chọn

        // ── Kết quả kiểm tra tồn kho (NV3) ───────────────────────────────
        public StockCheckResult? StockResult { get; set; }  // null = chưa kiểm tra
        public string? CheckedSku { get; set; }             // SKU vừa tra

        // ── Thống kê nhanh ────────────────────────────────────────────────
        public int TotalFound => Products.Count;    // Số sản phẩm tìm được
    }

    /// <summary>
    /// NV4 - ViewModel cho trang chi tiết sản phẩm
    /// </summary>
    public class ProductDetailViewModel
    {
        public Product Product { get; set; } = null!;      // Sản phẩm chính
        public List<ProductVariant> Variants { get; set; } = new(); // Các phiên bản
    }
}
