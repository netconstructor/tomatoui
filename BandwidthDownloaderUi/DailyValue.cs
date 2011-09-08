namespace BandwidthDownloaderUi
{
    using System;

    using BandwidthDownloaderUi.Views;

    using TomatoBandwidth;

    /// <summary>
    /// Daily bandwidth value.
    /// </summary>
    public class DailyValue : BandwidthValue
    {
        private DailyBandwidth bandwidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="DailyValue"/> class.
        /// </summary>
        /// <param name="bandwidth">
        /// The daily bandwidth.
        /// </param>
        public DailyValue(DailyBandwidth bandwidth)
        {
            this.bandwidth = bandwidth;
            this.Timestamp = bandwidth.Timestamp;
            this.ChangeTransferUnit(TransferUnit.Megabytes);
        }

        /// <summary>
        /// Gets Timestamp.
        /// </summary>
        public DateTime Timestamp { get; private set; }

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