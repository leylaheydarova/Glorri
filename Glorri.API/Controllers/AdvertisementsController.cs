using Glorri.API.DTOs.Advertisement;
using Glorri.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Glorri.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        readonly IAdvertisementService _service;

        public AdvertisementsController(IAdvertisementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return StatusCode(200, data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdvertisementCreateDto dto)
        {
            var message = await _service.CreateAsync(dto);
            return StatusCode(201, message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
        {
            var data = await _service.GetSingleAsync(id);
            return StatusCode(200, data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var message = await _service.RemoveAsync(id);
            return StatusCode(200, message);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, AdvertisementUpdateDto dto)
        {
            var message = await _service.UpdateAsync(id, dto);
            return StatusCode(200, message);
        }

        [HttpPatch("toggle/{id}")]
        public async Task<IActionResult> Toggle([FromRoute] int id)
        {
            var message = await _service.ToggleAsync(id);
            return StatusCode(200, message);
        }
    }
}
