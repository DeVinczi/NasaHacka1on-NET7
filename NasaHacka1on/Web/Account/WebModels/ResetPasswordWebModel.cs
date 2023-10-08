namespace NasaHacka1on.Web.Account.WebModels;

public class ResetPasswordWebModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Token { get; set; }
    public Guid User { get; set; }
}
