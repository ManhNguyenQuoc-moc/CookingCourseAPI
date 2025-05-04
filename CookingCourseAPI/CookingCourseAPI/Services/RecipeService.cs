using CookingCourseAPI.Data;
using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Models.Responses;
using CookingCourseAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCourseAPI.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly AppDbContext _context;

        public RecipeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<Recipe>> CreateRecipeAsync(RecipeCreateDto dto)
        {
            var recipe = new Recipe
            {
                Title = dto.Title,
                Instructions = dto.Instructions,
                VideoUrl = dto.VideoUrl,
                CourseId = dto.CourseId
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return new ApiResponse<Recipe>(true, "Tạo công thức thành công", recipe);
        }

        public async Task<ApiResponse<IEnumerable<Recipe>>> GetAllRecipesAsync()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return new ApiResponse<IEnumerable<Recipe>>(true, "Lấy danh sách thành công", recipes);
        }

        public async Task<ApiResponse<Recipe>> GetRecipeByIdAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return new ApiResponse<Recipe>(false, "Không tìm thấy công thức");

            return new ApiResponse<Recipe>(true, "Thành công", recipe);
        }

        public async Task<ApiResponse<string>> UpdateRecipeAsync(int id, RecipeCreateDto dto)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return new ApiResponse<string>(false, "Không tìm thấy công thức");

            recipe.Title = dto.Title;
            recipe.Instructions = dto.Instructions;
            recipe.VideoUrl = dto.VideoUrl;
            recipe.CourseId = dto.CourseId;

            await _context.SaveChangesAsync();
            return new ApiResponse<string>(true, "Cập nhật thành công");
        }

        public async Task<ApiResponse<string>> DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
                return new ApiResponse<string>(false, "Không tìm thấy công thức");

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return new ApiResponse<string>(true, "Xóa thành công");
        }
    }
}
