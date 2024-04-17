using MailKit;
using System;

namespace fmail
{
    /// <summary>
    /// Represents event arguments for a failed connection.
    /// </summary>
    /// <typeparam name="T">Type of the mail service client.</typeparam>
    class ConnectionFailedEventArgs<T> : EventArgs where T : IMailService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionFailedEventArgs{T}"/> class.
        /// </summary>
        /// <param name="connection">The client connection where the connection failed.</param>
        /// <param name="ex">The exception that occurred during connection.</param>
        public ConnectionFailedEventArgs(ClientConnection<T> connection, Exception ex)
        {
            Connection = connection;
            Exception = ex;
        }


        /// <summary>
        /// Gets the client connection where the connection failed.
        /// </summary>
        public ClientConnection<T> Connection
        {
            get; private set;
        }

        /// <summary>
        /// Gets the exception that occurred during connection.
        /// </summary>
        public Exception Exception
        {
            get; private set;
        }
    }
}
