namespace NasaHacka1on.BusinessLogic.Providers;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow { get; }

    public DateTimeProvider()
    {
        UtcNow = DateTimeOffset.UtcNow;
    }
}
