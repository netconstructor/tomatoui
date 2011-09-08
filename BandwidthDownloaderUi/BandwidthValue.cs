namespace BandwidthDownloaderUi
{
    /// <summary>
    /// Base class for different types of bandwidth values.
    /// </summary>
    public abstract class BandwidthValue
    {
        /// <summary>
        /// Gets or sets Download.
        /// </summary>
        public double Download { get; protected set; }

        /// <summary>
        /// Gets or sets Upload.
        /// </summary>
        public double Upload { get; protected set; }
    }
}