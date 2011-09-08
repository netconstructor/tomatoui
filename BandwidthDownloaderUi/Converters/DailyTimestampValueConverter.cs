namespace BandwidthDownloaderUi.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    using TomatoBandwidth;

    /// <summary>
    /// Converts the timestamp in <see cref="DailyValue"/>
    /// into string representation.
    /// </summary>
    [ValueConversion(typeof(DailyValue), typeof(String))]
    public class DailyTimestampValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts <see cref="DailyValue"/> timestamp into string representation.
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
            var bandwidth = value as DailyValue;
            var result = bandwidth.Timestamp.ToString("d.M");
            return result;
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