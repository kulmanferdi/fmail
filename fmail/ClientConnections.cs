using MailKit;
using System;
using System.Collections.Concurrent;

namespace fmail
{
    /// <summary>
    /// Represents a collection of client connections.
    /// </summary>
    /// <typeparam name="T">Type of the mail service client.</typeparam>
    class ClientConnections<T> where T : IMailService
    {
        readonly ConcurrentDictionary<object, ClientConnection<T>> connections;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConnections{T}"/> class.
        /// </summary>
        public ClientConnections()
        {
            connections = new ConcurrentDictionary<object, ClientConnection<T>>();
        }

        /// <summary>
        /// Adds a client connection to the collection.
        /// </summary>
        /// <param name="connection">The client connection to add.</param>
        /// <exception cref="InvalidOperationException">Thrown when a connection with the same sync root already exists in the collection.</exception>
        public void Add(ClientConnection<T> connection)
        {
            if (!connections.TryAdd(connection.Client.SyncRoot, connection))
                throw new InvalidOperationException();
        }

        /// <summary>
        /// Tries to get the client connection associated with the specified mail service client.
        /// </summary>
        /// <param name="client">The mail service client.</param>
        /// <param name="connection">When this method returns, contains the client connection associated with the specified mail service client, if found; otherwise, null.</param>
        /// <returns>true if the collection contains a client connection associated with the specified mail service client; otherwise, false.</returns>
        public bool TryGetValue(IMailService client, out ClientConnection<T> connection)
        {
            client = client ?? throw new ArgumentNullException(nameof(client));

            return connections.TryGetValue(client.SyncRoot, out connection);
        }

        /// <summary>
        /// Tries to get the client connection associated with the specified mail folder.
        /// </summary>
        /// <param name="folder">The mail folder.</param>
        /// <param name="connection">When this method returns, contains the client connection associated with the specified mail folder, if found; otherwise, null.</param>
        /// <returns>true if the collection contains a client connection associated with the specified mail folder; otherwise, false.</returns>
        public bool TryGetValue(IMailFolder folder, out ClientConnection<T> connection)
        {
            folder = folder ?? throw new ArgumentNullException(nameof(folder));

            return connections.TryGetValue(folder.SyncRoot, out connection);
        }
    }
}
