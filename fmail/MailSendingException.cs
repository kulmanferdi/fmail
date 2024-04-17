using System;

namespace fmail
{
    /// <summary>
    /// Represents an exception that occurs when unable to send an email using MailKit.
    /// </summary>
    public class MailSendingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailSendingException"/> class.
        /// </summary>
        public MailSendingException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailSendingException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MailSendingException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailSendingException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MailSendingException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailSendingException"/> class with a default error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MailSendingException(Exception innerException) : base("Failed to send email using MailKit.", innerException) { }
       
    }
}
