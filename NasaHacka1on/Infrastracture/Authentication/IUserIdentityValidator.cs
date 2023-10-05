using Microsoft.Extensions.Options;
using NasaHacka1on.Infrastracture.Extensions;
using System.Security.Claims;

namespace NasaHacka1on.Infrastracture.Authentication;
internal interface IUserIdentityValidator
{
    Task<UserIdentityValidationResult> ValidateAsync(ClaimsPrincipal claimsPrincipal);
}

internal class UserIdentityValidator : IUserIdentityValidator
{
    private readonly IClaimsIdentityFactory _claimsIdentityFactory;
    private readonly OAuthModel _options;

    public UserIdentityValidator(
        IClaimsIdentityFactory claimsIdentityFactory,
        IOptions<OAuthModel> options)
    {
        _claimsIdentityFactory = claimsIdentityFactory;
        _options = options.Value;
    }

    public async Task<UserIdentityValidationResult> ValidateAsync(ClaimsPrincipal claimsPrincipal)
    {
        if (!claimsPrincipal.Identity.IsAuthenticated)
        {
            return UserIdentityValidationResult.Failure();
        }

        var email = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == _options.EmailClaim);

        if (email is null)
        {
            var loginClaim = claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.Name);
            var validClaimsIdentityLogin = await _claimsIdentityFactory.CreateAsync(loginClaim.Value);

            if (!ContainsRequiredClaims(validClaimsIdentityLogin.Claims))
            {
                return UserIdentityValidationResult.Failure();
            }

            if (!CompareClaimsIdentity(claimsPrincipal.Claims, validClaimsIdentityLogin.Claims))
            {
                return UserIdentityValidationResult.Failure(validClaimsIdentityLogin);
            }

            return UserIdentityValidationResult.Success(validClaimsIdentityLogin);
        }

        var validClaimsIdentity = await _claimsIdentityFactory.CreateAsync(email.Value);

        if (!ContainsRequiredClaims(validClaimsIdentity.Claims))
        {
            return UserIdentityValidationResult.Failure();
        }

        if (!CompareClaimsIdentity(claimsPrincipal.Claims, validClaimsIdentity.Claims))
        {
            return UserIdentityValidationResult.Failure(validClaimsIdentity);
        }

        return UserIdentityValidationResult.Success(validClaimsIdentity);
    }

    private static bool CompareClaimsIdentity(IEnumerable<Claim> userClaims, IEnumerable<Claim> validClaims)
    {
        var userClaimsList = userClaims.ToList();
        var validClaimsList = validClaims.ToList();

        if (userClaimsList.GetEmail() == validClaimsList.GetEmail()
            && userClaimsList.GetName() == validClaimsList.GetName()
            && userClaimsList.GetRoles().All(x => validClaimsList.GetRoles().Contains(x))
            && userClaimsList.GetSubject() == validClaimsList.GetSubject())
        {
            return true;
        }

        return false;
    }

    private static bool ContainsRequiredClaims(IEnumerable<Claim> claims)
    {
        var claimsList = claims.ToList();

        if (claimsList.GetEmail() == null
            || claimsList.GetName() == null
            || claimsList.GetSubject() == null)
        {
            return false;
        }

        return true;
    }
}
