using CookingCourseAPI.DTOs;
using CookingCourseAPI.Helpers;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Models.Responses;
using CookingCourseAPI.Services;
using CookingCourseAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CookingCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        public UsersController(IUserService userService, ICourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("Không tìm thấy thông tin người dùng trong token");
            var userId = int.Parse(userIdClaim.Value);
            var profile = await _userService.GetProfileAsync(userId);
            if (profile == null) return NotFound();

            return Ok(profile);
        }
        // PUT: api/users/{id}
        //[Authorize]
        //[Authorize]
        [HttpPut("{userId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfile(int userId, [FromForm] UpdateProfileDto dto)
        {
            // Lấy ID người dùng từ token để đảm bảo người dùng chỉ có thể cập nhật chính họ
            //var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            //if (userIdClaim == null)
            //    return Unauthorized("Không tìm thấy thông tin người dùng trong token");

            //var tokenUserId = int.Parse(userIdClaim.Value);
            //if (tokenUserId != userId)
            //    return Forbid("Bạn không thể cập nhật thông tin của người dùng khác.");

            var user = await _userService.UpdateProfileAsync(userId, dto);
            if (user == null) return NotFound("Không tìm thấy người dùng.");

            return Ok(new { message = "Thông tin đã được cập nhật thành công." });
        }

        [HttpGet("users")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> GetAllUser()
        {
            var userslist = await _userService.GetAllUsersAsync();
            if (userslist == null || !userslist.Any())
                return NotFound("Không tìm thấy người dùng nào.");

            return Ok(userslist);
        }
        [HttpPut("users/{id}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserRole(int id, [FromBody] string newRole)
        {
            if (string.IsNullOrWhiteSpace(newRole) || (newRole != "Admin" && newRole != "User"))
                return BadRequest("Vai trò không hợp lệ. Chỉ chấp nhận 'Admin' hoặc 'User'.");

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Không tìm thấy người dùng.");

            var result = await _userService.UpdateUserRoleAsync(id, newRole);
            if (result==null)
                return StatusCode(500, "Cập nhật vai trò thất bại.");

            return Ok(new { message = "Cập nhật vai trò thành công." });
        }

        //[Authorize]
        [HttpGet("Mycourse/{userId}")]
        public async Task<IActionResult> GetCoursesByUserId(int userId)
        {
            var courses = await _courseService.GetCoursesByUserIdAsync(userId);
            return Ok(ApiResponse<IEnumerable<Course>>.SuccessResponse(courses));
        }
        [HttpPut("users/{id}/lock")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> ToggleUserLock(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Không tìm thấy người dùng.");

            var isLocked = await _userService.ToggleUserLockAsync(id);
            var message = isLocked ? "Tài khoản đã bị khóa." : "Tài khoản đã được mở khóa.";
            return Ok(new { message });
        }


    }
}
