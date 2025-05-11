namespace CookingCourseAPI.DTOs
{
    public class CourseProgressResult
    {
        public string message { get; set; }
        public bool isCourseCompleted { get; set; }
        public string currentLessonTitle { get; set; }
        public List<int> completedLessonIds { get; set; }
    }

}
