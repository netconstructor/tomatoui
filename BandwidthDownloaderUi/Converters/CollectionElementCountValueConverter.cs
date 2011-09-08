namespace BandwidthDownloaderUi.Converters
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    using BandwidthDownloaderUi.Views;

    /// <summary>
    /// Converts the <see cref="TransferUnit"/> to short abbreviaton.
    /// </summary>
    [ValueConversion(typeof(IList), typeof(int))]
    public class CollectionElementCountValueConverter : IValueConverter
    {
        /// <summary>
        /// Converts the collection into number of elements.
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
            return list.Count;
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