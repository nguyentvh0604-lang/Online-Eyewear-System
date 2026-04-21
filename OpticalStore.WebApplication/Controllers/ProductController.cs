using Microsoft.AspNetCore.Mvc;
using OpticalStore.Services.Interfaces;

namespace OpticalStore.WebApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET /Product
        public async Task<IActionResult> Index(string? keyword, int? categoryId)
        {
            var products = string.IsNullOrEmpty(keyword)
                ? await _productService.GetAllProductsAsync()
                : await _productService.SearchProductsAsync(keyword);

            ViewBag.Keyword = keyword;
            return View(products);
        }

        // GET /Product/Detail/5
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productService.GetProductDetailAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        private IActionResult? CheckLogin()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "Auth");
            return null;
        }

    }
}
