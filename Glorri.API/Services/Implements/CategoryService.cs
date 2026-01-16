using Glorri.API.DTOs.Category;
using Glorri.API.Exceptions;
using Glorri.API.Models;
using Glorri.API.Repositories.Interfaces;
using Glorri.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Glorri.API.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        readonly IGenericRepository<Category> _repository;

        public CategoryService(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateAsync(CategoryCreateDto dto)
        {
            var category = new Category()
            {
                Name = dto.Name,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow.AddHours(4)
            };
            await _repository.AddAsync(category);
            await _repository.SaveAsync();
            return "Category was created successfuly!";
        }

        public async Task<List<CategoryGetDto>> GetAllAsync()
        {
            var query = _repository.GetAll(false);
            var dtos = await query.Select(category => new CategoryGetDto()
            {
                Id = category.Id,
                Name = category.Name,
            }).ToListAsync();
            return dtos;
        }

        public async Task<string> RemoveAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id, true);
            if (category == null) throw new NotFoundException("category");
            _repository.Remove(category);
            await _repository.SaveAsync();
            return "Category was removed permanently!";
        }

        public async Task<string> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _repository.GetByIdAsync(id, true);
            if (category == null) throw new NotFoundException("category");

            category.Name = dto.Name != null ? dto.Name : category.Name;
            category.UpdatedDate = DateTime.UtcNow.AddHours(4);

            _repository.Update(category);
            await _repository.SaveAsync();
            return "Category was updated successfully!";
        }
    }
}
