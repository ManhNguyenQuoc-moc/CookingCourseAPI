using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;

namespace CookingCourseAPI.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;

        public NewsService(INewsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<News>> GetAllNewsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<News> GetNewsByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<News> CreateNewsAsync(News news)
        {
            news.CreatedAt = DateTime.Now;
            return await _repository.AddAsync(news);
        }

        public async Task<News> UpdateNewsAsync(News news)
        {
            return await _repository.UpdateAsync(news);
        }

        public async Task<bool> DeleteNewsAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}