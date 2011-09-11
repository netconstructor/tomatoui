namespace BandwidthDownloaderUi.Events
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using TomatoBandwidth;

    /// <summary>
    /// Raised when report has been downloaded.
    /// </summary>
    public class ReportDownloadedEventArgs : EventArgs
    {
        private List<DailyValue> daily;

        private List<MonthlyValue> monthly;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportDownloadedEventArgs"/> class.
        /// </summary>
        /// <param name="report">
        /// The report.
        /// </param>
        /// <param name="timestamp">
        /// The timestamp.
        /// </param>
        /// <param name="exception">Exception.</param>
        public ReportDownloadedEventArgs(BandwidthReport report, DateTime timestamp, Exception exception)
        {
            this.monthly = new List<MonthlyValue>();
            this.daily = new List<DailyValue>();
            this.Timestamp = timestamp;

            if (null != exception)
            {
                this.Error = exception;
                return;
            }

            report.Monthly.Each((value) =>
                {
                    var monthlyValue = new MonthlyValue(value);
                    this.monthly.Add(monthlyValue);
                });

            report.Daily.Each((value) =>
            {
                var dailyValue = new DailyValue(value);
                this.daily.Add(dailyValue);
            });            
        }

        /// <summary>
        /// Gets Error.
        /// </summary>
        public Exception Error { get; private set; }

        /// <summary>
        /// Gets Monthly.
        /// </summary>
        public IEnumerable<MonthlyValue> Monthly
        {
            get
            {
                return this.monthly;
            }
        }

        /// <summary>
        /// Gets Daily.
        /// </summary>
        public List<DailyValue> Daily
        {
            get
            {
                return this.daily;
            }
        }

        /// <summary>
        /// Gets Timestamp.
        /// </summary>
        public DateTime Timestamp { get; private set; }
    }
}