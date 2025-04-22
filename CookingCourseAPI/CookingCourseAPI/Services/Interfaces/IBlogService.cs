using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Models.Responses;
using CookingCourseAPI.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface IBlogService
    {
        Task<ApiResponse<IEnumerable<Blog>>> GetAllAsync();
        Task<ApiResponse<Blog>> GetByIdAsync(int id);
        Task<ApiResponse<Blog>> CreateAsync(CreateBlogDto dto);
        Task<ApiResponse<Blog>> UpdateAsync(int id, Blog blog);
        Task<ApiResponse<string>> DeleteAsync(int id);
        Task<ApiResponse<IEnumerable<Blog>>> GetBlogsByUserIdAsync(int userId);
        Task<ApiResponse<IEnumerable<Blog>>> SearchBlogsAsync(string keyword);
        
        Task<ApiResponse<PagedResult<Blog>>> GetPagedBlogsAsync(int pageNumber, int pageSize);
        //Task<ApiResponse<string>> ApproveBlogAsync(int blogId);
        Task<ApiResponse<string>> LikeBlogAsync(int blogId, int userId);
    }
}