namespace BandwidthDownloaderUi
{
    using System;
    using System.Net;

    using TomatoBandwidth;

    /// <summary>
    /// Used to parse the exception tree.    
    /// </summary>
    public class ExceptionParser
    {
        /// <summary>
        /// Iterates over the exception stack and tries to locate the exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <typeparam name="T">
        /// Type of the exception.
        /// </typeparam>
        /// <returns>
        /// The first exception of correct type or null.
        /// </returns>
        public static T FindException<T>(Exception exception) where T : Exception
        {
            Exception temp = exception;

            while (temp != null)
            {
                var webException = temp as T;
                if (null != webException)
                {
                    return webException;
                }

                temp = temp.InnerException;
            }

            return null;
        }

        /// <summary>
        /// Parses the exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <returns>
        /// Message and possible raw content.
        /// </returns>
        public string ParseException(Exception exception)
        {
            var webException = FindException<WebException>(exception);
            if (null != webException)
            {
                return Parse(webException);
            }

            var parseException = FindException<ParseException>(exception);
            if (null != parseException)
            {
                return Parse(parseException);
            }

            return exception.Message;
        }

        private static string Parse(WebException exception)
        {
            var msg = exception.Message;
            msg += Environment.NewLine;
            msg += "Please check connection settings (i.e. username, password, URL etc).";
            return msg;
        }

        private static string Parse(ParseException exception)
        {
            var msg = "Failed to parse bandwidth information from the web page.";
            msg += Environment.NewLine;
            msg += exception.Message;
            return msg;
        }
    }
}