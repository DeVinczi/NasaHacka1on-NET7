using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.AspNetCore.Identity;
using NasaHacka1on.BusinessLogic.Commands.Account.Messages;
using NasaHacka1on.BusinessLogic.Commands.Account.ValidationRules;
using NasaHacka1on.Cqrs;
using NasaHacka1on.Extensions;
using NasaHacka1on.Models.Models;

namespace NasaHacka1on.BusinessLogic.Commands.Account;

public class ResetPasswordCommand : ICommand
{
    public string Email { get; set; }
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

internal class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly IValidator _validator;
    private readonly IUserManager _userManager;
    private readonly ILogger<ResetPasswordCommandHandler> _logger;

    public ResetPasswordCommandHandler(
        IValidator validator,
        IUserManager userManager,
        ILogger<ResetPasswordCommandHandler> logger)
    {
        _validator = validator;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Gybs.IResult> HandleAsync(ResetPasswordCommand command)
    {
        var validationResult = await IsValidAsync(command);

        if (!validationResult.HasSucceeded)
        {
            return Result.Failure(validationResult.Errors);
        }

        var user = await _userManager.FindByIdAsync(command.UserId.ToString());

        if (user == null || user.Email != command.Email)
        {
            return Result.Failure(ResultErrorKeys.InvalidParameter, AccountResultErrorMessages.ChangePasswordInvalidTokenError);
        }

        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, command.Token.FromBase64String(), command.Password);

        if (!resetPasswordResult.Succeeded)
        {
            return HandleResetPasswordResult(resetPasswordResult);
        }

        return Result.Success();
    }

    private async Task<Gybs.IResult> IsValidAsync(ResetPasswordCommand command)
    {
        return await _validator
            .Require<EmailAddressIsValidRule>()
                .WithOptions(o => o.StopIfFailed())
                .WithData(command.Email)
            //.Require<ResetPasswordIsValidRule>()
            //.WithData(new ResetPasswordIsValidRuleData
            //{
            //    Token = command.Token,
            //    UserId = command.UserId,
            //})
            .Require<PasswordIsValidRule>()
                .WithData(command.Password)
            //.Require<ConfirmPasswordIsValidRule>()
            //    .WithOptions(o => o.StopIfFailed())
            //    .WithData(new ConfirmPasswordIsValidRuleData
            //    {
            //        Password = command.Password,
            //        ConfirmPassword = command.ConfirmPassword,
            //    })
            .ValidateAsync();
    }

    private Gybs.IResult HandleResetPasswordResult(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            if (result.Errors.Any(x => x.Code == AccountResultErrorMessages.InvalidToken))
            {
                return Result.Failure(ResultErrorKeys.Conflict, AccountResultErrorMessages.ChangePasswordInvalidTokenError);
            }
            else
            {
                _logger.LogInformation("Errors: {errors}", string.Join("; ", result.Errors));

                return Result.Failure(ResultErrorKeys.Conflict, AccountResultErrorMessages.UnknownError);
            }
        }

        return Result.Success();
    }
}
