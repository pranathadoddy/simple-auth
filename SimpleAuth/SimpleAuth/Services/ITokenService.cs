using Microsoft.IdentityModel.Tokens;
using SimpleAuth.Models;
using System.Security.Claims;

namespace SimpleAuth.Services
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
