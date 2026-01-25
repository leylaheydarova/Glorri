using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Glorri.API.DTOs.Auth
{
    public class LoginDto
    {
        [Required, MinLength(7)]
        public string Username { get; set; }
        [PasswordPropertyText, Required, MinLength(8)]
        public string Password { get; set; }
    }
}
