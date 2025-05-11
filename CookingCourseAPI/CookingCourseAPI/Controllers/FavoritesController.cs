using CookingCourseAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookingCourseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteRecipeService _favoriteService;

        public FavoritesController(IFavoriteRecipeService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int userId, int recipeId)
        {
            var added = await _favoriteService.AddToFavoritesAsync(userId, recipeId);
            return added ? Ok("Đã thêm vào yêu thích.") : BadRequest("Đã có trong yêu thích.");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFavorites(int userId)
        {
            var favorites = await _favoriteService.GetFavoritesByUserAsync(userId);
            return Ok(favorites);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromFavorites(int userId, int recipeId)
        {
            var removed = await _favoriteService.RemoveFromFavoritesAsync(userId, recipeId);
            return removed ? Ok("Đã xoá khỏi yêu thích.") : NotFound("Không tìm thấy trong danh sách yêu thích.");
        }
        // ✅ MỚI: Kiểm tra trạng thái yêu thích
        [HttpGet("status")]
        public async Task<IActionResult> CheckFavoriteStatus(int userId, int recipeId)
        {
            var isFavorited = await _favoriteService.IsRecipeFavoritedAsync(userId, recipeId);
            return Ok(new { isFavorited });
        }
    }

}
