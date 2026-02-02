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
        readonly IEmailService _emailService;
        readonly IHttpContextAccessor _accessor;
        readonly IOtpService _otpService;

        public AuthService(SignInManager<AppUser> signIngManager, UserManager<AppUser> userManager, ITokenService tokenService, IEmailService emailService, IHttpContextAccessor accessor, IOtpService service)
        {
            _signInManager = signIngManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _accessor = accessor;
            _otpService = service;
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

        public async Task LogoutAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpireDate > DateTime.UtcNow);
            if (user == null) throw new RefreshTokenException();

            user.RefreshToken = null;
            user.RefreshTokenExpireDate = null;
            await _userManager.UpdateAsync(user);
        }

        public async Task ForgotPasswordAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new NotFoundException("user");
            var otp = _otpService.GenerateOtpCode(username);

            await _emailService.SendEmailAsync(user.Email, "Reset password", $"Please, your otp code is: {otp} It is in use for 5 minutes!");
        }

        public async Task<string> ResetPasswordAsync(ForgetPasswordDto dto)
        {
            if (dto.Password != dto.ConfirmPassword) throw new Exception("Passwords do not match!");
            var user = await _userManager.FindByNameAsync(dto.Username);
            if (user == null) throw new NotFoundException("user");
            var result = _otpService.VerifyOtp(dto.Username, dto.OtpCode);
            if (!result) throw new Exception("Otp is not correct or expired!");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, dto.Password);
            return "Password reset!";
        }
    }
}
