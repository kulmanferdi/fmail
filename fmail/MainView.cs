﻿using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using MimeKit;
using MimeKit.Text;

using MailKit;
using MailKit.Net.Imap;

namespace fmail
{
    public partial class MainView : Form
    {
        public MainView()
        {
            InitializeComponent();

            // Attach event handlers for the folder tree view and message list
            folderTreeView.FolderSelected += OnFolderSelected;
            messageList.MessageSelected += OnMessageSelected;
            DeleteMessage.Click += DeleteMessageFromInbox;
            Markasunread.Click += MarkAsUnRead;

            // Attach event handlers for the navigation buttons
            inboxview_btn.Click += ShowInbox;
            sendmailview_btn.Click += ShowSendMail;
            settingsview_btn.Click += ShowSettings;
            aboutview_btn.Click += ShowAbout;
            refresh_btn.Click += RefreshInbox;
            logout_btn.Click += Logout;
                        
            // Set the initial view to the inbox
            sendNewMail1.Visible = false;
            settings1.Visible = false;
            about1.Visible = false;
                        
        }
               
        private void ShowAbout(object sender, EventArgs e)
        {
            // set the label text to "About"
            inbox_label.Text = "About";

            // Hide the other user controls
            DeleteMessage.Visible = false;
            Markasunread.Visible = false;
            refresh_btn.Visible = false;            
            sendNewMail1.Visible = false;
            folderTreeView.Visible = false;
            messageList.Visible = false;
            webBrowser.Visible = false;
            settings1.Visible = false;

            // Show the About user control
            about1.Visible = true;
            about1.BringToFront();
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            // set the label text to "Settings"
            inbox_label.Text = "Settings";

            // Hide the other user controls
            DeleteMessage.Visible = false;
            Markasunread.Visible = false;
            refresh_btn.Visible = false;
            sendNewMail1.Visible = false;
            folderTreeView.Visible = false;
            messageList.Visible = false;
            webBrowser.Visible = false;
            about1.Visible = false;

            // Show the Settings user control
            settings1.Visible = true;
            settings1.BringToFront();
        }

        private void RefreshInbox(object sender, EventArgs e)
        {
            Refresh();
        }

        private void ShowSendMail(object sender, EventArgs e)
        {
            // set the label text to "Send Mail"
            inbox_label.Text = "Send Mail";

            // Hide the other user controls
            DeleteMessage.Visible = false;
            refresh_btn.Visible = false;
            Markasunread.Visible = false;
            folderTreeView.Visible = false;
            messageList.Visible = false;
            webBrowser.Visible = false;
            settings1.Visible = false;
            about1.Visible = false;

            // Show the SendNewMail user control
            sendNewMail1.Visible = true;
            sendNewMail1.BringToFront();
        }

        private void ShowInbox(object sender, EventArgs e)
        {
            // set the label text to "Your inbox"
            inbox_label.Text = "Your inbox";

            // Hide the other user controls
            settings1.Visible = false;
            about1.Visible = false;
            sendNewMail1.Visible = false;

            // Show the inbox user controls
            folderTreeView.Visible = true;
            messageList.Visible = true;
            refresh_btn.Visible = true;
            webBrowser.Visible = true;
            DeleteMessage.Visible = true;
            Markasunread.Visible = true;
        }

        //Not working yet
        private void MarkAsUnRead(object sender, EventArgs e)
        {
            DialogResult unread = MessageBox.Show(
                "Mark Message as unread",
                "Mark this message as unread?",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
            );
            if (unread == DialogResult.OK)
            {               
                CurrentFolder.RemoveFlags(CurrentSelectedMessageUniqueId, MessageFlags.Seen, true);
            }
        }

        //Not working yet
        private void DeleteMessageFromInbox(object sender, EventArgs e)
        {
            DialogResult deleteSelectedMessage = MessageBox.Show(
                "Delete Message",
                "Are you sure to delete this message from your inbox [It will be moved to the trash only]",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
            );
            if (deleteSelectedMessage == DialogResult.OK)
            {  
                CurrentFolder.AddFlags(CurrentSelectedMessageUniqueId, MessageFlags.Deleted, true);
                //Refresh();                
            }
        }

        class RenderMessageCommand : ClientCommand<ImapClient>
        {
            readonly WebBrowser webBrowser;
            readonly IMailFolder folder;
            readonly UniqueId uid;
            readonly BodyPart body;

            string documentText;

            public RenderMessageCommand(ClientConnection<ImapClient> connection, IMailFolder folder, UniqueId uid, BodyPart body, WebBrowser webBrowser) : base(connection)
            {
                this.folder = folder;
                this.uid = uid;
                this.body = body;
                this.webBrowser = webBrowser;
            }

            void RenderMultipartRelated(MultipartRelated related)
            {
                var root = related.Root;
                var text = root as TextPart;

                if (root is Multipart multipart)
                {
                    // Note: the root document can sometimes be a multipart/alternative.
                    // A multipart/alternative is just a collection of alternate views.
                    // The last part is the format that most closely matches what the
                    // user saw in his or her email client's WYSIWYG editor.
                    for (int i = multipart.Count; i > 0; i--)
                    {
                        if (!(multipart[i - 1] is TextPart body))
                            continue;

                        // our preferred mime-type is text/html
                        if (body.ContentType.IsMimeType("text", "html"))
                        {
                            text = body;
                            break;
                        }

                        text/*??*/= body;
                    }
                }

                // check if we have a text/html document
                if (text != null)
                {
                    if (text.ContentType.IsMimeType("text", "html"))
                    {
                        // replace image src urls that refer to related MIME parts with "data:" urls
                        // Note: we could also save the related MIME part content to disk and use
                        // file:// urls instead.
                        var ctx = new MultipartRelatedImageContext(related);
                        var converter = new HtmlToHtml() { HtmlTagCallback = ctx.HtmlTagCallback };
                        var html = converter.Convert(text.Text);

                        documentText = html;
                    }
                    else
                    {
                        RenderText(text);
                    }
                }
                else
                {
                    // we don't know how to render this type of content
                    //documentText = "Error. Unable to render the selected email. You might try it later, in a different patch.";
                    return;
                }
            }

            void DownloadMultipartRelated(IMailFolder folder, UniqueId uid, BodyPartMultipart bodyPart, CancellationToken cancellationToken)
            {
                // download the entire multipart/related for simplicity since we'll probably end up needing all of the image attachments anyway...
                var related = folder.GetBodyPart(uid, bodyPart, cancellationToken) as MultipartRelated;

                RenderMultipartRelated(related);
            }

            void RenderText(TextPart text)
            {
                string html;

                if (text.IsHtml)
                {
                    // the text content is already in HTML format
                    html = text.Text;
                }
                else if (text.IsFlowed)
                {
                    var converter = new FlowedToHtml();

                    // the delsp parameter specifies whether or not to delete spaces at the end of flowed lines
                    if (!text.ContentType.Parameters.TryGetValue("delsp", out string delsp))
                        delsp = "no";

                    if (string.Compare(delsp, "yes", StringComparison.OrdinalIgnoreCase) == 0)
                        converter.DeleteSpace = true;

                    html = converter.Convert(text.Text);
                }
                else
                {
                    html = new TextToHtml().Convert(text.Text);
                }

                documentText = html;
            }

            void DownloadTextPart(IMailFolder folder, UniqueId uid, BodyPartText bodyPart, CancellationToken cancellationToken)
            {
                var entity = folder.GetBodyPart(uid, bodyPart, cancellationToken);

                RenderText((TextPart)entity);
            }

            void DownloadBodyPart(IMailFolder folder, UniqueId uid, BodyPart body, CancellationToken cancellationToken)
            {
                var multipart = body as BodyPartMultipart;

                if (multipart != null && body.ContentType.IsMimeType("multipart", "related"))
                {
                    DownloadMultipartRelated(folder, uid, multipart, cancellationToken);
                    return;
                }

                var text = body as BodyPartText;

                if (multipart != null)
                {
                    if (multipart.ContentType.IsMimeType("multipart", "alternative"))
                    {
                        // A multipart/alternative is just a collection of alternate views.
                        // The last part is the format that most closely matches what the
                        // user saw in his or her email client's WYSIWYG editor.
                        for (int i = multipart.BodyParts.Count; i > 0; i--)
                        {
                            if (multipart.BodyParts[i - 1] is BodyPartMultipart multi && multi.ContentType.IsMimeType("multipart", "related"))
                            {
                                if (multi.BodyParts.Count == 0)
                                    continue;

                                var start = multi.ContentType.Parameters["start"];
                                var root = multi.BodyParts[0];

                                if (!string.IsNullOrEmpty(start))
                                {
                                    // if the 'start' parameter is set, it overrides the default behavior of using the first
                                    // body part as the main document.
                                    root = multi.BodyParts.OfType<BodyPartText>().FirstOrDefault(x => x.ContentId == start);
                                }

                                if (root != null && root.ContentType.IsMimeType("text", "html"))
                                {
                                    DownloadBodyPart(folder, uid, multi, cancellationToken);
                                    return;
                                }

                                continue;
                            }

                            text = multipart.BodyParts[i - 1] as BodyPartText;

                            if (text != null)
                            {
                                DownloadTextPart(folder, uid, text, cancellationToken);
                                return;
                            }
                        }
                    }
                    else if (multipart.BodyParts.Count > 0)
                    {
                        // The main message body is usually the first part of a multipart/mixed.
                        DownloadBodyPart(folder, uid, multipart.BodyParts[0], cancellationToken);
                    }
                }
                else if (text != null)
                {
                    DownloadTextPart(folder, uid, text, cancellationToken);
                }
            }

            public override void Run(CancellationToken cancellationToken)
            {
                if (!folder.IsOpen)
                    folder.Open(FolderAccess.ReadWrite, cancellationToken);

                DownloadBodyPart(folder, uid, body, cancellationToken);

                // Proxy the DownloadBodyPart() method call to the GUI thread.
                Program.RunOnMainThread(webBrowser, Render);
            }

            void Render()
            {
                webBrowser.DocumentText = documentText;
            }
        }
        public UniqueId CurrentSelectedMessageUniqueId;
        public IMailFolder CurrentFolder;

        /// <summary>
        /// Event handler for when a message is selected, rendering the message in the web browser and marking it as "Seen".
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data containing information about the selected message.</param>
        void OnMessageSelected(object sender, MessageSelectedEventArgs e)
        {
            var command = new RenderMessageCommand(Program.ImapClientConnection, e.Folder, e.UniqueId, e.Body, webBrowser);
            var currentFolder = e.Folder;
            currentFolder.AddFlags(e.UniqueId, MessageFlags.Seen, true);
            Program.ImapCommandPipeline.Enqueue(command);
            CurrentSelectedMessageUniqueId = e.UniqueId;
            CurrentFolder = e.Folder;
        }

        /// <summary>
        /// Gets the unique identifier of the selected message.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data containing information about the selected message.</param>
        /// <returns>The unique identifier of the selected message.</returns>
        UniqueId GetSelectedMessageIndex(object sender, MessageSelectedEventArgs e)
        {
            return e.UniqueId;

            //yet to implement
        }      
      

        void OnFolderSelected(object sender, FolderSelectedEventArgs e)
        {
            messageList.OpenFolder(e.Folder);
        }

        public void LoadContent()
        {
            folderTreeView.LoadFolders();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Exit();
        }

        /// <summary>
        /// Refreshes the user interface by refreshing and loading the folders in the folder tree view.
        /// </summary>
        public override void Refresh()
        {
            folderTreeView.RefreshFolders();
            folderTreeView.LoadFolders();
        }

        /// <summary>
        /// Logs out the current user, disposing IMAP and SMTP client connections and clearing credentials.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void Logout(object sender, EventArgs e)
        {
            // Prompt the user for confirmation before logging out
            DialogResult logout = MessageBox.Show(
                   "Logout",
                   "Are you sure you want to logout?",
                   MessageBoxButtons.OKCancel,
                   MessageBoxIcon.Question,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly
             );

            // If the user confirms logout, dispose client connections and clear credentials
            if (logout == DialogResult.OK)
            {
                Program.ImapClientConnection.Dispose();
                Program.SmtpClientConnection.Dispose();
                Program.ClearCredentials();

                // Restart the application
                Application.Restart();
            }           
        }
    }
}
