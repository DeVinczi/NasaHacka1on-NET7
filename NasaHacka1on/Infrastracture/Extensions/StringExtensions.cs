namespace NasaHacka1on.Models.Extensions;

internal static class StringExtensions
{
    public static string ToFirstCharLower(this string str)
    {
        if (str?.Length > 0)
        {
            var letters = str.ToCharArray();
            letters[0] = char.ToLowerInvariant(str[0]);

            return new string(letters);
        }

        return str;
    }
}
