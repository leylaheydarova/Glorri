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

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegistgerDto dto)
        {
            var message = await _service.RegisterAsync(dto);
            return StatusCode(201, message);
            //todo: email verification
        }
    }
}
