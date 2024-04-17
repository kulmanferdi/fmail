using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace fmail
{
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();

            version_label2.Text = Program.CurrentVersion;

            git_btn.Click += GitClicked;
        }

        private void GitClicked(object sender, EventArgs e)
        {
            Process.Start("https://github.com/kulmanferdi");
        }
    }
}
