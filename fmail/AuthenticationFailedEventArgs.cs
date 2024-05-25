using System;
using MailKit;

namespace fmail
{
    /// <summary>
    /// Provides event arguments for when a client fails to authenticate.
    /// </summary>
    /// <typeparam name="T">The type of mail service client connection.</typeparam>
    class AuthenticationFailedEventArgs<T> : EventArgs where T : IMailService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationFailedEventArgs{T}"/> class.
        /// Event arguments for when a client fails to authenticate.
        /// </summary>
        /// <param name="connection">The client connection where the authentication failed.</param>
        /// <param name="ex">The exception that occurred during authentication.</param>
        public AuthenticationFailedEventArgs(ClientConnection<T> connection, Exception ex)
        {
            Connection = connection;
            Exception = ex;
        }

        /// <summary>
        /// Gets the client connection where the authentication failed.
        /// </summary>
        public ClientConnection<T> Connection
        {
            get; private set;
        }

        /// <summary>
        /// Gets the exception that occurred during authentication.
        /// </summary>
        public Exception Exception
        {
            get; private set;
        }
    }
}
