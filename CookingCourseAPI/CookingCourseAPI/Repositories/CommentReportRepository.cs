using CookingCourseAPI.Data;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingCourseAPI.Repositories
{
    public class CommentReportRepository : GenericRepository<CommentReport>, ICommentReportRepository
    {
        private readonly AppDbContext _context;

        public CommentReportRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Thêm phương thức lấy các bình luận bị báo cáo với chi tiết
        public async Task<IEnumerable<CommentReport>> GetReportedCommentsWithDetailsAsync()
        {
            return await _context.CommentReports
                .Include(cr => cr.Comment)  // Lấy bình luận chi tiết
                .Include(cr => cr.User)     // Lấy người dùng đã báo cáo
                .ToListAsync();
        }
    }
}