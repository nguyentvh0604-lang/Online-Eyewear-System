using Microsoft.AspNetCore.Mvc;
using OpticalStore.WebApplication.Models.ViewModels;
using OpticalStore.WebApplication.Services;

namespace OpticalStore.WebApplication.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: /Payment/Confirm
        public IActionResult Confirm()
        {
            var model = new PaymentViewModel
            {
                OrderId = 1,
                CustomerName = HttpContext.Session.GetString("UserName") ?? "",
                PaymentMethod = "COD",
                TotalAmount = 1200000
            };

            return View(model);
        }

        // POST: /Payment/Confirm
        [HttpPost]
        public IActionResult Confirm(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _paymentService.ProcessPayment(model);

            if (!result)
            {
                ViewBag.Error = "Thanh toán thất bại. Vui lòng thử lại.";
                return View(model);
            }

            TempData["Success"] = "Thanh toán thành công!";
            return RedirectToAction("Success");
        }

        // GET: /Payment/Success
        public IActionResult Success()
        {
            return View();
        }
    }
}
