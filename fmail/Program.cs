using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace fmail
{
 
    internal static class Program
    {
               
        // MailKit variables for connection and stuff
        public static ClientCommandPipeline<ImapClient> ImapCommandPipeline { get; private set; }
        public static ClientCommandPipeline<SmtpClient> SmtpCommandPipeLine { get; private set; }

        public static ClientConnection<ImapClient> ImapClientConnection { get; set; }
        public static ClientConnection<SmtpClient> SmtpClientConnection { get; set; }        
                
        public static ImapClient Client { get; set; }
        public static Login Login { get; private set; }
        public static MainView MainView { get; private set; }

        public static SynchronizationContext GuiContext { get; private set; }
        public static TaskScheduler GuiTaskScheduler { get; private set; }
        public static Thread GuiThread { get; private set; }

        // program variables
        public static readonly string CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static readonly int[] SmtpPortOptions = { 25, 465, 587 };
        public static readonly int[] ImapPortOptions = { 143, 993 };

        public static readonly List<Servers> ServerData = new List<Servers>
        {
            new Servers("Gmail", "imap.gmail.com", "smtp.gmail.com"),
            new Servers("Outlook", "imap-mail.outlook.com", "smtp-mail.outlook.com"),
            new Servers("Yahoo", "imap.mail.yahoo.com", "smtp.mail.yahoo.com"),
            new Servers("AOL", "imap.aol.com", "smtp.aol.com")
        };

        public static bool SmtpPortChanged = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Login = new Login();
            MainView = new MainView();
            GuiContext = SynchronizationContext.Current;
            GuiTaskScheduler = new CustomTaskScheduler(GuiContext);
            GuiThread = Thread.CurrentThread;

            ImapCommandPipeline = new ClientCommandPipeline<ImapClient>("IMAP Command Pipeline");
            SmtpCommandPipeLine = new ClientCommandPipeline<SmtpClient>("SMTP Command Pipeline");

            ImapCommandPipeline.CommandFailed += OnCommandFailed;
            SmtpCommandPipeLine.CommandFailed += OnCommandFailed;

            ImapCommandPipeline.ConnectionFailed += OnConnectionFailed;
            SmtpCommandPipeLine.ConnectionFailed += OnConnectionFailed;

            ImapCommandPipeline.AuthenticationFailed += OnAuthenticationFailed;
            SmtpCommandPipeLine.AuthenticationFailed += OnAuthenticationFailed;

            ImapCommandPipeline.Start();
            SmtpCommandPipeLine.Start();

            var imap_client = new ImapClient(new ProtocolLogger("imap.log"));
            var smtp_client = new SmtpClient(new ProtocolLogger("smtp.log"));

            var credentials = new NetworkCredential(string.Empty, string.Empty);

            ImapClientConnection = new ClientConnection<ImapClient>(imap_client, ServerData[0].ImapServer, ImapPortOptions[1], SecureSocketOptions.SslOnConnect, credentials);

            SmtpClientConnection = new ClientConnection<SmtpClient>(smtp_client, ServerData[0].SmtpServer, SmtpPortOptions[1], SecureSocketOptions.SslOnConnect, credentials);

            Application.Run(new Login());
        }

        static void OnConnectionFailed(object state)
        {
            var e = (ConnectionFailedEventArgs<ImapClient>)state;

            MessageBox.Show(MainView, e.Exception.Message, $"Failed to connect to {e.Connection.Host}:{e.Connection.Port}");
            Login.Visible = true;
            MainView.Visible = false;
        }

        static void OnConnectionFailed(object sender, ConnectionFailedEventArgs<ImapClient> e)
        {
            // This event is raised by the ImapClient and will be running in the IMAP Command Pipeline thread. Defer this back to the GUI thread.
            GuiContext.Send(OnConnectionFailed, e);
        }

        static void OnConnectionFailed(object sender, ConnectionFailedEventArgs<SmtpClient> e)
        {
            // This event is raised by the ImapClient and will be running in the IMAP Command Pipeline thread. Defer this back to the GUI thread.
            GuiContext.Send(OnConnectionFailed, e);
        }

        static void OnAuthenticationFailed(object state)
        {
            var e = (AuthenticationFailedEventArgs<ImapClient>)state;
            string errorMessage;

            if (e.Connection.Credentials.UserName.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                errorMessage = "Unsuccesfull login attempt. Make sure to read the user manual to be able to use your google account.";
            }
            if (e.Connection.Credentials.UserName.EndsWith("@outlook.com", StringComparison.OrdinalIgnoreCase))
            {
                errorMessage = "Unsuccesfull login attempt. Anyway...";
            }
            else
            {
                errorMessage = e.Exception.Message;
            }

            MessageBox.Show(MainView, errorMessage, $"Failed to authenticate {e.Connection.Credentials.UserName}");
            Login.Visible = true;
            MainView.Visible = false;
        }

        static void OnAuthenticationFailed(object sender, AuthenticationFailedEventArgs<ImapClient> e)
        {
            // This event is raised by the ImapClient and will be running in the IMAP Command Pipeline thread. Defer this back to the GUI thread.
            GuiContext.Send(OnAuthenticationFailed, e);
        }

        static void OnAuthenticationFailed(object sender, AuthenticationFailedEventArgs<SmtpClient> e)
        {
            // This event is raised by the ImapClient and will be running in the IMAP Command Pipeline thread. Defer this back to the GUI thread.
            GuiContext.Send(OnAuthenticationFailed, e);
        }

        static void OnCommandFailed(object state)
        {
            var e = (CommandFailedEventArgs)state;

            MessageBox.Show(MainView, e.Exception.Message, "Failed to send command.");
        }

        static void OnCommandFailed(object sender, CommandFailedEventArgs e)
        {
            // This event is raised by the ImapClient and will be running in the IMAP Command Pipeline thread. Defer this back to the GUI thread.
            GuiContext.Send(OnCommandFailed, e);
        }

        delegate void InvokeOnMainThreadDelegate();

        public static void RunOnMainThread(Control control, Action action)
        {
            control.Invoke(new InvokeOnMainThreadDelegate(action));
        }        
    }
}
