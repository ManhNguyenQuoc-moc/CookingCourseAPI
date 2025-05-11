using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;
using CookingCourseAPI.Services.Interfaces;

namespace CookingCourseAPI.Services
{
    public class ProgressService : IProgressService
    {
        private readonly IProgressRepository _repository;

        public ProgressService(IProgressRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> GetCourseProgressAsync(string userId, int courseId)
        {
            var progress = await _repository.GetCourseProgressAsync(userId, courseId);
            if (progress == null) return null;

            var course = await _repository.GetCourseByIdAsync(courseId);
            var completedVideoIds = await _repository.GetCompletedCourseVideoIdsAsync(userId, courseId);

            return new
            {
                CourseTitle = course?.Name ?? "Không rõ",
                IsCourseCompleted = progress.IsCourseCompleted,
                LastUpdated = progress.LastUpdated,
                CompletedCourseVideoIds = completedVideoIds
            };
        }

        public async Task<CourseProgressResult> UpdateCourseProgressAsync(string userId, int courseId, int courseVideoId)
        {
            // Lấy hoặc tạo tiến trình
            var progress = await _repository.GetCourseProgressAsync(userId, courseId);
            if (progress == null)
            {
                progress = new CourseProgress
                {
                    UserId = userId,
                    CourseId = courseId,
                    IsCourseCompleted = false,
                    LastUpdated = DateTime.UtcNow,
                    CompletedCourseVideos = new List<CompletedCourseVideo>()
                };
                await _repository.AddCourseProgressAsync(progress);
                await _repository.SaveChangesAsync(); // cần lưu lại để lấy Progress.Id
            }

            // Thêm video vào danh sách đã hoàn thành (chỉ thêm nếu chưa có)
            await _repository.AddCompletedCourseVideoAsync(userId, courseId, courseVideoId);

            // Lấy lại tiến trình sau khi cập nhật
            progress = await _repository.GetCourseProgressAsync(userId, courseId);

            var totalVideos = await _repository.GetTotalLessonsAsync(courseId);
            var completedVideos = (await _repository.GetCompletedCourseVideoIdsAsync(userId, courseId)).Count;

            if (completedVideos == totalVideos && !progress.IsCourseCompleted)
            {
                progress.IsCourseCompleted = true;
                progress.LastUpdated = DateTime.UtcNow;
                await _repository.SaveChangesAsync();
            }

            var currentVideo = await _repository.GetLessonAsync(courseVideoId);
            var completedVideoIds = await _repository.GetCompletedCourseVideoIdsAsync(userId, courseId);

            return new CourseProgressResult
            {
                message = "Cập nhật tiến trình thành công.",
                isCourseCompleted = progress.IsCourseCompleted,
                currentLessonTitle = currentVideo?.Title,
                completedLessonIds = completedVideoIds
            };
        }


        public async Task<object> GetLearningPathProgressAsync(string userId, int pathId)
        {
            var pathProgress = await _repository.GetLearningPathProgressAsync(userId, pathId);
            if (pathProgress == null) return null;

            return new
            {
                IsPathCompleted = pathProgress.IsPathCompleted,
                LastUpdated = pathProgress.LastUpdated
            };
        }

        public async Task<List<int>> GetCompletedLessonIdsAsync(string userId, int courseId)
        {
            return await _repository.GetCompletedCourseVideoIdsAsync(userId, courseId);
        }
    }
}
