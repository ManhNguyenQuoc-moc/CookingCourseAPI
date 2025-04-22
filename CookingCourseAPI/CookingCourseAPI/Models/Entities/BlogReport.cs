using System;

namespace CookingCourseAPI.Models.Entities
{
    public class BlogReport
    {
        public int Id { get; set; }

        public string Reason { get; set; }

        public DateTime ReportedAt { get; set; } = DateTime.UtcNow;

        // Liên kết đến Blog
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        // Liên kết đến User (người báo cáo)
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
