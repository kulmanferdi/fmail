using System;
using System.Net;
using System.Windows.Forms;
using MailKit.Security;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace fmail
{
    public partial class Login : Form
    {
        private const string usertext = "Your email...";
        private const string passwordtext = "Your password...";

        private static string user;

        // <summary>
        /// Initializes a new instance of the Login form.
        /// Sets up event handlers, combo box styles, and displays the current program version.
        /// </summary>
        public Login()
        {
            InitializeComponent();

            // Attach event handlers
            enableSSL_chk.CheckedChanged += EnableSSLChanged;
            password_txt.TextChanged += LoginChanged;
            username_txt.TextChanged += LoginChanged;
            server_combo.TextChanged += ServerChanged;
            port_combo.TextChanged += PortChanged;
            signin_btn.Click += SignInClicked;

            // Attach event handlers for focus events
            username_txt.GotFocus += RemoveUserText;
            password_txt.GotFocus += RemovePasswordText;

            // Attach event handlers for lost focus events
            username_txt.LostFocus += AddUserText;
            password_txt.LostFocus += AddPasswordText;

            // Set the drop-down style for the combo boxes
            server_combo.DropDownStyle = ComboBoxStyle.DropDownList;
            port_combo.DropDownStyle = ComboBoxStyle.DropDownList;

            // Set the version label to display the current version of the program
            version_label2.Text = Program.CurrentVersion;

        }

        /// <summary>
        /// Adds the default password text to the password text box if it's empty or contains only whitespace.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void AddPasswordText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(password_txt.Text))
            {
                password_txt.PasswordChar = '\0';
                password_txt.Text = passwordtext;
            }
        }

        /// <summary>
        /// Adds the default username text to the username text box if it's empty or contains only whitespace.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void AddUserText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username_txt.Text))
            {
                username_txt.Text = usertext;
            }                       
        }

        /// <summary>
        /// Removes the default password text from the password text box when it matches the predefined default text.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void RemovePasswordText(object sender, EventArgs e)
        {
            if (password_txt.Text == passwordtext)
            {
                password_txt.PasswordChar= '*';
                password_txt.Text = "";
            }
        }

        /// <summary>
        /// Removes the default username text from the username text box when it matches the predefined default text.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void RemoveUserText(object sender, EventArgs e)
        {
            if (username_txt.Text == usertext)
            {
                username_txt.Text = "";
            }
        }

        /// <summary>
        /// Initializes the UI components based on the current server settings.
        /// This method is typically called during the form's OnShown event.
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data.</param>
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
            username_txt.Text = usertext;
            password_txt.PasswordChar = '\0';
            password_txt.Text = passwordtext;

            base.OnShown(e);
        }

        /// <summary>
        /// Handles the event when the port selection changes in the combo box.
        /// Sets the SSL checkbox based on the selected port.
        /// </summary>
        /// <param name="sender">The source of the event, typically the port combo box.</param>
        /// <param name="e">An EventArgs object that contains the event data.</param>
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

        /// <summary>
        /// Checks if the user has entered a server, username, and password before enabling the Sign In button.
        /// </summary>
        void CheckLogIn()
        {
            signin_btn.Enabled = !string.IsNullOrEmpty(server_combo.Text) &&
                !string.IsNullOrEmpty(username_txt.Text) &&
                !string.IsNullOrEmpty(password_txt.Text);
        }

        /// <summary>
        /// Handles the event when the server selection changes in the combo box.
        /// Sets the SSL checkbox and port based on the selected server.
        /// </summary>
        /// <param name="sender">The source of the event, typically the server combo box.</param>
        /// <param name="e">An EventArgs object that contains the event data.</param>
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

        /// <summary>
        /// Handles the event when the SSL checkbox state changes.
        /// Sets the port in the combo box based on the SSL checkbox state if no port is specified.
        /// </summary>
        /// <param name="sender">The source of the event, typically the SSL checkbox.</param>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        void EnableSSLChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            var port = port_combo.Text;

            if (string.IsNullOrEmpty(port))
            {
                port_combo.Text = checkbox.Checked ? "993" : "143";
            }
        }

        /// <summary>
        /// Handles the Sign In button click event.
        /// Authenticates the user and configures server connections based on user input.
        /// </summary>
        /// <param name="sender">The source of the event, typically the Sign In button.</param>
        /// <param name="e">An EventArgs object that contains the event data.</param>
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

        /// <summary>
        /// Raises the Closed event.
        /// This override ensures that the application exits when the form is closed.
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Exit(); 
        }

        // Handles the Click event of the cancel_btn control.
        /// Clears the text in the username and password text boxes.
        /// </summary>
        /// <param name="sender">The source of the event, typically the cancel button.</param>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        private void cancel_btn_Click(object sender, EventArgs e)
        {
            username_txt.Clear();
            password_txt.Clear();                        
        }

        /// <summary>
        /// Extracts the username from an email address.
        /// </summary>
        /// <param name="email">The email address from which to extract the username.</param>
        /// <returns>The extracted username.</returns>
        public static string GetUsername()
        {
            // Split the email address by '@' symbol.
            string[] parts = user.Split('@');

            // Return the first part of the split array, which represents the username.
            return parts[0];
        }
    }
}
