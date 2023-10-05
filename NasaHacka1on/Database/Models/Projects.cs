
namespace NasaHacka1on.Database.Models;

public class Projects : IIdentifiable, IDeletable
{
    public Guid Id { get; set; }
    public DateTimeOffset? DeletedOnUtc { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
