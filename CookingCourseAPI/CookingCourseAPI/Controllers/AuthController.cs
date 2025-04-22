using CookingCourseAPI.DTOs;
using CookingCourseAPI.Services.Interfaces;
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
            var token = await _authService.LoginAsync(dto);
            if (token == null)
                return Unauthorized("Email hoặc mật khẩu không đúng.");

            return Ok(new
            {
                message = "Đăng nhập thành công.",
                token
            });
        }
       

    }
}
