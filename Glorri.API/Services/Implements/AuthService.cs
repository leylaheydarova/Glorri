using Glorri.API.DTOs.Auth;
using Glorri.API.Exceptions;
using Glorri.API.Models;
using Glorri.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
                throw new LoginException();
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) throw new LoginException(); ;

            var token = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireDate = DateTime.UtcNow.AddDays(7).AddHours(4);
            await _userManager.UpdateAsync(user);
            var tokenDto = new TokenDto()
            {
                AccessToken = token,
                ExpiresIn = DateTime.UtcNow.AddHours(5),
                RefreshToken = refreshToken
            };
            return tokenDto;
        }

        public async Task Logout(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpireDate > DateTime.UtcNow);
            if (user == null) throw new RefreshTokenException();

            user.RefreshToken = null;
            user.RefreshTokenExpireDate = null;
            await _userManager.UpdateAsync(user);
        }
    }
}
