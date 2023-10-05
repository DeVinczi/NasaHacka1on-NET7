namespace NasaHacka1on.Models.Models;

public enum SignInResultType
{
    Unknown = 0,
    Succeeded = 1,
    IsLockedOut = 2,
    IsNotAllowed = 3,
    RequiresTwoFactor = 4,
}
