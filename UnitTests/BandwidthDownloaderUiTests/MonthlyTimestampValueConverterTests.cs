namespace BandwidthDownloaderUiTests
{
    using System.Globalization;
    using System.Windows;

    using BandwidthDownloaderUi;
    using BandwidthDownloaderUi.Converters;

    using NUnit.Framework;

    using TomatoBandwidth;

    [TestFixture]
    public class MonthlyTimestampValueConverterTests
    {
        [Test]
        public void Should_convert_monthly_bandwidth_timestamp_to_string()
        {
            var converter = new MonthlyTimestampValueConverter();

            var monthlyBandwidthValue = new MonthlyValue(new MonthlyBandwidth(2011, 1, 0, 0));

            var result = (string)converter.Convert(monthlyBandwidthValue, typeof(MonthlyValue), null, CultureInfo.InvariantCulture);

            Assert.AreEqual("1/2011", result);
        }

        [Test]
        public void ConvertBack_should_return_DependencyProperty_UnsetValue()
        {
            var converter = new MonthlyTimestampValueConverter();

            var result = converter.ConvertBack(new object(), typeof(MonthlyBandwidth), null, CultureInfo.InvariantCulture);

            Assert.AreEqual(DependencyProperty.UnsetValue, result);
        }
    }
}