namespace BandwidthDownloaderUi.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    using BandwidthDownloaderUi.Views;

    using TomatoBandwidth;

    /// <summary>
    /// Converts the <see cref="TransferUnit"/> to short abbreviaton.
    /// </summary>
    [ValueConversion(typeof(TransferUnit), typeof(String))]
    public class TransferUnitValueConverter : IValueConverter
    {
        private Dictionary<TransferUnit, string> values = new Dictionary<TransferUnit, string>()
            {
                { TransferUnit.Kilobytes, "KB" },
                { TransferUnit.Megabytes, "MB" },
                { TransferUnit.Gigabytes, "GB" }
            };

        /// <summary>
        /// Converts the <see cref="TransferUnit"/> to short abbreviaton.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// Conversion result.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tu = (TransferUnit)Enum.Parse(typeof(TransferUnit), value.ToString(), true);
            return this.values[tu];
        }

        /// <summary>
        /// Converting back is not supported.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// Returns <see cref="DependencyProperty.UnsetValue"/> always.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}