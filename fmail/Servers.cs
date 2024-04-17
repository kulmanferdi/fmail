namespace fmail
{
    internal class Servers
    {
        public string ServerName { get; set; }
        public string ImapServer { get; set; }
        public string SmtpServer { get; set; }

        public Servers(string serverName, string imapServer, string smtpServer)
        {
            ServerName = serverName;
            ImapServer = imapServer;
            SmtpServer = smtpServer;
        }

        public override string ToString()
        {           
            return "Current server: " + ServerName + "\nSmtp: " + SmtpServer + "\nImap: " + ImapServer;
        }    
    }
}
