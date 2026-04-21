using OpticalStore.Repositories.Interfaces;
using OpticalStore.Repositories.Models;
using OpticalStore.Services.Interfaces;

namespace OpticalStore.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
            => await _productRepo.GetAllAsync();

        public async Task<Product?> GetProductDetailAsync(int id)
            => await _productRepo.GetByIdAsync(id);

        public async Task<IEnumerable<Product>> SearchProductsAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await _productRepo.GetAllAsync();
            return await _productRepo.SearchAsync(keyword);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.IsActive = true;
            return await _productRepo.CreateAsync(product);
        }

        public async Task<Product> UpdateProductAsync(Product product)
            => await _productRepo.UpdateAsync(product);

        public async Task<bool> DeleteProductAsync(int id)
            => await _productRepo.DeleteAsync(id);

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
            => await _productRepo.GetByCategoryAsync(categoryId);
    }
}
