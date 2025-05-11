namespace CookingCourseAPI.DTOs
{
    public class CourseRevenueDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        public int EnrollmentCount { get; set; }
        public decimal Revenue { get; set; }
    }

}
