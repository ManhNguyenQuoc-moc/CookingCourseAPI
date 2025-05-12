using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Models.Responses;
using CookingCourseAPI.Services.Interfaces;
using CookingCourseAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookingCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Blog>>>> GetAll()
        {
            var response = await _blogService.GetAllAsync();
            if (response.Success)
                return Ok(response);  // Trả về 200 OK và kết quả nếu thành công
            return BadRequest(response);  // Trả về 400 BadRequest nếu có lỗi
        }

        // GET: api/Blogs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Blog>>> GetById(int id)
        {
            var response = await _blogService.GetByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);  // Trả về 404 nếu không tìm thấy blog
        }

        // POST: api/Blogs
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBlogDto dto)
        {
            var result = await _blogService.CreateAsync(dto);
            return Ok(result);
        }

        // PUT: api/Blogs/{id}
        //[HttpPut("{id}")]
        //public async Task<ActionResult<ApiResponse<Blog>>> Update(int id, [FromBody] Blog blog)
        //{
        //    if (blog == null)
        //        return BadRequest("Blog cannot be null.");

        //    var response = await _blogService.UpdateAsync(id, blog);
        //    if (response.Success)
        //        return Ok(response);
        //    return NotFound(response);  // Trả về 404 nếu không tìm thấy blog để cập nhật
        //}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Blog>>> Update(int id, [FromBody] UpdateBlogDto blogDto)
        {
            if (blogDto == null)
                return BadRequest("Dữ liệu cập nhật không hợp lệ.");

            var response = await _blogService.UpdateAsync(id, blogDto);
            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        // DELETE: api/Blogs/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var response = await _blogService.DeleteAsync(id);
            if (response.Success)
                return Ok(response);
            return NotFound(response);  // Trả về 404 nếu không tìm thấy blog để xóa
        }
        [HttpPost("like/{blogId}")]
        public async Task<ApiResponse<string>> LikeBlogAsync(int blogId, [FromQuery] int userId)
        {
            return await _blogService.LikeBlogAsync(blogId, userId);
        }
        // GET: api/Blogs/paged?pageNumber=1&pageSize=10
        [HttpGet("paged")]
        public async Task<ActionResult<ApiResponse<PagedResult<Blog>>>> GetPagedBlogs(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _blogService.GetPagedBlogsAsync(pageNumber, pageSize);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);  // Trả về 400 BadRequest nếu có lỗi
        }
        // GET: api/Blogs/{blogId}/likes/count
        [HttpGet("{blogId}/likes/count")]
        public async Task<ActionResult<ApiResponse<int>>> GetLikesCount(int blogId)
        {
            var response = await _blogService.GetLikesCountAsync(blogId);

            if (!response.Success)
            {
                return NotFound(response);  // Trả về 404 nếu blog không tìm thấy
            }

            return Ok(response);  // Trả về 200 OK nếu thành công
        }

    }
}
