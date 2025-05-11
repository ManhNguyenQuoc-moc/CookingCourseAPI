using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Repositories
{
    public interface IFavoriteRecipeRepository : IGenericRepository<Recipe>
    {
        Task<bool> IsFavoriteAsync(int userId, int recipeId);
        Task AddFavoriteAsync(FavoriteRecipe favorite);
        Task<List<Recipe>> GetUserFavoritesAsync(int userId);
        Task RemoveFavoriteAsync(int userId, int recipeId);
    }
}
