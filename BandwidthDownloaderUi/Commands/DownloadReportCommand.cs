namespace BandwidthDownloaderUi.Commands
{
    using System;
    using System.Windows.Input;

    using BandwidthDownloaderUi.Events;
    using BandwidthDownloaderUi.Infra;

    using TomatoBandwidth;

    /// <summary>
    /// Interface for the report downloader command.
    /// </summary>
    public interface IDownloadReportCommand : ICommand
    {
        /// <summary>
        /// Raised when report has been downloaded.
        /// </summary>
        event EventHandler<ReportDownloadedEventArgs> ReportDownloaded;

        /// <summary>
        /// Raised before command is executed.
        /// </summary>
        event EventHandler Executing;

        /// <summary>
        /// Raised after command has been executed.
        /// </summary>
        event EventHandler Executed;
    }

    /// <summary>
    /// Command executed when bandwidth information is downloaded
    /// from the router.
    /// </summary>
    public class DownloadReportCommand : Command, IDownloadReportCommand
    {
        private bool isBusy;
        private IReportDownloader reportDownloader;

        private readonly ISettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadReportCommand"/> class.
        /// </summary>
        /// <param name="reportDownloader">
        ///   The report downloader.
        /// </param>
        /// <param name="settings"></param>
        public DownloadReportCommand(IReportDownloader reportDownloader, ISettings settings)
        {
            this.reportDownloader = reportDownloader;
            this.settings = settings;
            this.reportDownloader.DownloadCompleted += this.DownloadCompleted;
        }

        /// <summary>
        /// Raised when report has been downloaded.
        /// </summary>
        public event EventHandler<ReportDownloadedEventArgs> ReportDownloaded;

        /// <summary>
        /// Downloads the report.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        protected override void InternalExecute(object parameter)
        {
            this.isBusy = true;
            this.RaiseCanExecuteChanged();

            this.reportDownloader.Address = this.settings.Address;
            this.reportDownloader.UserName = this.settings.UserName;
            this.reportDownloader.Password = this.settings.Password;
            this.reportDownloader.Timeout = (int)TimeSpan.FromSeconds(this.settings.Timeout).TotalMilliseconds;

            this.reportDownloader.DownloadAsync(EventThread.CallingThread);
        }

        /// <summary>
        /// Checks if command can be executed.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// True if command can be executed.
        /// </returns>
        protected override bool InternalCanExecute(object parameter)
        {
            return !this.isBusy;
        }

        /// <summary>
        /// Raises the <see cref="ReportDownloaded"/> event.
        /// </summary>
        /// <param name="report">
        /// The report.
        /// </param>
        /// <param name="timeStamp">
        /// The time stamp.
        /// </param>
        /// <param name="exception">Exception.</param>
        protected virtual void RaiseReportDownloaded(BandwidthReport report, DateTime timeStamp, Exception exception)
        {
            var handler = this.ReportDownloaded;
            if (null != handler)
            {
                handler(this, new ReportDownloadedEventArgs(report, timeStamp, exception));
            }
        }

        private void DownloadCompleted(object sender, DownloadCompletedEventArgs e)
        {
            this.RaiseReportDownloaded(e.Report, Common.SystemTime(), e.Error);
            this.isBusy = false;
            this.RaiseCanExecuteChanged();
        }
    }
}