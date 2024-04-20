namespace fmail
{

    /// <summary>
    /// Represents email servers information including server name, IMAP server, and SMTP server.
    /// </summary>
    internal class Servers
    {

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the IMAP server address.
        /// </summary>
        public string ImapServer { get; set; }

        /// <summary>
        /// Gets or sets the SMTP server address.
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Servers"/> class with provided server details.
        /// </summary>
        /// <param name="serverName">The name of the server.</param>
        /// <param name="imapServer">The IMAP server address.</param>
        /// <param name="smtpServer">The SMTP server address.</param>
        public Servers(string serverName, string imapServer, string smtpServer)
        {
            ServerName = serverName;
            ImapServer = imapServer;
            SmtpServer = smtpServer;
        }

        /// <summary>
        /// Returns a string representation of the current server information.
        /// </summary>
        /// <returns>A string containing server name, SMTP server, and IMAP server information.</returns>
        public override string ToString()
        {           
            return "Current server: " + ServerName + "\nSmtp: " + SmtpServer + "\nImap: " + ImapServer;
        }    
    }
}
