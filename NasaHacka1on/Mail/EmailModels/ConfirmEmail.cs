namespace NasaHacka1on.Mail.EmailModels;

public sealed class ConfirmEmail
{
    public string Title { get; set; }
    public string Email { get; set; }
    public string ConfirmEmailAddressUrl { get; set; }

}
