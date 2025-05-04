namespace CookingCourseAPI.Models.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Recipe> Recipes { get; set; }    
        public ICollection<Rating> Ratings { get; set; }
    }

}
