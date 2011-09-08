namespace BandwidthDownloaderUiTests.AutomatedUITests
{
    using System;

    using BandwidthDownloaderUi;
    using BandwidthDownloaderUi.Commands;
    using BandwidthDownloaderUi.Events;

    using FakeItEasy;

    using NUnit.Framework;

    using TomatoBandwidth;
    using System.Collections.Generic;

    [TestFixture]
    public class When_Error_is_received
    {
        private static BandwidthReport CreateReport()
        {
            var monthly = new List<MonthlyBandwidth> { new MonthlyBandwidth(2011, 1, 0, 0)};
            var daily = new List<DailyBandwidth> { new DailyBandwidth(2011, 1, 1, 0, 0) };
            return new BandwidthReport(monthly, daily);
        }

        [Test]
        public void Then_Monthly_values_should_be_empty()
        {
            var fakeDownloader = A.Fake<IReportDownloader>();
            var fakeSettings = A.Fake<ISettings>();
            A.CallTo(() => fakeSettings.Address).Returns("http://localhost");
            var command = new DownloadReportCommand(fakeDownloader, fakeSettings);

            var itemCount = 0;

            command.ReportDownloaded += (sender, args) =>
                {
                    itemCount = args.Monthly.Count;
                };

            command.Execute(null);

            // Raise the event from the fake.
            var eventArgs = new DownloadCompletedEventArgs(CreateReport(), new Exception());
            fakeDownloader.DownloadCompleted += Raise.With(eventArgs).Now;

            Assert.AreEqual(0, itemCount);
        }

        [Test]
        public void Then_Daily_values_should_be_empty()
        {
            var fakeDownloader = A.Fake<IReportDownloader>();
            var fakeSettings = A.Fake<ISettings>();
            A.CallTo(() => fakeSettings.Address).Returns("http://localhost");
            var command = new DownloadReportCommand(fakeDownloader, fakeSettings);

            var itemCount = 0;

            command.ReportDownloaded += (sender, args) =>
            {
                itemCount = args.Daily.Count;
            };

            command.Execute(null);

            // Raise the event from the fake.
            var eventArgs = new DownloadCompletedEventArgs(CreateReport(), new Exception());
            fakeDownloader.DownloadCompleted += Raise.With(eventArgs).Now;

            Assert.AreEqual(0, itemCount);
        }
    }
}