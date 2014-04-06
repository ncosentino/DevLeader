using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using IronPython.Hosting;

namespace DevLeader.IronPython.WinForms
{
    /// <summary>
    /// A form that allows users to execute Python scripts from a file or from 
    /// entering them manually on the form.
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

        #region Internal Members
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            radInputFromFile.Checked = true;
        }

        private void ExecuteScript(string scriptBody, Stream outputStream)
        {
            var py = Python.CreateEngine();
            py.Runtime.IO.SetOutput(outputStream, Encoding.GetEncoding(1252));

            try
            {
                py.Execute(scriptBody);                
            }
            catch (Exception ex)
            {
                using (var writer = new StreamWriter(outputStream))
                {
                    writer.WriteLine(
                        "Oops! There was an exception while running the script:\r\n" +
                        ex.Message + "\r\n\r\n" + ex.StackTrace);
                }
            }
        }
        #endregion

        #region Event Handlers
        private void RadInputFromFile_CheckedChanged(object sender, EventArgs e)
        {
            bool optionEnabled = ((RadioButton)sender).Checked;
            txtInputScriptPath.Enabled = optionEnabled;
            cmdBrowseScript.Enabled = optionEnabled;
        }

        private void RadInputFromForm_CheckedChanged(object sender, EventArgs e)
        {
            bool optionEnabled = ((RadioButton)sender).Checked;
            txtInputScript.Enabled = optionEnabled;
        }

        private void CmdRunScript_Click(object sender, EventArgs e)
        {
            string scriptBody;
            if (radInputFromForm.Checked)
            {
                scriptBody = txtInputScript.Text;
            }
            else if (radInputFromFile.Checked)
            {
                var inputFilePath = txtInputScriptPath.Text;
                if (!File.Exists(inputFilePath))
                {
                    MessageBox.Show(
                        "The file '" + inputFilePath + "' does not exist.",
                        Application.ProductName);
                    txtInputScriptPath.Focus();
                    txtInputScriptPath.SelectAll();
                    return;
                }

                try
                {
                    using (var reader = new StreamReader(inputFilePath))
                    {
                        scriptBody = reader.ReadToEnd();
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show(
                        "An exception was caught while opening '" + inputFilePath + "':\r\n\r\n" +
                        ex.Message + "\r\n\r\n" + ex.StackTrace,
                        Application.ProductName);
                    return;
                }
            }
            else
            {
                throw new NotImplementedException("The option for executing scripts has not been implemented.");
            }

            // run our script and print the output
            txtOutput.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":\r\n";
            using (var outStream = new ScriptOutputStream(txtOutput))
            {
                ExecuteScript(scriptBody, outStream);
            }

            txtOutput.Text += "\r\n";

            // scroll to the end of the output
            txtOutput.SelectionLength = 0;
            txtOutput.SelectionStart = txtOutput.Text.Length - 1;
            txtOutput.ScrollToCaret();
        }

        private void CmdClearOutput_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
        }

        private void CmdBrowseScript_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()
            {
                Title = "Select a Python script file.",
                Multiselect = false,
                Filter = "Python Script|*.py|All Files|*.*"
            })
            {
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                txtInputScriptPath.Text = ofd.FileName;
                txtInputScriptPath.SelectAll();
            }
        }
        #endregion

        #region Classes
        private class ScriptOutputStream : Stream
        {
            #region Fields
            private readonly TextBox _control;
            #endregion

            #region Constructors
            public ScriptOutputStream(TextBox control)
            {
                _control = control;
            }
            #endregion

            #region Properties
            public override bool CanRead
            {
                get { return false; }
            }

            public override bool CanSeek
            {
                get { return false; }
            }

            public override bool CanWrite
            {
                get { return true; }
            }

            public override long Length
            {
                get { throw new NotImplementedException(); }
            }

            public override long Position
            {
                get { throw new NotImplementedException(); }
                set { throw new NotImplementedException(); }
            }
            #endregion

            #region Exposed Members
            public override void Flush()
            {
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotImplementedException();
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                _control.Text += Encoding.GetEncoding(1252).GetString(buffer, offset, count);
            }
            #endregion
        }
        #endregion
    }
}
