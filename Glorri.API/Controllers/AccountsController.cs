using Glorri.API.DTOs.AppUser;
using Glorri.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Glorri.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        readonly IAccoutService _service;

        public AccountsController(IAccoutService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegistgerDto dto)
        {
            var message = await _service.RegisterAsync(dto);
            return StatusCode(201, message);
            //todo: email verification
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetSingle([FromRoute] string username)
        {
            if (!HttpContext.User.Identity.IsAuthenticated) return Unauthorized();
            var data = await _service.GetSingleAsync(username);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAccount(string refreshToken)
        {
            var message = await _service.RemoveAccountAsync(refreshToken);
            return StatusCode(200, message);
        }

        [HttpPatch]
        public async Task<IActionResult> Update(string refreshToken, AppUserUpdateDto dto)
        {
            var message = await _service.UpdateAsync(refreshToken, dto);
            return StatusCode(200, message);
        }
    }
}
