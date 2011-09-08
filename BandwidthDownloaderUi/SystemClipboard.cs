namespace BandwidthDownloaderUi
{
    using System.Windows;

    /// <summary>
    /// Interface for the clipboard.
    /// </summary>
    public interface ISystemClipboard
    {
        /// <summary>
        /// Replaces the clipboard content.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        void ReplaceWith(string text);
    }

    /// <summary>
    /// System clipboard.
    /// </summary>
    public class SystemClipboard : ISystemClipboard
    {
        /// <summary>
        /// Replaces the clipboard content.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        public void ReplaceWith(string text)
        {
            Clipboard.SetText(text);
        }
    }
}
