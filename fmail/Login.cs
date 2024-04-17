using System;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using MailKit.Security;
using YourNamespace;

namespace fmail
{
    public partial class Login : Form
    {
        const string usertext = "Your email...";
        const string passwordtext = "Your password...";

        private static string user;

        public Login()
        {
            InitializeComponent();            

            enableSSL_chk.CheckedChanged += EnableSSLChanged;
            password_txt.TextChanged += LoginChanged;
            username_txt.TextChanged += LoginChanged;
            server_combo.TextChanged += ServerChanged;
            port_combo.TextChanged += PortChanged;
            signin_btn.Click += SignInClicked;
            
            username_txt.Text = usertext;
            password_txt.Text = passwordtext;

            username_txt.GotFocus += RemoveUserText;
            password_txt.GotFocus += RemovePasswordText;

            username_txt.LostFocus += AddUserText;
            password_txt.LostFocus += AddPasswordText;

            server_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            port_combo.DropDownStyle = ComboBoxStyle.DropDownList;

            version_label2.Text = Program.CurrentVersion;

        }

        private void AddPasswordText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(password_txt.Text))
            {
                password_txt.PasswordChar = '\0';
                password_txt.Text = passwordtext;
            }
        }

        private void AddUserText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username_txt.Text))
            {
                username_txt.Text = usertext;
            }
        }

        private void RemovePasswordText(object sender, EventArgs e)
        {
            if (password_txt.Text == passwordtext)
            {
                password_txt.PasswordChar= '*';
                password_txt.Text = "";
            }
        }

        private void RemoveUserText(object sender, EventArgs e)
        {
            if (username_txt.Text == usertext)
            {
                username_txt.Text = "";
            }
        }

        protected override void OnShown(EventArgs e)
        {
            foreach (var server in Program.ServerData)
            {
               if(Program.ImapClientConnection.Host == server.ImapServer)
                {
                    server_combo.Text = server.ServerName;
                    break;
                }                
            }           
            port_combo.Text = Program.ImapClientConnection.Port.ToString();
            enableSSL_chk.Checked = Program.ImapClientConnection.SslOptions == SecureSocketOptions.SslOnConnect;
            username_txt.Text = Program.ImapClientConnection.Credentials.UserName;
            password_txt.Text = Program.ImapClientConnection.Credentials.Password;

            base.OnShown(e);
        }
       
        void PortChanged(object sender, EventArgs e)
        {
            var port = port_combo.Text.Trim();

            switch (port)
            {
                case "143":
                    enableSSL_chk.Checked = false;
                    break;
                case "993":
                    enableSSL_chk.Checked = true;
                    break;
            }
        }
        void CheckLogIn()
        {
            signin_btn.Enabled = !string.IsNullOrEmpty(server_combo.Text) &&
                !string.IsNullOrEmpty(username_txt.Text) &&
                !string.IsNullOrEmpty(password_txt.Text);
        }

        void ServerChanged(object sender, EventArgs e)
        {
            switch (server_combo.Text)
            {
                case "imap.gmail.com":
                case "imap.mail.yahoo.com":
                case "imap-mail.outlook.com":
                    enableSSL_chk.Checked = true;
                    port_combo.Text = "993";
                    break;
            }

            CheckLogIn();
        }

        void LoginChanged(object sender, EventArgs e)
        {
            CheckLogIn();
        }

        void EnableSSLChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var port = port_combo.Text;

            if (string.IsNullOrEmpty(port))
            {
                port_combo.Text = checkbox.Checked ? "993" : "143";
            }
        }

        void SignInClicked(object sender, EventArgs e) 
        {
            try
            {
                if (username_txt.Text != usertext && password_txt.Text != passwordtext) { }              
            }
            catch (InvalidAuthenticationException ex)
            {
                // Handle the specific exception related to invalid authentication data
                Console.WriteLine("Invalid authentication data: " + ex.Message);
                MessageBox.Show(
                   "Error",
                   "Invalid authentication data: " + ex.Message,
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );
            }
            catch (Exception ex)
            {
                // Handle any other exception
                Console.WriteLine("An error occurred: " + ex.Message);
                MessageBox.Show(
                    "Error",
                    "An error occurred: " + ex.Message,
                    MessageBoxButtons.OK,MessageBoxIcon.Error 
                );
            }
            var sslOptions = SecureSocketOptions.StartTlsWhenAvailable;
            var host = server_combo.Text.Trim();
            var passwd = password_txt.Text;
            user = username_txt.Text;
                        
            int port = 0;
            foreach (var server in Program.ServerData)
            {                
                if(host == server.ServerName)                
                {
                    Program.ImapClientConnection.Host = server.ImapServer;
                    Program.SmtpClientConnection.Host = server.SmtpServer;
                   
                    break;
                }
            }
            if (!string.IsNullOrEmpty(port_combo.Text))
            {
                port = int.Parse(port_combo.Text);
            }

            var credentials = new NetworkCredential(user, passwd);
            

            if (enableSSL_chk.Checked)
            { 
                sslOptions = SecureSocketOptions.SslOnConnect;
            }

            //Program.ImapClientConnection.Host = host;
            //Program.SmtpClientConnection.Host = host;

            Program.ImapClientConnection.Port = port;
            Program.SmtpClientConnection.Port = Program.SmtpPortOptions[1];

            Program.ImapClientConnection.SslOptions = sslOptions;
            Program.SmtpClientConnection.SslOptions = sslOptions;
            
            Program.ImapClientConnection.Credentials = credentials;
            Program.SmtpClientConnection.Credentials = credentials;

            Program.MainView.LoadContent();

            Program.MainView.Visible = true;
            Visible = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Exit(); 
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            username_txt.Clear();
            password_txt.Clear();                        
        }
        public static string GetUsername()
        {  
            string[] temp = user.Split('@');

            return temp[0];
        }
    }
}
