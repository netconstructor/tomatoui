namespace BandwidthDownloaderUi.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    
    /// <summary>
    /// Calculates statistics value for collection of bandwidth values.
    /// </summary>
    [ValueConversion(typeof(List<BandwidthValue>), typeof(double))]
    public class ChartStatisticsValueConverter : IValueConverter
    {
        /// <summary>
        /// MaxDownload ConverterParameter.
        /// </summary>
        public const string MaxDownload = "MaxDownload";

        /// <summary>
        /// MinDownload ConverterParameter.
        /// </summary>        
        public const string MinDownload = "MinDownload";

        /// <summary>
        /// AvgDownload ConverterParameter.
        /// </summary>        
        public const string AvgDownload = "AvgDownload";

        /// <summary>
        /// SumDownload ConverterParameter.
        /// </summary>
        public const string SumDownload = "SumDownload";

        /// <summary>
        /// MaxUpload ConverterParameter.
        /// </summary>
        public const string MaxUpload = "MaxUpload";

        /// <summary>
        /// MinUpload ConverterParameter.
        /// </summary>
        public const string MinUpload = "MinUpload";

        /// <summary>
        /// AvgUpload ConverterParameter.
        /// </summary>        
        public const string AvgUpload = "AvgUpload";

        /// <summary>
        /// SumUpload ConverterParameter.
        /// </summary>
        public const string SumUpload = "SumUpload";

        private Dictionary<string, Func<IEnumerable<BandwidthValue>, double>> functions = new Dictionary<string, Func<IEnumerable<BandwidthValue>, double>>
            {
                { MaxDownload, (enumerable) => { return enumerable.Max(x => x.Download); } },
                { MinDownload, (enumerable) => { return enumerable.Min(x => x.Download); } },
                { AvgDownload, (enumerable) => { return enumerable.Average(x => x.Download); } },
                { SumDownload, (enumerable) => { return enumerable.Sum(x => x.Download); } },
                { MaxUpload, (enumerable) => { return enumerable.Max(x => x.Upload); } },
                { MinUpload, (enumerable) => { return enumerable.Min(x => x.Upload); } },
                { AvgUpload, (enumerable) => { return enumerable.Average(x => x.Upload); } },
                { SumUpload, (enumerable) => { return enumerable.Sum(x => x.Upload); } }
            };

        /// <summary>
        /// Calculates statistics for bandwidth values.
        /// </summary>
        /// <param name="value">
        /// The bandwidth values.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The statistics type.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// Calculated statistics value.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var temp = value as System.Collections.IList;
            var items = new List<BandwidthValue>();

            foreach (var obj in temp)
            {
                items.Add(obj as BandwidthValue);
            }

            if (0 == items.Count)
            {
                return 0;
            }

            var stat = parameter.ToString();

            var function = this.functions[stat];
            var result = function(items);
            return Math.Round(result, 0);
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