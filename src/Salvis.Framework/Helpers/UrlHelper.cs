using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.Framework.Helpers
{
    public class UrlHelper
    {
        public static string SerializeDictionary(IEnumerable<KeyValuePair<string, string>> dictionary)
        {
            var parameters = new StringBuilder();
            parameters.Append("?");
            foreach (var keyValuePair in dictionary)
            {
                parameters.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }
            return parameters.Remove(parameters.Length - 1, 1).ToString();
        }

        public static string SerializeDictionary(IEnumerable<KeyValuePair<string, string>> dictionary, string key, string value)
        {
            var parameters = new StringBuilder();
            parameters.Append("?");
            foreach (var keyValuePair in dictionary)
            {
                parameters.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }
            parameters.Append(String.Format("{0}={1}", key, value));
            return parameters.ToString();
        }
    }
}
