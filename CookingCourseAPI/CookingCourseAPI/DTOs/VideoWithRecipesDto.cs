using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.DTOs
{
    public class VideoWithRecipesDto
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public RecipeDto Recipe { get; set; }// ← Chỉ 1 công thức
    }
}
