using Glorri.API.DTOs.AppUser;

namespace Glorri.API.Services.Interfaces
{
    public interface IAccoutService
    {
        Task<List<AppUserGetDto>> GetAllAsync();
        Task<AppUserGetDto> GetSingleAsync(string username);
        Task<string> RegisterAsync(RegistgerDto dto);
        Task<string> RemoveAccountAsync(string refreshToken);
        Task<string> UpdateAsync(string refreshToken, AppUserUpdateDto dto);
        Task VerifyEmail(string userId, string token);
    }
}
