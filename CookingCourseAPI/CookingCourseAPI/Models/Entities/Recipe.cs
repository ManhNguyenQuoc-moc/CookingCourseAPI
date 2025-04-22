namespace CookingCourseAPI.Models.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
    }

}
