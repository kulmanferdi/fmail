using MailKit;

namespace fmail
{
    /// <summary>
    /// Represents information about a mail message.
    /// </summary>
    class MessageInfo
    {
        /// <summary>
        /// Gets the summary of the mail message.
        /// </summary>
        public readonly IMessageSummary Summary;

        /// <summary>
        /// Gets or sets the flags associated with the mail message.
        /// </summary>
        public MessageFlags Flags;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageInfo"/> class.
        /// </summary>
        /// <param name="summary">The summary of the mail message.</param>
        public MessageInfo(IMessageSummary summary)
        {
            Summary = summary;

            if (summary.Flags.HasValue)
                Flags = summary.Flags.Value;
        }
    }
}
