using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.AspNetCore.Identity;
using NasaHacka1on.BusinessLogic.Commands.Account.Messages;
using NasaHacka1on.BusinessLogic.Commands.Account.ValidationRules;
using NasaHacka1on.BusinessLogic.Providers;
using NasaHacka1on.Cqrs;
using NasaHacka1on.Database;
using NasaHacka1on.Database.Models;
using NasaHacka1on.Models.Models;

namespace NasaHacka1on.BusinessLogic.Commands.Account;

public class SignUpUserCommand : ICommand
{
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
internal class SignUpUserCommandHandler : ICommandHandler<SignUpUserCommand>
{
    private readonly IConfiguration _configuration;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<SignUpUserCommandHandler> _logger;
    private readonly IUserManager _userManager;
    private readonly IValidator _validator;
    private readonly CommunityCodeHubDataContext _dataContext;

    public SignUpUserCommandHandler(
        IConfiguration configuration,
        IDateTimeProvider dateTimeProvider,
        ILogger<SignUpUserCommandHandler> logger,
        IUserManager userManager,
        IValidator validator,
        CommunityCodeHubDataContext dataContext)
    {
        _configuration = configuration;
        _dataContext = dataContext;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<Gybs.IResult> HandleAsync(SignUpUserCommand command)
    {
        var validationResult = await IsValidAsync(command);

        if (!validationResult.HasSucceeded)
        {
            return Result.Failure(validationResult.Errors);
        }

        var user = new ApplicationUser
        {
            UserName = command.DisplayName,
            Email = command.Email,
            TwoFactorEnabled = false,
            Id = Guid.NewGuid()
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            return HandleSignUpResult(result);
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        await _dataContext.SaveChangesAsync();

        return Result.Success();
    }

    private async Task<Gybs.IResult> IsValidAsync(SignUpUserCommand command)
    {
        return await _validator
            .Require<EmailExistsInDatabaseRule>()
                .WithOptions(o => o.StopIfFailed())
                .WithData(command.Email)
            .Require<EmailAddressIsValidRule>()
                .WithOptions(o => o.StopIfFailed())
                .WithData(command.Email)
            .Require<PasswordIsValidRule>()
                .WithOptions(o => o.StopIfFailed())
                .WithData(command.Password)
            .Require<SignedUpUserIsValidRule>()
                .WithOptions(o => o.StopIfFailed())
                .WithData(command.DisplayName)
            .ValidateAsync();
    }

    private Gybs.IResult HandleSignUpResult(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            if (result.Errors.Any(x => x.Code == AccountResultErrorMessages.DuplicateEmail))
            {
                return Result.Success();
            }
            else
            {
                _logger.LogInformation("Errors: {errors}", string.Join("; ", result.Errors));

                return Result.Failure(ResultErrorKeys.Conflict, AccountResultErrorMessages.DuplicateUsername);
            }
        }

        return Result.Success();
    }
}