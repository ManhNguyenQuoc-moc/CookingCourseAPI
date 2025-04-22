using CookingCourseAPI.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingCourseAPI.Repositories.Interfaces
{
    public interface IBlogReportRepository
    {
        Task<IEnumerable<BlogReport>> GetReportsByBlogIdAsync(int blogId);
        Task<bool> HasUserReportedAsync(int blogId, int userId);
    }
}
