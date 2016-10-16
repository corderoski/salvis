using Salvis.Framework.OnlineServices.Entities;
using System.Collections.Generic;

namespace Salvis.Framework.OnlineServices
{
    public interface IOnlineService
    {

        IEnumerable<object> GetContentTypes();

        IEnumerable<IProviderResult> GetContentByType(string typeId, string seekPattern);

    }

    public abstract class OnlineService
    {

        internal const string ONLINE_SERVICE_CONTENT_COUNT = "OnlineService_ContentCount";

        internal const string ONLINE_SERVICE_TEXT_MAX_LENGTH = "OnlineService_TextMaxLength";

    }

}
