namespace BandwidthDownloaderUi.Infra
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Common utility functions.
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Gets the current system time.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Public for faking in unit tests.")]
        public static Func<DateTime> SystemTime = () => DateTime.Now;
    }
}