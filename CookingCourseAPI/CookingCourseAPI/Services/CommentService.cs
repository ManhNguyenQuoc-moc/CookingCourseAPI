    using CookingCourseAPI.Data;
    using CookingCourseAPI.DTOs;
    using CookingCourseAPI.Models.Entities;
    using CookingCourseAPI.Models.Responses;
    using CookingCourseAPI.Repositories;
    using CookingCourseAPI.Repositories.Interfaces;
    using CookingCourseAPI.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    namespace CookingCourseAPI.Services
    {
        public class CommentService : ICommentService
        {
            private readonly ICommentRepository _commentRepo;
       
            private readonly ICommentReportRepository _reportRepo;

            public CommentService(ICommentRepository commentRepo,
                              
                                       ICommentReportRepository reportRepo)
            {
                _commentRepo = commentRepo;
         
                _reportRepo = reportRepo;
            }

            public async Task<ApiResponse<Comment>> AddCommentAsync(CreateCommentDto dto)
            {
                var comment = new Comment
                {
                    Content = dto.Content,
                    BlogId = dto.BlogId,
                    UserId = dto.UserId,
                    CreatedAt = DateTime.UtcNow
                };

                await _commentRepo.AddAsync(comment);
                return new ApiResponse<Comment>(true, "Bình luận thành công", comment);
            }

            public async Task<ApiResponse<string>> LikeCommentAsync(int commentId, int userId)
            {
                var comment = await _commentRepo.GetByIdAsync(commentId);
                if (comment == null)
                    return new ApiResponse<string>(false, "Comment not found");

                // Khởi tạo danh sách Likes nếu chưa có
                comment.Likes ??= new List<int>();

                // Kiểm tra xem người dùng đã like comment chưa
                if (comment.Likes.Contains(userId))
                {
                    // Nếu đã like, bỏ like (unlike)
                    comment.Likes.Remove(userId);
                    await _commentRepo.UpdateAsync(comment);
                    return new ApiResponse<string>(true, "Unliked comment");
                }
                else
                {
                    // Nếu chưa like, thêm like
                    comment.Likes.Add(userId);
                    await _commentRepo.UpdateAsync(comment);
                    return new ApiResponse<string>(true, "Liked comment");
                }
            }


            public async Task<ApiResponse<IEnumerable<Comment>>> GetCommentsByBlogIdAsync(int blogId)
            {
                var comments = await _commentRepo.GetCommentByBlogIdAsync(
                    c => c.BlogId == blogId
                );

                return new ApiResponse<IEnumerable<Comment>>
                {
                    Success = true,
                    Message = "Lấy danh sách bình luận thành công",
                    Data = comments
                };
            }
    


            public async Task<ApiResponse<string>> ReportCommentAsync(CreateCommentReportDto dto)
            {
                var report = new CommentReport
                {
                    CommentId = dto.CommentId,
                    Reason = dto.Reason,
                    UserId = dto.UserId,
                    ReportedAt = DateTime.UtcNow
                };

                await _reportRepo.AddAsync(report);
                return new ApiResponse<string>(true, "Báo cáo bình luận thành công", null);
            }
        }

    }
