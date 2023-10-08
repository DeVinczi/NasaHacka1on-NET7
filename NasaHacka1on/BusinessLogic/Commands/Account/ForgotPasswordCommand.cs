using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;
using NasaHacka1on.BusinessLogic.Commands.Account.ValidationRules;
using NasaHacka1on.Cqrs;
using NasaHacka1on.Database;
using NasaHacka1on.Extensions;
using NasaHacka1on.Mail;
using NasaHacka1on.Models.Models;
using NasaHacka1on.Services;

namespace NasaHacka1on.BusinessLogic.Commands.Account;

public class ForgotPasswordCommand : ICommand
{
    public string Email { get; set; }
}

internal class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand>
{
    private readonly IMailService _mailService;
    private readonly CommunityCodeHubDataContext _dataContext;
    private readonly IValidator _validator;
    private readonly IUserManager _userManager;
    private readonly IConfiguration _configuration;

    public ForgotPasswordCommandHandler(
        IValidator validator,
        IUserManager userManager,
        CommunityCodeHubDataContext dataContext,
        IMailService mailService,
        IConfiguration configuration)
    {
        _mailService = mailService;
        _dataContext = dataContext;
        _validator = validator;
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<Gybs.IResult> HandleAsync(ForgotPasswordCommand command)
    {
        var validationResult = await IsValidAsync(command);

        if (!validationResult.HasSucceeded)
        {
            return Result.Failure(validationResult.Errors);
        }

        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user is null)
        {
            return Result.Failure(ResultErrorKeys.InvalidParameter, "Unknown Error");
        }
        var geek = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);


        var emailContent = new MailData()
        {
            EmailToId = command.Email,
            EmailSubject = "Reset your password",
            EmailBody = $"{_configuration["EmailRenderer:Settings:ApplicationUrl"]}/account/reset-password/{user.Id}/{token.ToBase64String()}",
            EmailToName = user.DisplayName
        };

        await _mailService.SendMailAsync(emailContent);

        await _dataContext.SaveChangesAsync();

        return Result.Success();
    }

    private async Task<Gybs.IResult> IsValidAsync(ForgotPasswordCommand command)
    {
        return await _validator
            .Require<EmailAddressIsValidRule>()
                .WithOptions(o => o.StopIfFailed())
                .WithData(command.Email)
            .ValidateAsync();
    }
}


