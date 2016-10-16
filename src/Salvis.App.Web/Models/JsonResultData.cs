
using System.Net;

namespace  Salvis.App.Web.Models
{

    /// <summary>
    /// 
    /// </summary>
    public class JsonResultData
    {
        public dynamic Data { get; set; }

        public MessageBox Message { get; set; }

        public HttpStatusCode Code { get; set; }
    }

}