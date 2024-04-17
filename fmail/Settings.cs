using MailKit.Net.Imap;
using MailKit.Security;
using System;
using System.Net;
using System.Windows.Forms;

namespace fmail
{
    public partial class Settings : UserControl
    {

        public static int imapPort { get; set; }
        public static int smtpPort { get; set; }
        public Settings()
        {
            InitializeComponent();

            save_btn.Click += SaveClicked;

            imap_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            smtp_combo.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void SaveClicked(object sender, EventArgs e)
        {
            Program.ImapClientConnection.Port = int.Parse(imap_combo.Text);
            Program.SmtpClientConnection.Port = int.Parse(smtp_combo.Text);

            imapPort = int.Parse(imap_combo.Text);
            smtpPort = int.Parse(smtp_combo.Text);

            Program.SmtpPortChanged = true;

            Program.ImapClientConnection.Dispose();

            Program.ImapClientConnection = new ClientConnection<ImapClient>(Program.ImapClientConnection.Client, Program.ImapClientConnection.Host, int.Parse(imap_combo.Text), SecureSocketOptions.SslOnConnect, Program.ImapClientConnection.Credentials); 
                       
        }
    }
}
