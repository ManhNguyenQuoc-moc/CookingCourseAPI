using CookingCourseAPI.Data;
using CookingCourseAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookingCourseAPI.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly AppDbContext _context;

        
        public CourseRepository(AppDbContext context) : base(context) {
            _context = context;
        }

        // Implement method from ICourseRepository
        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            return await _context.Courses
                                 .Include(c => c.Videos)  // Kéo theo video của khóa học
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Course>> GetCoursesByUserIdAsync(int userId)
        {
            return await _context.Enrollments
                .Include(e => e.Course) // PHẢI include nếu dùng e.Course
                .Where(e => e.UserId == userId)
                .Select(e => e.Course)
                .ToListAsync();
        }
        public async Task<IEnumerable<CourseVideo>> GetVideosByCourseIdAsync(int courseId)
        {
            return await _context.CourseVideos
                                 .Where(v => v.CourseId == courseId)
                                 .ToListAsync();
        }
        public async Task<bool> IsUserEnrolledAsync(int userId, int courseId)
        {
            return await _context.Enrollments
                .AnyAsync(e => e.UserId == userId && e.CourseId == courseId);
        }

    }
}
