using Glorri.API.DTOs.Role;

namespace Glorri.API.Services.Interfaces
{
    public interface IRoleService
    {
        Task<string> CreateAsync(RoleCreateDto dto);
        Task<string> RemoveAsync(int id);
        Task<List<RoleGetDto>> GetAllAsync();
        Task<RoleGetDto> GetSingleAsync(int id);
    }
}
