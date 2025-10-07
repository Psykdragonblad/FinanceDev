using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace FinanceDev.API.Helpers
{
    public class DateTimeConverterUsingDateTimeParse : JsonConverter<DateTime>
    {
        private readonly string _format;

        public DateTimeConverterUsingDateTimeParse(string format = "dd/MM/yyyy")
        {
            _format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (DateTime.TryParseExact(reader.GetString(), _format, new CultureInfo("pt-BR"), DateTimeStyles.None, out var date))
            {
                return date;
            }

            // fallback
            return DateTime.Parse(reader.GetString(), new CultureInfo("pt-BR"));
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }    
}
