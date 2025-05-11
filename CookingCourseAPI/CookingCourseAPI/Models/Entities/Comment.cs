using CookingCourseAPI.Models.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }

    public ICollection<Comment> Replies { get; set; } // Nếu có trả lời cho bình luận

    public List<int> Likes { get; set; } = new List<int>(); // Khởi tạo danh sách Likes
}
