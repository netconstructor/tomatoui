namespace TomatoBandwidth
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Helper methods for parsers.
    /// </summary>
    internal static class ParserHelper
    {
        private static char[] invalidCharacters = new char[] { '\n', '\t', '\r', ' ' };

        /// <summary>
        /// Finds the start of the bandwidth value array.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="arrayName">
        /// The array name.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        /// <returns>
        /// Start position.
        /// </returns>
        public static int FindArrayStart(string page, string arrayName, string errorMessage)
        {
            var start = page.IndexOf(arrayName, StringComparison.Ordinal);
            if (-1 == start)
            {
                throw new ParseException(errorMessage, page);
            }

            return start;
        }

        /// <summary>
        /// Finds the end position of the bandwidth array.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="endTag">
        /// The end tag.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>
        /// <returns>
        /// The end position.
        /// </returns>
        public static int FindArrayEnd(string page, int start, string endTag, string errorMessage)
        {
            var end = page.IndexOf(endTag, start, StringComparison.Ordinal);
            if (-1 == end)
            {
                throw new ParseException(errorMessage, page);
            }

            return end;
        }

        /// <summary>
        /// Removes invalid characters around the raw value.
        /// </summary>
        /// <param name="rawValue">
        /// The raw value.
        /// </param>
        /// <returns>
        /// Trimmed value.
        /// </returns>
        public static string TrimInvalidCharacters(string rawValue)
        {
            return rawValue.Trim(invalidCharacters);
        }

        /// <summary>
        /// Converts the non trimmed hex value into kilobytes.
        /// </summary>
        /// <param name="rawValue">
        /// The raw value.
        /// </param>
        /// <returns>
        /// Number of kilobytes.
        /// </returns>
        public static uint ToKilobytes(string rawValue)
        {
            return Convert.ToUInt32(ParserHelper.TrimInvalidCharacters(rawValue), 16);
        }

        /// <summary>
        /// Converts daily bandwidth timestamp into DateTime.
        /// </summary>
        /// <param name="rawValue">
        /// The raw value.
        /// </param>
        /// <returns>
        /// DateTime with correct year, month and day.
        /// </returns>
        public static DateTime DailyTimestampToDate(string rawValue)
        {
            var tempTime = Convert.ToInt32(ParserHelper.TrimInvalidCharacters(rawValue), 16);
            var year = ((tempTime >> 16) & 0xFF) + 1900;
            var month = (Common.LogicalRightShift(tempTime, 8) & 0xFF) + 1;
            var day = tempTime & 0xFF;

            return new DateTime(year, month, day);
        }
    }
}
