using System.Text.Json;
using System.Text.Json.Serialization;

namespace Customize.Domain.Extensions
{
    public static class JsonExtensions
    {
        private static JsonSerializerOptions Options()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() }
            };

            return options;
        }


        public static string Serialize(this object @object)
        {
            return JsonSerializer.Serialize(@object, Options());
        }

        public static T? Deserialize<T>(this string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString, Options());
        }
    }
}
