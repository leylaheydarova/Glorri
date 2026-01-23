using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Glorri.API.DTOs.AppUser
{
    public record RegistgerDto
    {
        [MinLength(3, ErrorMessage = "Minimum 3 letters")]
        public string FirstName { get; set; }
        [MinLength(3, ErrorMessage = "Minimum 3 letters")]
        public string LastName { get; set; }
        [Required, MinLength(7)]
        public string Username { get; set; }
        [Required, MinLength(10), MaxLength(15)]
        public string PhoneNumber { get; set; }
        [Required, EmailAddress(ErrorMessage = "Invaid email")]
        public string Email { get; set; }
        [PasswordPropertyText, Required, MinLength(8)]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        public IFormFile? Image { get; set; }
    }
}
//data annotation

//todo: fluent validation