using Microsoft.EntityFrameworkCore;
using OpticalStore.Repositories.Interfaces;
using OpticalStore.Repositories.Models;

namespace OpticalStore.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly OpticalStoreDbContext _context;

        public CartRepository(OpticalStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetCartByUserIdAsync(int userId)
        {
            return await _context.CartItems
                .Include(c => c.Variant)
                    .ThenInclude(v => v.Product)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.AddedAt)
                .ToListAsync();
        }

        public async Task<CartItem?> GetCartItemAsync(int userId, int variantId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.VariantId == variantId);
        }

        public async Task<CartItem> AddToCartAsync(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<bool> RemoveFromCartAsync(int cartId)
        {
            var item = await _context.CartItems.FindAsync(cartId);
            if (item == null) return false;

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            var userCart = await _context.CartItems.Where(c => c.UserId == userId).ToListAsync();
            if (!userCart.Any()) return true;

            _context.CartItems.RemoveRange(userCart);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}