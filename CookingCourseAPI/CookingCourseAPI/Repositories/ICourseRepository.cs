using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<Course?> GetCourseByIdAsync(int id);
        Task<IEnumerable<Course>> GetCoursesByUserIdAsync(int userId);
        Task<IEnumerable<CourseVideo>> GetVideosByCourseIdAsync(int courseId);

    }
}
