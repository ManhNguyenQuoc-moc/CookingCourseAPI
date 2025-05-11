using CookingCourseAPI.Data;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCourseAPI.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetByUserIdAsync(int userId)
        {
            return await _context.Blogs
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Blog>> SearchAsync(string keyword)
        {
            return await _context.Blogs
                .Where(b => b.Title.Contains(keyword) || b.Content.Contains(keyword))
                .ToListAsync();
        }

        public async Task<List<Blog>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Blogs
                .OrderByDescending(b => b.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Blogs.CountAsync();
        }
        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs
                .FirstOrDefaultAsync(blog => blog.Id == id);
        }
    }
}
