using Gybs.Logic.Validation;
using Gybs.Results;
using NasaHacka1on.Cqrs;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NasaHacka1on.BusinessLogic.Commands.Account.ValidationRules;

internal class EmailAddressIsValidRule : IValidationRule<string>
{
    public const string EmailAddressIsInvalidError = "email-address-is-invalid-error";

    public Task<Gybs.IResult> ValidateAsync(string email)
    {
        return Task.FromResult(IsValidEmail(email)
            ? Result.Success()
            : Result.Failure(ResultErrorKeys.InvalidParameter, EmailAddressIsInvalidError));
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            static string DomainMapper(Match match)
            {
                var idn = new IdnMapping();

                var domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
}
