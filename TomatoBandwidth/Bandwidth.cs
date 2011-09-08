namespace TomatoBandwidth
{
    using System;

    /// <summary>
    /// Information about single bandwidth amount in kilobytes.
    /// </summary>
    public class Bandwidth
    {
        /// <summary>
        /// Value used when converting kilobytes into megabytes.
        /// </summary>
        public const int Megabyte = 1024;

        /// <summary>
        /// Value used when converting kilobytes into gigabytes.
        /// </summary>
        public const int Gigabyte = 1048576;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bandwidth"/> class.
        /// </summary>
        /// <param name="kilobytes">
        /// The kilobytes.
        /// </param>
        public Bandwidth(long kilobytes)
        {
            this.Kilobytes = kilobytes;    
        }

        /// <summary>
        /// Gets or sets Kilobytes.
        /// </summary>
        public long Kilobytes { get; protected set; }

        /// <summary>
        /// Gets Megabytes.
        /// </summary>
        public double Megabytes
        {
            get
            {
                return this.ToMegabytes(2);
            }
        }

        /// <summary>
        /// Gets Gigabytes.
        /// </summary>
        public double Gigabytes
        {
            get
            {
                return this.ToGigabytes(2);
            }
        }

        /// <summary>
        /// Converts current bandwidth value into megabytes.
        /// </summary>
        /// <param name="digits">
        /// Number of digits in the result.
        /// </param>
        /// <returns>
        /// Bandwidth converted into megabytes.
        /// </returns>
        public virtual double ToMegabytes(int digits)
        {
            var result = (double)this.Kilobytes / Bandwidth.Megabyte;
            return Math.Round(result, digits);
        }

        /// <summary>
        /// Converts current bandwidth into gigabytes.
        /// </summary>
        /// <param name="digits">
        /// Number of digits in the result.
        /// </param>
        /// <returns>
        /// Bandwidth converted into gigabytes.
        /// </returns>
        public virtual double ToGigabytes(int digits)
        {
            var result = (double)this.Kilobytes / Bandwidth.Gigabyte;
            return Math.Round(result, digits);
        }
    }
}