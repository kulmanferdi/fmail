using MailKit;
using System;

namespace fmail
{
    /// <summary>
    /// Represents event arguments for when a mail folder is selected.
    /// </summary>
    class FolderSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FolderSelectedEventArgs"/> class.
        /// </summary>
        /// <param name="folder">The selected mail folder.</param>
        public FolderSelectedEventArgs(IMailFolder folder)
        {
            Folder = folder;
        }

        /// <summary>
        /// Gets the selected mail folder.
        /// </summary>
        public IMailFolder Folder
        {
            get; private set;
        }
    }
}
