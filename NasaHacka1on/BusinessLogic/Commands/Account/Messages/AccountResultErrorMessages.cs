using Microsoft.AspNetCore.Identity;

namespace NasaHacka1on.BusinessLogic.Commands.Account.Messages;

public static class AccountResultErrorMessages
{
    public const string UnknownError = "unknown-error";
    public const string ConfirmEmailTokenIsInvalid = "confirm-email-token-is-invalid";
    public const string UserNotExistOrInvalidPassword = "user-does-not-exist-or-invalid-password";
    public const string ChangePasswordInvalidTokenError = "change-password-invalid-token";
    public const string DuplicateUsername = "username-exists";

    public const string InvalidToken = nameof(IdentityErrorDescriber.InvalidToken);
    public const string DuplicateEmail = nameof(IdentityErrorDescriber.DuplicateEmail);
}
