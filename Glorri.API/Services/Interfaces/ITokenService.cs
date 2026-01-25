using Glorri.API.Models;

namespace Glorri.API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(AppUser user);
    }
}
