using Microsoft.AspNetCore.Identity;

namespace Christmas.Secret.Gifter.API.Services.Abstractions
{
    public interface IAuthorizeService
    {
        string GetToken(IdentityUser user);
    }
}