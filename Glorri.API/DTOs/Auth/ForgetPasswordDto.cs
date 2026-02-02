using System.ComponentModel.DataAnnotations;

namespace Glorri.API.DTOs.Auth
{
    public record ForgetPasswordDto
    {
        [Required]
        public string Username { get; set; }

        [Range(1000, 9999)]
        public int OtpCode { get; set; }

        [MinLength(8), Required]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
