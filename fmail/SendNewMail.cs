using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Net.Imap;

using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace fmail
{
    public partial class SendNewMail : UserControl
    {
        //placeholders for the textboxes
        private readonly string[] placeholders = new string[] { "To...", "Cc...", "Bcc...", "Subject...", "Enter your message here..." };

        //declaring the lists for the attachments       
        private readonly List<string> attachments;

        /// <summary>
        /// Constructor for the SendNewMail class.
        /// Initializes the form components, sets default values for textboxes, adds event handlers,
        /// and initializes the list for attachments.
        /// </summary>
        public SendNewMail()
        {
            // Initialize the form components
            InitializeComponent();

            // Set default values for textboxes using placeholders array
            SetPlaceholders();

            // Add event handlers for when textboxes gain focus to remove placeholder text
            to.GotFocus += RemoveToText;
            cc.GotFocus += RemoveCcText;
            bcc.GotFocus += RemoveBccText;
            subject.GotFocus += RemoveSubjectText;
            body.GotFocus += RemoveBodyText;

            // Add event handlers for when textboxes lose focus to add placeholder text if empty
            to.LostFocus += AddToText;
            cc.LostFocus += AddCcText;
            bcc.LostFocus += AddBccText;
            subject.LostFocus += AddSubjectText;
            body.LostFocus += AddBodyText;

            // Add event handler for sending email
            send.Click += Send;

            // Add event handler for attaching files to email
            attachfiles.Click += Attach;

            // Add event handler for removing attachments
            attachremove.Click += Remove;

            // Initialize the list to store attachments
            attachments = new List<string>();

            // Set the maximum size of the attached files label
            attachedfiles.MaximumSize = new System.Drawing.Size(300, 0);
            attachedfiles.AutoSize = true;
            attachremove.Enabled = false;

            // Init label content
            UpdateAttachedStatus();
            //attachedfiles.Text = "";
            status_label.Text = "";

            //progress bar
            //backgroundWorker.RunWorkerAsync();
        }
             
        private async void Send(object sender, EventArgs e)
        {
            //status_label.Visible = true;
            //sendingprogress.Visible = true;

            //status_label.Text = "Initializing...";
            //backgroundWorker.ReportProgress(5);
            //declaring the local variables used in the method       
            int emptyCount = 0;

            string username = Login.GetUsername();

            bool subjectEmpty = string.IsNullOrWhiteSpace(subject.Text);
            bool bodyEmpty = string.IsNullOrWhiteSpace(body.Text);
            bool draft = false;

            //backgroundWorker.ReportProgress(15);
            //status_label.Text = "Creating the message";
            //setting up the current message
            MimeMessage currentMessage = new MimeMessage();

            //setting up the sender
            currentMessage.From.Add(new MailboxAddress(username, Program.SmtpClientConnection.Credentials.UserName));


            //setting up the recepients
            SetRecepients(to, currentMessage, emptyCount);
            SetRecepients(cc, currentMessage, emptyCount);
            SetRecepients(bcc, currentMessage, emptyCount);
            //status_label.Text = "Setting recepients...";
            //backgroundWorker.ReportProgress(30);

            // Check if the subject is empty
            if (subjectEmpty)
            {
                DialogResult result = MessageBox.Show(
                    "Warning",
                    "Subject is empty. Do you want to continue?",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                // If the user cancels, return
                if (result == DialogResult.Cancel)
                {
                    return;
                }

                // Clear the subject if the user chooses to continue
                currentMessage.Subject = "";
            }
            else
            {
                //status_label.Text = "Subject is set";
                //backgroundWorker.ReportProgress(50);
                currentMessage.Subject = subject.Text;
            }
            // Check if the body is empty or if there are no attachments
            if (bodyEmpty && (body.Text == null || attachments.Count == 0))
            {
                DialogResult result = MessageBox.Show(
                    "Warning",
                    "Do you wish to send an empty message?",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                // If the user cancels, return
                if (result == DialogResult.Cancel)
                {
                    return;
                }

                // Set the body to empty
                currentMessage.Body = new TextPart("plain")
                {
                    Text = ""
                };
            }
            else
            {
                var builder = new BodyBuilder();

                // Set body text
                if (ContainsHTMLElements(body.Text))
                {
                    builder.HtmlBody = body.Text;
                }
                else builder.TextBody = body.Text;
                

                // Add attachments
                foreach (var attachment in attachments)
                {
                    builder.Attachments.Add(attachment);
                }

                // Set the message body
                currentMessage.Body = builder.ToMessageBody();
            }
            //status_label.Text = "Message body created";
            //backgroundWorker.ReportProgress(66);
            //if all the fields are empty, the message will be saved to drafts
            if (emptyCount == 3)
            {
                draft = true;
            }

            //backgroundWorker.ReportProgress(78);
            //status_label.Text = "Opening client connection";
            //setting up the smtp clien and sending the message
            //configuring the connection
            var client = new SmtpClient();
            _ = Program.SmtpPortOptions[1];
            int portNumber;
            if (Program.SmtpPortChanged)
            {
                portNumber = Settings.smtpPort;
            }
            else
            {
                portNumber = Program.SmtpClientConnection.Port;
            }
            client.Connect(
                Program.SmtpClientConnection.Host,
                portNumber,
                Program.SmtpClientConnection.SslOptions
            );
            client.Authenticate(Program.SmtpClientConnection.Credentials);
            //status_label.Text = "Successfull authentication";
            //backgroundWorker.ReportProgress(82);
            try
            {
                if (draft | draftCheck.Checked)
                {                    
                    AddToDrafts(currentMessage, Program.ImapClientConnection.Credentials.UserName, Program.ImapClientConnection.Credentials.Password);
                    
                }
                client.Send(currentMessage);

                //MessageBox.Show("Mail Sent Successfully!");
                //status_label.Text = "Mail Sent Successfully!";
                //backgroundWorker.ReportProgress(100);
            }
            catch (MailSendingException ex)
            {
                MessageBox.Show(
                    "Error",
                    "An error occurred while sending email: " + ex.Message,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SetPlaceholders();

                if (attachments.Count > 0)
                {
                    RemoveStuff();
                }
                client.Dispose();
            }

            //theoretically, the message should be sent,
            //if not you may have mistyped the email address
            
            //sendingprogress.Visible = false;
            //status_label.Visible = false;
        }

        /// <summary>
        /// Sets the recipients (To, Cc, Bcc) of a MimeMessage based on the content of a TextBox.
        /// </summary>
        /// <param name="t">The TextBox containing email addresses.</param>
        /// <param name="m">The MimeMessage to which recipients will be added.</param>
        private void SetRecepients(System.Windows.Forms.TextBox t, MimeMessage m, int empty)
        {    
            string me = CheckShortcut(t.Text, "me");
            string username = Login.GetUsername();

            // Check if the TextBox contains a placeholder value
            foreach (var placeholder in placeholders)
            {
                if (placeholder == t.Text)
                {           
                    t.Text = "";
                    empty++;
                    return; 
                }

            }

            // Check if the TextBox doesn't contain '@', indicating an invalid email address
            if (!t.Text.Contains("@") && t.Text != "")
            {
                MessageBox.Show(
                    "Error",
                    "Invalid email address.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }
            // Handle special characters for recipient separation ('#', '-', ',', '&', '_')           
            if (t.Text.Contains("#"))
            {
                t.Text = t.Text.Replace("#", ";");
            }

            if (t.Text.Contains("-"))
            {
                t.Text = t.Text.Replace("-", ";");
            }

            if (t.Text.Contains(","))
            {
                t.Text = t.Text.Replace(",", ";");
            }

            if (t.Text.Contains("&"))
            {
                t.Text = t.Text.Replace("&", ";");
            }

            if (t.Text.Contains("_"))
            {
                t.Text = t.Text.Replace("_", ";");
            }

            // Split the TextBox content into individual recipients
            string TEXT = t.Text;
            string[] temp_recepients = TEXT.Split(';');

            // Determine the recipient type (To, Cc, Bcc) based on the TextBox's name
            if (t.Name == "to")
            {
                // Add each recipient to the 'To' collection of the MimeMessage
                foreach (var recepient in temp_recepients)
                {
                    if (recepient == me) m.To.Add(new MailboxAddress(UTF8Encoding.UTF8, username, Program.ImapClientConnection.Credentials.UserName));
                    else
                    {
                        string[] temp = recepient.Split('@');
                        m.To.Add(new MailboxAddress(UTF8Encoding.UTF8, temp[0], recepient));
                    }
                    
                }
            }
            else if (t.Name == "cc")
            {
                // Add each recipient to the 'Cc' collection of the MimeMessage
                foreach (var recepient in temp_recepients)
                {
                    if (recepient == me) m.Cc.Add(new MailboxAddress(UTF8Encoding.UTF8, username, Program.ImapClientConnection.Credentials.UserName));
                    else
                    {
                        string[] temp = recepient.Split('@');
                        m.Cc.Add(new MailboxAddress(UTF8Encoding.UTF8, temp[0], recepient));
                    }
                }
            }
            else if (t.Name == "bcc")
            {
                // Add each recipient to the 'Bcc' collection of the MimeMessage
                foreach (var recepient in temp_recepients)
                {
                    if (recepient == me) m.Bcc.Add(new MailboxAddress(UTF8Encoding.UTF8, username, Program.ImapClientConnection.Credentials.UserName));
                    else
                    {
                        string[] temp = recepient.Split('@');
                        m.Bcc.Add(new MailboxAddress(UTF8Encoding.UTF8, temp[0], recepient));
                    }
                }
            }

        }

        /// <summary>
        /// Event handler for attaching files to the email.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Attach(object sender, EventArgs e)
        {
            // Create a new instance of OpenFileDialog to allow users to select files
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the filter to display all files, enable multi-select, and set the title of the dialog
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "Select file(s) to attach";

            // If the user selects a file and clicks OK
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the path of the selected file(s)               
                foreach (var file in openFileDialog.FileNames)
                {
                    // Add the path of the selected file to the list of attachments
                    attachments.Add(file);

                    // Update the attached files label to display the path of the selected file
                    //attachedfiles.Text = (file + ";\n");
                    attachment_list.Items.Add(file);

                    // Enable the remove attachment button
                    attachremove.Enabled = true;
                }

            }

            // Update the count of attachments
            UpdateAttachedStatus();
        }

        /// <summary>
        /// Adds the specified email message to the Drafts folder of the email client.
        /// If the Drafts folder doesn't exist, it creates one and then adds the message.       
        /// Adds an email message to the drafts folder of the user's email account.
        /// </summary>
        /// <param name="message">The MimeMessage representing the email message to be added to drafts.</param>
        /// <param name="username">The username of the email account.</param>
        /// <param name="password">The password of the email account.</param>
        public void AddToDrafts(MimeMessage message,string username, string password)
        {
                       
            using (var client = new ImapClient())
            {                
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                
                client.Connect(
                   Program.ImapClientConnection.Host,
                   Program.ImapClientConnection.Port,
                   Program.SmtpClientConnection.SslOptions
                );
                
                client.Authenticate(username, password);
                
                var draftFolder = client.GetFolder(SpecialFolder.Drafts);
                
                if (draftFolder != null)
                {
                    draftFolder.Open(FolderAccess.ReadWrite);
                    draftFolder.Append(message, MessageFlags.Draft);
                    draftFolder.Expunge();
                }
                else
                {
                    var toplevel = client.GetFolder(client.PersonalNamespaces[0]);
                    var DraftFolder = toplevel.Create(SpecialFolder.Drafts.ToString(), true);

                    DraftFolder.Open(FolderAccess.ReadWrite);
                    DraftFolder.Append(message, MessageFlags.Draft);
                    DraftFolder.Expunge();
                }
            }               
        }

        /// <summary>
        /// Clears the list of attachments, updates the attached files text box, disables the remove button,
        /// and updates the count of attachments.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">The event data.</param>
        private void Remove(object sender, EventArgs e)
        {
           RemoveStuff();
        }

        /// <summary>
        /// Removes various elements such as attachments, placeholder texts, and disables associated buttons.
        /// </summary>
        private void RemoveStuff()
        {
            // Clear the list of attachments
            attachments.Clear();

            // Clear the attached files text box
            //attachedfiles.Text = "";

            // Disable the remove attachment button
            attachremove.Enabled = false;

            // Update the count of attachments
            attachedcount.Text = "(" + attachments.Count.ToString() + ")";

            // Clear the list of attachments
            attachment_list.Items.Clear();

            //reset the status label
            Thread.Sleep(10);
            status_label.Text = "";
        }

        /// <summary>
        /// Sets default placeholder values for text boxes.
        /// </summary>
        private void SetPlaceholders()
        {
            // Set default values for textboxes using placeholders array
            to.Text = placeholders[0];
            cc.Text = placeholders[1];
            bcc.Text = placeholders[2];
            subject.Text = placeholders[3];
            body.Text = placeholders[4];
        }

        //creating the methods for the event handlers

        /// <summary>
        /// Removes the placeholder text from the "To" field when it matches the predefined placeholder.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">The event data.</param>
        public void RemoveToText(object sender, EventArgs e)
        {
            if (to.Text == placeholders[0])
            {
                to.Text = "";
            }
        }

        /// <summary>
        /// Removes the placeholder text from the "Cc" field when it matches the predefined placeholder.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">The event data.</param>
        public void RemoveCcText(object sender, EventArgs e)
        {
            if (cc.Text == placeholders[1])
            {
                cc.Text = "";
            }
        }

        /// <summary>
        /// Removes the placeholder text from the "Bcc" field when it matches the predefined placeholder.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">The event data.</param>
        public void RemoveBccText(object sender, EventArgs e)
        {
            if (bcc.Text == placeholders[2])
            {
                bcc.Text = "";
            }
        }

        /// <summary>
        /// Removes the placeholder text from the "Subject" field when it matches the predefined placeholder.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">The event data.</param>
        public void RemoveSubjectText(object sender, EventArgs e)
        {
            if (subject.Text == placeholders[3])
            {
                subject.Text = "";
            }
        }

        /// <summary>
        /// Removes the placeholder text from the "Body" field when it matches the predefined placeholder.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">The event data.</param>
        private void RemoveBodyText(object sender, EventArgs e)
        {
            if (body.Text == placeholders[4])
            {
                body.Text = "";
            }
        }

        /// <summary>
        /// Adds the placeholder text to the "To" field if it's empty or contains only whitespace.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">The event data.</param>
        public void AddToText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(to.Text))
            {
                to.Text = placeholders[0];
            }
        }

        /// <summary>
        /// Adds the placeholder text to the "Cc" field if it's empty or contains only whitespace.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">The event data.</param>
        public void AddCcText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cc.Text))
            {
                cc.Text = placeholders[1];
            }
        }

        /// <summary>
        /// Adds the placeholder text to the "Bcc" field if it's empty or contains only whitespace.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">The event data.</param>
        public void AddBccText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bcc.Text))
            {
                bcc.Text = placeholders[2];
            }
        }

        /// <summary>
        /// Adds the placeholder text to the "Subject" field if it's empty or contains only whitespace.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">The event data.</param>
        public void AddSubjectText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(subject.Text))
            {
                subject.Text = placeholders[3];
            }
        }

        /// <summary>
        /// Adds the placeholder text to the "Body" field if it's empty or contains only whitespace.
        /// </summary>
        /// <param name="sender">The object that raises the event.</param>
        /// <param name="e">The event data.</param>
        private void AddBodyText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(body.Text))
            {
                body.Text = placeholders[4];
            }
        }

        /*private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            sendingprogress.Value = e.ProgressPercentage;
            // Set the text.
            this.Text = e.ProgressPercentage.ToString();
        }*/


        /// <summary>
        /// Checks if the textbox contains a shortcut and returns the shortcut if it exists.
        /// </summary>
        /// <param name="textbox">The content of a textbox</param>
        /// <param name="str">The shortcut string.</param>
        private string CheckShortcut(string textbox, string str)
        {
            string[] separators = { ";" };
            string[] parts = textbox.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                if (part.Trim() == str)
                {
                    return str;
                }
            }

            return "";
        }

        /// <summary>
        /// Returns true if the HTML document contains only text.
        /// </summary>
        /// <param name="rootNode">Html root onde</param>
        private static bool HtmlIsJustText(HtmlNode rootNode)
        {
            return rootNode.Descendants().All(n => n.NodeType == HtmlNodeType.Text);
        }

        /// <summary>
        /// Checks if the text contains HTML elements.
        /// </summary>
        /// <param name="text">The text that may contain HTML elements</param>
        public static bool ContainsHTMLElements(string text)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(text);
            return !HtmlIsJustText(doc.DocumentNode);
        }

        /// <summary>
        /// Updates the displayed count of attachments.
        /// </summary>
        private void UpdateAttachedStatus()
        {
            // Update the text of the attachedcount control to display the number of attachments.
            // The count of attachments is obtained from the attachments list and converted to a string.
            // The count is enclosed in parentheses for visual clarity.
            attachedcount.Text = "(" + attachments.Count.ToString() + ")";
        }

        /// <summary>
        /// Hide the progress bar and status label when the user control is shown.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            sendingprogress.Visible = false;            

            base.OnVisibleChanged(e);
        }
    }
}

