using CookingCourseAPI.Data;
using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;
using Microsoft.EntityFrameworkCore;

public class CourseVideoRepository : GenericRepository<CourseVideo>, ICourseVideoRepository
{
    private readonly AppDbContext _context;

    public CourseVideoRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<CourseVideo> GetByIdAsync(int id)
    {
        return await _context.CourseVideos.FindAsync(id);  // Trả về video theo ID
    }

    public async Task<IEnumerable<CourseVideo>> GetAllAsync()
    {
        return await _context.CourseVideos.ToListAsync();  // Trả về danh sách tất cả video
    }

    public async Task<IEnumerable<CourseVideo>> GetVideosByCourseIdAsync(int courseId)
    {
        return await _context.CourseVideos
                             .Where(v => v.CourseId == courseId)
                             .ToListAsync();  // Sử dụng ToListAsync() sau khi thêm namespace
    }
    public async Task<IEnumerable<Recipe>> GetRecipesByVideoIdAsync(int videoId)
    {
        return await _context.Recipes
                             .Where(r => r.CourseVideoId == videoId)
                             .ToListAsync();
    }
    public async Task<VideoWithRecipesDto> GetVideoWithRecipeAsync(int videoId)
    {
        // Lấy video và công thức 
        var video = await _context.CourseVideos
                                  .Include(v => v.Recipe)
                                  .FirstOrDefaultAsync(v => v.Id == videoId);

        // Nếu video không tồn tại, trả về null
        if (video == null) return null;

        // Lấy công thức của video
        var recipe = video.Recipe;

        // Nếu không có công thức, trả về DTO với Recipe = null
        if (recipe == null)
        {
            return new VideoWithRecipesDto
            {
                Title = video.Title,
                Url = video.Url,
                Recipe = null // Không có công thức
            };
        }

        // Nếu có công thức, ánh xạ thành DTO và trả về
        return new VideoWithRecipesDto
        {
            Title = video.Title,
            Url = video.Url,
            Recipe = new RecipeDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Instructions = recipe.Instructions,
                Ingredients = recipe.Ingredients,
                CookingTips = recipe.CookingTips
            }
        };
    }
    public async Task RemoveRange(IEnumerable<CourseVideo> videos)
    {
        _context.Set<CourseVideo>().RemoveRange(videos);
        await _context.SaveChangesAsync();
    }

}
