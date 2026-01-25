using Glorri.API.DTOs.Auth;

namespace Glorri.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> LoginAsync(LoginDto dto);
    }
}
