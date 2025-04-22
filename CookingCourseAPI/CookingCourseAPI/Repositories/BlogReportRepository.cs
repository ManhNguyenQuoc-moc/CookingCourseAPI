using CookingCourseAPI.Data;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingCourseAPI.Repositories
{
    public class BlogReportRepository : IBlogReportRepository
    {
        private readonly AppDbContext _context;

        public BlogReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogReport>> GetReportsByBlogIdAsync(int blogId)
        {
            return await _context.BlogReports
                .Where(r => r.BlogId == blogId)
                .ToListAsync();
        }

        public async Task<bool> HasUserReportedAsync(int blogId, int userId)
        {
            return await _context.BlogReports
                .AnyAsync(r => r.BlogId == blogId && r.UserId == userId);
        }

        // Optional: Bạn có thể thêm các phương thức bổ sung nếu cần
        public async Task<bool> AddAsync(BlogReport report)
        {
            await _context.BlogReports.AddAsync(report);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var report = await _context.BlogReports.FindAsync(id);
            if (report == null) return false;

            _context.BlogReports.Remove(report);
            return await _context.SaveChangesAsync() > 0;
        }
        
    }
}
