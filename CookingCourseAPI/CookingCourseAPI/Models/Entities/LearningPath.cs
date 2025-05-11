namespace CookingCourseAPI.Models.Entities
{
    public class LearningPath
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<LearningPathCourse> LearningPathCourses { get; set; }
    }

}
