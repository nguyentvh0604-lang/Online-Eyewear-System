using Microsoft.AspNetCore.Mvc;
using OpticalStore.Services.Interfaces;

namespace OpticalStore.WebApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService) => _userService = userService;

        [HttpGet] public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userService.LoginAsync(email, password);
            if (user == null)
            {
                ViewBag.Error = "Email hoặc mật khẩu không đúng.";
                return View();
            }
            // Lưu thông tin vào Session
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("UserRole", user.Role);

            // Điều hướng theo role
            return user.Role == "admin" || user.Role == "manager"
                ? RedirectToAction("Dashboard", "Admin")
                : RedirectToAction("Index", "Product");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}
