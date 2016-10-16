using Newtonsoft.Json;

namespace Salvis.Framework.Helpers
{
    public class JsonHelper
    {
        public static T Deserializer<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, JsonSettingsDefault());
        }

        public static string Serializer<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSettingsDefault());
        }

        private static JsonSerializerSettings JsonSettingsDefault()
        {
            return new JsonSerializerSettings
            {
                DateParseHandling = DateParseHandling.DateTime,
                DefaultValueHandling = DefaultValueHandling.Populate,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                StringEscapeHandling = StringEscapeHandling.Default

            };
        }
    }
}