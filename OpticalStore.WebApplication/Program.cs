using Microsoft.EntityFrameworkCore;
using OpticalStore.Repositories;
using OpticalStore.Repositories.Interfaces;
using OpticalStore.Services;
using OpticalStore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm MVC
builder.Services.AddControllersWithViews();

// 2. Kết nối MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OpticalStoreDbContext>(options =>
    options.UseMySQL(connectionString!));

// 3. Đăng ký Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();

// 4. Đăng ký Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IUserService, UserService>();

// 5. Session (cho giỏ hàng, đăng nhập)
builder.Services.AddSession(opts => {
    opts.IdleTimeout = TimeSpan.FromMinutes(30);
    opts.Cookie.HttpOnly = true;
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
