namespace BandwidthDownloaderUi
{
    using BandwidthDownloaderUi.Views;

    using TomatoBandwidth;

    /// <summary>
    /// Monthly bandwidth value.
    /// </summary>
    public class MonthlyValue : BandwidthValue
    {
        private readonly MonthlyBandwidth bandwidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthlyValue"/> class.
        /// </summary>
        /// <param name="bandwidth">
        /// The monthly bandwidth.
        /// </param>
        public MonthlyValue(MonthlyBandwidth bandwidth)
        {
            this.bandwidth = bandwidth;
            this.Year = bandwidth.Year;
            this.Month = bandwidth.Month;
            this.ChangeTransferUnit(TransferUnit.Gigabytes);
        }

        /// <summary>
        /// Gets Year.
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Gets Month.
        /// </summary>
        public int Month { get; private set; }

        /// <summary>
        /// Changes the transfer unit.
        /// </summary>
        /// <param name="transferUnit">
        /// The transfer unit.
        /// </param>
        public void ChangeTransferUnit(TransferUnit transferUnit)
        {
            this.Download = this.bandwidth.Download.To(transferUnit);
            this.Upload = this.bandwidth.Upload.To(transferUnit);
        }
    }
}