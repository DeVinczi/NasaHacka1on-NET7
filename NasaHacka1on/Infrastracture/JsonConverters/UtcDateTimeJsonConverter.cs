using System.Text.Json;
using System.Text.Json.Serialization;

namespace NasaHacka1on.Models.JsonConverters;

public class UtcDateTimeJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return SwitchToUtcTime(reader.GetDateTime());
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(SwitchToUtcTime(value));
    }

    private static DateTime SwitchToUtcTime(DateTime value)
    {
        var dateTime = value.Kind switch
        {
            DateTimeKind.Utc => value,
            DateTimeKind.Local => value.ToUniversalTime(),
            _ or DateTimeKind.Unspecified => new DateTime(value.Ticks, DateTimeKind.Utc)
        };

        return dateTime;
    }
}
