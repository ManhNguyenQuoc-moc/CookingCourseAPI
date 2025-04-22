namespace CookingCourseAPI.Models.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string ImageUrl { get; set; }  

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<BlogReport> Reports { get; set; }
        public List<int>? Likes { get; set; }
    }

}
