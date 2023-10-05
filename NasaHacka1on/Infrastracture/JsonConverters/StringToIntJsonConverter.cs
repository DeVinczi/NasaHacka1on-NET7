using System.Buffers;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NasaHacka1on.Models.JsonConverters;

public sealed class StringToIntJsonConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.String)
        {
            var span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

            if (Utf8Parser.TryParse(span, out int number, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return number;
            }

            if (int.TryParse(reader.GetString(), out number))
            {
                return number;
            }
        }
        else if (reader.TryGetInt32(out var number))
        {
            return number;
        }

        return 0;
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}
