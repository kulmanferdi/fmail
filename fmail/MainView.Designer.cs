namespace fmail
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.sendmailview_btn = new System.Windows.Forms.Button();
            this.inboxview_btn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.panel3 = new System.Windows.Forms.Panel();
            this.refresh_btn = new System.Windows.Forms.Button();
            this.inbox_label = new System.Windows.Forms.Label();
            this.Markasunread = new System.Windows.Forms.Button();
            this.DeleteMessage = new System.Windows.Forms.Button();
            this.logout_btn = new System.Windows.Forms.Button();
            this.messageList = new fmail.MessageList();
            this.folderTreeView = new famil.FolderTreeView();
            this.settings1 = new fmail.Settings();
            this.about1 = new fmail.About();
            this.sendNewMail1 = new fmail.SendNewMail();
            this.aboutview_btn = new System.Windows.Forms.Button();
            this.settingsview_btn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.settingsview_btn);
            this.panel1.Controls.Add(this.aboutview_btn);
            this.panel1.Controls.Add(this.logout_btn);
            this.panel1.Controls.Add(this.sendmailview_btn);
            this.panel1.Controls.Add(this.inboxview_btn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(127, 650);
            this.panel1.TabIndex = 0;
            // 
            // sendmailview_btn
            // 
            this.sendmailview_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.sendmailview_btn.FlatAppearance.BorderSize = 0;
            this.sendmailview_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sendmailview_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendmailview_btn.Location = new System.Drawing.Point(0, 56);
            this.sendmailview_btn.Name = "sendmailview_btn";
            this.sendmailview_btn.Size = new System.Drawing.Size(127, 56);
            this.sendmailview_btn.TabIndex = 1;
            this.sendmailview_btn.Text = "SEND MAIL";
            this.sendmailview_btn.UseVisualStyleBackColor = true;
            // 
            // inboxview_btn
            // 
            this.inboxview_btn.Dock = System.Windows.Forms.DockStyle.Top;
            this.inboxview_btn.FlatAppearance.BorderSize = 0;
            this.inboxview_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.inboxview_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inboxview_btn.Location = new System.Drawing.Point(0, 0);
            this.inboxview_btn.Name = "inboxview_btn";
            this.inboxview_btn.Size = new System.Drawing.Size(127, 56);
            this.inboxview_btn.TabIndex = 0;
            this.inboxview_btn.Text = "INBOX";
            this.inboxview_btn.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.webBrowser);
            this.panel2.Controls.Add(this.messageList);
            this.panel2.Controls.Add(this.folderTreeView);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.settings1);
            this.panel2.Controls.Add(this.about1);
            this.panel2.Controls.Add(this.sendNewMail1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(127, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(935, 650);
            this.panel2.TabIndex = 1;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(450, 38);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(485, 612);
            this.webBrowser.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.refresh_btn);
            this.panel3.Controls.Add(this.inbox_label);
            this.panel3.Controls.Add(this.Markasunread);
            this.panel3.Controls.Add(this.DeleteMessage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(935, 38);
            this.panel3.TabIndex = 0;
            // 
            // refresh_btn
            // 
            this.refresh_btn.Location = new System.Drawing.Point(581, 9);
            this.refresh_btn.Name = "refresh_btn";
            this.refresh_btn.Size = new System.Drawing.Size(113, 23);
            this.refresh_btn.TabIndex = 3;
            this.refresh_btn.Text = "Refresh inbox";
            this.refresh_btn.UseVisualStyleBackColor = true;
            // 
            // inbox_label
            // 
            this.inbox_label.AutoSize = true;
            this.inbox_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.inbox_label.Location = new System.Drawing.Point(7, 9);
            this.inbox_label.Name = "inbox_label";
            this.inbox_label.Size = new System.Drawing.Size(126, 25);
            this.inbox_label.TabIndex = 2;
            this.inbox_label.Text = "Your inbox";
            // 
            // Markasunread
            // 
            this.Markasunread.Location = new System.Drawing.Point(700, 9);
            this.Markasunread.Name = "Markasunread";
            this.Markasunread.Size = new System.Drawing.Size(116, 23);
            this.Markasunread.TabIndex = 1;
            this.Markasunread.Text = "Mark as unread";
            this.Markasunread.UseVisualStyleBackColor = true;
            // 
            // DeleteMessage
            // 
            this.DeleteMessage.Location = new System.Drawing.Point(822, 9);
            this.DeleteMessage.Name = "DeleteMessage";
            this.DeleteMessage.Size = new System.Drawing.Size(75, 23);
            this.DeleteMessage.TabIndex = 0;
            this.DeleteMessage.Text = "Delete";
            this.DeleteMessage.UseVisualStyleBackColor = true;
            // 
            // logout_btn
            // 
            this.logout_btn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logout_btn.FlatAppearance.BorderSize = 0;
            this.logout_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.logout_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logout_btn.Location = new System.Drawing.Point(0, 594);
            this.logout_btn.Name = "logout_btn";
            this.logout_btn.Size = new System.Drawing.Size(127, 56);
            this.logout_btn.TabIndex = 4;
            this.logout_btn.Text = "LOG OUT";
            this.logout_btn.UseVisualStyleBackColor = true;
            // 
            // messageList
            // 
            this.messageList.Dock = System.Windows.Forms.DockStyle.Left;
            this.messageList.FullRowSelect = true;
            this.messageList.Location = new System.Drawing.Point(150, 38);
            this.messageList.Name = "messageList";
            this.messageList.Size = new System.Drawing.Size(300, 612);
            this.messageList.TabIndex = 2;
            // 
            // folderTreeView
            // 
            this.folderTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.folderTreeView.FullRowSelect = true;
            this.folderTreeView.Location = new System.Drawing.Point(0, 38);
            this.folderTreeView.Name = "folderTreeView";
            this.folderTreeView.Size = new System.Drawing.Size(150, 612);
            this.folderTreeView.TabIndex = 1;
            // 
            // settings1
            // 
            this.settings1.Location = new System.Drawing.Point(0, 38);
            this.settings1.Name = "settings1";
            this.settings1.Size = new System.Drawing.Size(935, 618);
            this.settings1.TabIndex = 6;
            // 
            // about1
            // 
            this.about1.Location = new System.Drawing.Point(0, 38);
            this.about1.Name = "about1";
            this.about1.Size = new System.Drawing.Size(935, 618);
            this.about1.TabIndex = 5;
            // 
            // sendNewMail1
            // 
            this.sendNewMail1.Location = new System.Drawing.Point(0, 38);
            this.sendNewMail1.Name = "sendNewMail1";
            this.sendNewMail1.Size = new System.Drawing.Size(935, 618);
            this.sendNewMail1.TabIndex = 7;
            // 
            // aboutview_btn
            // 
            this.aboutview_btn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.aboutview_btn.FlatAppearance.BorderSize = 0;
            this.aboutview_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.aboutview_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aboutview_btn.Location = new System.Drawing.Point(0, 538);
            this.aboutview_btn.Name = "aboutview_btn";
            this.aboutview_btn.Size = new System.Drawing.Size(127, 56);
            this.aboutview_btn.TabIndex = 5;
            this.aboutview_btn.Text = "ABOUT";
            this.aboutview_btn.UseVisualStyleBackColor = true;
            // 
            // settingsview_btn
            // 
            this.settingsview_btn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.settingsview_btn.FlatAppearance.BorderSize = 0;
            this.settingsview_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.settingsview_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsview_btn.Location = new System.Drawing.Point(0, 482);
            this.settingsview_btn.Name = "settingsview_btn";
            this.settingsview_btn.Size = new System.Drawing.Size(127, 56);
            this.settingsview_btn.TabIndex = 6;
            this.settingsview_btn.Text = "SETTINGS";
            this.settingsview_btn.UseVisualStyleBackColor = true;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 650);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainView";
            this.Text = "Fmail";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button sendmailview_btn;
        private System.Windows.Forms.Button inboxview_btn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private famil.FolderTreeView folderTreeView;
        private System.Windows.Forms.WebBrowser webBrowser;
        private MessageList messageList;
        private System.Windows.Forms.Button DeleteMessage;
        private System.Windows.Forms.Button Markasunread;       
        private System.Windows.Forms.Label inbox_label;
        private System.Windows.Forms.Button refresh_btn;
        private Settings settings1;
        private About about1;
        private SendNewMail sendNewMail1;
        private System.Windows.Forms.Button logout_btn;
        private System.Windows.Forms.Button settingsview_btn;
        private System.Windows.Forms.Button aboutview_btn;
    }
}