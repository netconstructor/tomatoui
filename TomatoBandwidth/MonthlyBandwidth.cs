namespace TomatoBandwidth
{
    /// <summary>
    /// Information about bandwidth usage during one month.
    /// </summary>
    public class MonthlyBandwidth
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonthlyBandwidth"/> class.
        /// </summary>
        /// <param name="year">
        /// The year.
        /// </param>
        /// <param name="month">
        /// The month.
        /// </param>
        /// <param name="download">
        /// The download.
        /// </param>
        /// <param name="upload">
        /// The upload.
        /// </param>
        public MonthlyBandwidth(int year, int month, long download, long upload)
        {
            this.Year = year;
            this.Month = month;
            this.Download = new Bandwidth(download);
            this.Upload = new Bandwidth(upload);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonthlyBandwidth"/> class.
        /// </summary>
        public MonthlyBandwidth()
        {
        }

        /// <summary>
        /// Gets or sets Year.
        /// </summary>
        public int Year { get; protected set; }

        /// <summary>
        /// Gets or sets Month.
        /// </summary>
        public int Month { get; protected set; }

        /// <summary>
        /// Gets or sets Download amount.
        /// </summary>
        public Bandwidth Download { get; protected set; }

        /// <summary>
        /// Gets or sets Upload amount.
        /// </summary>
        public Bandwidth Upload { get; protected set; }
    }
}