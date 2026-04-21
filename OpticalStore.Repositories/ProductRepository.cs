using Microsoft.EntityFrameworkCore;
using OpticalStore.Repositories.Interfaces;
using OpticalStore.Repositories.Models;

namespace OpticalStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OpticalStoreDbContext _context;

        public ProductRepository(OpticalStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
            => await _context.Products
                   .Include(p => p.Category)
                   .Include(p => p.Variants)
                   .Where(p => p.IsActive)
                   .ToListAsync();

        public async Task<Product?> GetByIdAsync(int id)
            => await _context.Products
                   .Include(p => p.Category)
                   .Include(p => p.Variants)
                     .ThenInclude(v => v.Inventory)
                   .FirstOrDefaultAsync(p => p.ProductId == id);

        public async Task<IEnumerable<Product>> SearchAsync(string keyword)
            => await _context.Products
                   .Where(p => p.IsActive && p.Name.Contains(keyword))
                   .ToListAsync();

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;
            product.IsActive = false; // Soft delete
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
            => await _context.Products
                   .Where(p => p.IsActive && p.CategoryId == categoryId)
                   .ToListAsync();
    }
}
