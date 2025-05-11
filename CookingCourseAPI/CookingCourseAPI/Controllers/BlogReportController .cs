using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CookingCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogReportController : ControllerBase
    {
        private readonly IBlogReportService _blogReportService;

        public BlogReportController(IBlogReportService blogReportService)
        {
            _blogReportService = blogReportService;
        }

        [HttpPost]
        public async Task<IActionResult> ReportBlog([FromBody] ReportBlogDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Reason) || request.BlogId <= 0 || request.UserId <= 0)
            {
                return BadRequest("Invalid report data.");
            }

            var report = new BlogReport
            {
                BlogId = request.BlogId,
                UserId = request.UserId,
                Reason = request.Reason,
                ReportedAt = request.CreatedAt != default ? request.CreatedAt : DateTime.UtcNow
            };

            var result = await _blogReportService.ReportBlogAsync(report);
            if (result)
            {
                return Ok("Blog has been reported successfully.");
            }
            else
            {
                return BadRequest("You have already reported this blog.");
            }
        }





        // GET: api/BlogReport/{blogId}
        [HttpGet("{blogId}")]
        public async Task<IActionResult> GetBlogReports(int blogId)
        {
            var reports = await _blogReportService.GetReportsByBlogIdAsync(blogId);
            if (reports == null || reports.Count() == 0)
            {
                return NotFound("No reports found for this blog.");
            }

            return Ok(reports);
        }
    }
}