namespace BandwidthDownloaderUi.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BandwidthDownloaderUi.Infra;

    /// <summary>
    /// Base class for daily and monthly viewmodels.
    /// </summary>
    public class ChartViewModel<TValueType, TFilterType> : ViewModel
    {
        protected List<TValueType> allValues = new List<TValueType>();

        private List<TValueType> filteredValues = new List<TValueType>();

        public bool FilteringDisabled { get;  protected set; }

        private DateTime? lastUpdated;

        private TransferUnit transferUnit = TransferUnit.Gigabytes;

        private List<TFilterType> filters = new List<TFilterType>();

        private TFilterType filterStart;

        private TFilterType filterEnd;

        public string XValues { get; set; }

        /// <summary>
        /// Gets or sets FilteredValues.
        /// </summary>
        public List<TValueType> FilteredValues
        {
            get
            {
                return this.filteredValues;
            }

            set
            {
                this.filteredValues = value;
                this.OnPropertyChanged("FilteredValues");
            }
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
        /// Gets or sets Filters.
        /// </summary>
        public List<TFilterType> Filters
        {
            get
            {
                return this.filters;
            }

            set
            {
                this.filters = value;
                this.OnPropertyChanged("Filters");
            }
        }

        /// <summary>
        /// Gets or sets the start filter.
        /// </summary>
        public TFilterType FilterStart
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
        public TFilterType FilterEnd
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
        /// Gets or sets LastUpdated.
        /// </summary>
        public DateTime? LastUpdated
        {
            get
            {
                return this.lastUpdated;
            }

            set
            {
                this.lastUpdated = value;
                this.OnPropertyChanged("LastUpdated");
            }
        }

        protected virtual void UpdateAllAndFilteredValues()
        {            
        }

        protected virtual void FilterValues()
        {            
        }
    }
}
