using Microsoft.EntityFrameworkCore.Storage; // Quan trọng: Thêm namespace này
using OpticalStore.Repositories.Models;

namespace OpticalStore.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        // Các method cũ
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);

        // CÁC METHOD MỚI BẮT BUỘC PHẢI CÓ
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CreateOrderItemsAsync(List<OrderItem> orderItems);
        Task UpdateStatusAsync(int orderId, string status);
    }
}