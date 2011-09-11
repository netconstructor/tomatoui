namespace BandwidthDownloaderUiTests
{
    using System;
    using System.Globalization;
    using System.Net;

    using BandwidthDownloaderUi;
    using BandwidthDownloaderUi.Converters;
    using BandwidthDownloaderUi.Views;

    using NUnit.Framework;

    using TomatoBandwidth;

    [TestFixture]
    public class ExceptionParserTests
    {
        [Test]
        public void Should_parse_exception()
        {
            var result = ExceptionParser.ParseException(new Exception("message"));

            Assert.AreEqual("message", result);
        }

        [Test]
        public void Should_parse_ParseException()
        {
            var exception = new ParseException("message", "content", null);

            var result = ExceptionParser.ParseException(exception);
            
            Assert.IsNotNullOrEmpty(result);            
        }

        [Test]
        public void Should_parse_WebException()
        {
            var exception = new WebException("message");

            var result = ExceptionParser.ParseException(exception);

            Assert.IsNotNullOrEmpty(result);
        }
    }
}