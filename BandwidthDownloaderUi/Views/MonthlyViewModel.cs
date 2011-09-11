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
    public class MonthlyViewModel : ViewModel
    {
        private List<MonthlyValue> allValues = new List<MonthlyValue>();

        private List<MonthlyValue> filteredValues = new List<MonthlyValue>();

        private List<Filter> filters = new List<Filter>();

        private TransferUnit transferUnit = TransferUnit.Gigabytes;

        private Filter filterStart;

        private Filter filterEnd;

        private bool filteringDisabled;

        private DateTime? lastUpdated;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthlyViewModel"/> class.
        /// </summary>
        /// <param name="downloadReportCommand">
        /// The download report command.
        /// </param>
        public MonthlyViewModel(IDownloadReportCommand downloadReportCommand)
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
                this.UpdateAllAndFilteredValues();
                this.OnPropertyChanged("TransferUnit");
            }
        }

        /// <summary>
        /// Gets FilteredValues.
        /// </summary>
        public List<MonthlyValue> FilteredValues
        {
            get
            {
                return this.filteredValues;
            }

            private set
            {
                this.filteredValues = value;
                this.OnPropertyChanged("FilteredValues");
            }
        }

        /// <summary>
        /// Gets Filters.
        /// </summary>
        public List<Filter> Filters
        {
            get
            {
                return this.filters;
            }

            private set
            {
                this.filters = value;
                this.OnPropertyChanged("Filters");
            }
        }

        /// <summary>
        /// Gets or sets the start filter.
        /// </summary>
        public Filter FilterStart
        {
            get
            {
                return this.filterStart;
            }

            set
            {
                this.filterStart = value;
                this.FilterValues();
                this.OnPropertyChanged("FilterStart");
            }
        }

        /// <summary>
        /// Gets or sets the end filter.
        /// </summary>
        public Filter FilterEnd
        {
            get
            {
                return this.filterEnd;
            }

            set
            {
                this.filterEnd = value;
                this.FilterValues();
                this.OnPropertyChanged("FilterEnd");
            }
        }

        /// <summary>
        /// Gets LastUpdated.
        /// </summary>
        public DateTime? LastUpdated
        {
            get
            {
                return this.lastUpdated;
            }

            private set
            {
                this.lastUpdated = value;
                this.OnPropertyChanged("LastUpdated");
            }
        }

        /// <summary>
        /// Gets XValues.
        /// </summary>
        public string XValues
        {
            get
            {
                return "Months";
            }
        }

        private void FilterValues()
        {
            if (this.filteringDisabled)
            {
                return;
            }

            var result = this.allValues.AsEnumerable();
            
            if (null != this.FilterStart)
            {
                result = result.Where(x => (x.Month >= this.filterStart.Month && x.Year >= this.filterStart.Year) || x.Year > this.filterStart.Year);
            }

            if (null != this.FilterEnd)
            {
                result = result.Where(x => (x.Month <= this.filterEnd.Month && x.Year <= this.filterEnd.Year) || x.Year < this.filterEnd.Year);
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
            temp.ForEach(x => x.ChangeTransferUnit(this.transferUnit));
            this.allValues = temp;

            this.Filters = e.Monthly.Select(x => new Filter(x)).ToList();

            // Disable the filtering so that setting FilterStart/End
            // does not cause filtering.);
            this.filteringDisabled = true;

            if (this.allValues.HasElements())
            {
                if (null == this.filterStart)
                {
                    this.FilterStart = new Filter(e.Monthly.FirstOrDefault());
                }

                if (null == this.FilterEnd)
                {
                    this.FilterEnd = new Filter(e.Monthly.LastOrDefault());
                }                
            }

            this.filteringDisabled = false;

            this.FilterValues();
        }

        private void UpdateAllAndFilteredValues()
        {
            // This method is a workaround for the fact that I cannot bind to ConverterParameter
            // and WPF Toolkit's chart DependentValueBinding does not support multivalue
            // binding. So I need update the list which the chart uses as itemssource
            // whenever the selected transfer unit (KB, MB, GB) changes.      
      
            // Updating all the values so that they are in sync.
            this.allValues.ForEach(x => x.ChangeTransferUnit(this.transferUnit));

            // If we would use observable collection and update status of single values
            // inside it then screen would flicker because each change causes it to redraw
            // the scale (Y axis).  We also need to create completely new instance of the list
            // otherwise the chart won't update.            
            this.FilteredValues = new List<MonthlyValue>(this.filteredValues);
        }
    }
}
