namespace TomatoBandwidth
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Specifies on which thread the completed event 
    /// of asynchronous method will be called.
    /// </summary>
    public enum EventThread
    {
        /// <summary>
        /// Default option. The event is raised on background
        /// thread.
        /// </summary>
        Background,

        /// <summary>
        /// The event is raised on the same thread that called
        /// the asynchronous method. Use this setting If you want 
        /// to receive the event on the user interface thread.
        /// </summary>
        CallingThread
    }

    /// <summary>
    /// Interface for report downloader.
    /// </summary>
    public interface IReportDownloader
    {
        /// <summary>
        /// Raised when <see cref="DownloadAsync()"/> completes.
        /// </summary>
        event EventHandler<DownloadCompletedEventArgs> DownloadCompleted;

        /// <summary>
        /// Raised when <see cref="DownloadDailyAsync()"/> completes.
        /// </summary>
        event EventHandler<DownloadCompletedEventArgs> DownloadDailyCompleted;

        /// <summary>
        /// Raised when <see cref="DownloadMonthlyAsync()"/> completes.
        /// </summary>
        event EventHandler<DownloadCompletedEventArgs> DownloadMonthlyCompleted;

        /// <summary>
        /// Gets or sets UserName.
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets HTTP Request Timeout in milliseconds.
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// Gets or sets Uri.
        /// </summary>
        Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets Encoding which is used when response
        /// from the router is read. Default encoding is UTF-8.
        /// </summary>
        Encoding Encoding { get; set; }

        /// <summary>
        /// Downloads monthly and daily report.
        /// </summary>
        /// <returns>
        /// Monthly and daily bandwidth report.
        /// </returns>
        BandwidthReport Download();

        /// <summary>
        /// Downloads monthly bandwidth report.
        /// </summary>
        /// <returns>
        /// Monthly bandwidth report.
        /// </returns>
        BandwidthReport DownloadMonthly();

        /// <summary>
        /// Downloads daily bandwidth report.
        /// </summary>
        /// <returns>
        /// Daily bandwidth report.
        /// </returns>
        BandwidthReport DownloadDaily();

        /// <summary>
        /// Starts to download report asynchronously.
        /// </summary>
        void DownloadAsync();

        /// <summary>
        /// Starts to download report asynchronously.
        /// </summary>
        /// <param name="eventThread">
        /// Specifies the thread to use when completed event is raised.
        /// </param>
        void DownloadAsync(EventThread eventThread);

        /// <summary>
        /// Starts to download daily report asynchronously.
        /// </summary>
        void DownloadDailyAsync();

        /// <summary>
        /// Starts to download daily report asynchronously.
        /// </summary>
        /// <param name="eventThread">
        /// Specifies the thread to use when completed event is raised.
        /// </param>
        void DownloadDailyAsync(EventThread eventThread);

        /// <summary>
        /// Starts to download monthly report asynchronously.
        /// </summary>
        void DownloadMonthlyAsync();

        /// <summary>
        /// Starts to download monthly report asynchronously.
        /// </summary>
        /// <param name="eventThread">
        /// Specifies the thread to use when completed event is raised.
        /// </param>
        void DownloadMonthlyAsync(EventThread eventThread);

        /// <summary>
        /// Begins asynchronous request to download bandwidth report.
        /// </summary>
        void BeginDownload();

        /// <summary>
        /// Ends asynchronous request to download bandwidth report.
        /// </summary>
        /// <returns>
        /// The bandwidth report.
        /// </returns>
        BandwidthReport EndDownload();

        /// <summary>
        /// Begins asynchronous request to download daily bandwidth report.
        /// </summary>
        void BeginDownloadDaily();

        /// <summary>
        /// Ends asynchronous request to download daily bandwidth report.
        /// </summary>
        /// <returns>
        /// Daily bandwidth report.
        /// </returns>
        BandwidthReport EndDownloadDaily();

        /// <summary>
        /// Begins asynchronous request to download monthly bandwidth report.
        /// </summary>
        void BeginDownloadMonthly();

        /// <summary>
        /// Ends asynchronous request to download monthly bandwidth report.
        /// </summary>
        /// <returns>
        /// Monthly bandwidth report.
        /// </returns>
        BandwidthReport EndDownloadMonthly();

        /// <summary>
        /// Releases all resources.
        /// </summary>
        void Dispose();
    }

    /// <summary>
    /// Used to download bandwidth report from router which is
    /// using Tomato firmware.
    /// </summary>
    public class ReportDownloader : IDisposable, IReportDownloader
    {
        /// <summary>
        /// If <see cref="Timeout"/> has been set to this value then
        /// default framework timeout is used.
        /// </summary>
        public const int UnsetTimeoutValue = -1;

        private readonly MonthlyReportParser monthlyReportParser = new MonthlyReportParser();

        private readonly DailyReportParser dailyReportParser = new DailyReportParser();

        private readonly Encoding defaultEncoding = Encoding.UTF8;        

        private Task<BandwidthReport> task = null;

        private bool asyncOperationInProgress;

        private bool hasBeenDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportDownloader"/> class.
        /// </summary>
        public ReportDownloader()
        {
            this.Encoding = this.defaultEncoding;
            this.Timeout = UnsetTimeoutValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportDownloader"/> class.
        /// </summary>
        /// <param name="uri">
        /// The address of the page that contains monthly and daily bandwidth information.
        /// </param>
        /// <param name="userName">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public ReportDownloader(Uri uri, string userName, string password)
        {
            this.Uri = uri;
            this.UserName = userName;
            this.Password = password;
            this.Timeout = UnsetTimeoutValue;
            this.Encoding = this.defaultEncoding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportDownloader"/> class.
        /// </summary>
        /// <param name="uri">
        /// The address of the page that contains monthly and daily bandwidth information.
        /// </param>
        /// <param name="userName">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="timeout">
        /// The timeout in milliseconds.
        /// </param>
        public ReportDownloader(Uri uri, string userName, string password, int timeout)
            : this(uri, userName, password)
        {
            this.Timeout = timeout;
        }

        ~ReportDownloader()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Raised when <see cref="DownloadAsync"/> completes.
        /// </summary>
        public event EventHandler<DownloadCompletedEventArgs> DownloadCompleted;

        /// <summary>
        /// Raised when <see cref="DownloadDailyAsync"/> completes.
        /// </summary>
        public event EventHandler<DownloadCompletedEventArgs> DownloadDailyCompleted;

        /// <summary>
        /// Raised when <see cref="DownloadMonthlyAsync"/> completes.
        /// </summary>
        public event EventHandler<DownloadCompletedEventArgs> DownloadMonthlyCompleted;

        /// <summary>
        /// Gets or sets UserName.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets HTTP Request Timeout in milliseconds.
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Gets or sets Uri.
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets or sets Encoding which is used when response
        /// from the router is read. Default encoding is UTF-8.
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Downloads monthly and daily report.
        /// </summary>
        /// <returns>
        /// Monthly and daily bandwidth report.
        /// </returns>
        public BandwidthReport Download()
        {
            return this.CreateReport(null);
        }

        /// <summary>
        /// Downloads monthly bandwidth report.
        /// </summary>
        /// <returns>
        /// Monthly bandwidth report.
        /// </returns>
        public BandwidthReport DownloadMonthly()
        {
            return this.CreateMonthlyReport(null);
        }

        /// <summary>
        /// Downloads daily bandwidth report.
        /// </summary>
        /// <returns>
        /// Daily bandwidth report.
        /// </returns>
        public BandwidthReport DownloadDaily()
        {
            return this.CreateDailyReport(null);
        }

        /// <summary>
        /// Starts to download report asynchronously.
        /// </summary>
        public void DownloadAsync()
        {
            this.DownloadAsync(EventThread.Background);
        }

        /// <summary>
        /// Starts to download report asynchronously.
        /// </summary>
        /// <param name="eventThread">
        /// Specifies the thread to use when completed event is raised.
        /// </param>
        public void DownloadAsync(EventThread eventThread)
        {
            var asyncTask = this.CreateTask(this.CreateReport);
            asyncTask.ContinueWith(
                (downloadTask) => this.AsyncOperationCompleted(downloadTask, this.InvokeDownloadCompleted),
                this.CreateTaskScheduler(eventThread));
            asyncTask.Start();            
        }

        /// <summary>
        /// Starts to download daily report asynchronously.
        /// </summary>
        public void DownloadDailyAsync()
        {
            this.DownloadDailyAsync(EventThread.Background);
        }

        /// <summary>
        /// Starts to download daily report asynchronously.
        /// </summary>
        /// <param name="eventThread">
        /// Specifies the thread to use when completed event is raised.
        /// </param>
        public void DownloadDailyAsync(EventThread eventThread)
        {
            var asyncTask = this.CreateTask(this.CreateDailyReport);
            asyncTask.ContinueWith(
                (downloadTask) => this.AsyncOperationCompleted(downloadTask, this.InvokeDownloadDailyCompleted),
                this.CreateTaskScheduler(eventThread));
            asyncTask.Start();
        }

        /// <summary>
        /// Starts to download monthly report asynchronously.
        /// </summary>
        public void DownloadMonthlyAsync()
        {
            this.DownloadMonthlyAsync(EventThread.Background);
        }

        /// <summary>
        /// Starts to download monthly report asynchronously.
        /// </summary>
        /// <param name="eventThread">
        /// Specifies the thread to use when completed event is raised.
        /// </param>
        public void DownloadMonthlyAsync(EventThread eventThread)
        {
            var asyncTask = this.CreateTask(this.CreateMonthlyReport);
            asyncTask.ContinueWith(
                (downloadTask) => this.AsyncOperationCompleted(downloadTask, this.InvokeDownloadMonthlyCompleted),
                this.CreateTaskScheduler(eventThread));
            asyncTask.Start();
        }

        /// <summary>
        /// Begins asynchronous request to download bandwidth report.
        /// </summary>
        public void BeginDownload()
        {
            this.task = this.CreateTask(this.CreateReport);
            this.task.Start();
        }

        /// <summary>
        /// Ends asynchronous request to download bandwidth report.
        /// </summary>
        /// <returns>
        /// The bandwidth report.
        /// </returns>
        public BandwidthReport EndDownload()
        {
            try
            {
                return GetTaskResult(this.task);
            }
            finally
            {
                this.asyncOperationInProgress = false;
                this.task.DisposeIfNotNull();
                this.task = null;
            }
        }

        /// <summary>
        /// Begins asynchronous request to download daily bandwidth report.
        /// </summary>
        public void BeginDownloadDaily()
        {
            this.task = this.CreateTask(this.CreateDailyReport);
            this.task.Start();
        }

        /// <summary>
        /// Ends asynchronous request to download daily bandwidth report.
        /// </summary>
        /// <returns>
        /// Daily bandwidth report.
        /// </returns>
        public BandwidthReport EndDownloadDaily()
        {
            try
            {
                return GetTaskResult(this.task);
            }
            finally
            {
                this.asyncOperationInProgress = false;
                this.task.DisposeIfNotNull();
                this.task = null;
            }            
        }

        /// <summary>
        /// Begins asynchronous request to download monthly bandwidth report.
        /// </summary>
        public void BeginDownloadMonthly()
        {
            this.task = this.CreateTask(this.CreateMonthlyReport);
            this.task.Start();
        }

        /// <summary>
        /// Ends asynchronous request to download monthly bandwidth report.
        /// </summary>
        /// <returns>
        /// Monthly bandwidth report.
        /// </returns>
        public BandwidthReport EndDownloadMonthly()
        {
            try
            {
                return GetTaskResult(this.task);
            }
            finally
            {
                this.asyncOperationInProgress = false;
                this.task.DisposeIfNotNull();
                this.task = null;
            }
        }

        /// <summary>
        /// Releases all resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.hasBeenDisposed)
            {
                return;
            }

            if (disposing)
            {
                this.task.DisposeIfNotNull();
            }

            this.task = null;
            this.hasBeenDisposed = true;
        }

        /// <summary>
        /// Downloads the page which contains bandwidth information.
        /// </summary>
        /// <returns>
        /// String which contains the downloaded page.
        /// </returns>
        protected virtual string DownloadReportPage()
        {
            var httpRequest = this.CreateWebRequest();
            return this.ReadResponse(httpRequest);            
        }

        /// <summary>
        /// Creates request which is sent to the router.
        /// </summary>
        /// <returns>
        /// Created request.
        /// </returns>
        protected virtual WebRequest CreateWebRequest()
        {
            var httpRequest = WebRequest.Create(this.Uri);

            if (UnsetTimeoutValue != this.Timeout)
            {
                httpRequest.Timeout = this.Timeout;
            }

            httpRequest.Credentials = new NetworkCredential(this.UserName, this.Password);
            return httpRequest;
        }

        /// <summary>
        /// Reads the response from the request.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The Response.
        /// </returns>
        protected virtual string ReadResponse(WebRequest request)
        {
            var stream = this.GetResponseStream(request);
            using (var reader = new StreamReader(stream, this.Encoding))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Gets the response stream.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The reponse stream.
        /// </returns>
        protected virtual Stream GetResponseStream(WebRequest request)
        {
            var response = request.GetResponse();
            return response.GetResponseStream();
        }

        /// <summary>
        /// Raises the <see cref="DownloadCompleted"/> event.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected virtual void InvokeDownloadCompleted(DownloadCompletedEventArgs e)
        {
            var handler = this.DownloadCompleted;
            if (null != handler)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="DownloadDailyCompleted"/> event.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected virtual void InvokeDownloadDailyCompleted(DownloadCompletedEventArgs e)
        {
            var handler = this.DownloadDailyCompleted;
            if (null != handler)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="DownloadMonthlyCompleted"/> event.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected virtual void InvokeDownloadMonthlyCompleted(DownloadCompletedEventArgs e)
        {
            var handler = this.DownloadMonthlyCompleted;
            if (null != handler)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Creates task scheduler.
        /// </summary>
        /// <param name="eventThread">
        /// The event thread.
        /// </param>
        /// <returns>
        /// Task scheduler for the task.
        /// </returns>
        protected virtual TaskScheduler CreateTaskScheduler(EventThread eventThread)
        {
            if (EventThread.Background == eventThread)
            {
                return TaskScheduler.Default;
            }

            return TaskScheduler.FromCurrentSynchronizationContext();
        }

        private static T GetTaskResult<T>(Task<T> taskToWait)
        {
            if (null == taskToWait)
            {
                throw new InvalidOperationException("Report download has not been started. Cannot return the result.");
            }

            taskToWait.Wait();
            return taskToWait.Result;
        }

        private Task<T> CreateTask<T>(Func<object, T> taskMethod)
        {
            if (this.asyncOperationInProgress)
            {
                throw new InvalidOperationException("Cannot download report. Previous asynchronous operation has not been completed.");
            }

            this.asyncOperationInProgress = true;

            return new Task<T>(taskMethod, null);
        }

        private BandwidthReport CreateReport(object state)
        {
            var page = this.DownloadReportPage();
            var daily = this.dailyReportParser.Parse(page);
            var monthly = this.monthlyReportParser.Parse(page);
            return new BandwidthReport(monthly, daily);
        }

        private BandwidthReport CreateMonthlyReport(object state)
        {
            var page = this.DownloadReportPage();
            return new BandwidthReport(this.monthlyReportParser.Parse(page));
        }

        private BandwidthReport CreateDailyReport(object state)
        {
            var page = this.DownloadReportPage();
            return new BandwidthReport(this.dailyReportParser.Parse(page));
        }

        private void AsyncOperationCompleted(Task<BandwidthReport> downloadTask, Action<DownloadCompletedEventArgs> action)
        {
            if (TaskStatus.RanToCompletion == downloadTask.Status)
            {
                action(new DownloadCompletedEventArgs(downloadTask.Result, null));
            }
            else
            {
                action(new DownloadCompletedEventArgs(null, downloadTask.Exception));
            }

            this.asyncOperationInProgress = false;            
        }
    }
}
