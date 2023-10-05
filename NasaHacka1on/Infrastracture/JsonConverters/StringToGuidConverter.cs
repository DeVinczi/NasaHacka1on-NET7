using System.Buffers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NasaHacka1on.Models.JsonConverters;

public sealed class StringToGuidConverter : JsonConverter<Guid?>
{
    public override Guid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.String)
        {
            var valueSpan = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;

            Span<char> guidCharArray = stackalloc char[valueSpan.Length];

            var utf8Decoder = Encoding.UTF8.GetDecoder();
            _ = utf8Decoder.GetChars(valueSpan, guidCharArray, true);

            guidCharArray.Trim();

            if (guidCharArray.IsEmpty)
            {
                return Guid.Empty;
            }

            if (Guid.TryParse(guidCharArray, out var result))
            {
                return result;
            }
        }

        return reader.GetGuid();
    }

    public override void Write(Utf8JsonWriter writer, Guid? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

