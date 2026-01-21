using Glorri.API.DTOs.Company;

namespace Glorri.API.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<string> CreateAsync(CompanyCreateDto dto);
        Task<string> RemoveAsync(int id);
        Task<List<CompanyGetDto>> GetAllAsync();
        Task<CompanyGetDto> GetSingleAsync(int id);
        Task<string> ToggleAsync(int id);
        Task<string> UpdateAsync(int id, CompanyUpdateDto dto);
    }
}
