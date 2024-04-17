using System;

namespace fmail
{
    /// <summary>
    /// Represents event arguments for a failed command execution.
    /// </summary>
    class CommandFailedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandFailedEventArgs"/> class.
        /// </summary>
        /// <param name="ex">The exception that occurred during command execution.</param>
        public CommandFailedEventArgs(Exception ex)
        {
            Exception = ex;
        }

        /// <summary>
        /// Gets the exception that occurred during command execution.
        /// </summary>
        public Exception Exception
        {
            get; private set;
        }
    }
}
