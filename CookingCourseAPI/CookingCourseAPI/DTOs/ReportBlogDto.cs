namespace CookingCourseAPI.DTOs
{
    public class ReportBlogDto
    {
        public string Reason { get; set; }
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}