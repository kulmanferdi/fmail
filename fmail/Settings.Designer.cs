namespace fmail
{
    partial class Settings
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
            this.smtp_lbl = new System.Windows.Forms.Label();
            this.imap_label = new System.Windows.Forms.Label();
            this.imap_combo = new System.Windows.Forms.ComboBox();
            this.smtp_combo = new System.Windows.Forms.ComboBox();
            this.save_btn = new System.Windows.Forms.Button();
            this.separate1 = new System.Windows.Forms.Label();
            this.clr_label = new System.Windows.Forms.Label();
            this.clr_draft = new System.Windows.Forms.Button();
            this.clr_spam = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // smtp_lbl
            // 
            this.smtp_lbl.AutoSize = true;
            this.smtp_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.smtp_lbl.Location = new System.Drawing.Point(34, 85);
            this.smtp_lbl.Name = "smtp_lbl";
            this.smtp_lbl.Size = new System.Drawing.Size(88, 20);
            this.smtp_lbl.TabIndex = 5;
            this.smtp_lbl.Text = "SMTP port:";
            // 
            // imap_label
            // 
            this.imap_label.AutoSize = true;
            this.imap_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.imap_label.Location = new System.Drawing.Point(34, 130);
            this.imap_label.Name = "imap_label";
            this.imap_label.Size = new System.Drawing.Size(84, 20);
            this.imap_label.TabIndex = 6;
            this.imap_label.Text = "IMAP port:";
            // 
            // imap_combo
            // 
            this.imap_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.imap_combo.FormattingEnabled = true;
            this.imap_combo.Items.AddRange(new object[] {
            "993",
            "143"});
            this.imap_combo.Location = new System.Drawing.Point(121, 127);
            this.imap_combo.Name = "imap_combo";
            this.imap_combo.Size = new System.Drawing.Size(217, 28);
            this.imap_combo.TabIndex = 8;
            // 
            // smtp_combo
            // 
            this.smtp_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.smtp_combo.FormattingEnabled = true;
            this.smtp_combo.Items.AddRange(new object[] {
            "25",
            "465",
            "587"});
            this.smtp_combo.Location = new System.Drawing.Point(121, 82);
            this.smtp_combo.Name = "smtp_combo";
            this.smtp_combo.Size = new System.Drawing.Size(217, 28);
            this.smtp_combo.TabIndex = 7;
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(810, 530);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(75, 52);
            this.save_btn.TabIndex = 9;
            this.save_btn.Text = "SAVE";
            this.save_btn.UseVisualStyleBackColor = true;
            // 
            // separate1
            // 
            this.separate1.AutoSize = true;
            this.separate1.Location = new System.Drawing.Point(32, 52);
            this.separate1.Name = "separate1";
            this.separate1.Size = new System.Drawing.Size(316, 13);
            this.separate1.TabIndex = 10;
            this.separate1.Text = " Ports --------------------------------------------------------------------------" +
    "-------------------";
            // 
            // clr_label
            // 
            this.clr_label.AutoSize = true;
            this.clr_label.Location = new System.Drawing.Point(35, 175);
            this.clr_label.Name = "clr_label";
            this.clr_label.Size = new System.Drawing.Size(313, 13);
            this.clr_label.TabIndex = 11;
            this.clr_label.Text = "Clear ---------------------------------------------------------------------------" +
    "------------------";
            // 
            // clr_draft
            // 
            this.clr_draft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clr_draft.Location = new System.Drawing.Point(40, 200);
            this.clr_draft.Name = "clr_draft";
            this.clr_draft.Size = new System.Drawing.Size(146, 42);
            this.clr_draft.TabIndex = 12;
            this.clr_draft.Text = "Clear draft folder";
            this.clr_draft.UseVisualStyleBackColor = true;
            // 
            // clr_spam
            // 
            this.clr_spam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.clr_spam.Location = new System.Drawing.Point(40, 248);
            this.clr_spam.Name = "clr_spam";
            this.clr_spam.Size = new System.Drawing.Size(146, 42);
            this.clr_spam.TabIndex = 13;
            this.clr_spam.Text = "Clear spam folder";
            this.clr_spam.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clr_spam);
            this.Controls.Add(this.clr_draft);
            this.Controls.Add(this.clr_label);
            this.Controls.Add(this.separate1);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.imap_combo);
            this.Controls.Add(this.smtp_combo);
            this.Controls.Add(this.imap_label);
            this.Controls.Add(this.smtp_lbl);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(935, 618);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label smtp_lbl;
        private System.Windows.Forms.Label imap_label;
        private System.Windows.Forms.ComboBox imap_combo;
        private System.Windows.Forms.ComboBox smtp_combo;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Label separate1;
        private System.Windows.Forms.Label clr_label;
        private System.Windows.Forms.Button clr_draft;
        private System.Windows.Forms.Button clr_spam;
    }
}
