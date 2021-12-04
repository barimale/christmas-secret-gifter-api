using Microsoft.AspNetCore.Identity;

namespace Albergue.Administrator.Services
{
    public interface IAuthorizeService
    {
        string GetToken(IdentityUser user);
    }
}