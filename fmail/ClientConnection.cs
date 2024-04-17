using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MailKit;
using MailKit.Security;

namespace fmail
{
    /// <summary>
    /// Represents a connection to a mail service client.
    /// </summary>
    /// <typeparam name="T">Type of the mail service client.</typeparam>
    class ClientConnection<T> : IDisposable where T : IMailService
    {
        bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConnection{T}"/> class.
        /// </summary>
        /// <param name="client">The mail service client.</param>
        /// <param name="host">The host address of the mail server.</param>
        /// <param name="port">The port number of the mail server.</param>
        /// <param name="sslOptions">The SSL options for the connection.</param>
        /// <param name="credentials">The network credentials for authentication.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="host"/>, <paramref name="credentials"/>, or <paramref name="client"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="port"/> is out of range.</exception>
        public ClientConnection(T client, string host, int port, SecureSocketOptions sslOptions, NetworkCredential credentials)
        {
            Client = client;
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Port = port >= 0 && port <= 65535 ? port : throw new ArgumentOutOfRangeException(nameof(port));
            SslOptions = sslOptions;
            Credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
        }

        /// <summary>
        /// Gets the mail service client.
        /// </summary>
        public T Client { get; private set; }

        /// <summary>
        /// Gets or sets the host address of the mail server.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port number of the mail server.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the SSL options for the connection.
        /// </summary>
        public SecureSocketOptions SslOptions { get; set; }

        /// <summary>
        /// Gets or sets the network credentials for authentication.
        /// </summary>
        public NetworkCredential Credentials { get; set; }

        /// <summary>
        /// Ensures that the client is connected to the mail server.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void EnsureConnected(CancellationToken cancellationToken)
        {
            if (Client.IsConnected)
                return;

            Client.Connect(Host, Port, SslOptions, cancellationToken);
        }

        /// <summary>
        /// Asynchronously ensures that the client is connected to the mail server.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task EnsureConnectedAsync(CancellationToken cancellationToken)
        {
            if (Client.IsConnected)
                return Task.CompletedTask;

            return Client.ConnectAsync(Host, Port, SslOptions, cancellationToken);
        }

        /// <summary>
        /// Ensures that the client is authenticated with the mail server.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        public void EnsureAuthenticated(CancellationToken cancellationToken)
        {
            if (Client.IsAuthenticated)
                return;

            Client.Authenticate(Credentials, cancellationToken);
        }

        /// <summary>
        /// Asynchronously ensures that the client is authenticated with the mail server.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public Task EnsureAuthenticatedAsync(CancellationToken cancellationToken)
        {
            if (Client.IsAuthenticated)
                return Task.CompletedTask;

            return Client.AuthenticateAsync(Credentials, cancellationToken);
        }
        /// <summary>
        /// Disposes the resources used by the client connection, clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>   
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                Client.Dispose();
                disposed = true;
            }
        }
        /// <summary>
        /// Disposes the resources used by the client connection.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
