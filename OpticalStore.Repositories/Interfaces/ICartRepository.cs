using OpticalStore.Repositories.Models;

namespace OpticalStore.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetCartByUserIdAsync(int userId);
        Task<CartItem?> GetCartItemAsync(int userId, int variantId);
        Task<CartItem> AddToCartAsync(CartItem cartItem);
        Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
        Task<bool> RemoveFromCartAsync(int cartId);
        Task<bool> ClearCartAsync(int userId);
    }
}