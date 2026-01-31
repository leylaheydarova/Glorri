using Glorri.API.DTOs.AppUser;
using Glorri.API.Exceptions;
using Glorri.API.Extensions;
using Glorri.API.Models;
using Glorri.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Glorri.API.Services.Implements
{
    public class AccountService : IAccoutService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IWebHostEnvironment _environment;
        readonly IHttpContextAccessor _accessor;
        readonly IEmailService _emailService;
        public AccountService(UserManager<AppUser> userManager, IWebHostEnvironment environment, IHttpContextAccessor accessor, IEmailService emailService)
        {
            _userManager = userManager;
            _environment = environment;
            _accessor = accessor;
            _emailService = emailService;
        }

        public async Task<string> RegisterAsync(RegistgerDto dto)
        {
            var user = new AppUser()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                UserName = dto.Username
            };

            //daha uzun versiya
            //if(dto.Image != null) 
            //{
            //    user.ImageName = dto.Image.UploadFile(_environment.WebRootPath, "assets/images/appusers");
            //}

            //else
            //{
            //    user.ImageName = "defaultimage.jpg";
            //}

            //daha qisa versiya
            user.ImageName = dto.Image != null ? dto.Image.UploadFile(_environment.WebRootPath, "assets/images/appusers") : "defaultimage.png";
            user.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/assets/images/appusers/{user.ImageName}";

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) throw new Exception("Register failed!");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var link = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/api/Accounts/verify-email?userId={user.Id}&token={encoded}";

            await _userManager.AddToRoleAsync(user, "Client");
            var messageBody = $@"
<h3>Welcome 🎉</h3>
<p>Please verify your email:</p>
<a href=""{link}"">Verify Email</a>
";
            await _emailService.SendEmailAsync(user.Email, "Verify Account", messageBody);

            return $"{user.UserName} registered successsfully!";
        }


        public async Task VerifyEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new NotFoundException("user");
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken.ToString());

        }

        public async Task<string> RemoveAccountAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpireDate > DateTime.UtcNow.AddHours(4));
            if (user == null) throw new RefreshTokenException();
            if (!user.ImageName.Contains("defaultimage.png"))
            {
                var path = $"{_environment.WebRootPath}/assets/images/appusers/{user.ImageName}";
                if (File.Exists(path)) File.Delete(path);
            }
            await _userManager.DeleteAsync(user);
            return "User account was removed successfully!";
        }

        public async Task<string> UpdateAsync(string refreshToken, AppUserUpdateDto dto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpireDate > DateTime.UtcNow.AddHours(4));
            if (user == null) throw new RefreshTokenException();
            user.FirstName = dto.FirstName != null ? dto.FirstName : user.FirstName;
            user.LastName = dto.LastName != null ? dto.LastName : user.LastName;
            if (dto.Image != null)
            {
                if (!user.ImageName.Contains("defaultimage.png"))
                {
                    var path = $"{_environment.WebRootPath}/assets/images/appusers/{user.ImageName}";
                    if (File.Exists(path)) File.Delete(path);
                }

                user.ImageName = dto.Image.UploadFile(_environment.WebRootPath, "assets/images/appusers");
                user.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/assets/images/appusers/{user.ImageName}";
            }
            await _userManager.UpdateAsync(user);
            return $"{user.UserName} updated account successfully!";
        }

        public async Task<List<AppUserGetDto>> GetAllAsync()
        {
            var query = await _userManager.Users.ToListAsync();
            var dtos = query.Select(user => new AppUserGetDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName
            }).ToList();
            return dtos;
        }

        public async Task<AppUserGetDto> GetSingleAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) throw new NotFoundException("user");
            var roles = await _userManager.GetRolesAsync(user);
            var dto = new AppUserGetDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName,
                Roles = roles
            };
            return dto;
        }
    }
}
