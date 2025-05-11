using CookingCourseAPI.Data;
using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;
using CookingCourseAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCourseAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PhotoService _photoService;

        public UserService(IUserRepository userRepository, PhotoService photoService)
        {
            _userRepository = userRepository;
            _photoService = photoService;
        }
        public async Task<User?> UpdateProfileAsync(int id, UpdateProfileDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            user.Name = dto.FullName ?? user.Name;
            user.Bio = dto.Bio ?? user.Bio;

            if (dto.Avatar != null)
            {
                var imageUrl = await _photoService.UploadImageAsync(dto.Avatar); // Gọi đúng chữ ký
                user.AvatarUrl = imageUrl;
            }

            await _userRepository.SaveChangesAsync();
            return user;
        }

        public async Task<UserProfileDto?> GetProfileAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            var dto = new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Bio = user.Bio,
                AvatarUrl = user.AvatarUrl
            };

            return dto;
        }
        public async Task<List<UserListDto>> GetAllUsersAsync()
        {
            var userList = await _userRepository.GetAllAsync();
            var userDtos = userList.Select(u => new UserListDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            }).ToList();
            return userDtos;
        }

        public async Task<User> SetUserLockStatusAsync(int id, bool isLocked)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found");

            user.IsLocked = isLocked;
            await _userRepository.SaveChangesAsync();

            return user;
        }
        public async Task<User> UpdateUserRoleAsync(int id, string newRole)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found");
            user.Role = newRole;
            await _userRepository.SaveChangesAsync();
            return user;
        }
        public async Task<bool> PermanentlyDeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;
            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
        public async Task<bool> ToggleUserLockAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            user.IsLocked = !user.IsLocked;
            await _userRepository.UpdateAsync(user);
            return user.IsLocked;
        }

    }
}
