using NasaHacka1on.Database.Enums;

namespace NasaHacka1on.Database.Models;

public class User : IIdentifiable, IDeletable
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTimeOffset? BirthdayDay { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeletedOnUtc { get; set; }
    public string Avatar { get; set; }
    public string BIO { get; set; }

    public Guid? UserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }

    public string[] Interests { get; set; }

    public virtual ICollection<Projects> Projects { get; set; }
}
