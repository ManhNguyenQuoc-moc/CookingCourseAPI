using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task<bool> AddCourseAsync(Course course);
        Task<bool> UpdateCourseAsync(int courseId, CourseCreateDto updatedCourseDto);

        Task<bool> DeleteCourseAsync(int id);
        Task<Course> AddCourseWithVideosAsync(CourseCreateDto courseCreateDto);
        Task<(bool Success, string Message)> EnrollUserInCourseAsync(int userId, int courseId);
        Task<IEnumerable<Course>> GetCoursesByUserIdAsync(int userId);
        Task<IEnumerable<CourseVideo>> GetVideosByCourseIdAsync(int courseId);
        Task<bool> CheckUserEnrollmentAsync(int userId, int courseId);
        Task<Course> UpdateCourseWithVideosAsync(int id, CourseCreateDto courseCreateDto);

    }
}
