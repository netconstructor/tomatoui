namespace BandwidthDownloaderUi.Infra
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Interface for base command.
    /// </summary>
    public interface IBaseCommand : ICommand
    {
        /// <summary>
        /// Raised before command is executed.
        /// </summary>
        event EventHandler Executing;

        /// <summary>
        /// Raised after command has been executed.
        /// </summary>
        event EventHandler Executed;

        /// <summary>
        /// Raises the <see cref="ICommand.CanExecuteChanged"/> event.
        /// </summary>
        void OnCanExecuteChanged();
    }

    /// <summary>
    /// Base class for commands.
    /// </summary>
    public class Command : IBaseCommand
    {
        /// <summary>
        /// Raised when can execute condition for the command changes.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Raised before command is executed.
        /// </summary>
        public event EventHandler Executing;

        /// <summary>
        /// Raised after command has been executed.
        /// </summary>
        public event EventHandler Executed;

        /// <summary>
        /// Calls <see cref="InternalExecute"/>.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void Execute(object parameter)
        {
            this.InvokeExecuting();
            this.InternalExecute(parameter);
            this.InvokeExecuted();
        }

        /// <summary>
        /// Calls <see cref="InternalCanExecute"/>.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// True if command can be executed.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return this.InternalCanExecute(parameter);
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

        /// <summary>
        /// Executes the command. Overwrite in derived classes.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        protected virtual void InternalExecute(object parameter)
        {
        }

        /// <summary>
        /// Checks if command can be executed. Overwrite in derived classes.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <returns>
        /// True if command can be executed.
        /// </returns>
        protected virtual bool InternalCanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Raises the <see cref="Executing"/> event.
        /// </summary>
        protected virtual void InvokeExecuting()
        {
            EventHandler handler = this.Executing;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="Executed"/> event.
        /// </summary>
        protected virtual void InvokeExecuted()
        {
            EventHandler handler = this.Executed;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}