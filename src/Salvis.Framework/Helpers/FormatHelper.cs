using System;
using System.Globalization;

namespace Salvis.Framework.Helpers
{
    public class FormatHelper
    {

        public const String APP_INT_CURRENCY = "en-US";

        public const String APP_SPANISH_CURRENCY = "es-MX";

        public const String DATE_FORMAT = "MMM/dd/yyyy";

        public const String DATE_FORMAT_STANDARD = "dd/MMM/yy";

        public const String DATETIME_FULL_FORMAT = "f";

        /// <summary>
        /// Standar format plus two point's decimals.
        /// </summary>
		public const String MONEY_CURRENCY_LETTER = "C2";

		public const String MONEY_CURRENCY_FORMAT = "###,###,###,##0";
        
        public static String GetCurrency(float value)
        {
            return value.ToString(MONEY_CURRENCY_LETTER, CultureInfo.CurrentUICulture);
        }

        public static String GetCurrency(float value, string culture)
        {
            return value.ToString(MONEY_CURRENCY_LETTER, new CultureInfo(culture));
        }

        public static String GetCurrency(double value)
        {
            return value.ToString(MONEY_CURRENCY_LETTER, new CultureInfo(APP_INT_CURRENCY));
        }

        public static String GetDateFormatByCulture(string culture)
        {
            return new CultureInfo(culture).DateTimeFormat.ShortDatePattern;
        }

    }
}
