namespace CookingCourseAPI.Services.Interfaces
{
    using CookingCourseAPI.DTOs;
    using CookingCourseAPI.Models.Entities;

    public interface ICourseVideoService
    {
        Task<IEnumerable<Recipe>> GetRecipesByVideoIdAsync(int videoId);
        Task<VideoWithRecipesDto> GetVideoWithRecipesDtoByIdAsync(int videoId);
        Task RemoveVideosByCourseIdAsync(int courseId);
    }

}
