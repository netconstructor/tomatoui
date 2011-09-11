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

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        /// <param name="execute">
        /// The execute action.
        /// </param>
        /// <param name="canExecute">
        /// The can execute action.
        /// </param>
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Raised when can execute condition for the command changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
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
        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        public virtual void OnCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (null != handler)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}