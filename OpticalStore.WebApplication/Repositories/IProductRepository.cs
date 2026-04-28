using OpticalStore.WebApplication.Models;

namespace OpticalStore.WebApplication.Repositories
{
    /// <summary>
    /// NV2 - Interface Repository: định nghĩa các thao tác CRUD + lọc danh mục + kiểm tra tồn kho
    /// </summary>
    public interface IProductRepository
    {
        // Lấy toàn bộ sản phẩm đang hoạt động
        List<Product> GetAll();

        // Lấy 1 sản phẩm theo ID
        Product? GetById(int id);

        // Lọc theo danh mục (NV2)
        List<Product> GetByCategory(int categoryId);

        // Tìm kiếm theo từ khóa, có thể kết hợp lọc danh mục (NV3)
        List<Product> Search(string? keyword, int? categoryId = null);

        // Lấy toàn bộ danh mục
        List<Category> GetAllCategories();

        // Kiểm tra tồn kho theo SKU (NV2)
        ProductVariant? GetVariantBySku(string sku);

        // Lấy tất cả variant của một sản phẩm
        List<ProductVariant> GetVariantsByProductId(int productId);
    }
}
