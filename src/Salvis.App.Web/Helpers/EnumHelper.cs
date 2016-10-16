using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Authentication;
using Resources;
using Salvis.Resources;

namespace Salvis.App.Web.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// This method translates the name of Enum to the corresponding Name on resource.
        /// </summary>
        /// <param name="param">The enum.</param>
        /// <returns>Name of enum translated</returns>
        public static string Translate(Enum param)
        {
            
            return Texts.ResourceManager.GetString(param.ToString(), CultureInfo.CurrentCulture);
        }


        /// <summary>
        /// Take of enumeration of enum to convert to List of KeyValuesPair.
        /// </summary>
        /// <param name="type">Enum's type.</param>
        /// <param name="translate">Specific if translate the value of enum based on resources.</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, string>> ToList(Type type, bool translate = true)
        {
            return Enum.GetValues(type).Cast<Enum>().Select(p => new KeyValuePair<int, string>(Convert.ToInt32(p), (translate) ? Translate(p) : p.ToString())).ToList();
        }

    }
}