namespace CookingCourseAPI.DTOs
{
    public class ChangePasswordDto
    {// Hoặc từ JWT nếu đã login
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
