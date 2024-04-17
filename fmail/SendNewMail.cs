using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using MailKit.Net.Smtp;

namespace fmail
{
    public partial class SendNewMail : UserControl
    {
        //placeholders for the textboxes
        private readonly string[] placeholders = new string[] { "To...", "Cc...", "Bcc...", "Subject...", "Enter your message here..." };

        //declaring the lists for the attachments       
        private List<string> attachments;

        /// <summary>
        /// Constructor for the SendNewMail class.
        /// Initializes the form components, sets default values for textboxes, adds event handlers,
        /// and initializes the list for attachments.
        /// </summary>
        public SendNewMail()
        {
            InitializeComponent();

            // Set default values for textboxes using placeholders array
            to.Text = placeholders[0];
            cc.Text = placeholders[1];
            bcc.Text = placeholders[2];
            subject.Text = placeholders[3];

            // Add event handlers for when textboxes gain focus to remove placeholder text
            to.GotFocus += RemoveToText;
            cc.GotFocus += RemoveCcText;
            bcc.GotFocus += RemoveBccText;
            subject.GotFocus += RemoveSubjectText;

            // Add event handlers for when textboxes lose focus to add placeholder text if empty
            to.LostFocus += AddToText;
            cc.LostFocus += AddCcText;
            bcc.LostFocus += AddBccText;
            subject.LostFocus += AddSubjectText;

            // Add event handler for sending email
            send.Click += Send;

            // Add event handler for attaching files to email
            attachfiles.Click += Attach;

            // Initialize the list to store attachments
            attachments = new List<string>();
        }

        private void Send(object sender, EventArgs e)
        {
            //declaring the local variables used in the method
            string username = Login.GetUsername();

            int emptyCount = 0;

            bool subjectEmpty = string.IsNullOrWhiteSpace(subject.Text);
            bool bodyEmpty = string.IsNullOrWhiteSpace(body.Text);

            //checking if the textboxes are empty, then clearing the placeholders
            if (to.Text == placeholders[0])
            {
                to.Text = "";
                emptyCount++;
            }
            if (cc.Text == placeholders[1])
            {
                cc.Text = "";
                emptyCount++;
            }
            if (bcc.Text == placeholders[2])
            {
                bcc.Text = "";
                emptyCount++;
            }
            if (emptyCount == 3)
            {
                MessageBox.Show("Error",
                   "At least 1 recepient address is required.",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error
               );
            }



            //setting up the current message
            MimeMessage currentMessage = new MimeMessage();
            currentMessage.To.Add(new MailboxAddress(username, Program.SmtpClientConnection.Credentials.UserName));
            currentMessage.From.Add(new MailboxAddress(username, Program.SmtpClientConnection.Credentials.UserName));

            //setting up the recepients
            SetRecepients(to, currentMessage);
            SetRecepients(cc, currentMessage);
            SetRecepients(bcc, currentMessage);

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
                builder.TextBody = body.Text;

                // Add attachments
                foreach (var attachment in attachments)
                {
                    builder.Attachments.Add(attachment);
                }

                // Set the message body
                currentMessage.Body = builder.ToMessageBody();
            }

            //setting up the smtp clien and sending the message
            //configuring the connection
            SmtpClient client = new SmtpClient();
            var smtpPort = 0;
            if (Program.SmtpPortChanged)
            {
                smtpPort = Settings.smtpPort;
            }
            else
            {
                smtpPort = Program.SmtpClientConnection.Port;
                client.Connect(
                    Program.SmtpClientConnection.Host,
                    smtpPort,
                    Program.SmtpClientConnection.SslOptions
                );
            }
                client.Authenticate(Program.SmtpClientConnection.Credentials);

                try
                {
                    client.Send(currentMessage);

                    MessageBox.Show("Mail Sent Successfully!");
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
                    to.Text = placeholders[0];
                    cc.Text = placeholders[1];
                    bcc.Text = placeholders[2];

                    subject.Text = placeholders[3];
                    body.Text = "";

                    if (attachments.Count > 0)
                    {
                        attachfiles.Text = "";
                        attachments.Clear();
                    }

                    client.Dispose();
                }

                //theoretically, the message should be sent
            }

            /// <summary>
            /// Sets the recipients (To, Cc, Bcc) of a MimeMessage based on the content of a TextBox.
            /// </summary>
            /// <param name="t">The TextBox containing email addresses.</param>
            /// <param name="m">The MimeMessage to which recipients will be added.</param>
            private void SetRecepients(TextBox t, MimeMessage m)
            {
                // Check if the TextBox is empty or doesn't contain '@', indicating an invalid email address
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

                // If the TextBox is empty, return without further processing
                if (t.Text == "") return;

                // Handle special characters for recipient separation ('##', '#', '-', ',')
                if (t.Text.Contains("##"))
                {
                    bcc.Text = bcc.Text.Replace("##", ";");
                }

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
                    t.Text = t.Text.Replace("-", ";");
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
                        string[] temp = recepient.Split('@');
                        m.To.Add(new MailboxAddress(UTF8Encoding.UTF8, temp[0], recepient));
                    }
                }
                else if (t.Name == "cc")
                {
                    // Add each recipient to the 'Cc' collection of the MimeMessage
                    foreach (var recepient in temp_recepients)
                    {
                        string[] temp = recepient.Split('@');
                        m.Cc.Add(new MailboxAddress(UTF8Encoding.UTF8, temp[0], recepient));
                    }
                }
                else if (t.Name == "bcc")
                {
                    // Add each recipient to the 'Bcc' collection of the MimeMessage
                    foreach (var recepient in temp_recepients)
                    {
                        string[] temp = recepient.Split('@');
                        m.Bcc.Add(new MailboxAddress(UTF8Encoding.UTF8, temp[0], recepient));
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
                // Set the filter to display all files
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
                        attachedfiles.Text = (file + ";\n");
                    }

                }
            }

            //creating the methods for the event handlers
            public void RemoveToText(object sender, EventArgs e)
            {
                if (to.Text == placeholders[0])
                {
                    to.Text = "";
                }
            }

            public void RemoveCcText(object sender, EventArgs e)
            {
                if (cc.Text == placeholders[1])
                {
                    cc.Text = "";
                }
            }

            public void RemoveBccText(object sender, EventArgs e)
            {
                if (bcc.Text == placeholders[2])
                {
                    bcc.Text = "";
                }
            }

            public void RemoveSubjectText(object sender, EventArgs e)
            {
                if (subject.Text == placeholders[3])
                {
                    subject.Text = "";
                }
            }

            public void AddToText(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(to.Text))
                {
                    to.Text = placeholders[0];
                }
            }

            public void AddCcText(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(cc.Text))
                {
                    cc.Text = placeholders[1];
                }
            }

            public void AddBccText(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(bcc.Text))
                {
                    bcc.Text = placeholders[2];
                }
            }
            public void AddSubjectText(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(subject.Text))
                {
                    subject.Text = placeholders[3];
                }
            }
        }
}
