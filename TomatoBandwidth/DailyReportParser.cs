namespace TomatoBandwidth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Used to parse the daily bandwidth information.
    /// </summary>
    internal class DailyReportParser
    {
        private static readonly Regex dailyHistoryRegex = new Regex(
            "(?<=daily_history(?:\\s*)=(?:\\s*)\\[(?:\\s*)(?:(?:\\r\\n)" +
            "*)(?:\\s*))(.|\\n)*(?=(?:\\s*)\\];)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        private static readonly Regex singleItem = new Regex(
              "((?<=\\[)([^\\[\\]])*)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        /// <summary>
        /// Parses the daily bandwidth values from the page.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <returns>
        /// Collection of daily bandwidth values.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Public instance method.")]
        public List<DailyBandwidth> Parse(string page)
        {
            return ParseFromPage(page);
        }

        private static List<DailyBandwidth> ParseFromPage(string page)
        {
            var dailyBandwidths = new List<DailyBandwidth>();

            var monthlyHistoryArray = GetBandwidthArray(page);
            if (string.IsNullOrEmpty(monthlyHistoryArray))
            {
                // Most likely empty array
                return dailyBandwidths;
            }

            // If the match was succesfull the value looks like this
            // [0x6f021f,0x80257c,0x40581],[0x6f0301,0x4ece6b,0x32c98],[0x6f0302,0x4ceb17c,0x26d2c7]...
            // Divide it into parts where each part contains values which are between [ and ].
            var matches = singleItem.Matches(monthlyHistoryArray);
            if (0 == matches.Count)
            {
                var msg = "The monthly bandwidth array inside the page does not contain valid bandwidth information.";
                msg += " It should contain comma separated items like [0x6f021f,0x80257c,0x40581],[0x6f0301,0x4ece6b,0x32c98],[0x6f0302,0x4ceb17c,0x26d2c7]";
                throw new ParseException(msg, page);
            }

            for (var i = 0; i < matches.Count; i++)
            {
                var value = matches[i].Value.Trim();
                var daily = CreateDailyBandwidth(value, page);
                dailyBandwidths.Add(daily);
            }

            return dailyBandwidths;            
        }

        private static string GetBandwidthArray(string page)
        {
            if (string.IsNullOrEmpty(page))
            {
                var msg = "The page is either null or empty. Cannot parse daily download history";
                throw new ParseException(msg, page);
            }

            //// Using simple IndexOf/Subsstring to find the raw array.
            //// Using only regexp is difficult if we want to support cases
            //// where the array migth contain line feeds, tabs etc. Also
            //// this gives us better error messages.
            var start = ParserHelper.FindArrayStart(page, "daily_history", "Cannot find daily_history from the page.");

            const string EndTag = "];";
            var end = ParserHelper.FindArrayEnd(page, start, EndTag, "Cannot find end tag for daily history array from the page.");

            var rawArray = page.Substring(start, (end - start) + EndTag.Length);

            var match = dailyHistoryRegex.Match(rawArray);
            if (!match.Success)
            {
                var msg = "Cannot find match for daily bandwidth array from the page.";
                throw new ParseException(msg, page);
            }

            return ParserHelper.TrimInvalidCharacters(match.Value);
        }

        private static DailyBandwidth CreateDailyBandwidth(string rawValue, string page)
        {
            var values = rawValue.Split(',').ToList();

            if (3 != values.Count)
            {
                var msg = "Cannot parse daily bandwidth from <{0}> because it does not contain exactly three elements separated by comma."
                    .FormatWith(rawValue);
                throw new ParseException(msg, page);
            }

            try
            {
                var date = ParserHelper.DailyTimestampToDate(values[0]);

                var download = ParserHelper.ToKilobytes(values[1]);
                var upload = ParserHelper.ToKilobytes(values[2]);

                var daily = new DailyBandwidth(date.Year, date.Month, date.Day, download, upload);
                return daily;
            }
            catch (Exception exp)
            {
                throw new ParseException("Failed to parse single daily bandwidth information", page, rawValue, exp);
            }
        }
    }
}
