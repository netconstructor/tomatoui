namespace BandwidthDownloaderUi.Converters
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts the <see cref="IList.Count"/> to <see cref="Visibility"/>.
    /// </summary>
    [ValueConversion(typeof(IList), typeof(Visibility))]
    public class ListCountToVisibilityValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts the collection element count into visibility.
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
            var list = value as IList;

            if (0 == list.Count)
            {
                // Using hidden so that the UI doesn't jump when we have results.
                return Visibility.Hidden;
            }

            return Visibility.Visible;
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