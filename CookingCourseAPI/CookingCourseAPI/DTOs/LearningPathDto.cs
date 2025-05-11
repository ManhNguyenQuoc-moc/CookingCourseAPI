namespace CookingCourseAPI.DTOs
{
    public class LearningPathDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<int> CourseIds { get; set; }
    }
}
