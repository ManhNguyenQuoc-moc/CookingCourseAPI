namespace CookingCourseAPI.DTOs
{
    public class CourseProgressResultDto
    {
        public string Message { get; set; }
        public bool IsCourseCompleted { get; set; }
        public string CurrentLessonTitle { get; set; } // Tên của bài học vừa được xử lý
        public List<int> CompletedLessonIds { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
