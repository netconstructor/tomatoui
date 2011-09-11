namespace BandwidthDownloaderUi.Views
{
    using System;
    using System.Windows.Input;

    using BandwidthDownloaderUi.Commands;
    using BandwidthDownloaderUi.Events;
    using BandwidthDownloaderUi.Infra;

    /// <summary>
    /// The transfer unit.
    /// </summary>
    public enum TransferUnit
    {
        /// <summary>
        /// Kilobytes.
        /// </summary>
        Kilobytes,

        /// <summary>
        /// Megabytes.
        /// </summary>
        Megabytes,

        /// <summary>
        /// Gigabytes.
        /// </summary>
        Gigabytes
    }

    /// <summary>
    /// View model for the main view.
    /// </summary>
    public class MainViewModel : ViewModel
    {
        private DailyViewModel daily;

        private MonthlyViewModel monthly;

        private SettingsViewModel settings;

        private bool isBusy;

        private string errorMessage;

        private Exception error;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="downloadReportCommand">
        /// The download report command.
        /// </param>
        /// <param name="ignoreCommand">
        /// The ignore command.
        /// </param>
        /// <param name="openUriCommand">
        /// The open uri command.
        /// </param>
        /// <param name="clipboardCommand">
        /// The clipboard command.
        /// </param>
        /// <param name="daily">
        /// The daily.
        /// </param>
        /// <param name="monthly">
        /// The monthly.
        /// </param>
        /// <param name="settingsViewModel">Settings viewmodel.</param>
        public MainViewModel(
            IDownloadReportCommand downloadReportCommand, 
            IBaseCommand ignoreCommand,
            OpenUriCommand openUriCommand,
            IBaseCommand clipboardCommand,
            DailyViewModel daily, 
            MonthlyViewModel monthly,
            SettingsViewModel settingsViewModel)
        {
            downloadReportCommand.ReportDownloaded += this.ReportDownloaded;
            downloadReportCommand.Executing += (sender, args) =>
                {
                    this.IsBusy = true;
                };
            this.DownloadReportCommand = downloadReportCommand;

            ignoreCommand.Executed += (sender, args) =>
                {
                    this.Error = null;
                };
            this.IgnoreCommand = ignoreCommand;

            this.OpenUriCommand = openUriCommand;
            this.ClipboardCommand = clipboardCommand;

            this.daily = daily;
            this.monthly = monthly;
            this.settings = settingsViewModel;
        }

        /// <summary>
        /// Gets or sets Daily viewmodel.
        /// </summary>
        public DailyViewModel Daily
        {
            get
            {
                return this.daily;
            }

            set
            {
                this.daily = value;
                this.OnPropertyChanged("Daily");
            }
        }

        /// <summary>
        /// Gets or sets Monthly viewmodel.
        /// </summary>
        public MonthlyViewModel Monthly
        {
            get
            {
                return this.monthly;
            }

            set
            {
                this.monthly = value;
                this.OnPropertyChanged("Monthly");
            }
        }

        /// <summary>
        /// Gets or sets Settings.
        /// </summary>
        public SettingsViewModel Settings
        {
            get
            {
                return this.settings;
            }

            set
            {
                this.settings = value;
                this.OnPropertyChanged("Settings");
            }
        }

        /// <summary>
        /// Gets a value indicating whether viewmodel is busy.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            private set
            {
                this.isBusy = value;
                this.OnPropertyChanged("IsBusy");
            }
        }

        /// <summary>
        /// Gets ErrorMessage.
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }

            private set
            {
                this.errorMessage = value;
                this.OnPropertyChanged("ErrorMessage");
            }
        }

        /// <summary>
        /// Gets Error.
        /// </summary>
        public Exception Error
        {
            get
            {
                return this.error;
            }
            
            private set
            {
                this.error = value;
                this.OnPropertyChanged("Error");
            }
        }

        /// <summary>
        /// Gets DownloadReportCommand.
        /// </summary>
        public ICommand DownloadReportCommand { get; private set; }

        /// <summary>
        /// Gets IgnoreCommand.
        /// </summary>
        public ICommand IgnoreCommand { get; private set; }

        /// <summary>
        /// Gets ClipboardCommand.
        /// </summary>
        public IBaseCommand ClipboardCommand { get; private set; }

        /// <summary>
        /// Gets OpenUriCommand.
        /// </summary>
        public ICommand OpenUriCommand { get; private set; }

        private void ReportDownloaded(object sender, ReportDownloadedEventArgs e)
        {
            this.IsBusy = false;
            if (null != e.Error)
            {
                this.ReportErrorToUser(e.Error);
            }
            else
            {
                this.ErrorMessage = null;
                this.Error = null;
            }
        }

        private void ReportErrorToUser(Exception exception)
        {
            var result = ExceptionParser.ParseException(exception);
            this.Error = exception;
            this.ErrorMessage = result;
            this.ClipboardCommand.OnCanExecuteChanged();
        }
    }
}
