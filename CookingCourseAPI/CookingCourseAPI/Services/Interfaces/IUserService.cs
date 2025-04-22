using CookingCourseAPI.DTOs;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> UpdateProfileAsync(int id, UpdateProfileDto dto);
        Task<UserProfileDto?> GetProfileAsync(int id);
        Task<List<UserListDto>> GetAllUsersAsync();
        Task<User> SetUserLockStatusAsync(int id, bool isLocked);
        Task<User> UpdateUserRoleAsync(int id, string newRole);
        Task<bool> PermanentlyDeleteUserAsync(int id);

    }
}
