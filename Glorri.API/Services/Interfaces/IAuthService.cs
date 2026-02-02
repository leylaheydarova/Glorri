using Glorri.API.DTOs.Auth;

namespace Glorri.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task ForgotPasswordAsync(string username);
        Task<TokenDto> LoginAsync(LoginDto dto);
        Task LogoutAsync(string refreshToken);
        Task<string> ResetPasswordAsync(ForgetPasswordDto dto);
    }
}
