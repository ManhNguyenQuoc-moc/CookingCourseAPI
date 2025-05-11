using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Repositories
{
    public interface INewsRepository
    {
        Task<List<News>> GetAllAsync();
        Task<News> GetByIdAsync(int id);
        Task<News> AddAsync(News news);
        Task<News> UpdateAsync(News news);
        Task<bool> DeleteAsync(int id);
    }
}