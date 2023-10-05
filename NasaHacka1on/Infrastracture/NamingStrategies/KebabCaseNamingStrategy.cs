using System.Text.Json;
using System.Text.RegularExpressions;

namespace BackendHackaton.Infrastracture.NamingStrategies;

internal partial class KebabCaseNamingStrategy : JsonNamingPolicy
{
    private static readonly Regex _regex = KebabCase();

    public override string ConvertName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return string.Empty;
        }

        return KebabCase().Replace(name, "-$1").ToLower();
    }

    [GeneratedRegex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled)]
    private static partial Regex KebabCase();
}

