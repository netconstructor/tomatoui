namespace TomatoBandwidth
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception which is thrown when parsing fails.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "Enforcing the mandatory Content parameter.")]
    [Serializable]
    public class ParseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="content">
        /// The page content.
        /// </param>
        public ParseException(string message, string content)
            : base(message)
        {
            this.Content = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public ParseException(string message, string content, Exception innerException)
            : base(message, innerException)
        {
            this.Content = content;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="details">
        /// The details.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public ParseException(string message, string content, string details, Exception innerException)
            : this(message, content, innerException)
        {
            this.Details = details;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected ParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Content = info.GetString("Content");
            this.Details = info.GetString("Details");
        }

        /// <summary>
        /// Gets Content.
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Gets Details.
        /// </summary>
        public string Details { get; private set; }

        /// <summary>
        /// Adds custom properties into serialization info.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Content", this.Content);
            info.AddValue("Details", this.Details);
        }
    }
}
