using System;

namespace Salvis.Framework.Helpers
{
    public class StringHelper
    {

        private const String TRUNCATE_STRING_CHAR = "...";
        private const String MESSAGE_PREFIX_REPLY = "Rv";
        private const String MESSAGE_PREFIX_FORWARD = "Fwd";

        /// <summary>
        /// Truncates a specified string to a fixed limit.
        /// </summary>
        /// <param name="value">string containing the text for truncate.</param>
        /// <param name="sizeLimit">int to limit from.</param>
        /// <returns></returns>
        public static String TruncateString(String value, int sizeLimit)
        {
            return value.Length > sizeLimit ? value.Substring(0, sizeLimit) + TRUNCATE_STRING_CHAR : value;
        }
       

        public static string SetPreFixMessageSubject(string subject, string composeType)
        {
            switch (composeType.ToUpper())
            {
                case "REPLY":
                case "REPLYALL":
                    subject = string.Format("{0}: {1}", MESSAGE_PREFIX_REPLY, subject);
                    break;
                case "FORWARD":
                    subject = string.Format("{0}: {1}", MESSAGE_PREFIX_FORWARD, subject);
                    break;

            }
            return subject;
        }
    }
}
