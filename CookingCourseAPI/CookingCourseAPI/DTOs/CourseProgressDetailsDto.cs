namespace CookingCourseAPI.DTOs
{
    public class CourseProgressDetailsDto
    {
        public string CourseTitle { get; set; }
        public bool IsCourseCompleted { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<int> CompletedLessonIds { get; set; }
        public int TotalLessonsInCourse { get; set; }
        public int CompletedLessonsCount => CompletedLessonIds?.Count ?? 0;
    }
}
