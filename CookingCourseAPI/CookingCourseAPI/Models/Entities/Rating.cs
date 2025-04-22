namespace CookingCourseAPI.Models.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int RatingValue { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }

}
