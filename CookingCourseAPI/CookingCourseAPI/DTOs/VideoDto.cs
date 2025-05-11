namespace CookingCourseAPI.DTOs
{
    public class VideoDto
    {
        public int Id { get; set; }
        public string Title { get; set; } // Tiêu đề video
        public string Url { get; set; }
        public string RecipeTitle { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }    
        public string CookingTips { get; set; }
    }
}
