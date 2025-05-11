using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        Task<User?> FindByEmailAsync(string email);
        Task<User?> FindByIdAsync(int userId);
    }

}
