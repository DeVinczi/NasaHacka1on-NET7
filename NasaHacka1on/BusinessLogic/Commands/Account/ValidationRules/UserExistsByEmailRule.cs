using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;
using NasaHacka1on.Cqrs;
using NasaHacka1on.Database;

namespace NasaHacka1on.BusinessLogic.Commands.Account.ValidationRules;

internal class UserExistsByEmailRule : IValidationRule<string>
{
    public const string UserDoesNotExistOrInvalidPassword = "user-does-not-exist-or-invalid-password";

    private readonly CommunityCodeHubDataContext _dataContext;

    public UserExistsByEmailRule(CommunityCodeHubDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Gybs.IResult> ValidateAsync(string email)
    {
        //TO DO: Jak dodam wysylanie emaili to tutaj dodac && x.ConfirmedEmail
        var exists = await _dataContext.Users.AnyAsync(x => x.Email == email);

        if (!exists)
        {
            return Result.Failure(ResultErrorKeys.InvalidParameter, UserDoesNotExistOrInvalidPassword);
        }

        return Result.Success();
    }
}
