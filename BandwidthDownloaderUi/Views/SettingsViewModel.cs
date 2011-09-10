namespace BandwidthDownloaderUi.Views
{
    using System.ComponentModel;
    using System.Windows.Input;

    using BandwidthDownloaderUi.Infra;

    public class SettingsViewModel : ViewModel, IDataErrorInfo
    {
        private readonly ISettings settings;

        private string userName;

        private string password;

        private string address;

        private string error;

        private string timeoutText;

        private bool isValid;

        private DelegateCommand saveCommand;

        public SettingsViewModel(ISettings settings)
        {
            this.settings = settings;
            this.UserName = settings.UserName;
            this.Password = settings.Password;
            this.Address = settings.Address;
            this.TimeoutText = settings.Timeout.ToString();
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
                this.RaisePropertyChanged("UserName");
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
                this.RaisePropertyChanged("Password");
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
                this.RaisePropertyChanged("Address");
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
                this.RaisePropertyChanged("TimeoutText");
            }
        }

        public string this[string columnName]
        {
            get
            {
                if ("TimeoutText" == columnName)
                {
                    var msg = this.ValidateTimeout();
                    if (!string.IsNullOrEmpty(msg))
                    {
                        this.saveCommand.RaiseCanExecuteChanged();
                        return msg;
                    }
                }

                this.saveCommand.RaiseCanExecuteChanged();
                return null;
            }
        }

        public string Error
        {
            get
            {
                return this.error;
            }
        }

        private bool CanSaveSettings(object arg)
        {
            var msg = this.ValidateTimeout();
            return string.IsNullOrEmpty(msg);
        }

        private void SaveSettings(object obj)
        {
            this.settings.Address = this.Address;
            this.settings.UserName = this.UserName;
            this.settings.Password = this.Password;
            this.settings.Timeout = int.Parse(this.TimeoutText);
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
