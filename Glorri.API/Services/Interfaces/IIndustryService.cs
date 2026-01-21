using Glorri.API.DTOs.Industry;

namespace Glorri.API.Services.Interfaces
{
    public interface IIndustryService
    {
        Task<string> CreateAsync(IndustryCreateDto dto);
        Task<string> RemoveAsync(int id);
        Task<string> UpdateAsync(int id, IndustryUpdateDto dto);
        Task<List<IndustryGetDto>> GetAllAsync();
        Task<IndustryGetDto> GetSingleAsync(int id);
        Task<string> ToggleAsync(int id);
    }
}
