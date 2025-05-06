namespace CookingCourseAPI.Services
{
    using CookingCourseAPI.DTOs;
    using CookingCourseAPI.Models.Entities;
    using CookingCourseAPI.Repositories;
    using CookingCourseAPI.Services.Interfaces;

    public class CourseVideoService : ICourseVideoService
    {
        private readonly ICourseVideoRepository _courseVideoRepository;

        public CourseVideoService(ICourseVideoRepository courseVideoRepository)
        {
            _courseVideoRepository = courseVideoRepository;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesByVideoIdAsync(int videoId)
        {
            return await _courseVideoRepository.GetRecipesByVideoIdAsync(videoId);
        }
        public async Task<VideoWithRecipesDto> GetVideoWithRecipesDtoByIdAsync(int videoId)
        {
            return await _courseVideoRepository.GetVideoWithRecipeAsync(videoId);
        }
    }

}
