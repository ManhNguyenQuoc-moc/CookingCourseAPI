using CookingCourseAPI.Data;
using CookingCourseAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookingCourseAPI.Repositories
{
    public class ProgressRepository : IProgressRepository
    {
        private readonly AppDbContext _context;

        public ProgressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CourseProgress> GetCourseProgressAsync(string userId, int courseId)
        {
            return await _context.CourseProgresses
                .Include(p => p.CompletedCourseVideos) // Include để truy xuất danh sách
                .FirstOrDefaultAsync(p => p.UserId == userId && p.CourseId == courseId);
        }

        public async Task AddCourseProgressAsync(CourseProgress progress)
        {
            await _context.CourseProgresses.AddAsync(progress);
        }

        public async Task<CourseVideo> GetLessonByIdAsync(int lessonId)
        {
            return await _context.CourseVideos.FindAsync(lessonId);
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            return await _context.Courses
                .Include(c => c.Videos)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<CourseVideo> GetLessonAsync(int courseVideoId)
        {
            return await _context.CourseVideos.FirstOrDefaultAsync(v => v.Id == courseVideoId);
        }

        public async Task<int> GetTotalLessonsInCourseAsync(int courseId)
        {
            var course = await GetCourseByIdAsync(courseId);
            return course?.Videos?.Count ?? 0;
        }

        public async Task<int> GetTotalLessonsAsync(int courseId)
        {
            return await _context.CourseVideos.CountAsync(l => l.CourseId == courseId);
        }

        public async Task<int> GetCompletedLessonsAsync(int courseId, int currentCourseVideoId)
        {
            return await _context.CourseVideos
                .Where(l => l.CourseId == courseId && l.Id <= currentCourseVideoId)
                .CountAsync();
        }

        public async Task<List<Course>> GetCoursesInLearningPathAsync(int learningPathId)
        {
            return await _context.LearningPaths
                .Where(lp => lp.Id == learningPathId)
                .SelectMany(lp => lp.LearningPathCourses)
                .Select(lpc => lpc.Course)
                .ToListAsync();
        }

        public async Task<int> GetCompletedCoursesCountAsync(string userId, List<int> courseIds)
        {
            return await _context.CourseProgresses
                .Where(cp => cp.UserId == userId && courseIds.Contains(cp.CourseId) && cp.IsCourseCompleted)
                .CountAsync();
        }

        public async Task<LearningPath> GetLearningPathByCourseIdAsync(int courseId)
        {
            return await _context.LearningPaths
                .FirstOrDefaultAsync(lp => lp.LearningPathCourses.Any(c => c.CourseId == courseId));
        }

        public async Task<LearningPathProgress> GetLearningPathProgressAsync(string userId, int pathId)
        {
            return await _context.LearningPathProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.LearningPathId == pathId);
        }

        public async Task AddLearningPathProgressAsync(LearningPathProgress progress)
        {
            await _context.LearningPathProgresses.AddAsync(progress);
        }

        // ✅ Thêm video vào danh sách đã hoàn thành
        public async Task AddCompletedCourseVideoAsync(string userId, int courseId, int courseVideoId)
        {
            var progress = await GetCourseProgressAsync(userId, courseId);
            if (progress == null) return;

            var alreadyExists = progress.CompletedCourseVideos.Any(cv => cv.CourseVideoId == courseVideoId);
            if (!alreadyExists)
            {
                progress.CompletedCourseVideos.Add(new CompletedCourseVideo
                {
                    CourseVideoId = courseVideoId,
                    CourseProgressId = progress.Id
                });

                progress.LastUpdated = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        // ✅ Lấy danh sách video đã hoàn thành
        public async Task<List<int>> GetCompletedCourseVideoIdsAsync(string userId, int courseId)
        {
            var progress = await _context.CourseProgresses
                .Include(p => p.CompletedCourseVideos)
                .FirstOrDefaultAsync(p => p.UserId == userId && p.CourseId == courseId);

            return progress?.CompletedCourseVideos
                .Select(cv => cv.CourseVideoId)
                .ToList() ?? new List<int>();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
