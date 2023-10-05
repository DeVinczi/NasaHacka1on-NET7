namespace NasaHacka1on.Models.Models;

public sealed class SignedInUser
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public bool ShouldLockOutOnFailure { get; set; }
}
