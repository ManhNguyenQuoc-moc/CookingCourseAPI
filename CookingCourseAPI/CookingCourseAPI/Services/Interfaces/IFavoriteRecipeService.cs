using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface IFavoriteRecipeService
    {
        Task<bool> AddToFavoritesAsync(int userId, int recipeId);
        Task<List<Recipe>> GetFavoritesByUserAsync(int userId);
        Task<bool> RemoveFromFavoritesAsync(int userId, int recipeId);
        Task<bool> IsRecipeFavoritedAsync(int userId, int recipeId);

    }
}
