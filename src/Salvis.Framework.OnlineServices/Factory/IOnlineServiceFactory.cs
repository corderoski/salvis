using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salvis.Framework.OnlineServices.Factory
{

    public enum OnlineProvider
    {
        None = 0,
        iTunes = 1,
        Spreaker = 2,
        Other = 9
    }

    public interface IOnlineServiceFactory
    {

        IOnlineService Create(OnlineProvider provider);

    }


}
