using CookingCourseAPI.Data;
using CookingCourseAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CookingCourseAPI.Repositories
{
    public class FavoriteRecipeRepository : GenericRepository<Recipe>, IFavoriteRecipeRepository
    {
        private readonly AppDbContext _context;

        public FavoriteRecipeRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<bool> IsFavoriteAsync(int userId, int recipeId)
        {
            return await _context.FavoriteRecipes
                .AnyAsync(fr => fr.UserId == userId && fr.RecipeId == recipeId);
        }

        public async Task AddFavoriteAsync(FavoriteRecipe favorite)
        {
            _context.FavoriteRecipes.Add(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Recipe>> GetUserFavoritesAsync(int userId)
        {
            return await _context.FavoriteRecipes
                .Where(fr => fr.UserId == userId)
                .Include(fr => fr.Recipe)
                .Select(fr => fr.Recipe)
                .ToListAsync();
        }

        public async Task RemoveFavoriteAsync(int userId, int recipeId)
        {
            var favorite = await _context.FavoriteRecipes
                .FirstOrDefaultAsync(fr => fr.UserId == userId && fr.RecipeId == recipeId);

            if (favorite != null)
            {
                _context.FavoriteRecipes.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }
    }

}
