namespace BandwidthDownloaderUi.Infra
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Base class for commands.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> execute;

        private readonly Func<object, bool> canExecute;

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Raised when can execute condition for the command changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public virtual void RaiseCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (null != handler)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}