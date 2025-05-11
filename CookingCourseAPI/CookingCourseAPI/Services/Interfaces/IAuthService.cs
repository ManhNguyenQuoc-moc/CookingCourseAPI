using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool?> RegisterAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
        Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    }
}
