using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Repositories
{
    public interface IProgressRepository
    {
        Task<CourseProgress> GetCourseProgressAsync(string userId, int courseId);
        Task AddCourseProgressAsync(CourseProgress progress);

        Task<Course> GetCourseByIdAsync(int courseId);
        Task<CourseVideo> GetLessonByIdAsync(int lessonId);
        Task<CourseVideo> GetLessonAsync(int courseVideoId);

        Task<int> GetTotalLessonsInCourseAsync(int courseId);
        Task<int> GetTotalLessonsAsync(int courseId);
        Task<int> GetCompletedLessonsAsync(int courseId, int currentCourseVideoId);

        Task<List<Course>> GetCoursesInLearningPathAsync(int learningPathId);
        Task<int> GetCompletedCoursesCountAsync(string userId, List<int> courseIds);

        Task<LearningPath> GetLearningPathByCourseIdAsync(int courseId);
        Task<LearningPathProgress> GetLearningPathProgressAsync(string userId, int pathId);
        Task AddLearningPathProgressAsync(LearningPathProgress progress);

        // ✅ Các phương thức mới
        Task AddCompletedCourseVideoAsync(string userId, int courseId, int courseVideoId);
        Task<List<int>> GetCompletedCourseVideoIdsAsync(string userId, int courseId);

        Task SaveChangesAsync();
    }
}
