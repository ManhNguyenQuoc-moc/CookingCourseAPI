using CookingCourseAPI.Models.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }  // Họ và tên
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } = "User";

    public string? AvatarUrl { get; set; }

    public bool IsLocked { get; set; }
    public string? Bio { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
    public ICollection<Blog> Blogs { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
    public ICollection<Rating> Ratings { get; set; }
}
