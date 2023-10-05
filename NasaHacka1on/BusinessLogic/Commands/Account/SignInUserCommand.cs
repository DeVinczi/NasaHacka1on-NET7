using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;
using NasaHacka1on.BusinessLogic.Commands.Account.Messages;
using NasaHacka1on.BusinessLogic.Commands.Account.ValidationRules;
using NasaHacka1on.Cqrs;
using NasaHacka1on.Database;
using NasaHacka1on.Models.Models;

namespace NasaHacka1on.BusinessLogic.Commands.Account;

public class SignInUserCommand : ICommand
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}

internal class SignInUserCommandHandler : ICommandHandler<SignInUserCommand>
{
    private readonly IValidator _validator;
    private readonly ISignInManager _signInManager;
    private readonly CommunityCodeHubDataContext _dataContext;

    public SignInUserCommandHandler(
        IValidator validator,
        ISignInManager signInManager,
        CommunityCodeHubDataContext dataContext)
    {
        _validator = validator;
        _signInManager = signInManager;
        _dataContext = dataContext;
    }

    public async Task<Gybs.IResult> HandleAsync(SignInUserCommand command)
    {
        var validationResult = await IsValidAsync(command);

        if (!validationResult.HasSucceeded)
        {
            return Result.Failure(validationResult.Errors);
        }

        var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == command.Email);

        if (user is null)
        {
            return Result.Failure(ResultErrorKeys.InvalidParameter, AccountResultErrorMessages.UserNotExistOrInvalidPassword);
        }

        var signedInUser = new SignedInUser
        {
            Username = user?.UserName ?? string.Empty,
            Password = command.Password,
            RememberMe = command.RememberMe,
            ShouldLockOutOnFailure = true,
        };

        var result = HandleSignInResult(await _signInManager.PasswordSignInAsync(signedInUser));

        if (!result.HasSucceeded)
        {
            return result;
        }

        return Result.Success();
    }

    private async Task<Gybs.IResult> IsValidAsync(SignInUserCommand command)
    {
        return await _validator
            .Require<EmailAddressIsValidRule>()
                .WithData(command.Email)
            .Require<UserExistsByEmailRule>()
                .WithData(command.Email)
            .ValidateAsync();
    }

    private static Gybs.IResult HandleSignInResult(SignInResultType result)
    {
        return result switch
        {
            SignInResultType.Succeeded => Result.Success(),
            SignInResultType.Unknown
            or SignInResultType.IsLockedOut
            or SignInResultType.IsNotAllowed
            or SignInResultType.RequiresTwoFactor => Result.Failure(ResultErrorKeys.InvalidParameter, AccountResultErrorMessages.UserNotExistOrInvalidPassword),
            _ => throw new ArgumentOutOfRangeException(nameof(result), result, null),
        };
    }
}