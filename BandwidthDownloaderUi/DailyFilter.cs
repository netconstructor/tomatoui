namespace BandwidthDownloaderUi
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Used to filter daily charts.
    /// </summary>
    public class DailyFilter : IEquatable<DailyFilter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyFilter"/> class.
        /// </summary>
        /// <param name="timestamp">
        /// The timestamp.
        /// </param>
        public DailyFilter(DateTime timestamp)
        {
            this.Date = timestamp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DailyFilter"/> class.
        /// </summary>
        /// <param name="dailyValue">
        /// The daily value.
        /// </param>
        public DailyFilter(DailyValue dailyValue)
        {
            this.Date = dailyValue.Timestamp;
            this.Timestamp = this.Date.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Gets Timestamp.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets Timestamp.
        /// </summary>
        public string Timestamp { get; private set; }

        /// <summary>
        /// Checks if the filters are equal by comparing year
        /// and month.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// True if filters have same year and month.
        /// </returns>
        public bool Equals(DailyFilter other)
        {
            if (null == other)
            {
                return false;
            }

            return this.Date == other.Date;
        }
    }
}