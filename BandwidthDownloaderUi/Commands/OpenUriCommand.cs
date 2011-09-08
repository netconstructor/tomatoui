namespace BandwidthDownloaderUi.Commands
{
    using System;
    using System.Diagnostics;

    using BandwidthDownloaderUi.Infra;

    /// <summary>
    /// Used to open uri such as email or website in external
    /// application.
    /// </summary>
    public class OpenUriCommand : Command
    {
        /// <summary>
        /// Opens the uri using <see cref="Process.Start()"/>.
        /// </summary>
        /// <param name="parameter">
        /// The <see cref="Uri"/> which is opened.
        /// </param>
        protected override void InternalExecute(object parameter)
        {
            var uri = parameter as Uri;
            Process.Start(new ProcessStartInfo(uri.ToString()));  
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
            var uri = parameter as Uri;
            if (null == uri)
            {
                return false;
            }

            return true;
        }
    }
}