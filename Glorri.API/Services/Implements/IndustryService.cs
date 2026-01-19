using Glorri.API.DTOs.Industry;
using Glorri.API.Models;
using Glorri.API.Repositories.Interfaces;
using Glorri.API.Services.Interfaces;

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
    }
}
