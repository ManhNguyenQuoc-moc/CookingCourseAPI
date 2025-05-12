using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Services
{
    public interface INewsService
    {
        Task<List<News>> GetAllNewsAsync();
        Task<News> GetNewsByIdAsync(int id);
        Task<News> CreateNewsAsync(News news);
        Task<News> UpdateNewsAsync(News news);
        Task<bool> DeleteNewsAsync(int id);
    }
}