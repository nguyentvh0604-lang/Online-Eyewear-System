using System;
using System.Threading.Tasks;
using OpticalStore.Repositories.Interfaces;
using OpticalStore.Repositories.Models;
using OpticalStore.Services.Interfaces;

namespace OpticalStore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        // Tiêm IUserRepository vào qua constructor
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            // Lấy user theo email
            var user = await _userRepo.GetByEmailAsync(email);

            // Nếu không tìm thấy hoặc tài khoản đã bị khóa (IsActive = false)
            if (user == null || !user.IsActive) return null;

            // Xác thực mật khẩu đã mã hóa
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            return isValid ? user : null;
        }

        public async Task<User> RegisterAsync(string fullName, string email, string phone, string password)
        {
            // Kiểm tra email đã tồn tại trong hệ thống chưa
            var existing = await _userRepo.GetByEmailAsync(email);
            if (existing != null)
            {
                throw new Exception("Email đã được sử dụng.");
            }

            // Tạo đối tượng User mới
            var user = new User
            {
                FullName = fullName,
                Email = email,
                Phone = phone,
                // Mã hóa mật khẩu trước khi lưu vào Database
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = "customer", // Mặc định role là khách hàng
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            // Lưu vào Database thông qua Repository
            return await _userRepo.CreateAsync(user);
        }
    }
}