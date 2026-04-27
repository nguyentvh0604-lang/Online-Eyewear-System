using Microsoft.AspNetCore.Mvc;
using OpticalStore.WebApplication.Models;

namespace OpticalStore.WebApplication.Controllers
{
    public class AdminController : Controller
    {
        private bool IsAdmin()
            => HttpContext.Session.GetString("UserRole") == "admin";

        public IActionResult Dashboard()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            ViewBag.TotalProducts = StaticData.Products.Count;
            ViewBag.TotalUsers    = StaticData.Users.Count;
            ViewBag.TotalOrders   = StaticData.GetAllOrders().Count;
            ViewBag.Revenue       = StaticData.GetAllOrders()
                                        .Where(o => o.Status == "completed")
                                        .Sum(o => o.FinalAmount);
            return View();
        }

        public IActionResult Orders()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            return View(StaticData.GetAllOrders());
        }

        [HttpPost]
        public IActionResult UpdateStatus(int orderId, string status)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            StaticData.UpdateOrderStatus(orderId, status);
            TempData["Success"] = $"Đã cập nhật trạng thái đơn #{orderId}";
            return RedirectToAction("Orders");
        }
    }
}
