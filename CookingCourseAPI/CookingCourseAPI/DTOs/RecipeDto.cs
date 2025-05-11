namespace CookingCourseAPI.DTOs
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }
        public string Ingredients { get; set; }      // 🥕 Nguyên liệu  
        public string CookingTips { get; set; }      // 💡 Mẹo nấu (hoặc bạn đặt Notes/Tips gì đó)
    }
}
