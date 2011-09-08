namespace TomatoBandwidth
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Extension methods.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Disposes the object if it is not null.
        /// </summary>
        /// <param name="disposable">
        /// The disposable.
        /// </param>
        public static void DisposeIfNotNull(this IDisposable disposable)
        {
            if (null != disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// Formats the string.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// Formatted string.
        /// </returns>
        public static string FormatWith(this string value, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, value, args);
        }
    }
}
