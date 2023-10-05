using Gybs.Extensions;
using Gybs.Logic.Validation;
using Gybs.Results;
using NasaHacka1on.BusinessLogic.Extensions;
using NasaHacka1on.Cqrs;

namespace NasaHacka1on.BusinessLogic.Commands.Account.ValidationRules;
internal class PasswordIsValidRule : IValidationRule<string>
{
    public const string PasswordNotProvided = "password-not-provided";
    public const string PasswordIsTooShort = "password-is-too-short";
    public const string PasswordIsTooLong = "password-is-too-long";
    public const string PasswordDoesNotContainNonAlphanumericCharacter = "password-does-not-contain-non-alphanumeric-character";
    public const string PasswordDoesNotContainDigit = "password-does-not-contain-digit";
    public const string PasswordDoesNotContainLetter = "password-does-not-contain-letter";
    public const string PasswordDoesNotContainUppercaseLetter = "password-does-not-contain-uppercase-letter";
    public const string PasswordDoesNotContainLowercaseLetter = "password-does-not-contain-lowercase-letter";

    public Task<Gybs.IResult> ValidateAsync(string password)
    {
        if (!password.IsPresent())
        {
            return Task.FromResult(Result.Failure(ResultErrorKeys.InvalidParameter, PasswordNotProvided));
        }

        if (password.Length < 9)
        {
            return Task.FromResult(Result.Failure(ResultErrorKeys.InvalidParameter, PasswordIsTooShort));
        }

        if (password.Length > 30)
        {
            return Task.FromResult(Result.Failure(ResultErrorKeys.InvalidParameter, PasswordIsTooLong));
        }

        var validationErrors = ValidateCharacters(password);

        if (validationErrors.Any())
        {
            return Task.FromResult(Result.Failure(validationErrors));
        }

        return Task.FromResult(Result.Success());
    }

    private IReadOnlyDictionary<string, IReadOnlyCollection<string>> ValidateCharacters(string password)
    {
        if (!HasNonAlphanumericCharacter(password))
        {
            return ValidationExtensions.PrepareDictionary(ResultErrorKeys.InvalidParameter, PasswordDoesNotContainNonAlphanumericCharacter);
        }

        if (!HasDigit(password))
        {
            return ValidationExtensions.PrepareDictionary(ResultErrorKeys.InvalidParameter, PasswordDoesNotContainDigit);
        }

        if (!HasLetter(password))
        {
            return ValidationExtensions.PrepareDictionary(ResultErrorKeys.InvalidParameter, PasswordDoesNotContainLetter);
        }

        if (!HasUppercase(password))
        {
            return ValidationExtensions.PrepareDictionary(ResultErrorKeys.InvalidParameter, PasswordDoesNotContainUppercaseLetter);
        }

        if (!HasLowercase(password))
        {
            return ValidationExtensions.PrepareDictionary(ResultErrorKeys.InvalidParameter, PasswordDoesNotContainLowercaseLetter);
        }

        return ValidationExtensions.CreateEmptyDictionary();
    }

    private static bool HasNonAlphanumericCharacter(string password)
    {
        return !password.All(char.IsLetterOrDigit);
    }

    private static bool HasDigit(string password)
    {
        return password.Any(char.IsDigit);
    }

    private static bool HasLetter(string password)
    {
        return password.Any(char.IsLetter);
    }

    private static bool HasUppercase(string password)
    {
        return password.Any(char.IsUpper);
    }

    private static bool HasLowercase(string password)
    {
        return password.Any(char.IsLower);
    }
}
