namespace CookingCourseAPI.DTOs
{
    public class RegisterDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? ConfirmPassword { get; set; } // Tùy chọn nếu bạn muốn xác nhận mật khẩu
    }
}
