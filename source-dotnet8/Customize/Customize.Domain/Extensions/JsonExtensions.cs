using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Customize.Domain.Extensions
{
    public static class JsonExtensions
    {
        private static JsonSerializerSettings Options()
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var options = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter () },
                ContractResolver = contractResolver 
            };

            return options;
        }


        public static string Serialize(this object @object)
        {
            return  JsonConvert.SerializeObject(@object, Options());
        }

        public static T? Deserialize<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, Options());
        }
    }
}
