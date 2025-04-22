using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool?> RegisterAsync(RegisterDto dto);
        Task<string?> LoginAsync(LoginDto dto);
    }
}
