using MailKit;
using MimeKit;
using System;

namespace fmail
{
    /// <summary>
    /// Represents event arguments for when a mail message is selected.
    /// </summary>
    class MessageSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageSelectedEventArgs"/> class.
        /// </summary>
        /// <param name="folder">The mail folder containing the selected message.</param>
        /// <param name="uid">The unique identifier of the selected message.</param>
        /// <param name="body">The body part of the selected message.</param>
        public MessageSelectedEventArgs(IMailFolder folder, UniqueId uid, BodyPart body)
        {
            Folder = folder;
            UniqueId = uid;
            Body = body;
        }

        /// <summary>
        /// Gets the mail folder containing the selected message.
        /// </summary>
        public IMailFolder Folder
        {
            get; private set;
        }

        /// <summary>
        /// Gets the unique identifier of the selected message.
        /// </summary>
        public UniqueId UniqueId
        {
            get; private set;
        }

        /// <summary>
        /// Gets the body part of the selected message.
        /// </summary>
        public BodyPart Body
        {
            get; private set;
        }
        
    }
}
