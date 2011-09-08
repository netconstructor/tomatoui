namespace BandwidthDownloaderUiTests
{
    using System.Globalization;

    using BandwidthDownloaderUi;
    using BandwidthDownloaderUi.Converters;
    using BandwidthDownloaderUi.Views;

    using NUnit.Framework;

    [TestFixture]
    public class TransferUnitValueConverterTests
    {
        [Test]
        public void Should_convert_Kilobytes_to_KB()
        {
            var converter = new TransferUnitValueConverter();

            var result = (string)converter.Convert(TransferUnit.Kilobytes, typeof(TransferUnit), null, CultureInfo.InvariantCulture);

            Assert.AreEqual("KB", result);
        }

        [Test]
        public void Should_convert_Megabytes_to_MB()
        {
            var converter = new TransferUnitValueConverter();

            var result = (string)converter.Convert(TransferUnit.Megabytes, typeof(TransferUnit), null, CultureInfo.InvariantCulture);

            Assert.AreEqual("MB", result);
        }

        [Test]
        public void Should_convert_Gigabytes_to_GB()
        {
            var converter = new TransferUnitValueConverter();

            var result = (string)converter.Convert(TransferUnit.Gigabytes, typeof(TransferUnit), null, CultureInfo.InvariantCulture);

            Assert.AreEqual("GB", result);
        }
    }
}