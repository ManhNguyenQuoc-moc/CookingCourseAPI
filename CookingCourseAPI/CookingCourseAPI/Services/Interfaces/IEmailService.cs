namespace CookingCourseAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendPasswordResetEmail(string email, string resetToken);
    }
}
