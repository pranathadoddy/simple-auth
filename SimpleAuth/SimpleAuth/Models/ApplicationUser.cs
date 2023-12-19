using Microsoft.AspNetCore.Identity;
using SimpleAuth.Enums;

namespace SimpleAuth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Role Role { get; set; }
    }
}
