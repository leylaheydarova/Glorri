using Microsoft.AspNetCore.Identity;

namespace Glorri.API.Models
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
