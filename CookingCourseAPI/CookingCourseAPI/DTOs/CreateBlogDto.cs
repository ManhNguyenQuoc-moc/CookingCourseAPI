namespace CookingCourseAPI.DTOs
{
    public class CreateBlogDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        public int UserId { get; set; }
    }
}
