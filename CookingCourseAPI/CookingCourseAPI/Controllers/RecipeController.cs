using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Responses;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookingCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreateDto dto)
        {
            var response = await _recipeService.CreateRecipeAsync(dto);
            return StatusCode(response.Success ? 200 : 400, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _recipeService.GetAllRecipesAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _recipeService.GetRecipeByIdAsync(id);
            return StatusCode(response.Success ? 200 : 404, response);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] RecipeCreateDto dto)
        {
            var response = await _recipeService.UpdateRecipeAsync(id, dto);
            return StatusCode(response.Success ? 200 : 404, response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _recipeService.DeleteRecipeAsync(id);
            return StatusCode(response.Success ? 200 : 404, response);
        }
    }
}
