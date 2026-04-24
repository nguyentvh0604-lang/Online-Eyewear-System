using Microsoft.AspNetCore.Mvc;
using OpticalStore.WebApplication.Models.ViewModels;
using OpticalStore.WebApplication.Services;

namespace OpticalStore.WebApplication.Controllers
{
    /// <summary>
    /// NV4 - ProductController
    /// Luồng: Action → Service → Repository → trả ViewModel về View
    /// </summary>
    public class ProductController : Controller
    {
        // Nhận IProductService qua Dependency Injection (đăng ký trong Program.cs)
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // ─────────────────────────────────────────────────────────────────
        // GET /Product  hoặc  /Product?keyword=ray&categoryId=1
        // Hiển thị danh sách sản phẩm, có tìm kiếm + lọc danh mục (NV3+NV5)
        // ─────────────────────────────────────────────────────────────────
        public IActionResult Index(string? keyword, int? categoryId)
        {
            // Gọi Service lấy danh sách theo điều kiện (NV3)
            var products = _productService.GetProducts(keyword, categoryId);

            // Gọi Service lấy danh mục cho dropdown (NV2)
            var categories = _productService.GetCategories();

            // Xây dựng ViewModel (NV4)
            var vm = new ProductListViewModel
            {
                Products           = products,
                Categories         = categories,
                Keyword            = keyword,
                SelectedCategoryId = categoryId,
            };

            return View(vm);
        }

        // ─────────────────────────────────────────────────────────────────
        // GET /Product/Detail/5
        // Trang chi tiết sản phẩm (NV4)
        // ─────────────────────────────────────────────────────────────────
        public IActionResult Detail(int id)
        {
            var product = _productService.GetProductDetail(id);

            // Không tìm thấy → 404
            if (product == null)
                return NotFound($"Không tìm thấy sản phẩm có ID = {id}");

            var vm = new ProductDetailViewModel
            {
                Product  = product,
                Variants = product.Variants,
            };

            return View(vm);
        }

        // ─────────────────────────────────────────────────────────────────
        // POST /Product/CheckStock
        // Kiểm tra tồn kho theo SKU (NV2+NV3), trả về trang danh sách
        // Dùng POST để tránh SKU xuất hiện trên URL
        // ─────────────────────────────────────────────────────────────────
        [HttpPost]
        public IActionResult CheckStock(string sku, string? keyword, int? categoryId)
        {
            // Gọi Service kiểm tra tồn kho (NV3)
            var stockResult = _productService.CheckStock(sku ?? "");

            // Lấy lại danh sách + danh mục để render lại trang
            var products   = _productService.GetProducts(keyword, categoryId);
            var categories = _productService.GetCategories();

            var vm = new ProductListViewModel
            {
                Products           = products,
                Categories         = categories,
                Keyword            = keyword,
                SelectedCategoryId = categoryId,
                StockResult        = stockResult,   // Kết quả kiểm tra (NV3)
                CheckedSku         = sku,
            };

            return View("Index", vm); // Render lại trang Index với kết quả
        }
    }
}
