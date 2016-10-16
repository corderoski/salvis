using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salvis.Framework.Services;

namespace Salvis.Framework.OnlineServices.Factory
{

    public class OnlineServiceFactory : IOnlineServiceFactory
    {

        public IOnlineService Create(OnlineProvider provider)
        {
            switch (provider)
            {
                case OnlineProvider.iTunes:
                    return new AppleITunesOnlineService();
                case OnlineProvider.Other:
                    throw new NotImplementedException(String.Format("The {0} provider is not available.", provider));
                default:
                    throw new ArgumentOutOfRangeException("provider");
            }
        }
    }

}
