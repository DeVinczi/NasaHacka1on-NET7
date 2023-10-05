namespace NasaHacka1on.Database.Models;

public interface IIdentifiable
{
    Guid Id { get; set; }
}

public interface IDeletable
{
    DateTimeOffset? DeletedOnUtc { get; set; }
}

