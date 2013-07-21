using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Example.Application;

namespace Example.Presentation
{
    /// <summary>
    /// The main form that is presented to the user.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers
        
        private void CmdProcess_Click(object sender, EventArgs e)
        {
            var processor = Processor.Create();
            processor.CheckProcess += (eventSender, eventArgs) =>
            {
                var result = MessageBox.Show(
                    "Should the processor skip the following character?\r\n" + eventArgs.Input,
                    "Skip This Character?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                eventArgs.Skip = result == DialogResult.Yes;
            };

            txtOutput.Text = processor.Capitalize(txtInput.Text);
        }

        #endregion
    }
}