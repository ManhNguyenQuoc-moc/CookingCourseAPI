namespace CookingCourseAPI.DTOs
{
    public class UpdateProfileDto
    {
        public string? FullName { get; set; }
        public string? Bio { get; set; }
        public IFormFile? Avatar { get; set; } 
    }
}
