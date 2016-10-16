using System;
using Salvis.Framework.Helpers;

namespace Salvis.Framework.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public int MessagesMaxUnreadShowing
        {
            get
            {
                return 6;
            }
        }

        public int TruncatedTextMaxLength
        {
            get
            {
                return ConfigurationHelper.GetSetting<int>("textMaxLenght");
            }
        }

    }
}
