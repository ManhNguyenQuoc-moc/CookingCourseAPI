using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Models.Responses;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<ApiResponse<Recipe>> CreateRecipeAsync(RecipeCreateDto dto);
        Task<ApiResponse<IEnumerable<Recipe>>> GetAllRecipesAsync();
        Task<ApiResponse<Recipe>> GetRecipeByIdAsync(int id);
        Task<ApiResponse<string>> UpdateRecipeAsync(int id, RecipeCreateDto dto);
        Task<ApiResponse<string>> DeleteRecipeAsync(int id);
    }
}
