using NasaHacka1on.Infrastracture.Services;
using System.Security.Claims;

namespace NasaHacka1on.Infrastracture.Authentication;

internal interface IClaimsIdentityFactory
{
    Task<ClaimsIdentity> CreateAsync(string claim);
}

internal class ClaimsIdentityFactory : IClaimsIdentityFactory
{
    private readonly IAccountService _accountService;

    public ClaimsIdentityFactory(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<ClaimsIdentity> CreateAsync(string claim)
    {
        var user = _accountService.GetUserByEmail(claim);
        var claimsIdentity = new ClaimsIdentity();

        if (user is null)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.AuthenticationMethod, "OAuth"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, claim));
            return claimsIdentity;
        }

        claimsIdentity.AddClaim(new Claim("sub", user.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.AuthenticationMethod, "Internal"));

        return claimsIdentity;
    }
}