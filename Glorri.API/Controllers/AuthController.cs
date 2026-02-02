using Glorri.API.DTOs.Auth;
using Glorri.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Glorri.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _service.LoginAsync(dto);
            return StatusCode(200, token);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string refreshToken)
        {
            await _service.LogoutAsync(refreshToken);
            return Ok();
        }

        [HttpGet("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string username)
        {
            await _service.ForgotPasswordAsync(username);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ForgetPasswordDto dto)
        {
            var message = await _service.ResetPasswordAsync(dto);
            return Ok(message);
        }
    }
}
