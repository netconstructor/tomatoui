namespace BandwidthDownloaderUiTests.MonthlyViewModelTests
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
            var monthly = new List<MonthlyBandwidth>()
                {
                    new MonthlyBandwidth(2011, 1, 0, 0),
                    new MonthlyBandwidth(2011, 2, 0, 0),
                    new MonthlyBandwidth(2011, 3, 0, 0),
                    new MonthlyBandwidth(2011, 4, 0, 0),
                    new MonthlyBandwidth(2011, 5, 0, 0),
                };
            var daily = new List<DailyBandwidth> { new DailyBandwidth(2011, 1, 1, 0, 0) };
            var bandWidthReport = new BandwidthReport(monthly, daily);
            return new ReportDownloadedEventArgs(bandWidthReport, DateTime.Now, null);            
        }

        [Test]
        public void Then_values_should_not_be_empty()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new MonthlyViewModel(fakeDownloadReportCommand);
            
            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            Assert.AreNotEqual(0, vm.FilteredValues.Count);
        }

        [Test]
        public void Then_filters_should_not_be_empty()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new MonthlyViewModel(fakeDownloadReportCommand);

            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            Assert.AreNotEqual(0, vm.Filters.Count);            
        }

        [Test]
        public void Then_FilterStart_should_be_the_first_value()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new MonthlyViewModel(fakeDownloadReportCommand);

            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            Assert.AreEqual(2011, vm.FilterStart.Year);
            Assert.AreEqual(1, vm.FilterStart.Month);
        }

        [Test]
        public void Then_FilterEnd_should_be_the_last_value()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new MonthlyViewModel(fakeDownloadReportCommand);

            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            Assert.AreEqual(2011, vm.FilterEnd.Year);
            Assert.AreEqual(5, vm.FilterEnd.Month);
        }

        [Test]
        public void And_FilterStart_has_been_set_filtering_should_be_done()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new MonthlyViewModel(fakeDownloadReportCommand);
            vm.FilterStart = new Filter(2011, 2);

            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;
            
            Assert.AreEqual(4, vm.FilteredValues.Count);
        }

        [Test]
        public void And_FilterEnd_has_been_set_filtering_should_be_done()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new MonthlyViewModel(fakeDownloadReportCommand);
            vm.FilterEnd = new Filter(2011, 3);

            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            Assert.AreEqual(3, vm.FilteredValues.Count);

        }
    }
}