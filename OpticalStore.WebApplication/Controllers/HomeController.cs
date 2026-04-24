using Microsoft.AspNetCore.Mvc;

namespace OpticalStore.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
