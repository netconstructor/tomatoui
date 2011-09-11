namespace BandwidthDownloaderUiTests.DailyViewModelTests
{
    using System;
    using System.Linq;

    using BandwidthDownloaderUi;
    using BandwidthDownloaderUi.Commands;
    using BandwidthDownloaderUi.Events;
    using BandwidthDownloaderUi.Views;

    using FakeItEasy;

    using NUnit.Framework;

    using TomatoBandwidth;

    using System.Collections.Generic;

    [TestFixture]
    public class When_EndFilter_Changes
    {
        public static ReportDownloadedEventArgs CreateEventArgs()
        {
            var daily = new List<DailyBandwidth>()
                {
                    new DailyBandwidth(2010, 11, 1, 0, 0),
                    new DailyBandwidth(2010, 12, 1, 0, 0),
                    new DailyBandwidth(2011, 1, 1, 0, 0),
                    new DailyBandwidth(2011, 2, 1, 0, 0),
                    new DailyBandwidth(2011, 3, 1, 0, 0),

                };
            var monthly = new List<MonthlyBandwidth>() { new MonthlyBandwidth(2011, 1, 0, 0) };
            var bandWidthReport = new BandwidthReport(monthly, daily);
            return new ReportDownloadedEventArgs(bandWidthReport, DateTime.Now, null);
        }

        [Test]
        public void Then_Values_are_updated()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new DailyViewModel(fakeDownloadReportCommand);
            
            // This causes the viewmodel to get the initial values
            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            Assert.AreEqual(5, vm.FilteredValues.Count, "Before changing filter theres hould be 5 values");

            // Change the end filter
            vm.FilterEnd = new DailyFilter(new DateTime(2011, 1, 1));

            Assert.AreEqual(11, vm.FilteredValues[0].Timestamp.Month);
            Assert.AreEqual(12, vm.FilteredValues[1].Timestamp.Month);
            Assert.AreEqual(1, vm.FilteredValues[2].Timestamp.Month);
        }
    }
}