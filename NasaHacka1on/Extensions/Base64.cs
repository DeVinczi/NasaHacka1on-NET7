using System.Text;

namespace NasaHacka1on.Extensions;

public static class Base64
{
    public static string ToBase64String(this string text)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
    }

    public static string FromBase64String(this string base64)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
    }
}
