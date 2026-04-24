using OpticalStore.WebApplication.Repositories;
using OpticalStore.WebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ── Đăng ký Dependency Injection (không dùng DB) ─────────────────────────
// NV2: Repository — đọc dữ liệu hardcode từ StaticData
builder.Services.AddScoped<IProductRepository, ProductRepository>();
// NV3: Service — xử lý logic tìm kiếm, lọc, kiểm tra tồn kho
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
