using Glorri.API.DTOs.Advertisement;
using Glorri.API.Exceptions;
using Glorri.API.Models;
using Glorri.API.Repositories.Interfaces;
using Glorri.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Glorri.API.Services.Implements
{
    public class AdvertisementService : IAdvertisementService
    {
        readonly IGenericRepository<Advertisement> _repository;

        public AdvertisementService(IGenericRepository<Advertisement> repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateAsync(AdvertisementCreateDto dto)
        {
            var advertisement = new Advertisement()
            {
                Description = dto.Description,
                CompanyId = dto.CompanyId,
                CategoryId = dto.CategoryId,
                CreatedDate = DateTime.UtcNow.AddHours(4),
                EndDate = dto.EndDate,
                PublishDate = dto.PublishDate,
                Requirement = dto.Requirement,
                VacancyType = dto.VacancyType,
            };
            await _repository.AddAsync(advertisement);
            await _repository.SaveAsync();
            return "Advertisement was created successfully!";
        }

        public async Task<List<AdvertisementGetDto>> GetAllAsync()
        {
            var query = _repository.GetAllWhere(a => !a.IsDeleted, false, "Company");
            var dtos = await query.Select(advertisement => new AdvertisementGetDto()
            {
                Id = advertisement.Id,
                CategoryId = advertisement.CategoryId,
                CompanyId = advertisement.CompanyId,
                Description = advertisement.Description,
                EndDate = advertisement.EndDate,
                PublishDate = advertisement.PublishDate,
                Requirement = advertisement.Requirement,
                VacancyType = advertisement.VacancyType.ToString(),
                CompanyName = advertisement.Company.Name
            }).ToListAsync();
            return dtos;
        }

        public async Task<AdvertisementGetDto> GetSingleAsync(int id)
        {
            var advertisement = await _repository.GetWhereAsync(a => a.Id == id && !a.IsDeleted, false, "Company");
            if (advertisement == null) throw new NotFoundException("advertisement");
            var dto = new AdvertisementGetDto()
            {
                Id = advertisement.Id,
                CategoryId = advertisement.CategoryId,
                CompanyId = advertisement.CompanyId,
                Description = advertisement.Description,
                EndDate = advertisement.EndDate,
                PublishDate = advertisement.PublishDate,
                Requirement = advertisement.Requirement,
                VacancyType = advertisement.VacancyType.ToString(),
                CompanyName = advertisement.Company.Name
            };
            return dto;
        }

        public async Task<string> RemoveAsync(int id)
        {

            var advertisement = await _repository.GetByIdAsync(id, true);
            if (advertisement == null) throw new NotFoundException("advertisement");
            _repository.Remove(advertisement);
            await _repository.SaveAsync();
            return "Asdvertisement was removed permanently!";
        }

        public async Task<string> ToggleAsync(int id)
        {
            var advertisement = await _repository.GetByIdAsync(id, true);
            if (advertisement == null) throw new NotFoundException("advertisement");
            var result = _repository.Toggle(advertisement);
            if (result) advertisement.DeletedDate = DateTime.UtcNow.AddHours(4);
            else advertisement.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _repository.Update(advertisement);
            await _repository.SaveAsync();
            return result ? "Asdvertisement was removed temporarily!" : "Advertisement was recovered successfully!";
        }

        public async Task<string> UpdateAsync(int id, AdvertisementUpdateDto dto)
        {
            var advertisement = await _repository.GetWhereAsync(a => a.Id == id && !a.IsDeleted, false, "Company");
            if (advertisement == null) throw new NotFoundException("advertisement");
            advertisement.Description = dto.Description != null ? dto.Description : advertisement.Description;
            advertisement.Requirement = dto.Requirement != null ? dto.Requirement : advertisement.Requirement;
            advertisement.PublishDate = dto.PublishDate != null ? dto.PublishDate.Value : advertisement.PublishDate;
            advertisement.EndDate = dto.EndDate != null ? dto.EndDate.Value : advertisement.EndDate;
            advertisement.VacancyType = dto.VacancyType != null ? dto.VacancyType.Value : advertisement.VacancyType;
            advertisement.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _repository.Update(advertisement);
            await _repository.SaveAsync();
            return "Advertisement was updated successfully!";
        }
    }
}
