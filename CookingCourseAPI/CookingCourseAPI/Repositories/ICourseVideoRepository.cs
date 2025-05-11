using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Repositories
{
    public interface ICourseVideoRepository : IGenericRepository<CourseVideo>
    {
        Task<IEnumerable<CourseVideo>> GetVideosByCourseIdAsync(int courseId);
        Task<CourseVideo> GetByIdAsync(int id);
        Task<IEnumerable<CourseVideo>> GetAllAsync();
        Task<IEnumerable<Recipe>> GetRecipesByVideoIdAsync(int videoId);
        Task<VideoWithRecipesDto> GetVideoWithRecipeAsync(int videoId);

        Task RemoveRange(IEnumerable<CourseVideo> videos);
    }
}
