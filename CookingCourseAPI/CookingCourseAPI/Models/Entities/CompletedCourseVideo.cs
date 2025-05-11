namespace CookingCourseAPI.Models.Entities
{
    public class CompletedCourseVideo
    {
        public int Id { get; set; }

        public int CourseVideoId { get; set; }  // Khóa ngoại đến CourseVideo
        public int CourseProgressId { get; set; }  // Khóa ngoại đến CourseProgress

        public CourseProgress CourseProgress { get; set; }
    }
}
