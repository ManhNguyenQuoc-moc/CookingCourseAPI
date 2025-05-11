using Microsoft.AspNetCore.Mvc;

namespace CookingCourseAPI.DTOs
{
    public class CourseCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public decimal Price { get; set; }
        public bool IsFree { get; set; }
        [FromForm]
        public List<VideoWithRecipesDto> Videos { get; set; }
    }
}
