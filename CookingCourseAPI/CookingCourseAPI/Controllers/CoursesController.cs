using Microsoft.AspNetCore.Mvc;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Models.Responses;
using CookingCourseAPI.Services.Interfaces;
using CookingCourseAPI.DTOs;
using CookingCourseAPI.Services;

namespace CookingCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ICourseVideoService _courseVideoService;

        public CoursesController(ICourseService courseService, ICourseVideoService courseVideoService)
        {
            _courseService = courseService;
            _courseVideoService = courseVideoService;
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
        public async Task<IActionResult> Update(int id, [FromBody] Course updatedCourse)
        {
            var existing = await _courseService.GetCourseByIdAsync(id);
            if (existing == null)
                return NotFound(ApiResponse<string>.FailResponse("Course not found"));

            existing.Name = updatedCourse.Name;
            existing.Description = updatedCourse.Description;

            var result = await _courseService.UpdateCourseAsync(existing);
            if (!result)
                return BadRequest(ApiResponse<string>.FailResponse("Update failed"));

            return Ok(ApiResponse<string>.SuccessResponse("Course updated successfully"));
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
        public async Task<IActionResult> CreateCourseWithVideos([FromBody] CourseCreateDto courseCreateDto)
        {
            if (courseCreateDto == null || !courseCreateDto.Videos.Any())
            {
                return BadRequest("Invalid course data or no videos provided.");
            }

            var createdCourse = await _courseService.AddCourseWithVideosAsync(courseCreateDto);

            if (createdCourse == null)
            {
                return BadRequest("Failed to create course.");
            }

            return Ok(createdCourse);  // Trả về khóa học đã tạo
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


    }
}