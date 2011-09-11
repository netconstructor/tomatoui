namespace BandwidthDownloaderUi
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Markup;

    using BandwidthDownloaderUi.Commands;
    using BandwidthDownloaderUi.Infra;
    using BandwidthDownloaderUi.Views;

    using TomatoBandwidth;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Sets the default culture for all framework elements.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Ensure the current culture passed into bindings is the OS culture. 
            // By default, WPF uses en-US as the culture, regardless of the system settings.
            var language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(language));

            this.InitializeMainWindow();     
            this.MainWindow.Show();
        }

        private void InitializeMainWindow()
        {
            var settings = new Settings();

            var downlodReportCommand = new DownloadReportCommand(new ReportDownloader(), settings);
            
            var daily = new DailyViewModel(downlodReportCommand);
            var monthly = new MonthlyViewModel(downlodReportCommand);
            var settingsViewmodel = new SettingsViewModel(settings);
            
            var ignoreCommand = new Command();
            var openUriCommand = new OpenUriCommand();
            var clipboardCommand = new CopyErrorReportToClipboardCommand(new SystemClipboard());

            this.MainWindow = new MainWindow();

            this.MainWindow.DataContext = new MainViewModel(downlodReportCommand, ignoreCommand, openUriCommand, clipboardCommand, daily, monthly, settingsViewmodel);
        }
    }
}
