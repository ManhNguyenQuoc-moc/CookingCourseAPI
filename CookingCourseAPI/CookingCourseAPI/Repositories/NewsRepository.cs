using CookingCourseAPI.Data;
using CookingCourseAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookingCourseAPI.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly AppDbContext _context;

        public NewsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<News>> GetAllAsync()
        {
            return await _context.News.OrderByDescending(n => n.CreatedAt).ToListAsync();
        }

        public async Task<News> GetByIdAsync(int id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task<News> AddAsync(News news)
        {
            _context.News.Add(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<News> UpdateAsync(News news)
        {
            _context.News.Update(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}