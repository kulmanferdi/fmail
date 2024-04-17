using MailKit;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace fmail
{
    /// <summary>
    /// Represents a pipeline for executing client commands in a background thread.
    /// </summary>
    /// <typeparam name="T">Type of the mail service client.</typeparam>
    class ClientCommandPipeline<T> where T : IMailService
    {
        readonly ConcurrentQueue<ClientCommand<T>> queue;
        readonly CancellationTokenSource cancellation;
        readonly ManualResetEvent resetEvent;
        readonly Thread thread;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientCommandPipeline{T}"/> class with the specified name.
        /// </summary>
        /// <param name="name">The name of the pipeline thread.</param>
        public ClientCommandPipeline(string name)
        {
            cancellation = new CancellationTokenSource();
            queue = new ConcurrentQueue<ClientCommand<T>>();
            resetEvent = new ManualResetEvent(false);
            thread = new Thread(MainLoop)
            {
                Name = name,
                IsBackground = true,
            };
        }

        /// <summary>
        /// Starts processing client commands in the pipeline.
        /// </summary>
        public void Start()
        {
            if (thread.ThreadState.HasFlag(ThreadState.Unstarted))
                thread.Start();
        }

        /// <summary>
        /// Stops the pipeline, aborting the processing thread.
        /// </summary>
        public void Stop()
        {
            if (!thread.ThreadState.HasFlag(ThreadState.Running))
                return;

            cancellation.Cancel();
            resetEvent.Set();
            thread.Abort();
        }

        /// <summary>
        /// Enqueues a client command to be processed in the pipeline.
        /// </summary>
        /// <param name="command">The client command to enqueue.</param>
        public void Enqueue(ClientCommand<T> command)
        {
            queue.Enqueue(command);
            resetEvent.Set();
        }

        /// <summary>
        /// Event raised when a connection to the mail service fails.
        /// </summary>
        public event EventHandler<ConnectionFailedEventArgs<T>> ConnectionFailed;

        /// <summary>
        /// Event raised when authentication with the mail service fails.
        /// </summary>
        public event EventHandler<AuthenticationFailedEventArgs<T>> AuthenticationFailed;

        /// <summary>
        /// Event raised when a client command execution fails.
        /// </summary>
        public event EventHandler<CommandFailedEventArgs> CommandFailed;

        void MainLoop()
        {
            while (!cancellation.IsCancellationRequested)
            {
                if (queue.TryDequeue(out var command))
                {
                    try
                    {
                        command.Connection.EnsureConnected(cancellation.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        ConnectionFailed?.Invoke(this, new ConnectionFailedEventArgs<T>(command.Connection, ex));
                        continue;
                    }

                    try
                    {
                        command.Connection.EnsureAuthenticated(cancellation.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        AuthenticationFailed?.Invoke(this, new AuthenticationFailedEventArgs<T>(command.Connection, ex));
                        continue;
                    }

                    try
                    {
                        command.Run(cancellation.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        CommandFailed?.Invoke(this, new CommandFailedEventArgs(ex));
                        continue;
                    }
                }
                else
                {
                    resetEvent.WaitOne();
                    resetEvent.Reset();
                }
            }
        }
    }
}
