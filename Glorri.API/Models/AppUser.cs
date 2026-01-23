using Microsoft.AspNetCore.Identity;

namespace Glorri.API.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}
