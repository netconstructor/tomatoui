namespace TomatoBandwidth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Used to parse monthly bandwidth information.
    /// </summary>
    internal class MonthlyReportParser
    {
        private static readonly Regex monthlyHistoryRegex = new Regex(
            "(?<=monthly_history(?:\\s*)=(?:\\s*)\\[(?:\\s*)(?:(?:\\r\\n)" +
            "*)(?:\\s*))(.|\\n)*(?=(?:\\s*)\\];)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        private static readonly Regex singleItem = new Regex(
              "((?<=\\[)([^\\[\\]])*)",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        /// <summary>
        /// Parses the monthly bandwidth information from the string.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// Monthly bandwidth report.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Public instance method.")]
        public List<MonthlyBandwidth> Parse(string content)
        {            
            return ParseFromPage(content);
        }

        private static List<MonthlyBandwidth> ParseFromPage(string page)
        {
            var monhtlyBandwidthCollection = new List<MonthlyBandwidth>();

            var monthlyHistoryArray = GetBandwidthArray(page);
            if (string.IsNullOrEmpty(monthlyHistoryArray))
            {
                // Most likely empty array
                return monhtlyBandwidthCollection;
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
                var monthly = CreateMonhtlyBandwidth(value, page);
                monhtlyBandwidthCollection.Add(monthly);
            }

            return monhtlyBandwidthCollection;
        }

        private static string GetBandwidthArray(string page)
        {
            if (string.IsNullOrEmpty(page))
            {                
                var msg = "The page is either null or empty. Cannot parse monthly download history";
                throw new ParseException(msg, page);
            }

            //// Using simple IndexOf/Subsstring to find the raw array.
            //// Using only regexp is difficult if we want to support cases
            //// where the array migth contain line feeds, tabs etc. Also
            //// this gives us better error messages.
            var start = ParserHelper.FindArrayStart(page, "monthly_history", "Cannot find monthly_history from the page.");

            const string EndTag = "];";
            var end = ParserHelper.FindArrayEnd(page, start, EndTag, "Cannot find end tag for monthly history array from the page.");

            var rawArray = page.Substring(start, (end - start) + EndTag.Length);

            var match = monthlyHistoryRegex.Match(rawArray);
            if (!match.Success)
            {
                var msg = "Cannot find match for monthly bandwidth array from the page.";
                throw new ParseException(msg, page);
            }

            return ParserHelper.TrimInvalidCharacters(match.Value);
        }

        private static MonthlyBandwidth CreateMonhtlyBandwidth(string rawValue, string page)
        {
            var values = rawValue.Split(',').ToList();
            
            if (3 != values.Count)
            {
                var msg = "Cannot parse monthly bandwidth from <{0}> because it does not contain exactly three elements separated by comma."
                    .FormatWith(rawValue);
                throw new ParseException(msg, page);
            }

            try
            {
                // Time conversion is directly from the javascript. 
                // .NET does not support >>> (shift right zero fill).
                var tempTime = Convert.ToInt32(ParserHelper.TrimInvalidCharacters(values[0]), 16);
                var year = ((tempTime >> 16) & 0xFF) + 1900;
                var month = (Common.LogicalRightShift(tempTime, 8) & 0xFF) + 1;

                var download = ParserHelper.ToKilobytes(values[1]);
                var upload = ParserHelper.ToKilobytes(values[2]);

                var monthly = new MonthlyBandwidth(year, month, download, upload);
                return monthly;
            }
            catch (Exception exp)
            {
                throw new ParseException("Failed to parse single monthly bandwidth information", page, rawValue, exp);
            }
        }
    }
}
