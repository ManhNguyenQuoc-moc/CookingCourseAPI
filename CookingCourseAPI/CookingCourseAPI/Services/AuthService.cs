
using CookingCourseAPI.DTOs;
using CookingCourseAPI.Helpers;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;
using CookingCourseAPI.Services.Interfaces;
using global::CookingCourseAPI.Helpers;
using global::CookingCourseAPI.Models.Entities;
using global::CookingCourseAPI.Repositories;
using global::CookingCourseAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCourseAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                AvatarUrl = "D:\\CookingCourseAPI\\CookingCourseAPI\\CookingCourseAPI\\Image\\profile.png",
                Bio = "Chưa cập nhật",
                IsLocked = true
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

            return JwtHelper.GenerateToken(user);
        }
    }
}


