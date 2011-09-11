namespace BandwidthDownloaderUi.Views
{
    using System.Collections.Generic;
    using System.Linq;

    using BandwidthDownloaderUi.Commands;
    using BandwidthDownloaderUi.Events;

    /// <summary>
    /// View model for the daily view.
    /// </summary>
    public class DailyViewModel : ChartViewModel<DailyValue, DailyFilter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyViewModel"/> class.
        /// </summary>
        /// <param name="downloadReportCommand">
        /// The download report command.
        /// </param>
        public DailyViewModel(IDownloadReportCommand downloadReportCommand)
        {
            downloadReportCommand.ReportDownloaded += this.DownloadCompleted;
            this.TransferUnit = TransferUnit.Megabytes;
            this.XValues = "Days";
        }

        /// <summary>
        /// Filters values.
        /// </summary>
        protected override void FilterValues()
        {
            if (this.FilteringDisabled)
            {
                return;
            }

            var result = this.AllValues.AsEnumerable();

            if (null != this.FilterStart)
            {
                result = result.Where(x => (x.Timestamp >= this.FilterStart.Date));
            }

            if (null != this.FilterEnd)
            {
                result = result.Where(x => (x.Timestamp <= this.FilterEnd.Date));
            }

            // If we would use observable collection and update status of single values
            // inside it then screen would flicker because each change causes it to redraw
            // the scale (Y axis).  We also need to create completely new instance of the list
            // otherwise the chart won't update.            
            this.FilteredValues = new List<DailyValue>(result);
        }

        /// <summary>
        /// Updates all and filtered values.
        /// </summary>
        protected override void UpdateAllAndFilteredValues()
        {
            // This method is a workaround for the fact that I cannot bind to ConverterParameter
            // and WPF Toolkit's chart DependentValueBinding does not support multivalue
            // binding. So I need update the list which the chart uses as itemssource
            // whenever the selected transfer unit (KB, MB, GB) changes.      

            // Updating all the values so that they are in sync.
            this.AllValues.ForEach(x => x.ChangeTransferUnit(this.TransferUnit));

            // If we would use observable collection and update status of single values
            // inside it then screen would flicker because each change causes it to redraw
            // the scale (Y axis).  We also need to create completely new instance of the list
            // otherwise the chart won't update.            
            this.FilteredValues = new List<DailyValue>(this.FilteredValues);
        }

        private void DownloadCompleted(object sender, ReportDownloadedEventArgs e)
        {
            this.LastUpdated = e.Timestamp;

            // Update with the currently selected transfer unit
            var temp = new List<DailyValue>(e.Daily);
            temp.ForEach(x => x.ChangeTransferUnit(this.TransferUnit));
            this.AllValues = temp;

            this.Filters = e.Daily.Select(x => new DailyFilter(x)).ToList();

            // Disable the filtering so that setting FilterStart/End
            // does not cause filtering.
            this.FilteringDisabled = true;

            if (this.AllValues.HasElements())
            {
                if (null == this.FilterStart)
                {
                    this.FilterStart = new DailyFilter(e.Daily.FirstOrDefault());
                }

                if (null == this.FilterEnd)
                {
                    this.FilterEnd = new DailyFilter(e.Daily.LastOrDefault());
                }
            }

            this.FilteringDisabled = false;

            this.FilterValues();
        }
    }
}
