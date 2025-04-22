using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Responses;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface ICommentService
    {
        Task<ApiResponse<Comment>> AddCommentAsync(CreateCommentDto dto);
        Task<ApiResponse<string>> LikeCommentAsync(int commentId, int userId);
        Task<ApiResponse<IEnumerable<Comment>>> GetCommentsByBlogIdAsync(int blogId);
        Task<ApiResponse<string>> ReportCommentAsync(CreateCommentReportDto dto);
    }
}
