namespace BandwidthDownloaderUi.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BandwidthDownloaderUi.Commands;
    using BandwidthDownloaderUi.Events;
    using BandwidthDownloaderUi.Infra;

    /// <summary>
    /// View model for the daily view.
    /// </summary>
    public class MonthlyViewModel : ChartViewModel<MonthlyValue, Filter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonthlyViewModel"/> class.
        /// </summary>
        /// <param name="downloadReportCommand">
        /// The download report command.
        /// </param>
        public MonthlyViewModel(IDownloadReportCommand downloadReportCommand)
        {
            downloadReportCommand.ReportDownloaded += this.DownloadCompleted;
            this.XValues = "Months";
        }

        protected override void FilterValues()
        {
            if (this.FilteringDisabled)
            {
                return;
            }

            var result = this.allValues.AsEnumerable();
            
            if (null != this.FilterStart)
            {
                result = result.Where(x => (x.Month >= this.FilterStart.Month && x.Year >= this.FilterStart.Year) || x.Year > this.FilterStart.Year);
            }

            if (null != this.FilterEnd)
            {
                result = result.Where(x => (x.Month <= this.FilterEnd.Month && x.Year <= this.FilterEnd.Year) || x.Year < this.FilterEnd.Year);
            }

            // If we would use observable collection and update status of single values
            // inside it then screen would flicker because each change causes it to redraw
            // the scale (Y axis).  We also need to create completely new instance of the list
            // otherwise the chart won't update.            
            this.FilteredValues = new List<MonthlyValue>(result);
        }

        private void DownloadCompleted(object sender, ReportDownloadedEventArgs e)
        {
            this.LastUpdated = e.Timestamp;

            // Update with the currently selected transfer unit
            var temp = new List<MonthlyValue>(e.Monthly);            
            temp.ForEach(x => x.ChangeTransferUnit(this.TransferUnit));
            this.allValues = temp;

            this.Filters = e.Monthly.Select(x => new Filter(x)).ToList();

            // Disable the filtering so that setting FilterStart/End
            // does not cause filtering.
            this.FilteringDisabled = true;

            if (this.allValues.HasElements())
            {
                if (null == this.FilterStart)
                {
                    this.FilterStart = new Filter(e.Monthly.FirstOrDefault());
                }

                if (null == this.FilterEnd)
                {
                    this.FilterEnd = new Filter(e.Monthly.LastOrDefault());
                }                
            }

            this.FilteringDisabled = false;

            this.FilterValues();
        }

        protected override void UpdateAllAndFilteredValues()
        {
            // This method is a workaround for the fact that I cannot bind to ConverterParameter
            // and WPF Toolkit's chart DependentValueBinding does not support multivalue
            // binding. So I need update the list which the chart uses as itemssource
            // whenever the selected transfer unit (KB, MB, GB) changes.      
      
            // Updating all the values so that they are in sync.
            this.allValues.ForEach(x => x.ChangeTransferUnit(this.TransferUnit));

            // If we would use observable collection and update status of single values
            // inside it then screen would flicker because each change causes it to redraw
            // the scale (Y axis).  We also need to create completely new instance of the list
            // otherwise the chart won't update.            
            this.FilteredValues = new List<MonthlyValue>(this.FilteredValues);
        }
    }
}
