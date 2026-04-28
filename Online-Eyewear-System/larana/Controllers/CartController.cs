using Microsoft.AspNetCore.Mvc;
using OpticalStore.WebApplication.Models;

namespace OpticalStore.WebApplication.Controllers
{
    public class CartController : Controller
    {
        private int? CurrentUserId => HttpContext.Session.GetInt32("UserId");

        // GET /Cart
        public IActionResult Index()
        {
            if (CurrentUserId == null)
                return RedirectToAction("Login", "Auth", new { returnUrl = "/Cart" });

            var items = StaticData.GetCart(CurrentUserId.Value);
            return View(items);
        }

        // POST /Cart/Add
        [HttpPost]
        public IActionResult Add(int variantId, int quantity = 1)
        {
            if (CurrentUserId == null)
                return RedirectToAction("Login", "Auth", new { returnUrl = "/Cart" });

            StaticData.AddToCart(CurrentUserId.Value, variantId, quantity);
            TempData["CartMsg"] = "Đã thêm vào giỏ hàng!";
            return RedirectToAction("Index");
        }

        // POST /Cart/UpdateQuantity
        [HttpPost]
        public IActionResult UpdateQuantity(int cartItemId, int quantity)
        {
            if (CurrentUserId == null)
                return RedirectToAction("Login", "Auth");

            StaticData.UpdateCartItem(cartItemId, quantity);
            return RedirectToAction("Index");
        }

        // POST /Cart/Remove
        [HttpPost]
        public IActionResult Remove(int cartItemId)
        {
            if (CurrentUserId == null)
                return RedirectToAction("Login", "Auth");

            StaticData.RemoveCartItem(cartItemId);
            return RedirectToAction("Index");
        }
    }
}
