using Glorri.API.DTOs.Company;

namespace Glorri.API.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<string> CreateAsync(CompanyCreateDto dto);
        Task<string> RemoveAsync(int id);
        Task<List<CompanyGetDto>> GetAllAsync();
    }
}
