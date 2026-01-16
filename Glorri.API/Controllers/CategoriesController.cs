using Glorri.API.DTOs.Category;
using Glorri.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Glorri.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _service.GetAllAsync();
            return StatusCode(200, data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto dto)
        {
            var message = await _service.CreateAsync(dto);
            return StatusCode(201, message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var message = await _service.RemoveAsync(id);
            return StatusCode(204, message);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, CategoryUpdateDto dto)
        {
            var message = await _service.UpdateAsync(id, dto);
            return StatusCode(200, message);
        }
    }
}
