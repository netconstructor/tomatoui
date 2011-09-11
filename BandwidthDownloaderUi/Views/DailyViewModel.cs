namespace BandwidthDownloaderUi.Views
{
    using System.Collections.Generic;

    using BandwidthDownloaderUi.Commands;
    using BandwidthDownloaderUi.Events;
    using BandwidthDownloaderUi.Infra;

    /// <summary>
    /// View model for the daily view.
    /// </summary>
    public class DailyViewModel : ViewModel
    {
        private List<DailyValue> values = new List<DailyValue>();

        private TransferUnit transferUnit = TransferUnit.Megabytes;

        /// <summary>
        /// Initializes a new instance of the <see cref="DailyViewModel"/> class.
        /// </summary>
        /// <param name="downloadReportCommand">
        /// The download report command.
        /// </param>
        public DailyViewModel(IDownloadReportCommand downloadReportCommand)
        {
            downloadReportCommand.ReportDownloaded += this.DownloadCompleted;
        }

        /// <summary>
        /// Gets or sets TransferUnit.
        /// </summary>
        public TransferUnit TransferUnit
        {
            get
            {
                return this.transferUnit;
            }

            set
            {
                this.transferUnit = value;
                this.UpdateValues();
                this.OnPropertyChanged("TransferUnit");
            }
        }

        /// <summary>
        /// Gets Daily bandwidth.
        /// </summary>
        public List<DailyValue> Values
        {
            get
            {
                return this.values;
            }

            private set
            {
                this.values = value;
                this.OnPropertyChanged("Values");
            }
        }

        private void DownloadCompleted(object sender, ReportDownloadedEventArgs e)
        {
            // Update with the currently selected transfer unit
            e.Daily.ForEach(x => x.ChangeTransferUnit(this.transferUnit));
            this.Values = e.Daily;
        }

        private void UpdateValues()
        {
            // This is a workaround for the fact that I cannot bind to ConverterParameter
            // and WPF Toolkit's chart DependentValueBinding does not support multivalue
            // binding. So I need update the list which the chart uses as itemssource
            // whenever the selected transfer unit (KB, MB, GB) changes.            
            this.Values.ForEach(x => x.ChangeTransferUnit(this.transferUnit));

            // If we would use observable collection and update status of single values
            // inside it then screen would flicker because each change causes it to redraw
            // the scale (Y axis).  We also need to create completely new instance of the list
            // otherwise the chart won't update.            
            this.Values = new List<DailyValue>(this.Values);
        }
    }
}
