using Microsoft.EntityFrameworkCore;
using OpticalStore.Repositories.Models;

namespace OpticalStore.Repositories
{
    public class OpticalStoreDbContext : DbContext
    {
        public OpticalStoreDbContext(DbContextOptions<OpticalStoreDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Prescription> Prescriptions => Set<Prescription>();
        public DbSet<Promotion> Promotions => Set<Promotion>();
        public DbSet<Policy> Policies => Set<Policy>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Shipment> Shipments => Set<Shipment>();
        public DbSet<ReturnRequest> ReturnRequests => Set<ReturnRequest>();
    }
}
