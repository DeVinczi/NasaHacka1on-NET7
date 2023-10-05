using System.Collections.ObjectModel;

namespace NasaHacka1on.BusinessLogic.Extensions;

public static class ValidationExtensions
{
    private static readonly IDictionary<string, IReadOnlyCollection<string>> EmptyReadonlyDictionary = new Dictionary<string, IReadOnlyCollection<string>>(0);

    public static IReadOnlyDictionary<string, IReadOnlyCollection<string>> PrepareDictionary(string errorCode, string reason)
    {
        var errors = new Dictionary<string, List<string>>()
        {
            { errorCode, new List<string> { reason } }
        };

        return errors.ToDictionary(pair => pair.Key, pair => pair.Value as IReadOnlyCollection<string>);
    }

    public static IReadOnlyDictionary<string, IReadOnlyCollection<string>> PrepareReadOnlyDictionary(string errorCode, string reason)
    {
        var errors = new Dictionary<string, List<string>>()
        {
            { errorCode, new List<string> { reason } }
        };

        return errors.ToDictionary(pair => pair.Key, pair => pair.Value as IReadOnlyCollection<string>);
    }

    public static IReadOnlyDictionary<string, IReadOnlyCollection<string>> CreateEmptyDictionary()
    {
        return new ReadOnlyDictionary<string, IReadOnlyCollection<string>>(EmptyReadonlyDictionary);
    }
}
