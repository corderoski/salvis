using System;
using System.Configuration;
using System.Text;

namespace Salvis.Framework.Helpers
{
    public abstract class ConfigurationHelper
    {
        #region -Fields And Properties
        public static readonly Encoding DefaultEncoding = System.Text.Encoding.UTF8;
        #endregion

        public static String GetSetting(string name)
        {
            return GetSetting<String>(name);
        }

        public static T GetSetting<T>(string name)
        {
            var temp = ConfigurationManager.AppSettings[name];
            return (T)(temp == null ? null : Convert.ChangeType(temp, typeof(T)));
        }
        
        public static T GetModuleSetting<T>(string name)
        {
            
            var conf = ConfigurationManager.OpenExeConfiguration(Environment.CurrentDirectory +
                String.Format("\\{0}", typeof(ConfigurationHelper).Module.Name));
            var temp = conf.AppSettings.Settings[name].Value;
            return (T)(temp == null ? null : Convert.ChangeType(temp, typeof(T)));
        }

        public static void SaveValue(String key, String value)
        {
            var conf = ConfigurationManager.OpenExeConfiguration(Environment.CurrentDirectory);
            conf.AppSettings.Settings[key].Value = value;
            conf.Save();
        }

        #region Virtual

        protected virtual Configuration GetConfiguration(string appConfigPath)
        {
            return ConfigurationManager.OpenExeConfiguration(appConfigPath);
        }

        #endregion

    }
}
