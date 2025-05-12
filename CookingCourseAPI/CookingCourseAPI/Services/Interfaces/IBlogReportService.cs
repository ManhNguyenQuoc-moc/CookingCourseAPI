using CookingCourseAPI.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface IBlogReportService
    {
        Task<bool> ReportBlogAsync(BlogReport report);
        Task<IEnumerable<BlogReport>> GetReportsByBlogIdAsync(int blogId);
        Task<bool> HasUserReportedAsync(int blogId, int userId);
        Task<bool> DeleteReportAsync(int reportId);

    }
}