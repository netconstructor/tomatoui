namespace BandwidthDownloaderUi
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Used to filter charts.
    /// </summary>
    public class Filter : IEquatable<Filter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        /// <param name="year">
        /// The year.
        /// </param>
        /// <param name="month">
        /// The month.
        /// </param>
        public Filter(int year, int month)
        {
            this.Year = year;
            this.Month = month;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        /// <param name="monthlyValue">
        /// The monthly value.
        /// </param>
        public Filter(MonthlyValue monthlyValue)
        {
            this.Year = monthlyValue.Year;
            this.Month = monthlyValue.Month;
            this.Timestamp = new DateTime(this.Year, this.Month, 1).ToString(CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern);
        }

        /// <summary>
        /// Gets Year.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Gets Month.
        /// </summary>
        public int Month { get; private set; }

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
        public bool Equals(Filter other)
        {
            if (null == other)
            {
                return false;
            }

            return this.Year == other.Year && this.Month == other.Month;
        }
    }
}