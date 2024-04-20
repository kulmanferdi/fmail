using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace fmail
{

    /// <summary>
    /// Represents the About user control, displaying information about the program.
    /// </summary>
    public partial class About : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="About"/> class.
        /// </summary>
        public About()
        {
            InitializeComponent();

            // Set the version label to the current version of the program
            version_label2.Text = Program.CurrentVersion;

            // Attach the event handler for the Git button click event
            git_btn.Click += GitClicked;
        }

        /// <summary>
        /// Event handler for the Git button click event, which opens the GitHub repository link in the default browser.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void GitClicked(object sender, EventArgs e)
        {
            // Open the GitHub repository link of this procject in the default web browser
            Process.Start("https://github.com/kulmanferdi/fmail");
        }
    }
}
