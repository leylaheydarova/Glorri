namespace Glorri.API.DTOs.AppUser
{
    public class AppUserUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public IFormFile? Image { get; set; }
    }
}
