namespace CookingCourseAPI.Models.Entities
{
    public class LearningPath
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }

}
