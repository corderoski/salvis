using Salvis.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.Framework.Net
{
    public class Requester
    {

        public static String MakeHttpRequest(string apiUrl, IDictionary<string, string> parameters = null, string method = "GET")
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();
            var param = UrlHelper.SerializeDictionary(parameters);
            
            var validUrl = String.Format("{0}{1}", apiUrl, param);

            var request = HttpWebRequest.CreateHttp(validUrl);

            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Method = method;

            if (method.Equals("POST", StringComparison.InvariantCultureIgnoreCase))
            {
                var encoder = new ASCIIEncoding();
                byte[] data = encoder.GetBytes("");

                request.ContentLength = data.Length;
                request.GetRequestStream().Write(data, 0, data.Length);
            }

            string contentResponse = null;
            try
            {
                using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    contentResponse = reader.ReadToEnd();
                }
            }
            catch (WebException)
            {
                contentResponse = "HttpRequest resulted in error.";
            }

            return contentResponse;
        }

    }
}
