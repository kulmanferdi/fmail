using MailKit.Net.Imap;
using MailKit.Security;
using System;
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

            // Set the drop-down style for the combo boxes
            imap_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            smtp_combo.DropDownStyle = ComboBoxStyle.DropDownList;

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
    }
}
