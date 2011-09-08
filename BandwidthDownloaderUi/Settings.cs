namespace BandwidthDownloaderUi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ApplicationSettings;

    /// <summary>
    /// Interface for application settings.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Gets Address.
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Gets UserName.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets Password.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Gets Timeout.
        /// </summary>
        int Timeout { get; }
    }

    /// <summary>
    /// Application settings.
    /// </summary>
    public class Settings : AppSettings, ISettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            this.Address = this.GetValue("Address");
            this.UserName = this.GetValue("UserName");
            this.Password = this.GetValue("Password");
            this.Timeout = this.GetValue<int>("Timeout");
        }

        /// <summary>
        /// Gets Address.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Gets UserName.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets Password.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Gets Timeout.
        /// </summary>
        public int Timeout { get; private set; }
    }
}
