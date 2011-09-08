namespace BandwidthDownloaderUi.Commands
{
    using System;
    using System.Globalization;
    using System.Text;

    using BandwidthDownloaderUi.Infra;

    using TomatoBandwidth;

    /// <summary>
    /// Creates the error report and copies it to clipboard.
    /// </summary>
    public class CopyErrorReportToClipboardCommand : Command
    {
        private readonly ISystemClipboard clipboard;

        private ExceptionParser exceptionParser = new ExceptionParser();

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyErrorReportToClipboardCommand"/> class.
        /// </summary>
        /// <param name="clipboard">
        /// The clipboard.
        /// </param>
        public CopyErrorReportToClipboardCommand(ISystemClipboard clipboard)
        {
            this.clipboard = clipboard;
        }

        /// <summary>
        /// Copies the error report to clipboard.
        /// </summary>
        /// <param name="parameter">
        /// The exception.
        /// </param>
        protected override void InternalExecute(object parameter)
        {
            var error = parameter as Exception;

            var errorMessages = this.exceptionParser.ParseException(error);

            var builder = new StringBuilder();

            builder.AppendLine("ATTENTION!");
            builder.AppendLine("Please fill information about your router and tomato firmware it uses");
            builder.AppendLine("You can also fill in your email address");
            builder.AppendLine(string.Empty);

            builder.AppendLine("Bandwidth Downloader UI Error report");
            builder.AppendLine(string.Format("Generated: {0}", DateTime.Now.ToString(CultureInfo.InvariantCulture)));
            builder.AppendLine(string.Empty);

            builder.AppendLine("Contact information");
            builder.AppendLine("Name and/or email: YOUR EMAIL ADDRESS (OPTIONAL)");
            builder.AppendLine(string.Empty);

            builder.AppendLine("Hardware information");
            builder.AppendLine("Router: NAME AND VERSION OF YOUR ROUTER");
            builder.AppendLine("Firmware: NAME AND VERSIO OF THE TOMATO FIRMWARE YOUR ROUTER USES");
            builder.AppendLine(string.Empty);

            builder.AppendLine("Error messages:");
            builder.AppendLine(errorMessages);
            builder.AppendLine("Stack trace:");
            builder.AppendLine(error.ToString());

            var parseException = ExceptionParser.FindException<ParseException>(error);
            if (null != parseException)
            {
                builder.AppendLine("ParseException details");
                builder.Append(parseException.Details);
                builder.AppendLine(string.Empty);
                builder.AppendLine("Page that was parsed");
                builder.Append(parseException.Content);
            }

            builder.AppendLine("End of error report");

            this.clipboard.ReplaceWith(builder.ToString());
        }

        /// <summary>
        /// Checks if command can be executed.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// True if command can be executed.
        /// </returns>
        protected override bool InternalCanExecute(object parameter)
        {
            var error = parameter as Exception;
            return error != null;
        }
    }
}