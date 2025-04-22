using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Models.Responses;
using CookingCourseAPI.Repositories.Interfaces;
using CookingCourseAPI.Services.Interfaces;
using CookingCourseAPI.Wrappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingCourseAPI.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepo;
        private readonly IBlogReportRepository _reportRepo;

        public BlogService(IBlogRepository blogRepo, IBlogReportRepository reportRepo)
        {
            _blogRepo = blogRepo;
            _reportRepo = reportRepo;
        }

        public async Task<ApiResponse<IEnumerable<Blog>>> GetAllAsync()
        {
            var blogs = await _blogRepo.GetAllAsync();
            return new ApiResponse<IEnumerable<Blog>>(true, "Lấy danh sách blog thành công", blogs);
        }

        public async Task<ApiResponse<Blog>> GetByIdAsync(int id)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null)
                return new ApiResponse<Blog>(false, "Blog not found", null);  // Khi không tìm thấy blog
            return new ApiResponse<Blog>(true, "Lấy danh sách blog thành công", blog);  // Khi tìm thấy blog, trả về dữ liệu
        }

        public async Task<ApiResponse<Blog>> CreateAsync(CreateBlogDto dto)
        {
         try { 
            var blog = new Blog
            {
                
                Title = dto.Title,
                Content = dto.Content,
                ImageUrl = dto.ImageUrl,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _blogRepo.AddAsync(blog);
            return new ApiResponse<Blog>(true, "Tạo Blog thành công", blog);
        }
    
            catch (Exception ex)
            {
                return new ApiResponse<Blog>(false, "Đã xảy ra lỗi: " + ex.Message, null);
            }
        }

        public async Task<ApiResponse<Blog>> UpdateAsync(int id, Blog blog)
        {
            var existing = await _blogRepo.GetByIdAsync(id);
            if (existing == null)
                return new ApiResponse<Blog>(false, "Blog not found", null);

            existing.Title = blog.Title;
            existing.Content = blog.Content;
            existing.UpdatedAt = DateTime.Now;

            await _blogRepo.UpdateAsync(existing);
            return new ApiResponse<Blog>(true, "Cập nhật blog thành công", existing);
        }

        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null)
                return new ApiResponse<string>(false, "Blog not found", null);

            await _blogRepo.DeleteAsync(blog);
            return new ApiResponse<string>(true ,"Blog deleted",null);
        }

        public async Task<ApiResponse<IEnumerable<Blog>>> GetBlogsByUserIdAsync(int userId)
        {
            var blogs = await _blogRepo.GetAllAsync();
            var userBlogs = blogs.Where(b => b.UserId == userId);
            return new ApiResponse<IEnumerable<Blog>>(true, "Lấy danh sách blog củangười dùng "+userId +"thành công", userBlogs);
        }

        public async Task<ApiResponse<IEnumerable<Blog>>> SearchBlogsAsync(string keyword)
        {
            var blogs = await _blogRepo.GetAllAsync();
            var result = blogs.Where(b =>
                (!string.IsNullOrEmpty(b.Title) && b.Title.Contains(keyword)) ||
                (!string.IsNullOrEmpty(b.Content) && b.Content.Contains(keyword))
            );

            return new ApiResponse<IEnumerable<Blog>>(true, "Lấy danh sách blog thành công", result);
        }

        //public async Task<ApiResponse<string>> ReportBlogAsync(int blogId, string reason)
        //{
        //    var blog = await _blogRepo.GetByIdAsync(blogId);
        //    if (blog == null)
        //        return new ApiResponse<string>(false, "Blog not found", null);

        //    await _reportRepo.AddAsync(new BlogReport
        //    {
        //        BlogId = blogId,
        //        Reason = reason,
        //        ReportedAt = DateTime.Now
        //    });

        //    return new ApiResponse<string>(true ,"Reported successfully", null);
        //}

        public async Task<ApiResponse<PagedResult<Blog>>> GetPagedBlogsAsync(int page, int size)
        {
            var total = await _blogRepo.CountAsync();
            var items = await _blogRepo.GetPagedAsync(page, size);
            var result = new PagedResult<Blog>(items, total, page, size);
            return new ApiResponse<PagedResult<Blog>>(true,"",result);
        }

        //public async Task<ApiResponse<string>> ApproveBlogAsync(int blogId)
        //{
        //    var blog = await _blogRepo.GetByIdAsync(blogId);
        //    if (blog == null)
        //        return new ApiResponse<string>("Blog not found", false);

        //    blog.IsApproved = true;
        //    await _blogRepo.UpdateAsync(blog);
        //    return new ApiResponse<string>("Blog approved");
        //}

        public async Task<ApiResponse<string>> LikeBlogAsync(int blogId, int userId)
        {
            var blog = await _blogRepo.GetByIdAsync(blogId);
            if (blog == null)
                return new ApiResponse<string>(false, "Blog not found");

            // Kiểm tra xem blog có danh sách likes chưa, nếu chưa thì khởi tạo
            blog.Likes ??= new List<int>();

            // Nếu người dùng đã like thì thực hiện unlike (xóa like)
            if (blog.Likes.Contains(userId))
            {
                blog.Likes.Remove(userId); // Xóa like của user
                await _blogRepo.UpdateAsync(blog);
                return new ApiResponse<string>(true, "Unliked blog"); // Trả về thông báo bỏ like
            }
            else
            {
                // Nếu người dùng chưa like thì thực hiện like (thêm like)
                blog.Likes.Add(userId); // Thêm like của user
                await _blogRepo.UpdateAsync(blog);
                return new ApiResponse<string>(true, "Liked blog"); // Trả về thông báo đã like
            }
        }

    }
}
