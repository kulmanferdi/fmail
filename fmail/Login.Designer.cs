namespace fmail
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.passwd_label = new System.Windows.Forms.Label();
            this.user_label = new System.Windows.Forms.Label();
            this.port_label = new System.Windows.Forms.Label();
            this.server_label = new System.Windows.Forms.Label();
            this.enableSSL_chk = new System.Windows.Forms.CheckBox();
            this.port_combo = new System.Windows.Forms.ComboBox();
            this.password_txt = new System.Windows.Forms.TextBox();
            this.username_txt = new System.Windows.Forms.TextBox();
            this.server_combo = new System.Windows.Forms.ComboBox();
            this.signin_btn = new System.Windows.Forms.Button();
            this.login_label = new System.Windows.Forms.Label();
            this.version_label = new System.Windows.Forms.Label();
            this.version_label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.cancel_btn);
            this.panel1.Controls.Add(this.passwd_label);
            this.panel1.Controls.Add(this.user_label);
            this.panel1.Controls.Add(this.port_label);
            this.panel1.Controls.Add(this.server_label);
            this.panel1.Controls.Add(this.enableSSL_chk);
            this.panel1.Controls.Add(this.port_combo);
            this.panel1.Controls.Add(this.password_txt);
            this.panel1.Controls.Add(this.username_txt);
            this.panel1.Controls.Add(this.server_combo);
            this.panel1.Controls.Add(this.signin_btn);
            this.panel1.Location = new System.Drawing.Point(1, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(488, 348);
            this.panel1.TabIndex = 0;
            // 
            // cancel_btn
            // 
            this.cancel_btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancel_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cancel_btn.Location = new System.Drawing.Point(310, 299);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(96, 40);
            this.cancel_btn.TabIndex = 4;
            this.cancel_btn.Text = "CANCEL";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // passwd_label
            // 
            this.passwd_label.AutoSize = true;
            this.passwd_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.passwd_label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.passwd_label.Location = new System.Drawing.Point(70, 226);
            this.passwd_label.Name = "passwd_label";
            this.passwd_label.Size = new System.Drawing.Size(112, 25);
            this.passwd_label.TabIndex = 9;
            this.passwd_label.Text = "Password:";
            // 
            // user_label
            // 
            this.user_label.AutoSize = true;
            this.user_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.user_label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.user_label.Location = new System.Drawing.Point(66, 194);
            this.user_label.Name = "user_label";
            this.user_label.Size = new System.Drawing.Size(116, 25);
            this.user_label.TabIndex = 8;
            this.user_label.Text = "Username:";
            // 
            // port_label
            // 
            this.port_label.AutoSize = true;
            this.port_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.port_label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.port_label.Location = new System.Drawing.Point(125, 164);
            this.port_label.Name = "port_label";
            this.port_label.Size = new System.Drawing.Size(57, 25);
            this.port_label.TabIndex = 7;
            this.port_label.Text = "Port:";
            // 
            // server_label
            // 
            this.server_label.AutoSize = true;
            this.server_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.server_label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.server_label.Location = new System.Drawing.Point(101, 130);
            this.server_label.Name = "server_label";
            this.server_label.Size = new System.Drawing.Size(81, 25);
            this.server_label.TabIndex = 6;
            this.server_label.Text = "Server:";
            // 
            // enableSSL_chk
            // 
            this.enableSSL_chk.AutoSize = true;
            this.enableSSL_chk.ForeColor = System.Drawing.Color.Snow;
            this.enableSSL_chk.Location = new System.Drawing.Point(202, 276);
            this.enableSSL_chk.Name = "enableSSL_chk";
            this.enableSSL_chk.Size = new System.Drawing.Size(82, 17);
            this.enableSSL_chk.TabIndex = 7;
            this.enableSSL_chk.Text = "Enable SSL";
            this.enableSSL_chk.UseVisualStyleBackColor = true;
            // 
            // port_combo
            // 
            this.port_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.port_combo.FormattingEnabled = true;
            this.port_combo.Items.AddRange(new object[] {
            "993",
            "143"});
            this.port_combo.Location = new System.Drawing.Point(188, 161);
            this.port_combo.Name = "port_combo";
            this.port_combo.Size = new System.Drawing.Size(217, 28);
            this.port_combo.TabIndex = 6;
            // 
            // password_txt
            // 
            this.password_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.password_txt.Location = new System.Drawing.Point(188, 227);
            this.password_txt.Name = "password_txt";
            this.password_txt.PasswordChar = '*';
            this.password_txt.Size = new System.Drawing.Size(217, 26);
            this.password_txt.TabIndex = 2;
            // 
            // username_txt
            // 
            this.username_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.username_txt.Location = new System.Drawing.Point(188, 195);
            this.username_txt.Name = "username_txt";
            this.username_txt.Size = new System.Drawing.Size(217, 26);
            this.username_txt.TabIndex = 1;
            // 
            // server_combo
            // 
            this.server_combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.server_combo.FormattingEnabled = true;
            this.server_combo.Items.AddRange(new object[] {
            "Gmail",
            "Outlook",
            "Yahoo",
            "AOL"});
            this.server_combo.Location = new System.Drawing.Point(188, 127);
            this.server_combo.Name = "server_combo";
            this.server_combo.Size = new System.Drawing.Size(217, 28);
            this.server_combo.TabIndex = 5;
            // 
            // signin_btn
            // 
            this.signin_btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.signin_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.signin_btn.Location = new System.Drawing.Point(188, 299);
            this.signin_btn.Name = "signin_btn";
            this.signin_btn.Size = new System.Drawing.Size(96, 40);
            this.signin_btn.TabIndex = 3;
            this.signin_btn.Text = "SIGN IN";
            this.signin_btn.UseVisualStyleBackColor = true;
            this.signin_btn.Click += new System.EventHandler(this.SignInClicked);
            // 
            // login_label
            // 
            this.login_label.AutoSize = true;
            this.login_label.BackColor = System.Drawing.Color.Transparent;
            this.login_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.login_label.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.login_label.Location = new System.Drawing.Point(182, 20);
            this.login_label.Name = "login_label";
            this.login_label.Size = new System.Drawing.Size(138, 42);
            this.login_label.TabIndex = 1;
            this.login_label.Text = "LOGIN";
            // 
            // version_label
            // 
            this.version_label.AutoSize = true;
            this.version_label.BackColor = System.Drawing.Color.Transparent;
            this.version_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.version_label.Location = new System.Drawing.Point(355, 428);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(86, 13);
            this.version_label.TabIndex = 2;
            this.version_label.Text = "FMail version:";
            // 
            // version_label2
            // 
            this.version_label2.AutoSize = true;
            this.version_label2.BackColor = System.Drawing.Color.Transparent;
            this.version_label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.version_label2.Location = new System.Drawing.Point(443, 429);
            this.version_label2.Name = "version_label2";
            this.version_label2.Size = new System.Drawing.Size(25, 13);
            this.version_label2.TabIndex = 3;
            this.version_label2.Text = "ver";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(490, 450);
            this.Controls.Add(this.version_label2);
            this.Controls.Add(this.version_label);
            this.Controls.Add(this.login_label);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.Text = "FMail";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label login_label;
        private System.Windows.Forms.Label passwd_label;
        private System.Windows.Forms.Label user_label;
        private System.Windows.Forms.Label port_label;
        private System.Windows.Forms.Label server_label;
        private System.Windows.Forms.CheckBox enableSSL_chk;
        private System.Windows.Forms.ComboBox port_combo;
        private System.Windows.Forms.TextBox password_txt;
        private System.Windows.Forms.TextBox username_txt;
        private System.Windows.Forms.ComboBox server_combo;
        private System.Windows.Forms.Button signin_btn;
        private System.Windows.Forms.Label version_label;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Label version_label2;
    }
}

