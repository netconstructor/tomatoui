namespace BandwidthDownloaderUi.Converters
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts the <see cref="bool"/> to <see cref="Visibility"/>.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts the <see cref="bool"/> to <see cref="Visibility"/>.
        /// </summary>
        /// <param name="value">
        /// The value which implements <see cref="IList"/>.
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
            var boolValue = (bool)value;

            if (boolValue)
            {
                return Visibility.Visible;
            }

            return Visibility.Hidden;
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