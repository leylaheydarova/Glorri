using Glorri.API.DTOs.AppUser;

namespace Glorri.API.Services.Interfaces
{
    public interface IAccoutService
    {
        Task<string> RegisterAsync(RegistgerDto dto);
    }
}
