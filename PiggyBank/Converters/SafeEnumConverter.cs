using System.Text.Json;
using System.Text.Json.Serialization;

namespace PiggyBank.Converters
{
    public class SafeEnumConverter<T> : JsonConverter<T?> where T : struct, Enum
    {
        //Если не можем сериализовать, то выставляем null, а не выбрасываем exception
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if (Enum.TryParse<T>(str, out var result))
            {
                return result;
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}
