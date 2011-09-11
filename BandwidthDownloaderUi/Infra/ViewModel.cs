namespace BandwidthDownloaderUi.Infra
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    /// Base class for view models.
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Raised when value of the property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.ThrowExceptionIfPropertyDoesNotExist(propertyName);

            var handler = this.PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Throws exception if the property is not a public property of this object.
        /// </summary>
        /// <param name="propertyName">
        /// The property Name.
        /// </param>
        [Conditional("DEBUG")]
        protected virtual void ThrowExceptionIfPropertyDoesNotExist(string propertyName)
        {
            Type type = this.GetType();
            if (null == type.GetProperty(propertyName))
            {
                var msg = "{0} is not a public property of type {1}".FormatWith(propertyName, type.FullName);
                throw new ArgumentException(msg);
            }
        }
    }
}
