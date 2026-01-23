using Glorri.API.DTOs.AppUser;
using Glorri.API.Extensions;
using Glorri.API.Models;
using Glorri.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Glorri.API.Services.Implements
{
    public class AccountService : IAccoutService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IWebHostEnvironment _environment;
        readonly IHttpContextAccessor _accessor;
        public AccountService(UserManager<AppUser> userManager, IWebHostEnvironment environment, IHttpContextAccessor accessor)
        {
            _userManager = userManager;
            _environment = environment;
            _accessor = accessor;
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
            user.ImageName = dto.Image != null ? dto.Image.UploadFile(_environment.WebRootPath, "assets/images/appusers") : "defaultimage.jpg";
            user.ImageUrl = $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/assets/images/appusers/{user.ImageName}";

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) throw new Exception("Register failed!");

            await _userManager.AddToRoleAsync(user, "Client");
            return $"{user.UserName} registered successsfully!";
        }
    }
}
