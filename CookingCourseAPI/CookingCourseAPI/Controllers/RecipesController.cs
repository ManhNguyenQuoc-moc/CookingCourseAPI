using CookingCourseAPI.Data;
using CookingCourseAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookingCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecipesController(AppDbContext context)
        {
           
        }

        // GET: api/Recipes/Favorites/5
        [HttpGet("Favorites/{userId}")]
        //public async Task<ActionResult<IEnumerable<Recipe>>> GetFavoriteRecipes(int userId)
        //{
        //    //var favorites = await _context.FavoriteRecipes
        //    //    .Where(fr => fr.UserId == userId)
        //    //    .Include(fr => fr.Recipe)
        //    //    .Select(fr => fr.Recipe)
        //    //    .ToListAsync();

        //    return Ok(favorites);
        //}

        // POST: api/Recipes/Favorites
        [HttpPost("Favorites")]
        public async Task<ActionResult> AddFavoriteRecipe([FromBody] FavoriteRecipe favoriteRecipe)
        {
            //// Kiểm tra trùng
            //bool exists = await _context.FavoriteRecipes
            //    .AnyAsync(fr => fr.UserId == favoriteRecipe.UserId && fr.RecipeId == favoriteRecipe.RecipeId);

            //if (exists)
            //{
            //    return BadRequest("This recipe is already in favorites.");
            //}

            //_context.FavoriteRecipes.Add(favoriteRecipe);
            //await _context.SaveChangesAsync();

            return Ok("Favorite recipe added.");
        }
    }
}

