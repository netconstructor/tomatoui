namespace BandwidthDownloaderUiTests.AutomatedUITests
{
    using System;

    using BandwidthDownloaderUi;
    using BandwidthDownloaderUi.Commands;

    using FakeItEasy;

    using NUnit.Framework;

    using TomatoBandwidth;

    [TestFixture]
    public class When_Command_is_executed
    {
        [Test]
        public void Then_report_should_be_downloaded()
        {
            var fakeDownloader = A.Fake<IReportDownloader>();
            var fakeSettings = A.Fake<ISettings>();
            A.CallTo(() => fakeSettings.Address).Returns("http://localhost");
            var command = new DownloadReportCommand(fakeDownloader, fakeSettings);

            command.Execute(null);

            A.CallTo(() => fakeDownloader.DownloadAsync(EventThread.CallingThread)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Then_ReportDownloaded_event_should_be_raised()
        {
            var fakeDownloader = A.Fake<IReportDownloader>();
            var fakeSettings = A.Fake<ISettings>();
            A.CallTo(() => fakeSettings.Address).Returns("http://localhost");
            var command = new DownloadReportCommand(fakeDownloader, fakeSettings);
            var eventRaised = false;
            command.ReportDownloaded += (sender, args) => { eventRaised = true; };

            command.Execute(null);

            // Raise the event from the fake.
            var eventArgs = new DownloadCompletedEventArgs(new BandwidthReport(), null);
            fakeDownloader.DownloadCompleted += Raise.With(eventArgs).Now;

            Assert.IsTrue(eventRaised);
        }
    }
}