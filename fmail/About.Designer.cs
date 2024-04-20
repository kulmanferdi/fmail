namespace fmail
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.title_lbl = new System.Windows.Forms.Label();
            this.version_label2 = new System.Windows.Forms.Label();
            this.version_label = new System.Windows.Forms.Label();
            this.iconbox = new System.Windows.Forms.PictureBox();
            this.git_btn = new System.Windows.Forms.Button();
            this.src_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.iconbox)).BeginInit();
            this.SuspendLayout();
            // 
            // title_lbl
            // 
            this.title_lbl.AutoSize = true;
            this.title_lbl.BackColor = System.Drawing.Color.Transparent;
            this.title_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.title_lbl.Location = new System.Drawing.Point(369, 0);
            this.title_lbl.Name = "title_lbl";
            this.title_lbl.Size = new System.Drawing.Size(193, 73);
            this.title_lbl.TabIndex = 4;
            this.title_lbl.Text = "FMail";
            // 
            // version_label2
            // 
            this.version_label2.AutoSize = true;
            this.version_label2.BackColor = System.Drawing.Color.Transparent;
            this.version_label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.version_label2.Location = new System.Drawing.Point(454, 535);
            this.version_label2.Name = "version_label2";
            this.version_label2.Size = new System.Drawing.Size(59, 33);
            this.version_label2.TabIndex = 6;
            this.version_label2.Text = "ver";
            // 
            // version_label
            // 
            this.version_label.AutoSize = true;
            this.version_label.BackColor = System.Drawing.Color.Transparent;
            this.version_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.version_label.Location = new System.Drawing.Point(236, 535);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(212, 33);
            this.version_label.TabIndex = 5;
            this.version_label.Text = "FMail version:";
            // 
            // iconbox
            // 
            this.iconbox.Image = ((System.Drawing.Image)(resources.GetObject("iconbox.Image")));
            this.iconbox.Location = new System.Drawing.Point(242, 76);
            this.iconbox.Name = "iconbox";
            this.iconbox.Size = new System.Drawing.Size(450, 450);
            this.iconbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconbox.TabIndex = 7;
            this.iconbox.TabStop = false;
            // 
            // git_btn
            // 
            this.git_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.git_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.git_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.git_btn.Location = new System.Drawing.Point(857, 578);
            this.git_btn.Name = "git_btn";
            this.git_btn.Size = new System.Drawing.Size(75, 30);
            this.git_btn.TabIndex = 9;
            this.git_btn.Text = "GitHub";
            this.git_btn.UseVisualStyleBackColor = true;
            // 
            // src_label
            // 
            this.src_label.AutoSize = true;
            this.src_label.Location = new System.Drawing.Point(756, 587);
            this.src_label.Name = "src_label";
            this.src_label.Size = new System.Drawing.Size(95, 13);
            this.src_label.TabIndex = 10;
            this.src_label.Text = "View source code:";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.src_label);
            this.Controls.Add(this.git_btn);
            this.Controls.Add(this.version_label2);
            this.Controls.Add(this.version_label);
            this.Controls.Add(this.title_lbl);
            this.Controls.Add(this.iconbox);
            this.Name = "About";
            this.Size = new System.Drawing.Size(935, 618);
            ((System.ComponentModel.ISupportInitialize)(this.iconbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title_lbl;
        private System.Windows.Forms.Label version_label2;
        private System.Windows.Forms.Label version_label;
        private System.Windows.Forms.PictureBox iconbox;
        private System.Windows.Forms.Button git_btn;
        private System.Windows.Forms.Label src_label;
    }
}
