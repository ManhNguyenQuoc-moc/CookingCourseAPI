using CookingCourseAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CookingCourseAPI.Models.DTOs; // hoặc đúng namespace nơi chứa LessonCompletionRequest

namespace CookingCourseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progressService;

        public ProgressController(IProgressService progressService)
        {
            _progressService = progressService;
        }

        // API để cập nhật tiến trình khi hoàn thành bài học
        [HttpPost("complete-lesson")]
        public async Task<IActionResult> CompleteLesson([FromBody] LessonCompletionRequest request)
        {
            // Cập nhật tiến trình khóa học
            var result = await _progressService.UpdateCourseProgressAsync(request.UserId, request.CourseId, request.LessonId);

            if (result == null)
            {
                return BadRequest("Có lỗi xảy ra khi cập nhật tiến trình.");
            }

            // Trả về kết quả với thông tin tiến trình và các bài học đã hoàn thành
            return Ok(new
            {
                message = result.message,
                isCourseCompleted = result.isCourseCompleted,
                currentLessonTitle = result.currentLessonTitle,  // Trả về tên bài học hiện tại
                completedLessonIds = result.completedLessonIds   // Trả về danh sách các bài học đã hoàn thành
            });
        }

        // API để lấy tiến trình khóa học
        [HttpGet("get-course-progress/{userId}/{courseId}")]
        public async Task<IActionResult> GetCourseProgress(string userId, int courseId)
        {
            var result = await _progressService.GetCourseProgressAsync(userId, courseId);
            if (result == null) return NotFound("Không tìm thấy tiến trình học.");
            return Ok(result);
        }

        // API để lấy tiến trình lộ trình học
        [HttpGet("get-learning-path-progress/{userId}/{pathId}")]
        public async Task<IActionResult> GetPathProgress(string userId, int pathId)
        {
            var result = await _progressService.GetLearningPathProgressAsync(userId, pathId);
            if (result == null) return NotFound("Không tìm thấy tiến trình lộ trình học.");
            return Ok(result);
        }
        // API để lấy danh sách bài học đã hoàn thành theo user + course
        [HttpGet("user/{userId}/course/{courseId}/completed")]
        public async Task<IActionResult> GetCompletedLessons(string userId, int courseId)
        {
            var completedLessonIds = await _progressService.GetCompletedLessonIdsAsync(userId, courseId);
            return Ok(new { completedLessonIds });
        }
    }
}

