using Microsoft.AspNetCore.Mvc;
using OpticalStore.Repositories.Interfaces;
using OpticalStore.Repositories.Models;
using OpticalStore.Services.Interfaces;

namespace OpticalStore.WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;

        public OrderController(
            IOrderService orderService,
            IOrderRepository orderRepository,
            IPrescriptionRepository prescriptionRepository)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
            _prescriptionRepository = prescriptionRepository;
        }

        // Trang checkout
        public async Task<IActionResult> Checkout()
        {
            int userId = 1; // Hardcode test
            
            // Lấy danh sách prescription cũ để user chọn
            var prescriptions = await _prescriptionRepository.GetPrescriptionsByUserIdAsync(userId);
            ViewBag.Prescriptions = prescriptions;

            return View();
        }

        // Xử lý checkout
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutDto model)
        {
            try
            {
                int userId = 1; // Hardcode test
                var order = await _orderService.CheckoutAsync(userId, model);
                return RedirectToAction(nameof(Detail), new { id = order.OrderId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        // Lịch sử đơn hàng
        public async Task<IActionResult> History()
        {
            int userId = 1; // Hardcode test
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }

        // Chi tiết đơn hàng
        public async Task<IActionResult> Detail(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        // Cập nhật trạng thái đơn (cho Staff)
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            return RedirectToAction(nameof(Detail), new { id = orderId });
        }
    }
}