
namespace NasaHacka1on.Database.Models;

public class Projects : IIdentifiable, IDeletable
{
    public Guid Id { get; set; }
    public DateTimeOffset? DeletedOnUtc { get; set; }
    public string Name { get; set; }
    public string GithubLink { get; set; }
    public string WebsiteLink { get; set; }

    public Guid ProjectInfoId { get; set; }
    public virtual ProjectInfo ProjectInfo { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
