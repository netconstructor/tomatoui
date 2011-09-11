namespace BandwidthDownloaderUi
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using BandwidthDownloaderUi.Views;

    using TomatoBandwidth;

    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts the bandwidth to specific unit.
        /// </summary>
        /// <param name="bandwidth">
        /// The bandwidth.
        /// </param>
        /// <param name="transferUnit">
        /// The transfer unit.
        /// </param>
        /// <returns>
        /// Conversion result.
        /// </returns>
        public static double To(this Bandwidth bandwidth, TransferUnit transferUnit)
        {
            if (TransferUnit.Kilobytes == transferUnit)
            {
                return bandwidth.Kilobytes;
            }

            if (TransferUnit.Megabytes == transferUnit)
            {
                return bandwidth.Megabytes;
            }

            return bandwidth.Gigabytes;
        }

        /// <summary>
        /// Executes action for each element in the enumeration.
        /// </summary>
        /// <param name="enumerable">
        /// The enumerable.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <typeparam name="T">
        /// Type of the value.
        /// </typeparam>
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var value in enumerable)
            {
                action(value);
            }
        }

        /// <summary>
        /// Checks if the collection contains elements.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the collection.</typeparam>
        /// <param name="collection">Collection to check.</param>
        /// <returns>True if the collection has elements.</returns>
        public static bool HasElements<T>(this IList<T> collection)
        {
            return collection.Count != 0;
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
        internal static string FormatWith(this string value, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, value, args);
        }
    }
}