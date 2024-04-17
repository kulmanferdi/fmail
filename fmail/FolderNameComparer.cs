using MailKit;
using System;
using System.Collections.Generic;

namespace fmail
{
    /// <summary>
    /// Represents a comparer for sorting mail folders based on their names.
    /// </summary>
    class FolderNameComparer : IComparer<IMailFolder>
    {
        public static readonly FolderNameComparer Default = new FolderNameComparer();

        /// <summary>
        /// Compares two mail folders based on their names.
        /// </summary>
        /// <param name="x">The first mail folder to compare.</param>
        /// <param name="y">The second mail folder to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of x and y:
        /// - Less than 0: x precedes y in the sort order.
        /// - 0: x and y have the same position in the sort order.
        /// - Greater than 0: x follows y in the sort order.
        /// </returns>
        public int Compare(IMailFolder x, IMailFolder y)
        {
            return string.Compare(x.Name, y.Name, StringComparison.CurrentCulture);
        }
    }
}
