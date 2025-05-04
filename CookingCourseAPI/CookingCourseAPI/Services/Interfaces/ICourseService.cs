using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Models.Responses;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface ICourseService
    {
        Task<ApiResponse<Course>> CreateCourseAsync(CourseCreateDto dto);
        Task<ApiResponse<IEnumerable<Course>>> GetAllCoursesAsync();
        Task<ApiResponse<Course>> GetCourseByIdAsync(int id);
        Task<ApiResponse<Course>> UpdateCourseAsync(int id, CourseCreateDto dto);
        Task<ApiResponse<string>> DeleteCourseAsync(int id);
    }
}
