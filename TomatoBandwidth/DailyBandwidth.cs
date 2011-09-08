namespace TomatoBandwidth
{
    using System;

    /// <summary>
    /// Information about daily bandwidth usage.
    /// </summary>
    public class DailyBandwidth : MonthlyBandwidth
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyBandwidth"/> class.
        /// </summary>
        public DailyBandwidth()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DailyBandwidth"/> class.
        /// </summary>
        /// <param name="year">
        /// The year.
        /// </param>
        /// <param name="month">
        /// The month.
        /// </param>
        /// <param name="day">
        /// The day.
        /// </param>
        /// <param name="download">
        /// The download.
        /// </param>
        /// <param name="upload">
        /// The upload.
        /// </param>
        public DailyBandwidth(int year, int month, int day, long download, long upload)
            : base(year, month, download, upload)
        {
            this.Day = day;
        }

        /// <summary>
        /// Gets or sets Day.
        /// </summary>
        public int Day { get; protected set; }

        /// <summary>
        /// Gets date of the bandwidth.
        /// </summary>
        public DateTime Timestamp
        {
            get
            {
                return new DateTime(this.Year, this.Month, this.Day);
            }
        }
    }
}