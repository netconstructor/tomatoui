namespace BandwidthDownloaderUiTests
{
    using System.Globalization;

    using BandwidthDownloaderUi;
    using BandwidthDownloaderUi.Converters;
    using BandwidthDownloaderUi.Views;

    using NUnit.Framework;

    using TomatoBandwidth;

    using System.Collections.Generic;

    [TestFixture]
    public class ChartStatisticsValueConverterTests
    {
        [Test]
        public void Should_return_max_download_value()
        {
            List<MonthlyValue> values = GetValues();

            var converter = new ChartStatisticsValueConverter();
            var result = (double)converter.Convert(values, typeof(double), ChartStatisticsValueConverter.MaxDownload, CultureInfo.InvariantCulture);

            Assert.AreEqual(100, result);
        }

        [Test]
        public void Should_return_min_download_value()
        {
            var values = this.GetValues();

            var converter = new ChartStatisticsValueConverter();
            var result = (double)converter.Convert(values, typeof(double), ChartStatisticsValueConverter.MinDownload, CultureInfo.InvariantCulture);

            Assert.AreEqual(10, result);
        }

        [Test]
        public void Should_return_average_download_value()
        {
            var values = this.GetValues();

            var converter = new ChartStatisticsValueConverter();
            var result = (double)converter.Convert(values, typeof(double), ChartStatisticsValueConverter.AvgDownload, CultureInfo.InvariantCulture);

            Assert.AreEqual(60, result);
        }

        [Test]
        public void Should_return_sum_of_downloads()
        {
            var values = this.GetValues();

            var converter = new ChartStatisticsValueConverter();
            var result = (double)converter.Convert(values, typeof(double), ChartStatisticsValueConverter.SumDownload, CultureInfo.InvariantCulture);

            Assert.AreEqual(180, result);            
        }

        [Test]
        public void Should_return_max_upload_value()
        {
            List<MonthlyValue> values = GetValues();

            var converter = new ChartStatisticsValueConverter();
            var result = (double)converter.Convert(values, typeof(double), ChartStatisticsValueConverter.MaxUpload, CultureInfo.InvariantCulture);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Should_return_min_upload_value()
        {
            var values = this.GetValues();

            var converter = new ChartStatisticsValueConverter();
            var result = (double)converter.Convert(values, typeof(double), ChartStatisticsValueConverter.MinUpload, CultureInfo.InvariantCulture);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Should_return_average_upload_value()
        {
            var values = this.GetValues();

            var converter = new ChartStatisticsValueConverter();
            var result = (double)converter.Convert(values, typeof(double), ChartStatisticsValueConverter.AvgUpload, CultureInfo.InvariantCulture);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Should_return_sum_of_uploads()
        {
            var values = this.GetValues();

            var converter = new ChartStatisticsValueConverter();
            var result = (double)converter.Convert(values, typeof(double), ChartStatisticsValueConverter.SumUpload, CultureInfo.InvariantCulture);

            Assert.AreEqual(6, result);
        }


        private List<MonthlyValue> GetValues()
        {
            var values = new List<MonthlyValue>();
            values.Add(new MonthlyValue(new MonthlyBandwidth(2011, 1, 100, 1)));
            values.Add(new MonthlyValue(new MonthlyBandwidth(2011, 1, 70, 2)));
            values.Add(new MonthlyValue(new MonthlyBandwidth(2011, 1, 10, 3)));

            values.ForEach(x => x.ChangeTransferUnit(TransferUnit.Kilobytes));

            return values;
        }

    }
}