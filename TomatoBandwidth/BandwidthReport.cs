namespace TomatoBandwidth
{
    using System.Collections.Generic;

    /// <summary>
    /// Type of the report.
    /// </summary>
    public enum ReportType
    {
        /// <summary>
        /// Contains daily and monthly bandwidth information.
        /// </summary>
        DailyAndMonthly,

        /// <summary>
        /// Contains daily bandwidth information.
        /// </summary>
        Daily,

        /// <summary>
        /// Contains monthly bandwidth information.
        /// </summary>
        Monthly
    }

    /// <summary>
    /// Bandwidth report.
    /// </summary>
    public class BandwidthReport
    {
        private readonly List<MonthlyBandwidth> monthly = new List<MonthlyBandwidth>();

        private readonly List<DailyBandwidth> daily = new List<DailyBandwidth>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BandwidthReport"/> class.
        /// </summary>
        public BandwidthReport()
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BandwidthReport"/> class.
        /// </summary>
        /// <param name="monthly">
        /// The monthly bandwidth.
        /// </param>
        /// <param name="daily">The daily bandwidth.</param>
        public BandwidthReport(IEnumerable<MonthlyBandwidth> monthly, IEnumerable<DailyBandwidth> daily)
        {
            this.monthly.AddRange(monthly);
            this.daily.AddRange(daily);
            this.ReportType = ReportType.DailyAndMonthly;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BandwidthReport"/> class.
        /// </summary>
        /// <param name="monthly">
        /// The monthly.
        /// </param>
        public BandwidthReport(IEnumerable<MonthlyBandwidth> monthly)
        {
            this.monthly.AddRange(monthly);
            this.ReportType = ReportType.Monthly;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BandwidthReport"/> class.
        /// </summary>
        /// <param name="daily">
        /// The daily.
        /// </param>
        public BandwidthReport(IEnumerable<DailyBandwidth> daily)
        {
            this.daily.AddRange(daily);
            this.ReportType = ReportType.Daily;
        }

        /// <summary>
        /// Gets ReportType.
        /// </summary>
        public ReportType ReportType { get; private set; }

        /// <summary>
        /// Gets Monthly bandwidth values.
        /// </summary>
        public IEnumerable<MonthlyBandwidth> Monthly
        {
            get
            {
                return this.monthly;
            }
        }

        /// <summary>
        /// Gets Daily bandwidth values.
        /// </summary>
        public IEnumerable<DailyBandwidth> Daily
        {
            get
            {
                return this.daily;
            }
        }
    }
}