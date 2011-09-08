namespace BandwidthDownloaderUiTests
{
    using System;
    using System.IO;
    using System.Reflection;

    using White.Core;
    using White.Core.Factory;
    using White.Core.UIItems;
    using White.Core.UIItems.WindowItems;

    public class ApplicationRunner : IDisposable
    {
        private bool hasBeenDisposed;

        public Application Application { get; private set; }

        public Window MainWindow { get; private set; }

        public ApplicationRunner()
        {
            this.Application = Application.Launch(GetAbsolutePathToExecutable());
            this.MainWindow = this.Application.GetWindow("Bandwidth Downloader", InitializeOption.NoCache);
        }

        ~ApplicationRunner()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Releases all resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T GetControl<T>(string controlName) where T : UIItem
        {
            var control = this.MainWindow.Get<T>(controlName);
            if (null == control)
            {
                throw new Exception(string.Format("Cannot find control {0} from main window", controlName));
            }

            return control;
        }

        /// <summary>
        /// Releases the unmanaged resources and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.hasBeenDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (null != this.Application)
                {
                    this.Application.Kill();
                }
            }

            this.Application = null;
            this.MainWindow = null;
            this.hasBeenDisposed = true;
        }

        private static string GetAbsolutePathToExecutable()
        {
            var assembly = Assembly.GetAssembly(typeof(ApplicationRunner));
            var uri = new Uri(assembly.CodeBase);
            var assemblyLocation = Path.GetDirectoryName(uri.AbsolutePath);

            // Couldn't figure out nicer way to resolve the root of the solution
            // without hard coding the path (absolute or relative)
            var solutionFolder = new DirectoryInfo(assemblyLocation).Parent.Parent.Parent.Parent.FullName;

            var uiLoation = @"BandwidthDownloaderUi\bin\Debug\BandwidthDownloaderUi.exe";

            var exePath = Path.Combine(solutionFolder, uiLoation);
            return exePath;
        }
    }
}