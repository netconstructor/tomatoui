namespace BandwidthDownloaderUi.Views
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Input;

    using BandwidthDownloaderUi.Infra;

    /// <summary>
    /// ViewModel for settings.
    /// </summary>
    public class SettingsViewModel : ViewModel, IDataErrorInfo
    {
        private readonly ISettings settings;

        private string userName;

        private string password;

        private string address;

        private string error;

        private string timeoutText;

        private DelegateCommand saveCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        public SettingsViewModel(ISettings settings)
        {
            this.settings = settings;
            this.userName = settings.UserName;
            this.password = settings.Password;
            this.address = settings.Address;
            this.timeoutText = settings.Timeout.ToString(CultureInfo.InvariantCulture);
            this.saveCommand = new DelegateCommand(this.SaveSettings, this.CanSaveSettings);
        }

        /// <summary>
        /// Gets SaveCommand.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                return this.saveCommand;
            }
        }

        /// <summary>
        /// Gets or sets UserName.
        /// </summary>
        public string UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                this.userName = value;
                this.OnPropertyChanged("UserName");
            }
        }

        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
                this.OnPropertyChanged("Password");
            }
        }

        /// <summary>
        /// Gets or sets Address.
        /// </summary>
        public string Address
        {
            get
            {
                return this.address;
            }

            set
            {
                this.address = value;
                this.OnPropertyChanged("Address");
            }
        }

        /// <summary>
        /// Gets or sets TimeoutText.
        /// </summary>
        public string TimeoutText
        {
            get
            {
                return this.timeoutText;
            }

            set
            {
                this.timeoutText = value;
                this.OnPropertyChanged("TimeoutText");
            }
        }

        /// <summary>
        /// Gets Error.
        /// </summary>
        public string Error
        {
            get
            {
                return this.error;
            }
        }

        /// <summary>
        /// Validates the property.
        /// </summary>
        /// <param name="columnName">
        /// The column name.
        /// </param>
        /// <returns>
        /// Validation error.
        /// </returns>
        public string this[string columnName]
        {
            get
            {
                if ("TimeoutText" == columnName)
                {
                    var msg = this.ValidateTimeout();
                    if (!string.IsNullOrEmpty(msg))
                    {
                        this.saveCommand.OnCanExecuteChanged();
                        return msg;
                    }
                }

                this.saveCommand.OnCanExecuteChanged();
                return null;
            }
        }

        private bool CanSaveSettings(object arg)
        {
            var msg = this.ValidateTimeout();
            return string.IsNullOrEmpty(msg);
        }

        private void SaveSettings(object obj)
        {
            if (!this.CanSaveSettings(obj))
            {
                return;
            }

            this.settings.Address = this.Address;
            this.settings.UserName = this.UserName;
            this.settings.Password = this.Password;
            this.settings.Timeout = int.Parse(this.TimeoutText, CultureInfo.InvariantCulture);
            this.settings.SaveSettings();
        }

        private string ValidateTimeout()
        {
            int result;
            if (!int.TryParse(this.timeoutText, out result))
            {
                return "Timeout must be numeric";
            }

            if (result < 0 || result > 100)
            {
                return "Timeout must be between 0 and 100";
            }

            return null;
        }
    }
}
