namespace CookingCourseAPI.DTOs
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; } = null!;
        public string NewPassword { get; set; }
    }
}
