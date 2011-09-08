namespace BandwidthDownloaderUi.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    using TomatoBandwidth;

    /// <summary>
    /// Converts the year and month information in <see cref="MonthlyValue"/>
    /// into string representation.
    /// </summary>
    [ValueConversion(typeof(MonthlyValue), typeof(String))]
    public class MonthlyTimestampValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts <see cref="MonthlyValue"/> timestamp into string representation.
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
            var monthlyBandwidth = value as MonthlyValue;

            return string.Format("{0}/{1}", monthlyBandwidth.Month, monthlyBandwidth.Year);
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