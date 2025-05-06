namespace CookingCourseAPI.Models.Entities
{
    public class CourseVideo
    {
        public int Id { get; set; }
        public string Title { get; set; }          // Tên video
        public string Url { get; set; }            // Link video
        public int CourseId { get; set; }          // FK tới Course
        public Course Course { get; set; }
        public Recipe Recipe { get; set; }

        // Reference navigation
    }
}
