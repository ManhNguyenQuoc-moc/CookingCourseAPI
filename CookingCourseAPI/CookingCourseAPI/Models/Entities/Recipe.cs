namespace CookingCourseAPI.Models.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }      // 🥕 Nguyên liệu  
        public string CookingTips { get; set; }      // 💡 Mẹo nấu (hoặc bạn đặt Notes/Tips gì đó)
        public int CourseVideoId { get; set; }        // 🔗 Liên kết tới video
        public CourseVideo CourseVideo { get; set; }
        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
    }

}
