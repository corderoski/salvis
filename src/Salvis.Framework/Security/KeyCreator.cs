using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Salvis.Framework.Security
{

    public struct Key
    {
        public String Value;
        public Object Param;
    }

    public class KeyCreator
    {

        /// <summary>
        /// Indicates chars that must be available.
        /// </summary>
        public const String INVALID_CHARS_REGEX = "([^a-zA-Z0-9])+";

        /// <summary>
        /// A String to replace for.
        /// </summary>
        private const String ReplaceFor = "";

        /// <summary>
        /// Specifies the value of a disabled limit.
        /// </summary>
        private const Int32 CharLimitDisableValue = -1;


        public static String RequestKey(String value, int limit = CharLimitDisableValue)
        {
            return RequestKey(value, limit, null, false);
        }

        /// <summary>
        /// Requests an crypted in-time value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="limit"></param>
        /// <param name="date">Time to encrypt, if null, use the server's UTC</param>
        /// <returns></returns>
        public static String RequestTimedKey(String value, int limit = CharLimitDisableValue, DateTime? date = null)
        {
            return RequestKey(value, CharLimitDisableValue, date.HasValue ? date.Value : DateTimeOffset.Now.DateTime, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">the value to encrypt.</param>
        /// <param name="limit"></param>
        /// <param name="date">A DateTime to preserve, could be a Token's lifetime.</param>
        /// <param name="excludeInvalidChars"></param>
        /// <returns></returns>
        public static String RequestKey(String value = "", int limit = CharLimitDisableValue, DateTime? date = null, bool excludeInvalidChars = false)
        {
            string key = null;

            if (date.HasValue)
            {
                var ticks = date.Value.Ticks + "";
                key = Crypto.ToCrypt(value + "$" + ticks);                  //  encrypt Value + Time. Order affects the result.
            }
            else if (!String.IsNullOrEmpty(value))
            {
                key = Crypto.ToCrypt(value);         //  Value
            }
            else //if (String.IsNullOrEmpty(value))
            {
                key = Crypto.ToCrypt(Guid.NewGuid().ToString());         //  encrypt new ID
            }

            if (excludeInvalidChars)
            {
                var regex = new Regex(INVALID_CHARS_REGEX);
                key = regex.Replace(key, ReplaceFor);                    //  exclude chars
            }

            if (limit > CharLimitDisableValue)
            {
                if (key.Length > limit)
                    key = key.Substring(0, limit);
            }

            return key;
        }

        public static Key RequestValue(String key)
        {
            var crypto = Crypto.ToDecrypt(key);
            var pos = crypto.Split('$');
            var _key = new Key();
            _key.Value = pos.First();
            _key.Param = pos.Last();
            return _key;
        }

        public static Key RequestTimedValue(String key)
        {
            var crypto = Crypto.ToDecrypt(key);
            var pos = crypto.Split('$');
            var _key = new Key();
            _key.Param = new DateTime(Convert.ToInt64(pos[0]));
            _key.Value = pos[1];

            return _key;
        }

    }
}
