using OpticalStore.Repositories.Models;

namespace OpticalStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CheckoutAsync(int userId, CheckoutDto model);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
        Task UpdateOrderStatusAsync(int orderId, string status);
    }

    // DTO để nhận dữ liệu từ View khi checkout
    public class CheckoutDto
    {
        public string ShippingAddress { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int? PromotionId { get; set; }
        
        // Loại đơn hàng
        public bool IsPreOrder { get; set; }
        public bool IsPrescriptionOrder { get; set; }
        
        // Thông tin đơn kính
        public int? ExistingPrescriptionId { get; set; }
        public PrescriptionDto? NewPrescription { get; set; }
    }

    public class PrescriptionDto
    {
        public decimal? OdSphere { get; set; }
        public decimal? OdCylinder { get; set; }
        public int? OdAxis { get; set; }
        public decimal? OsSphere { get; set; }
        public decimal? OsCylinder { get; set; }
        public int? OsAxis { get; set; }
        public decimal? Pd { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}