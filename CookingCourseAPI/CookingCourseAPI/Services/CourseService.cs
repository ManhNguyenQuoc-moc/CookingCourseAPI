using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;
using CookingCourseAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseVideoRepository _courseVideoRepository;
    private readonly IGenericRepository<Enrollment> _enrollCourseRepository;
    private readonly IGenericRepository<Recipe> _recipeRepository;
    public CourseService(ICourseRepository courseRepository, ICourseVideoRepository courseVideoRepository, IGenericRepository<Enrollment> enrollCourseRepository, IGenericRepository<Recipe> recipeRepository)
    {
        _courseRepository = courseRepository;
        _courseVideoRepository = courseVideoRepository;
        _enrollCourseRepository = enrollCourseRepository;
        _recipeRepository = recipeRepository;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _courseRepository.GetAllAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _courseRepository.GetCourseByIdAsync(id);
    }

    public async Task<bool> AddCourseAsync(Course course)
    {
        await _courseRepository.AddAsync(course);

        if (course.Videos != null && course.Videos.Any())
        {
            foreach (var video in course.Videos)
            {
                video.CourseId = course.Id;
                await _courseVideoRepository.AddAsync(video);
            }
        }

        return await _courseRepository.SaveChangesAsync();
    }

    public async Task<bool> UpdateCourseAsync(Course course)
    {
        await _courseRepository.UpdateAsync(course);

        if (course.Videos != null && course.Videos.Any())
        {
            foreach (var video in course.Videos)
            {
                video.CourseId = course.Id;
                await _courseVideoRepository.UpdateAsync(video);
            }
        }

        return await _courseRepository.SaveChangesAsync();
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null) return false;

        foreach (var video in course.Videos)
        {
            await _courseVideoRepository.DeleteAsync(video);
        }

        await _courseRepository.DeleteAsync(course);

        return await _courseRepository.SaveChangesAsync();
    }
    public async Task<(bool Success, string Message)> EnrollUserInCourseAsync(int userId, int courseId)
    {
        var exists = await _enrollCourseRepository
            .AnyAsync(e => e.UserId == userId && e.CourseId == courseId);

        if (exists)
        {
            return (false, "Người dùng đã đăng ký khóa học này rồi.");
        }

        var newEnroll = new Enrollment
        {
            UserId = userId,
            CourseId = courseId,
            EnrolledDate = DateTime.UtcNow
        };

        await _enrollCourseRepository.AddAsync(newEnroll);
        await _enrollCourseRepository.SaveChangesAsync();

        return (true, "Đăng ký thành công khóa học.");
    }
    public async Task<IEnumerable<Course>> GetCoursesByUserIdAsync(int userId)
    {
        return await _courseRepository.GetCoursesByUserIdAsync(userId);
    }
    public async Task<IEnumerable<CourseVideo>> GetVideosByCourseIdAsync(int courseId)
    {
        return await _courseRepository.GetVideosByCourseIdAsync(courseId);
    }
    

    public async Task<Course> AddCourseWithVideosAsync(CourseCreateDto courseCreateDto)
    {
        var course = new Course
        {
            Name = courseCreateDto.Name,
            Description = courseCreateDto.Description,
            Price = courseCreateDto.Price,
            IsFree = courseCreateDto.IsFree
        };

        await _courseRepository.AddAsync(course);
        await _courseRepository.SaveChangesAsync();

        if (courseCreateDto.Videos != null && courseCreateDto.Videos.Any())
        {
            foreach (var videoDto in courseCreateDto.Videos)
            {
                var video = new CourseVideo
                {
                    Title = videoDto.Title,
                    Url = videoDto.Url,
                    CourseId = course.Id
                };

                await _courseVideoRepository.AddAsync(video);
                await _courseVideoRepository.SaveChangesAsync(); // Đảm bảo video.Id có giá trị để gán vào recipe

                // Tạo công thức gắn với video
                if (!string.IsNullOrEmpty(videoDto.RecipeTitle) && !string.IsNullOrEmpty(videoDto.Instructions))
                {
                    var recipe = new Recipe
                    {
                        Title = videoDto.RecipeTitle,
                        Instructions = videoDto.Instructions,
                        CourseVideoId = video.Id // Gắn recipe với video
                    };

                    await _recipeRepository.AddAsync(recipe);
                }
            }
        }

        await _recipeRepository.SaveChangesAsync();
        return course;
    }


}
