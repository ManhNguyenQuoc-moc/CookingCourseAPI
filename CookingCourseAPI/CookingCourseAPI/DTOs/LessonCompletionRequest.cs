namespace CookingCourseAPI.Models.DTOs
{
    public class LessonCompletionRequest
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public int LessonId { get; set; }
    }
}
