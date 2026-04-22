using Microsoft.AspNetCore.Mvc;
using OpticalStore.Repositories.Interfaces;
using OpticalStore.Repositories.Models;

namespace OpticalStore.WebApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        // Hiển thị giỏ hàng
        public async Task<IActionResult> Index()
        {
            // Tạm thời hardcode userId = 1 để test
            int userId = 1;
            var cartItems = await _cartRepository.GetCartByUserIdAsync(userId);
            return View(cartItems);
        }

        // Thêm vào giỏ hàng
        [HttpPost]
        public async Task<IActionResult> AddToCart(int variantId, int quantity = 1)
        {
            int userId = 1; // Hardcode test

            var existingItem = await _cartRepository.GetCartItemAsync(userId, variantId);
            
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _cartRepository.UpdateCartItemAsync(existingItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    VariantId = variantId,
                    Quantity = quantity,
                    AddedAt = DateTime.Now
                };
                await _cartRepository.AddToCartAsync(cartItem);
            }

            return RedirectToAction(nameof(Index));
        }

        // Cập nhật số lượng
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartId, int quantity)
        {
            var item = await _cartRepository.GetCartByUserIdAsync(1);
            var cartItem = item.FirstOrDefault(c => c.CartId == cartId);
            
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _cartRepository.UpdateCartItemAsync(cartItem);
            }

            return RedirectToAction(nameof(Index));
        }

        // Xóa khỏi giỏ
        [HttpPost]
        public async Task<IActionResult> Remove(int cartId)
        {
            await _cartRepository.RemoveFromCartAsync(cartId);
            return RedirectToAction(nameof(Index));
        }
    }
}