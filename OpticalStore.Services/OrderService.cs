using Microsoft.EntityFrameworkCore.Storage;
using OpticalStore.Repositories.Interfaces;
using OpticalStore.Repositories.Models;
using OpticalStore.Services.Interfaces;

namespace OpticalStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;

        public OrderService(
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IPrescriptionRepository prescriptionRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _prescriptionRepository = prescriptionRepository;
        }

        public async Task<Order> CheckoutAsync(int userId, CheckoutDto model)
        {
            // 1. Lấy giỏ hàng
            var cartItems = await _cartRepository.GetCartByUserIdAsync(userId);
            var cartList = cartItems.ToList();

            if (!cartList.Any())
                throw new Exception("Giỏ hàng trống.");

            // 2. Xử lý Prescription (nếu có)
            int? prescriptionId = null;
            string orderType = "standard";

            if (model.IsPrescriptionOrder)
            {
                orderType = "prescription";
                if (model.NewPrescription != null)
                {
                    var prescription = new Prescription
                    {
                        UserId = userId,
                        OdSphere = model.NewPrescription.OdSphere,
                        OdCylinder = model.NewPrescription.OdCylinder,
                        OdAxis = model.NewPrescription.OdAxis,
                        OsSphere = model.NewPrescription.OsSphere,
                        OsCylinder = model.NewPrescription.OsCylinder,
                        OsAxis = model.NewPrescription.OsAxis,
                        Pd = model.NewPrescription.Pd,
                        Note = model.NewPrescription.Note,
                        CreatedAt = DateTime.Now
                    };
                    prescriptionId = await _prescriptionRepository.CreatePrescriptionAsync(prescription);
                }
                else
                {
                    prescriptionId = model.ExistingPrescriptionId;
                }
            }
            else if (model.IsPreOrder)
            {
                orderType = "pre_order";
            }

            // 3. Tính tiền
            decimal totalAmount = cartList.Sum(i => i.Variant?.Price ?? 0);
            decimal finalAmount = totalAmount; // Chưa tính discount

            // 4. Transaction
            IDbContextTransaction? transaction = null;
            try
            {
                transaction = await _orderRepository.BeginTransactionAsync();

                // Tạo Order
                var order = new Order
                {
                    UserId = userId,
                    PrescriptionId = prescriptionId,
                    OrderType = orderType,
                    Status = "pending",
                    TotalAmount = totalAmount,
                    FinalAmount = finalAmount,
                    ShippingAddress = model.ShippingAddress,
                    Note = model.Note,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                var createdOrder = await _orderRepository.CreateAsync(order);

                // Tạo OrderItems
                var orderItems = new List<OrderItem>();
                foreach (var item in cartList)
                {
                    var price = item.Variant?.Price ?? 0;
                    orderItems.Add(new OrderItem
                    {
                        OrderId = createdOrder.OrderId,
                        VariantId = item.VariantId,
                        Quantity = item.Quantity,
                        UnitPrice = price,
                        Subtotal = price * item.Quantity
                    });
                }
                await _orderRepository.CreateOrderItemsAsync(orderItems);

                // Xóa giỏ
                await _cartRepository.ClearCartAsync(userId);

                await transaction.CommitAsync();
                return createdOrder;
            }
            catch
            {
                if (transaction != null)
                    await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);
            return orders.ToList();
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            await _orderRepository.UpdateStatusAsync(orderId, status);
        }
    }
}