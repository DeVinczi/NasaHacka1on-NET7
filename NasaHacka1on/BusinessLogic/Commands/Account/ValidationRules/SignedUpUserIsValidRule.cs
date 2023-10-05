using Gybs.Extensions;
using Gybs.Logic.Validation;
using Gybs.Results;
using NasaHacka1on.BusinessLogic.Extensions;
using NasaHacka1on.BusinessLogic.Helpers;
using NasaHacka1on.Cqrs;

namespace NasaHacka1on.BusinessLogic.Commands.Account.ValidationRules;

internal class SignedUpUserIsValidRule : IValidationRule<string>
{
    public const string DisplayNameNotProvided = "display-name-not-provided";
    public const string DisplayNameTooLong = "display-name-too-long";

    public Task<Gybs.IResult> ValidateAsync(string displayName)
    {
        var errors = Validate(displayName);

        return Task.FromResult(errors.Any()
            ? Result.Failure(errors)
            : Result.Success());
    }

    private static IReadOnlyDictionary<string, IReadOnlyCollection<string>> Validate(string displayName)
    {
        if (!displayName.IsPresent())
        {
            return ValidationExtensions.PrepareDictionary(ResultErrorKeys.InvalidParameter, DisplayNameNotProvided);
        }
        else if (displayName.Length > LengthConstraints.DisplayNameMaxLength)
        {
            return ValidationExtensions.PrepareDictionary(ResultErrorKeys.InvalidParameter, DisplayNameTooLong);
        }

        return ValidationExtensions.CreateEmptyDictionary();
    }
}

