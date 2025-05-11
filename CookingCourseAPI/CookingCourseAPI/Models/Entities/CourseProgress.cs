namespace CookingCourseAPI.Models.Entities
{
    public class CourseProgress
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public bool IsCourseCompleted { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<CompletedCourseVideo> CompletedCourseVideos { get; set; } = new();
    }
}