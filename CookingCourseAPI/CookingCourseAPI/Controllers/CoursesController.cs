using Microsoft.AspNetCore.Mvc;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Models.Responses;
using CookingCourseAPI.Services.Interfaces;
using CookingCourseAPI.DTOs;
using CookingCourseAPI.Services;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace CookingCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ICourseVideoService _courseVideoService;
        private readonly PhotoService _photoService;

        public CoursesController(ICourseService courseService, ICourseVideoService courseVideoService, PhotoService photoService)
        {
            _courseService = courseService;
            _courseVideoService = courseVideoService;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(ApiResponse<IEnumerable<Course>>.SuccessResponse(courses));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound(ApiResponse<Course>.FailResponse("Course not found"));

            return Ok(ApiResponse<Course>.SuccessResponse(course));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CourseCreateDto updatedCourse)
        {
            try
            {
                var existingCourse = await _courseService.GetCourseByIdAsync(id);
                if (existingCourse == null)
                    return NotFound(ApiResponse<string>.FailResponse("Khóa học không tồn tại."));

                // Upload image if provided
                if (updatedCourse.Image != null)
                {
                    try
                    {
                        var imageUrl = await _photoService.UploadImageAsync(updatedCourse.Image);
                        existingCourse.ImageUrl = imageUrl;
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ApiResponse<string>.FailResponse($"Lỗi upload ảnh: {ex.Message}"));
                    }
                }

                // Update course
                var result = await _courseService.UpdateCourseAsync(id, updatedCourse);
                if (!result)
                    return BadRequest(ApiResponse<string>.FailResponse("Cập nhật khóa học thất bại."));

                return Ok(ApiResponse<string>.SuccessResponse("Cập nhật khóa học thành công."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailResponse($"Lỗi server: {ex.Message}"));
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);
            if (!result)
                return NotFound(ApiResponse<string>.FailResponse("Course not found"));

            return Ok(ApiResponse<string>.SuccessResponse("Course deleted successfully"));
        }
        // API tạo khóa học và nhiều video
        [HttpPost]
        public async Task<IActionResult> CreateCourseWithVideos([FromForm] CourseCreateDto courseCreateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Select(kvp => new { Field = kvp.Key, Errors = kvp.Value.Errors.Select(e => e.ErrorMessage) });
                return BadRequest(new { message = "ModelState invalid", errors });
            }

            if (courseCreateDto?.Videos == null || !courseCreateDto.Videos.Any())
            {
                return BadRequest("Invalid course data or no videos provided.");
            }

            var createdCourse = await _courseService.AddCourseWithVideosAsync(courseCreateDto);

            if (createdCourse == null)
            {
                return BadRequest("Failed to create course.");
            }

            return Ok(createdCourse);
        }


        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll([FromBody] CourseEnrollDto enrollDto)
        {
            var result = await _courseService.EnrollUserInCourseAsync(enrollDto.UserId, enrollDto.CourseId);

            if (!result.Success)
            {
                return BadRequest(ApiResponse<string>.FailResponse(result.Message));
            }

            return Ok(ApiResponse<string>.SuccessResponse(result.Message));
        }
        [HttpGet("{id}/videos")]
        public async Task<IActionResult> GetVideosByCourseId(int id)
        {
            var videos = await _courseService.GetVideosByCourseIdAsync(id);

            if (videos == null || !videos.Any())
                return NotFound(ApiResponse<string>.FailResponse("No videos found for this course."));

            return Ok(ApiResponse<IEnumerable<CourseVideo>>.SuccessResponse(videos));
        }
        [HttpGet("{videoId}/recipes")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesByVideoId(int videoId)
        {
            var recipes = await _courseVideoService.GetRecipesByVideoIdAsync(videoId);
            if (recipes == null || !recipes.Any())
            {
                return NotFound($"No recipes found for videoId: {videoId}");
            }

            return Ok(recipes);
        }
        [HttpGet("{videoId}/with-recipes")]
        public async Task<ActionResult<VideoWithRecipesDto>> GetVideoWithRecipes(int videoId)
        {
            var videoDto = await _courseVideoService.GetVideoWithRecipesDtoByIdAsync(videoId);
            if (videoDto == null)
            {
                return NotFound($"Video with id {videoId} not found.");
            }

            return Ok(videoDto);
        }

        [HttpGet("check-enrollment")]
        public async Task<IActionResult> CheckEnrollment([FromQuery] int userId, [FromQuery] int courseId)
        {
            var isEnrolled = await _courseService.CheckUserEnrollmentAsync(userId, courseId);
            return Ok(new { isEnrolled });
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCourseWithVideos(int id, [FromForm] CourseCreateDto courseCreateDto)
        {
            // Kiểm tra nếu dữ liệu video là null hoặc không có video
            if (courseCreateDto?.Videos == null || !courseCreateDto.Videos.Any())
            {
                return BadRequest("Invalid course data or no videos provided.");
            }

            // Gọi service để cập nhật khóa học
            var updatedCourse = await _courseService.UpdateCourseWithVideosAsync(id, courseCreateDto);

            if (updatedCourse == null)
            {
                return BadRequest("Failed to update course.");
            }

            return Ok(updatedCourse);
        }

    }
}