using Microsoft.AspNetCore.Mvc;
using OpticalStore.WebApplication.Models;

namespace OpticalStore.WebApplication.Controllers
{
    public class AuthController : Controller
    {
        // GET /Auth/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
                return RedirectToAction("Index", "Home");
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST /Auth/Login
        [HttpPost]
        public IActionResult Login(string email, string password, string? returnUrl)
        {
            var user = StaticData.Login(email, password);
            if (user == null)
            {
                ViewBag.Error = "Email hoặc mật khẩu không đúng.";
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            HttpContext.Session.SetInt32("UserId",   user.UserId);
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("UserRole", user.Role);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return user.Role == "admin"
                ? RedirectToAction("Dashboard", "Admin")
                : RedirectToAction("Index", "Product");
        }

        // GET /Auth/Register
        [HttpGet]
        public IActionResult Register() => View();

        // POST /Auth/Register
        [HttpPost]
        public IActionResult Register(string fullName, string email, string password, string confirmPassword, string phone)
        {
            if (password != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp.";
                return View();
            }
            bool ok = StaticData.Register(fullName, email, password, phone);
            if (!ok)
            {
                ViewBag.Error = "Email đã được sử dụng.";
                return View();
            }
            TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            return RedirectToAction("Login");
        }

        // GET /Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // GET /Auth/Profile
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");
            var user = StaticData.GetUserById(userId.Value);
            return View(user);
        }
    }
}
