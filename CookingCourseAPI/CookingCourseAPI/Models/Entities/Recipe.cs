namespace CookingCourseAPI.Models.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public int CourseVideoId { get; set; }        // 🔗 Liên kết tới video
        public CourseVideo CourseVideo { get; set; }
        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
    }

}
