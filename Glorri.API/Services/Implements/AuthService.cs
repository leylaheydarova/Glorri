using Glorri.API.DTOs.Auth;
using Glorri.API.Models;
using Glorri.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Glorri.API.Services.Implements
{
    public class AuthService : IAuthService
    {
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;
        readonly ITokenService _tokenService;

        public AuthService(SignInManager<AppUser> signIngManager, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _signInManager = signIngManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<TokenDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null)
            {
                throw new Exception("Password or username is not correct");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) throw new Exception("Password or username is not correct"); ;

            var token = _tokenService.GenerateAccessToken(user);

            var tokenDto = new TokenDto()
            {
                AccessToken = token,
                ExpiresIn = DateTime.UtcNow.AddHours(5)
            };
            return tokenDto;
        }
    }
}
