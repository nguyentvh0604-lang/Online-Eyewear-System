using Microsoft.AspNetCore.Mvc;
using OpticalStore.WebApplication.Models;
using OpticalStore.WebApplication.Models.ViewModels;

namespace OpticalStore.WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private int? CurrentUserId => HttpContext.Session.GetInt32("UserId");

        // GET /Order/Checkout
        public IActionResult Checkout()
        {
            if (CurrentUserId == null)
                return RedirectToAction("Login", "Auth", new { returnUrl = "/Order/Checkout" });

            var cart = StaticData.GetCart(CurrentUserId.Value);
            if (!cart.Any())
            {
                TempData["CartMsg"] = "Giỏ hàng trống, vui lòng thêm sản phẩm trước.";
                return RedirectToAction("Index", "Cart");
            }

            var vm = new CheckoutViewModel
            {
                CartItems = cart,
                ShippingAddress = StaticData.GetUserById(CurrentUserId.Value)?.Address ?? ""
            };
            return View(vm);
        }

        // POST /Order/Checkout
        [HttpPost]
        public IActionResult Checkout(string shippingAddress, string note, string orderType)
        {
            if (CurrentUserId == null)
                return RedirectToAction("Login", "Auth");

            try
            {
                var order = StaticData.PlaceOrder(CurrentUserId.Value, shippingAddress, note, orderType ?? "standard");
                TempData["Success"] = $"Đặt hàng thành công! Mã đơn: #{order.OrderId}";
                return RedirectToAction("Detail", new { id = order.OrderId });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Checkout");
            }
        }

        // GET /Order/History
        public IActionResult History()
        {
            if (CurrentUserId == null)
                return RedirectToAction("Login", "Auth");

            var orders = StaticData.GetOrdersByUser(CurrentUserId.Value);
            return View(orders);
        }

        // GET /Order/Detail/5
        public IActionResult Detail(int id)
        {
            if (CurrentUserId == null)
                return RedirectToAction("Login", "Auth");

            var order = StaticData.GetOrderById(id);
            if (order == null || order.UserId != CurrentUserId.Value)
                return NotFound();

            // Attach navigation for order items
            foreach (var item in order.OrderItems)
            {
                var variant = StaticData.GetVariantById(item.VariantId);
                if (variant != null)
                {
                    item.Variant = variant;
                    // Attach product to variant
                    var product = StaticData.Products.FirstOrDefault(p => p.Variants.Any(v => v.VariantId == variant.VariantId));
                    // We need to embed product info in variant name for display
                    // Store it via a simple trick: use a wrapper
                }
            }

            return View(order);
        }
    }
}
