namespace BandwidthDownloaderUi.Events
{
    using System;
    using System.Collections.Generic;

    using TomatoBandwidth;

    /// <summary>
    /// Raised when report has been downloaded.
    /// </summary>
    public class ReportDownloadedEventArgs : EventArgs
    {
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
            this.Monthly = new List<MonthlyValue>();
            this.Daily = new List<DailyValue>();
            this.Timestamp = timestamp;

            if (null != exception)
            {
                this.Error = exception;
                return;
            }

            report.Monthly.Each((value) =>
                {
                    var monthlyValue = new MonthlyValue(value);
                    this.Monthly.Add(monthlyValue);
                });

            report.Daily.Each((value) =>
            {
                var dailyValue = new DailyValue(value);
                this.Daily.Add(dailyValue);
            });            
        }

        /// <summary>
        /// Gets Error.
        /// </summary>
        public Exception Error { get; private set; }

        /// <summary>
        /// Gets Monthly.
        /// </summary>
        public List<MonthlyValue> Monthly { get; private set; }

        /// <summary>
        /// Gets Daily.
        /// </summary>
        public List<DailyValue> Daily { get; private set; }

        /// <summary>
        /// Gets Timestamp.
        /// </summary>
        public DateTime Timestamp { get; private set; }
    }
}