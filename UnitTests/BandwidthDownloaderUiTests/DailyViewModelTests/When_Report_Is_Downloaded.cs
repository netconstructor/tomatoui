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
            var daily = new List<DailyBandwidth>()
                {
                    new DailyBandwidth(2011, 1, 1, 0, 0),
                    new DailyBandwidth(2011, 2, 1, 0, 0),
                    new DailyBandwidth(2011, 3, 1, 0, 0),
                    new DailyBandwidth(2011, 4, 1, 0, 0),
                    new DailyBandwidth(2011, 5, 1, 0, 0),
                };
            var monthly = new List<MonthlyBandwidth>() { new MonthlyBandwidth(2011, 1, 0, 0) };
            var bandWidthReport = new BandwidthReport(monthly, daily);
            return new ReportDownloadedEventArgs(bandWidthReport, DateTime.Now, null);
        }


        [Test]
        public void Then_values_should_not_be_empty()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new DailyViewModel(fakeDownloadReportCommand);

            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            Assert.AreNotEqual(0, vm.FilteredValues.Count);
        }

        [Test]
        public void Then_filters_should_not_be_empty()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new DailyViewModel(fakeDownloadReportCommand);

            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            Assert.AreNotEqual(0, vm.Filters.Count);
        }

        [Test]
        public void Then_FilterStart_should_be_the_first_value()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new DailyViewModel(fakeDownloadReportCommand);

            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            var expected = new DateTime(2011, 1, 1);
            Assert.AreEqual(expected, vm.FilterStart.Date);
        }

        [Test]
        public void Then_FilterEnd_should_be_the_last_value()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new DailyViewModel(fakeDownloadReportCommand);

            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            var expected = new DateTime(2011, 5, 1);
            Assert.AreEqual(expected, vm.FilterEnd.Date);
        }

        [Test]
        public void And_FilterStart_has_been_set_filtering_should_be_done()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new DailyViewModel(fakeDownloadReportCommand);
            vm.FilterStart = new DailyFilter(new DateTime(2011, 2, 1));

            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            Assert.AreEqual(4, vm.FilteredValues.Count);
        }

        [Test]
        public void And_FilterEnd_has_been_set_filtering_should_be_done()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new DailyViewModel(fakeDownloadReportCommand);
            vm.FilterEnd = new DailyFilter(new DateTime(2011, 3, 1));

            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            Assert.AreEqual(3, vm.FilteredValues.Count);

        }
    }
}