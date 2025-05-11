using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;
using CookingCourseAPI.Repositories.Interfaces;
using CookingCourseAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingCourseAPI.Services
{
    public class BlogReportService : IBlogReportService
    {
        private readonly IBlogReportRepository _repository;

        public BlogReportService(IBlogReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ReportBlogAsync(BlogReport report)
        {
            if (await _repository.HasUserReportedAsync(report.BlogId, report.UserId))
                return false;

            await _repository.AddAsync(report);
            return true;
        }



        public async Task<IEnumerable<BlogReport>> GetReportsByBlogIdAsync(int blogId)
        {
            return await _repository.GetReportsByBlogIdAsync(blogId);
        }

        public async Task<bool> HasUserReportedAsync(int blogId, int userId)
        {
            return await _repository.HasUserReportedAsync(blogId, userId);
        }

        public async Task<bool> DeleteReportAsync(int reportId)
        {
            return await _repository.DeleteAsync(reportId);
        }
    }
}
