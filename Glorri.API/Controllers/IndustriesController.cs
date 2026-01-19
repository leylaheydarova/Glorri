using Glorri.API.DTOs.Industry;
using Glorri.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Glorri.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustriesController : ControllerBase
    {
        readonly IIndustryService _service;

        public IndustriesController(IIndustryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IndustryCreateDto dto)
        {
            var message = await _service.CreateAsync(dto);
            return StatusCode(201, message);
        }
    }
}
