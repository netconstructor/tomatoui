namespace BandwidthDownloaderUiTests.DailyViewModelTests
{
    using System;

    using BandwidthDownloaderUi;
    using BandwidthDownloaderUi.Commands;
    using BandwidthDownloaderUi.Events;
    using BandwidthDownloaderUi.Views;

    using FakeItEasy;

    using NUnit.Framework;

    using TomatoBandwidth;

    using System.Collections.Generic;

    [TestFixture]
    public class When_Report_Is_Downloaded
    {
        public static ReportDownloadedEventArgs CreateEventArgs()
        {
            var monthly = new List<MonthlyBandwidth>() { new MonthlyBandwidth(2011, 1, 0, 0) };
            var daily = new List<DailyBandwidth> { new DailyBandwidth(2011, 1, 1, 0, 0) };
            var bandWidthReport = new BandwidthReport(monthly, daily);
            return new ReportDownloadedEventArgs(bandWidthReport, DateTime.Now, null);            
        }

        [Test]
        public void Then_values_should_not_be_empty()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new DailyViewModel(fakeDownloadReportCommand);
            
            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            Assert.AreNotEqual(0, vm.Values.Count);
        }
    }
}