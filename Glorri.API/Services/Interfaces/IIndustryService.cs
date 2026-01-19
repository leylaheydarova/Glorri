using Glorri.API.DTOs.Industry;

namespace Glorri.API.Services.Interfaces
{
    public interface IIndustryService
    {
        Task<string> CreateAsync(IndustryCreateDto dto);
    }
}
