using Microsoft.AspNetCore.Mvc;
using OpticalStore.WebApplication.Models;
using OpticalStore.WebApplication.Models.ViewModels;

namespace OpticalStore.WebApplication.Controllers
{
    public class ProductController : Controller
    {
        // GET /Product?keyword=...&categoryId=...
        public IActionResult Index(string? keyword, int? categoryId)
        {
            var products   = StaticData.Search(keyword, categoryId);
            var categories = StaticData.Categories;

            var vm = new ProductListViewModel
            {
                Products           = products,
                Categories         = categories,
                Keyword            = keyword,
                SelectedCategoryId = categoryId,
            };
            return View(vm);
        }

        // GET /Product/Detail/5
        public IActionResult Detail(int id)
        {
            var product = StaticData.GetById(id);
            if (product == null) return NotFound($"Không tìm thấy sản phẩm ID = {id}");

            var vm = new ProductDetailViewModel
            {
                Product  = product,
                Variants = product.Variants,
            };
            return View(vm);
        }

        // POST /Product/CheckStock
        [HttpPost]
        public IActionResult CheckStock(string sku, string? keyword, int? categoryId)
        {
            StockCheckResult? result = null;
            var variant = StaticData.GetVariantBySku(sku ?? "");

            if (variant == null)
            {
                result = new StockCheckResult { Found = false, Sku = sku ?? "" };
            }
            else
            {
                result = new StockCheckResult
                {
                    Found  = true,
                    Sku    = variant.Sku,
                    Color  = variant.Color,
                    Size   = variant.Size,
                    Price  = variant.Price,
                    Stock  = variant.Stock,
                    Status = variant.Stock == 0 ? "Hết hàng"
                           : variant.Stock <= 5  ? "Còn ít"
                                                 : "Còn hàng",
                };
            }

            var vm = new ProductListViewModel
            {
                Products           = StaticData.Search(keyword, categoryId),
                Categories         = StaticData.Categories,
                Keyword            = keyword,
                SelectedCategoryId = categoryId,
                StockResult        = result,
                CheckedSku         = sku,
            };
            return View("Index", vm);
        }
    }
}
