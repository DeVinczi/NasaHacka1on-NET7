using System.Security.Claims;

namespace NasaHacka1on.Infrastracture.Authentication;

public class UserIdentityValidationResult
{
    public bool IsValid { get; private set; }
    public ClaimsIdentity ValidClaimsIdentity { get; private set; }

    private UserIdentityValidationResult()
    {
    }

    public static UserIdentityValidationResult Failure(ClaimsIdentity validClaimsIdentity = null)
    {
        return new UserIdentityValidationResult
        {
            IsValid = false,
            ValidClaimsIdentity = validClaimsIdentity
        };
    }

    public static UserIdentityValidationResult Success(ClaimsIdentity validClaimsIdentity)
    {
        return new UserIdentityValidationResult
        {
            IsValid = true,
            ValidClaimsIdentity = validClaimsIdentity
        };
    }
}
