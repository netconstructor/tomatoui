namespace BandwidthDownloaderUiTests
{
    using System;

    using BandwidthDownloaderUi;
    using BandwidthDownloaderUi.Commands;

    using FakeItEasy;

    using NUnit.Framework;

    using TomatoBandwidth;

    [TestFixture]
    public class CopyErrorReportToClipboardCommandTests
    {
        [Test]
        public void Should_create_report_from_empty_exception()
        {
            var fakeClipboard = A.Fake<ISystemClipboard>();
            var command = new CopyErrorReportToClipboardCommand(fakeClipboard);
            
            command.Execute(new Exception());

            A.CallTo(() => fakeClipboard.ReplaceWith(A<string>.That.Not.IsNullOrEmpty())).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_create_report_from_exception()
        {
            var fakeClipboard = A.Fake<ISystemClipboard>();
            var command = new CopyErrorReportToClipboardCommand(fakeClipboard);
            
            command.Execute(new Exception("Error message"));

            A.CallTo(() => fakeClipboard.ReplaceWith(A<string>.That.Not.IsNullOrEmpty())).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_create_report_from_parse_exception()
        {
            var fakeClipboard = A.Fake<ISystemClipboard>();
            var command = new CopyErrorReportToClipboardCommand(fakeClipboard);

            command.Execute(new ParseException("Error message", "content"));

            A.CallTo(() => fakeClipboard.ReplaceWith(A<string>.That.Not.IsNullOrEmpty())).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void Should_create_report_from_parse_exception_when_content_is_null()
        {
            var fakeClipboard = A.Fake<ISystemClipboard>();
            var command = new CopyErrorReportToClipboardCommand(fakeClipboard);

            command.Execute(new ParseException("Error message", null, null));

            A.CallTo(() => fakeClipboard.ReplaceWith(A<string>.That.Not.IsNullOrEmpty())).MustHaveHappened(Repeated.Exactly.Once);
        }

    }
}