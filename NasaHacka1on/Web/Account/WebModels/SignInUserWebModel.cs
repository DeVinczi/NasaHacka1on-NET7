namespace NasaHacka1on.Web.Account.WebModels;

public sealed class SignInUserWebModel
{
    public string Email { get; init; }
    public string Password { get; init; }
    public bool RememberMe { get; init; }
}
