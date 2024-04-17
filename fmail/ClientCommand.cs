using MailKit;
using System;
using System.Threading;

namespace fmail
{

    /// <summary>
    /// Represents an abstract client command to be executed within a <see cref="ClientCommandPipeline{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of the mail service client.</typeparam>
    abstract class ClientCommand<T> where T : IMailService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCommand{T}"/> class.
        /// </summary>
        /// <param name="connection">The client connection to be used for the command.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="connection"/> is null.</exception>
        protected ClientCommand(ClientConnection<T> connection)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>
        /// Gets the client connection used for the command.
        /// </summary>
        public ClientConnection<T> Connection { get; private set; }

        /// <summary>
        /// Run the client command.
        /// </summary>
        /// <remarks>
        /// <para>Runs the client command.</para>
        /// <para>This method will be called by the <see cref="ClientCommandPipeline{T}"/> on a background thread.</para>
        /// </remarks>
        /// <param name="cancellationToken">The cancellation token.</param>
        public abstract void Run(CancellationToken cancellationToken);
    }
}
