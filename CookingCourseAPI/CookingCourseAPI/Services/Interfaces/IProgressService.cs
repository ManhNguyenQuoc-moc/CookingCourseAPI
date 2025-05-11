using CookingCourseAPI.DTOs;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface IProgressService
    {
        Task<object> GetCourseProgressAsync(string userId, int courseId);
        Task<CourseProgressResult> UpdateCourseProgressAsync(string userId, int courseId, int courseVideoId);
        Task<object> GetLearningPathProgressAsync(string userId, int pathId);
        Task<List<int>> GetCompletedLessonIdsAsync(string userId, int courseId); // vẫn giữ tên LessonIds để không ảnh hưởng front-end
    }
}
