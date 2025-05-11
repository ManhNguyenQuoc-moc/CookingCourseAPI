using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;
using CookingCourseAPI.Services.Interfaces;

namespace CookingCourseAPI.Services
{
    public class FavoriteRecipeService : IFavoriteRecipeService
    {
        private readonly IFavoriteRecipeRepository _repository;

        public FavoriteRecipeService(IFavoriteRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddToFavoritesAsync(int userId, int recipeId)
        {
            if (await _repository.IsFavoriteAsync(userId, recipeId))
                return false;

            var favorite = new FavoriteRecipe
            {
                UserId = userId,
                RecipeId = recipeId
            };

            await _repository.AddFavoriteAsync(favorite);
            return true;
        }

        public async Task<List<Recipe>> GetFavoritesByUserAsync(int userId)
        {
            return await _repository.GetUserFavoritesAsync(userId);
        }

        public async Task<bool> RemoveFromFavoritesAsync(int userId, int recipeId)
        {
            if (!await _repository.IsFavoriteAsync(userId, recipeId))
                return false;

            await _repository.RemoveFavoriteAsync(userId, recipeId);
            return true;
        }
        public async Task<bool> IsRecipeFavoritedAsync(int userId, int recipeId)
        {
            return await _repository.IsFavoriteAsync(userId, recipeId);
        }
    }

}
