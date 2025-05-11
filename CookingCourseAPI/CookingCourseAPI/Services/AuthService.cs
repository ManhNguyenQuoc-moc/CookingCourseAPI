
using CookingCourseAPI.DTOs;
using CookingCourseAPI.Helpers;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;
using CookingCourseAPI.Services.Interfaces;
using global::CookingCourseAPI.Helpers;
using global::CookingCourseAPI.Models.Entities;
using global::CookingCourseAPI.Repositories;
using global::CookingCourseAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace CookingCourseAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public AuthService(IUserRepository userRepository, IConfiguration configuration, IEmailService emailService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<bool?> RegisterAsync(RegisterDto dto)
        {
            if (await _userRepository.EmailExistsAsync(dto.Email) == true) return false;
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                Role = "User",
                AvatarUrl = "https://khoinguonsangtao.vn/wp-content/uploads/2022/08/anh-meme-meo-cute-cuc-cung.jpg",
                Bio = "Chưa cập nhật",
                IsLocked = false
            };

            await _userRepository.AddAsync(user);
            var saved = await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !PasswordHasher.Verify(dto.Password, user.PasswordHash))
                return null;

            if (user.IsLocked)
                return "LOCKED";

            return JwtHelper.GenerateToken(user);
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null) return false;

            // Tạo token reset mật khẩu sử dụng GenerateToken từ JwtHelper
            var resetToken = JwtHelper.GenerateToken(user);  // Sử dụng hàm GenerateToken đã có
            var tokenExpiration = DateTime.UtcNow.AddHours(1);  // Token hết hạn trong 1 giờ

            // Lưu token và thời gian hết hạn vào cơ sở dữ liệu
            user.PasswordResetToken = resetToken;
            user.PasswordResetTokenExpiration = tokenExpiration;
            await _userRepository.UpdateAsync(user);

            // Gửi email chứa token cho người dùng
            try
            {
                // Gửi email chứa token cho người dùng
                await _emailService.SendPasswordResetEmail(user.Email, resetToken);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        // Đặt lại mật khẩu
        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null) return false;

            // Kiểm tra token và thời gian hết hạn
            if (user.PasswordResetToken != token || user.PasswordResetTokenExpiration < DateTime.UtcNow)
                return false;  // Token không hợp lệ hoặc đã hết hạn

            // Cập nhật mật khẩu mới
            user.PasswordHash = PasswordHasher.Hash(newPassword);
            user.PasswordResetToken = null;  // Xóa token sau khi sử dụng
            user.PasswordResetTokenExpiration = null;  // Xóa thời gian hết hạn của token
            await _userRepository.UpdateAsync(user);

            return true;
        }

        // Thay đổi mật khẩu của người dùng
        public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null) return false;

            // Kiểm tra mật khẩu hiện tại của người dùng
            if (!PasswordHasher.Verify(currentPassword, user.PasswordHash))
                return false;

            // Cập nhật mật khẩu mới
            user.PasswordHash = PasswordHasher.Hash(newPassword);
            await _userRepository.UpdateAsync(user);

            return true;
        }
    }
}


