using CookingCourseAPI.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingCourseAPI.Repositories.Interfaces
{
    public interface IBlogReportRepository
    {
        Task<IEnumerable<BlogReport>> GetReportsByBlogIdAsync(int blogId);
        Task<bool> HasUserReportedAsync(int blogId, int userId); // kiểm tra tồn tại
        Task AddAsync(BlogReport report);                        // chỉ thêm, không cần trả về bool
        Task<bool> DeleteAsync(int id);                          // xóa có kiểm tra tồn tại
    }
}
