using CookingCourseAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookingCourseAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BlogReport> BlogReports { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }
        public DbSet<CommentReport> CommentReports { get; set; }
        public DbSet<CourseVideo> CourseVideos { get; set; }




        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User - Enrollment (n-n)
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.UserId, e.CourseId });

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa User nếu có Enrollment

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa Course nếu có Enrollment

            // Blog - Comment (1-n)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Restrict); // Giữ Cascade hoặc đổi thành Restrict nếu cần

            // Comment - ParentComment (self-reference 1-n)
            

            // Blog - BlogReport (1-n)
            modelBuilder.Entity<BlogReport>()
                .HasOne(br => br.Blog)
                .WithMany(b => b.Reports)
                .HasForeignKey(br => br.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            // Rating - Course/User (n-1)
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Ratings)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // FavoriteRecipe (n-n)
            modelBuilder.Entity<FavoriteRecipe>()
                .HasKey(fr => new { fr.UserId, fr.RecipeId });

            modelBuilder.Entity<FavoriteRecipe>()
                .HasOne(fr => fr.User)
                .WithMany(u => u.FavoriteRecipes)
                .HasForeignKey(fr => fr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FavoriteRecipe>()
                .HasOne(fr => fr.Recipe)
                .WithMany(r => r.FavoriteRecipes)
                .HasForeignKey(fr => fr.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Recipe>()
                 .HasOne(r => r.CourseVideo)
                 .WithOne(v => v.Recipe) // dùng đúng thuộc tính navigation trong CourseVideo
                 .HasForeignKey<Recipe>(r => r.CourseVideoId)
                 .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
