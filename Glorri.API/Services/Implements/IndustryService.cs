using Glorri.API.DTOs.Industry;
using Glorri.API.Exceptions;
using Glorri.API.Models;
using Glorri.API.Repositories.Interfaces;
using Glorri.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Glorri.API.Services.Implements
{
    public class IndustryService : IIndustryService
    {
        readonly IGenericRepository<Industry> _repository;

        public IndustryService(IGenericRepository<Industry> repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateAsync(IndustryCreateDto dto)
        {
            var industry = new Industry()
            {
                Name = dto.Name,
                CreatedDate = DateTime.UtcNow.AddHours(4)
            };
            await _repository.AddAsync(industry);
            await _repository.SaveAsync();
            return "Industry was created successfully!";
        }

        public async Task<List<IndustryGetDto>> GetAllAsync()
        {
            var query = _repository.GetAllWhere(c => !c.IsDeleted, false);
            var dtos = await query.Select(industry => new IndustryGetDto()
            {
                Id = industry.Id,
                Name = industry.Name,
            }).ToListAsync();
            return dtos;
        }

        public async Task<IndustryGetDto> GetSingleAsync(int id)
        {
            var industry = await _repository.GetWhereAsync(c => c.Id == id && !c.IsDeleted, false);
            if (industry == null) throw new NotFoundException("industry");
            var dto = new IndustryGetDto()
            {
                Id = industry.Id,
                Name = industry.Name,
            };
            return dto;
        }

        public async Task<string> RemoveAsync(int id)
        {
            var industry = await _repository.GetByIdAsync(id, true);
            if (industry == null) throw new NotFoundException("industry");
            _repository.Remove(industry);
            await _repository.SaveAsync();
            return "Industry was removed permanently!";
        }

        public async Task<string> ToggleAsync(int id)
        {
            var industry = await _repository.GetByIdAsync(id, true);
            if (industry == null) throw new NotFoundException("industry");
            var result = _repository.Toggle(industry);
            if (result) industry.DeletedDate = DateTime.UtcNow.AddHours(4);
            else if (!result) industry.UpdatedDate = DateTime.UtcNow.AddHours(4);
            await _repository.SaveAsync();
            return result ? "Industry was deleted temporarily!" : "Industry was recovered successfully!";
        }

        public async Task<string> UpdateAsync(int id, IndustryUpdateDto dto)
        {
            var industry = await _repository.GetWhereAsync(c => c.Id == id && !c.IsDeleted, true);
            if (industry == null) throw new NotFoundException("industry");

            industry.Name = dto.Name != null ? dto.Name : industry.Name;
            industry.UpdatedDate = DateTime.UtcNow.AddHours(4);

            _repository.Update(industry);
            await _repository.SaveAsync();
            return "Industry was updated successfully!";
        }
    }
}
