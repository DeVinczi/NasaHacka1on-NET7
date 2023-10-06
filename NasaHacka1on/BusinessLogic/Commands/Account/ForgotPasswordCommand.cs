using Gybs.Logic.Cqrs;

namespace NasaHacka1on.BusinessLogic.Commands.Account;

public class ForgotPasswordCommand : ICommand
{
    public string Email { get; set; }
}

