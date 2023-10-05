using Microsoft.AspNetCore.Identity;

namespace NasaHacka1on.Database.Models;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string DisplayName { get; init; }
}
