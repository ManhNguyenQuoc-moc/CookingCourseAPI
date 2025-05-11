using CookingCourseAPI.DTOs;
using CookingCourseAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookingCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = await _authService.RegisterAsync(dto);
            if (user == false)
                return BadRequest("Email đã tồn tại.");
            return Ok(new
            {
                message = "Đăng ký thành công.",
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null)
                return Unauthorized("Email hoặc mật khẩu không đúng.");

            if (result == "LOCKED")
                return Unauthorized("Tài khoản của bạn đã bị khóa.");

            return Ok(new { token = result });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var result = await _authService.ForgotPasswordAsync(dto.Email);
            if (!result)
                return NotFound("Không tìm thấy người dùng với email này.");
            return Ok(new { message = "Email đặt lại mật khẩu đã được gửi." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var result = await _authService.ResetPasswordAsync(dto.Email, dto.Token, dto.NewPassword);
            if (!result)
                return BadRequest("Token không hợp lệ hoặc đã hết hạn.");
            return Ok(new { message = "Đặt lại mật khẩu thành công." });
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("ID người dùng không hợp lệ.");
            }

            var result = await _authService.ChangePasswordAsync(userId, dto.CurrentPassword, dto.NewPassword);
            if (!result)
                return BadRequest("Mật khẩu hiện tại không đúng.");

            return Ok(new { message = "Đổi mật khẩu thành công." });
        }


    }
}
