using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Models.Responses;
using CookingCourseAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CookingCourseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/comments/blog/5
        [HttpGet("blog/{blogId}")]
        public async Task<IActionResult> GetCommentsByBlogId(int blogId)
        {
            var result = await _commentService.GetCommentsByBlogIdAsync(blogId);
            return Ok(result);
        }


        // POST: api/comments
        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] CreateCommentDto dto)
        {
            var response = await _commentService.AddCommentAsync(dto);
            return StatusCode(response.Success ? 201 : 400, response);
        }

        // POST: api/comments/like
        [HttpPost("like")]
        public async Task<IActionResult> LikeComment([FromQuery] int commentId, [FromQuery] int userId)
        {
            var response = await _commentService.LikeCommentAsync(commentId, userId);
            return StatusCode(response.Success ? 200 : 400, response);
        }

        // POST: api/comments/report
        [HttpPost("report")]
        public async Task<IActionResult> ReportComment([FromBody] CreateCommentReportDto dto)
        {
            var response = await _commentService.ReportCommentAsync(dto);
            return StatusCode(response.Success ? 200 : 400, response);
        }
    }
}
