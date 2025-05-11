using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;
using CookingCourseAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using CookingCourseAPI.Services;
using CookingCourseAPI.Models.Responses;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ICourseVideoRepository _courseVideoRepository;
    private readonly IGenericRepository<Enrollment> _enrollCourseRepository;
    private readonly IGenericRepository<Recipe> _recipeRepository;
    private readonly PhotoService photo;


    public CourseService(
        ICourseRepository courseRepository,
        ICourseVideoRepository courseVideoRepository,
        IGenericRepository<Enrollment> enrollCourseRepository,
        IGenericRepository<Recipe> recipeRepository,PhotoService photoService)
    {
        _courseRepository = courseRepository;
        _courseVideoRepository = courseVideoRepository;
        _enrollCourseRepository = enrollCourseRepository;
        _recipeRepository = recipeRepository;
        photo = photoService;
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

    public async Task<bool> UpdateCourseAsync(int courseId, CourseCreateDto updatedCourseDto)
    {
        //var existingCourse = await _courseRepository.GetCourseByIdAsync(courseId);
        //if (existingCourse == null)
        //    throw new InvalidOperationException("Khóa học không tồn tại.");

        //// Update basic course information
        //existingCourse.Name = updatedCourseDto.Name;
        //existingCourse.Description = updatedCourseDto.Description;
        //existingCourse.Price = updatedCourseDto.Price;
        //existingCourse.IsFree = updatedCourseDto.IsFree;

        //// Handle videos
        //if (updatedCourseDto.Videos != null)
        //{
        //    // Create a list of video IDs from the DTO
        //    var updatedVideoIds = updatedCourseDto.Videos
        //        .Where(v => v.Id > 0)
        //        .Select(v => v.Id)
        //        .ToList();

        //    // Delete videos that are no longer in the updated list
        //    foreach (var oldVideo in existingCourse.Videos.ToList())
        //    {
        //        if (!updatedVideoIds.Contains(oldVideo.Id))
        //        {
        //            // Remove video and associated recipe
        //            existingCourse.Videos.Remove(oldVideo);
        //            var oldRecipe = await _recipeRepository.GetFirstOrDefaultAsync(r => r.CourseVideoId == oldVideo.Id);
        //            if (oldRecipe != null)
        //            {
        //                await _recipeRepository.DeleteAsync(oldRecipe);
        //            }
        //            await _courseVideoRepository.DeleteAsync(oldVideo);
        //        }
        //    }

        //    // Update or add videos
        //    foreach (var videoDto in updatedCourseDto.Videos)
        //    {
        //        if (videoDto.Id > 0)
        //        {
        //            // Update existing video
        //            var video = existingCourse.Videos.FirstOrDefault(v => v.Id == videoDto.Id);
        //            if (video != null)
        //            {
        //                video.Title = videoDto.Title;
        //                video.Url = videoDto.Url;

        //                // Update or add recipe
        //                var recipe = await _recipeRepository.GetFirstOrDefaultAsync(r => r.CourseVideoId == video.Id);
        //                if (!string.IsNullOrWhiteSpace(videoDto.RecipeTitle))
        //                {
        //                    if (recipe == null)
        //                    {
        //                        // Add new recipe
        //                        recipe = new Recipe
        //                        {
        //                            Title = videoDto.RecipeTitle,
        //                            Instructions = videoDto.Instructions,
        //                            Ingredients = videoDto.Ingredients,
        //                            CookingTips = videoDto.CookingTips,
        //                            CourseVideoId = video.Id
        //                        };
        //                        await _recipeRepository.AddAsync(recipe);
        //                    }
        //                    else
        //                    {
        //                        // Update existing recipe
        //                        recipe.Title = videoDto.RecipeTitle;
        //                        recipe.Instructions = videoDto.Instructions;
        //                        recipe.Ingredients = videoDto.Ingredients;
        //                        recipe.CookingTips = videoDto.CookingTips;
        //                    }
        //                }
        //                else if (recipe != null)
        //                {
        //                    // Delete recipe if no recipe data is provided
        //                    await _recipeRepository.DeleteAsync(recipe);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // Add new video
        //            var newVideo = new CourseVideo
        //            {
        //                Title = videoDto.Title,
        //                Url = videoDto.Url,
        //                CourseId = courseId
        //            };
        //            await _courseVideoRepository.AddAsync(newVideo);
        //            await _courseVideoRepository.SaveChangesAsync(); // Ensure ID is generated

        //            // Add recipe for new video if provided
        //            if (!string.IsNullOrWhiteSpace(videoDto.RecipeTitle))
        //            {
        //                var newRecipe = new Recipe
        //                {
        //                    Title = videoDto.RecipeTitle,
        //                    Instructions = videoDto.Instructions,
        //                    Ingredients = videoDto.Ingredients,
        //                    CookingTips = videoDto.CookingTips,
        //                    CourseVideoId = newVideo.Id
        //                };
        //                await _recipeRepository.AddAsync(newRecipe);
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    // If no videos are provided, clear all existing videos and recipes
        //    foreach (var oldVideo in existingCourse.Videos.ToList())
        //    {
        //        var oldRecipe = await _recipeRepository.GetFirstOrDefaultAsync(r => r.CourseVideoId == oldVideo.Id);
        //        if (oldRecipe != null)
        //        {
        //            await _recipeRepository.DeleteAsync(oldRecipe);
        //        }
        //        existingCourse.Videos.Remove(oldVideo);
        //        await _courseVideoRepository.DeleteAsync(oldVideo);
        //    }
        //}

        //// Save changes
        //await _courseRepository.UpdateAsync(existingCourse);
        //var savedCount = await _courseRepository.SaveChangesAsync();
        //await _courseVideoRepository.SaveChangesAsync();
        //await _recipeRepository.SaveChangesAsync();
        //if(savedCount == true)
        //{
        //    return true;
        //}
        return false; // Return true if any changes were saved
    }




    public async Task<bool> DeleteCourseAsync(int id)
    {
        // Lấy khóa học theo ID
        var course = await _courseRepository.GetCourseByIdAsync(id);

        // Nếu không tìm thấy khóa học, trả về false
        if (course == null) return false;

        // Kiểm tra xem có video nào không
        if (course.Videos != null)
        {
            foreach (var video in course.Videos)
            {
                // Xóa từng video liên quan
                await _courseVideoRepository.DeleteAsync(video);
            }
        }

        // Xóa khóa học
        await _courseRepository.DeleteAsync(course);

        // Lưu thay đổi và trả về kết quả
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
        if (courseCreateDto == null)
            throw new ArgumentNullException(nameof(courseCreateDto));

        string urlImage = null;

        // Try uploading image (with better exception logging)
        try
        {
            if (courseCreateDto.Image != null)
            {
                urlImage = await photo.UploadImageAsync(courseCreateDto.Image);
                if (string.IsNullOrEmpty(urlImage))
                {
                    // Log here if needed
                    throw new InvalidOperationException("Image upload failed.");
                }
            }
        }
        catch (Exception ex)
        {
            // Log exception
            Console.Error.WriteLine($"Image upload failed: {ex.Message}");
            throw; // Re-throw the exception after logging it
        }

        // Create course object
        var course = new Course
        {
            Name = courseCreateDto.Name,
            Description = courseCreateDto.Description,
            Price = courseCreateDto.Price,
            IsFree = courseCreateDto.IsFree,
            ImageUrl = urlImage
        };

        try
        {
            // Add the course and save changes (ensures course.Id is set before adding videos)
            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync(); // Save to get the course.Id
        }
        catch (Exception ex)
        {
            // Handle repository save failure (optional logging)
            Console.Error.WriteLine($"Course creation failed: {ex.Message}");
            throw;
        }

        // Add videos and related recipes (if any)
        if (courseCreateDto.Videos != null && courseCreateDto.Videos.Any())
        {
            var recipeList = new List<Recipe>();  // Prepare list for batch insertion

            foreach (var videoDto in courseCreateDto.Videos)
            {
                var video = new CourseVideo
                {
                    Title = videoDto.Title,
                    Url = videoDto.Url,
                    CourseId = course.Id
                };

                try
                {
                    // Add video and save changes
                    await _courseVideoRepository.AddAsync(video);
                    // No save here yet, to allow batch saving later

                    // Check for recipe data
                    if (!string.IsNullOrWhiteSpace(videoDto.Recipe.Title) &&
                        !string.IsNullOrWhiteSpace(videoDto.Recipe.Instructions))
                    {
                        var recipe = new Recipe
                        {
                            Title = videoDto.Recipe.Title,
                            Instructions = videoDto.Recipe.Instructions,
                            Ingredients = videoDto.Recipe.Ingredients,  // Add Ingredients
                            CookingTips = videoDto.Recipe.CookingTips,  // Add CookingTips
                            CourseVideoId = video.Id
                        };

                        // Add recipe to list (don't save immediately)
                        recipeList.Add(recipe);
                    }
                }
                catch (Exception ex)
                {
                    // Handle video creation failure
                    Console.Error.WriteLine($"Video/recipe creation failed: {ex.Message}");
                    throw; // You might want to rethrow or handle differently based on your requirements
                }
            }

            // Save all recipes at once in batch
            if (recipeList.Any())
            {
                await _recipeRepository.AddRangeAsync(recipeList);
                await _recipeRepository.SaveChangesAsync(); // Save all recipes in batch
            }

            // Save all videos at once (already done above with video save)
            await _courseVideoRepository.SaveChangesAsync();
        }

        return course;
    }

    public async Task<bool> CheckUserEnrollmentAsync(int userId, int courseId)
    {
        return await _courseRepository.IsUserEnrolledAsync(userId, courseId);
    }
    public async Task<Course> UpdateCourseWithVideosAsync(int id, CourseCreateDto courseCreateDto)
    {
        // Tìm khóa học hiện tại
        var existingCourse = await _courseRepository.GetByIdAsync(id);

        if (existingCourse == null)
        {
            throw new ArgumentException("Course not found.");
        }

        // Cập nhật thông tin khóa học
        existingCourse.Name = courseCreateDto.Name;
        existingCourse.Description = courseCreateDto.Description;
        existingCourse.Price = courseCreateDto.Price;
        existingCourse.IsFree = courseCreateDto.IsFree;

        // Nếu có hình ảnh mới, cập nhật lại
        if (courseCreateDto.Image != null)
        {
            string urlImage = await photo.UploadImageAsync(courseCreateDto.Image);
            existingCourse.ImageUrl = urlImage;
        }

        // Cập nhật video cho khóa học
        if (courseCreateDto.Videos != null && courseCreateDto.Videos.Any())
        {
            // Xóa tất cả video cũ trước khi cập nhật
            var existingVideos = await _courseVideoRepository.GetVideosByCourseIdAsync(id);
            await _courseVideoRepository.RemoveRange(existingVideos);  // Xóa video cũ trước khi thêm video mới

            foreach (var videoDto in courseCreateDto.Videos)
            {
                var video = new CourseVideo
                {
                    Title = videoDto.Title,
                    Url = videoDto.Url,
                    CourseId = existingCourse.Id
                };

                await _courseVideoRepository.AddAsync(video);
                await _courseVideoRepository.SaveChangesAsync();

                // Cập nhật công thức nếu có
                if (!string.IsNullOrWhiteSpace(videoDto.Recipe.Title) && !string.IsNullOrWhiteSpace(videoDto.Recipe.Instructions))
                {
                    var recipe = new Recipe
                    {
                        Title = videoDto.Recipe.Title,
                        Instructions = videoDto.Recipe.Instructions,
                        Ingredients = videoDto.Recipe.Ingredients,
                        CookingTips = videoDto.Recipe.CookingTips,
                        CourseVideoId = video.Id
                    };

                    // Thêm công thức vào cơ sở dữ liệu
                    await _recipeRepository.AddAsync(recipe);
                }
            }
        }

        // Lưu thay đổi vào cơ sở dữ liệu
        await _courseRepository.SaveChangesAsync();
        await _recipeRepository.SaveChangesAsync();

        return existingCourse;
    }
}
