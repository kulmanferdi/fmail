namespace fmail
{
    partial class SendNewMail
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.to = new System.Windows.Forms.TextBox();
            this.cc = new System.Windows.Forms.TextBox();
            this.bcc = new System.Windows.Forms.TextBox();
            this.subject = new System.Windows.Forms.TextBox();
            this.body = new System.Windows.Forms.TextBox();
            this.attachfiles = new System.Windows.Forms.Button();
            this.send = new System.Windows.Forms.Button();
            this.attachedfiles = new System.Windows.Forms.Label();
            this.attachedcount = new System.Windows.Forms.Label();
            this.attachremove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // to
            // 
            this.to.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.to.Location = new System.Drawing.Point(53, 12);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(820, 26);
            this.to.TabIndex = 0;
            // 
            // cc
            // 
            this.cc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cc.Location = new System.Drawing.Point(53, 44);
            this.cc.Name = "cc";
            this.cc.Size = new System.Drawing.Size(820, 26);
            this.cc.TabIndex = 1;
            // 
            // bcc
            // 
            this.bcc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bcc.Location = new System.Drawing.Point(53, 76);
            this.bcc.Name = "bcc";
            this.bcc.Size = new System.Drawing.Size(820, 26);
            this.bcc.TabIndex = 2;
            // 
            // subject
            // 
            this.subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.subject.Location = new System.Drawing.Point(53, 108);
            this.subject.Name = "subject";
            this.subject.Size = new System.Drawing.Size(820, 26);
            this.subject.TabIndex = 3;
            // 
            // body
            // 
            this.body.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.body.Location = new System.Drawing.Point(53, 140);
            this.body.Multiline = true;
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(820, 341);
            this.body.TabIndex = 4;
            // 
            // attachfiles
            // 
            this.attachfiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.attachfiles.Location = new System.Drawing.Point(53, 487);
            this.attachfiles.Name = "attachfiles";
            this.attachfiles.Size = new System.Drawing.Size(90, 41);
            this.attachfiles.TabIndex = 5;
            this.attachfiles.Text = "Attach files";
            this.attachfiles.UseVisualStyleBackColor = true;
            // 
            // send
            // 
            this.send.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.send.Location = new System.Drawing.Point(783, 533);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(90, 42);
            this.send.TabIndex = 6;
            this.send.Text = "SEND";
            this.send.UseVisualStyleBackColor = true;
            // 
            // attachedfiles
            // 
            this.attachedfiles.AutoSize = true;
            this.attachedfiles.Location = new System.Drawing.Point(149, 487);
            this.attachedfiles.Name = "attachedfiles";
            this.attachedfiles.Size = new System.Drawing.Size(66, 13);
            this.attachedfiles.TabIndex = 7;
            this.attachedfiles.Text = "Attachments";
            // 
            // attachedcount
            // 
            this.attachedcount.AutoSize = true;
            this.attachedcount.Location = new System.Drawing.Point(149, 534);
            this.attachedcount.Name = "attachedcount";
            this.attachedcount.Size = new System.Drawing.Size(91, 13);
            this.attachedcount.TabIndex = 8;
            this.attachedcount.Text = "Attachment count";
            // 
            // attachremove
            // 
            this.attachremove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.attachremove.Location = new System.Drawing.Point(53, 534);
            this.attachremove.Name = "attachremove";
            this.attachremove.Size = new System.Drawing.Size(90, 41);
            this.attachremove.TabIndex = 9;
            this.attachremove.Text = "Remove attachments";
            this.attachremove.UseVisualStyleBackColor = true;
            // 
            // SendNewMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.attachremove);
            this.Controls.Add(this.attachedcount);
            this.Controls.Add(this.attachedfiles);
            this.Controls.Add(this.send);
            this.Controls.Add(this.attachfiles);
            this.Controls.Add(this.body);
            this.Controls.Add(this.subject);
            this.Controls.Add(this.bcc);
            this.Controls.Add(this.cc);
            this.Controls.Add(this.to);
            this.Name = "SendNewMail";
            this.Size = new System.Drawing.Size(935, 618);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox to;
        private System.Windows.Forms.TextBox cc;
        private System.Windows.Forms.TextBox bcc;
        private System.Windows.Forms.TextBox subject;
        private System.Windows.Forms.TextBox body;
        private System.Windows.Forms.Button attachfiles;
        private System.Windows.Forms.Button send;
        private System.Windows.Forms.Label attachedfiles;
        private System.Windows.Forms.Label attachedcount;
        private System.Windows.Forms.Button attachremove;
    }
}
