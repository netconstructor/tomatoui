namespace TomatoBandwidth
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Event arguments for asynchronous methods.
    /// </summary>
    public class DownloadCompletedEventArgs : AsyncCompletedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="report">
        /// The report.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public DownloadCompletedEventArgs(BandwidthReport report, Exception exception)
            : base(exception, false, null)
        {
            this.Report = report;
        }

        /// <summary>
        /// Gets Report.
        /// </summary>
        public BandwidthReport Report { get; private set; }
    }
}