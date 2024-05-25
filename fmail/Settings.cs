using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace fmail
{

    /// <summary>
    /// Represents the Settings user control, allowing users to configure IMAP and SMTP ports.
    /// </summary>
    public partial class Settings : UserControl
    {

        /// <summary>
        /// Gets or sets the IMAP port number.
        /// </summary>
        public static int imapPort { get; set; }

        /// <summary>
        /// Gets or sets the SMTP port number.
        /// </summary>
        public static int smtpPort { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            InitializeComponent();

            // Attach the event handler for the save button click event
            save_btn.Click += SaveClicked;
            clr_draft.Click += ClearDraftsClicked;
            clr_spam.Click += ClearSpamClicked;

            imapPort = 465;
            smtpPort = 993;           

            // Set the drop-down style for the combo boxes
            imap_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            smtp_combo.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        /// <summary>
        /// Clears the contents of the "Spam" folder by marking all emails within it as "Deleted" without notifying the mail server.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void ClearSpamClicked(object sender, EventArgs e)
        {
            IMailFolder folder = Program.ImapClientConnection.Client.GetFolder("Spam");
            var uids = folder.Search(SearchQuery.All);
            foreach (var uid in uids)
            {
                folder.Store(uid, new StoreFlagsRequest(StoreAction.Add, MessageFlags.Deleted) { Silent = true });
            }            
            folder.Expunge();
        }

        /// <summary>
        /// Clears the contents of the "Drafts" folder by marking all emails within it as "Deleted" without notifying the mail server.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void ClearDraftsClicked(object sender, EventArgs e)
        {
            IMailFolder folder = Program.ImapClientConnection.Client.GetFolder("Drafts");
            var uids = folder.Search(SearchQuery.All);
            foreach (var uid in uids)
            {
                folder.Store(uid, new StoreFlagsRequest(StoreAction.Add, MessageFlags.Deleted) { Silent = true });
            }
            folder.Expunge();
        }

        /// <summary>
        /// Event handler for the save button click event, which saves the configured IMAP and SMTP ports.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void SaveClicked(object sender, EventArgs e)
        {
            // Update the IMAP and SMTP port settings in the program
            Program.ImapClientConnection.Port = int.Parse(imap_combo.Text);
            Program.SmtpClientConnection.Port = int.Parse(smtp_combo.Text);

            // Update the static properties with the configured port numbers
            imapPort = int.Parse(imap_combo.Text);
            smtpPort = int.Parse(smtp_combo.Text);

            // Indicate that the SMTP port has been changed
            Program.SmtpPortChanged = true;

            // Dispose the existing IMAP client connection
            Program.ImapClientConnection.Dispose();

            // Create a new IMAP client connection with the updated port
            Program.ImapClientConnection = new ClientConnection<ImapClient>(Program.ImapClientConnection.Client, Program.ImapClientConnection.Host, int.Parse(imap_combo.Text), SecureSocketOptions.SslOnConnect, Program.ImapClientConnection.Credentials); 
                       
        }

        /// <summary>
        /// Disables the clear drafts and clear spam buttons when the user control is hidden.
        /// Sets the default IMAP and SMTP port numbers when the user control is shown.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            imap_combo.Text = imapPort.ToString();
            smtp_combo.Text = smtpPort.ToString();


            clr_draft.Visible = false;
            clr_spam.Visible = false;
            clr_label.Visible = false;


            clr_draft.Enabled = false;
            clr_spam.Enabled = false;

            base.OnVisibleChanged(e);
        }

    }
}
