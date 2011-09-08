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
    public class When_TransferUnit_Changes
    {
        public static ReportDownloadedEventArgs CreateEventArgs()
        {
            var oneGigabytes = 1048576;
            var monthly = new List<MonthlyBandwidth>() { new MonthlyBandwidth(2011, 1, oneGigabytes, oneGigabytes) };
            var daily = new List<DailyBandwidth> { new DailyBandwidth(2011, 1, 1, oneGigabytes, oneGigabytes) };
            var bandWidthReport = new BandwidthReport(monthly, daily);
            return new ReportDownloadedEventArgs(bandWidthReport, DateTime.Now, null);            
        }

        [Test]
        public void Then_Daily_values_are_updated()
        {
            var fakeDownloadReportCommand = A.Fake<IDownloadReportCommand>();
            var vm = new DailyViewModel(fakeDownloadReportCommand);
            vm.TransferUnit= TransferUnit.Kilobytes;

            // This causes the viewmodel to get the initial values
            fakeDownloadReportCommand.ReportDownloaded += Raise.With(CreateEventArgs()).Now;

            // Verify that initial values are using kilobytes
            Assert.AreEqual(vm.Values[0].Download, 1048576);
            Assert.AreEqual(vm.Values[0].Upload, 1048576);

            // Change the transfer unit to gigabytes
            vm.TransferUnit = TransferUnit.Gigabytes;

            Assert.AreEqual(vm.Values[0].Download, 1);
            Assert.AreEqual(vm.Values[0].Upload, 1);
        }
    }
}