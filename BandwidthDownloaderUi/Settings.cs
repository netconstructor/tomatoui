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
        /// Gets or sets Address.
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Gets or sets UserName.
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets Timeout.
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        void SaveSettings();
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
            : base(FileOption.FileMustExist)
        {
            this.Address = this.GetValue("Address");
            this.UserName = this.GetValue("UserName");
            this.Password = this.GetValue("Password");
            this.Timeout = this.GetValue<int>("Timeout");
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            this.SetValue("Address", this.Address);
            this.SetValue("UserName", this.UserName);
            this.SetValue("Password", this.Password);
            this.SetValue("Timeout", this.Timeout);
            this.Save();
        }

        /// <summary>
        /// Gets or sets Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets UserName.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets Timeout.
        /// </summary>
        public int Timeout { get; set; }
    }
}
