namespace Example.Presentation
{
    partial class MainForm
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
            this.tlpLayout = new System.Windows.Forms.TableLayoutPanel();
            this.grpOutput = new System.Windows.Forms.GroupBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.cmdProcess = new System.Windows.Forms.Button();
            this.tlpLayout.SuspendLayout();
            this.grpOutput.SuspendLayout();
            this.grpInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpLayout
            // 
            this.tlpLayout.ColumnCount = 2;
            this.tlpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLayout.Controls.Add(this.grpOutput, 1, 0);
            this.tlpLayout.Controls.Add(this.grpInput, 0, 0);
            this.tlpLayout.Controls.Add(this.cmdProcess, 0, 1);
            this.tlpLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpLayout.Name = "tlpLayout";
            this.tlpLayout.RowCount = 2;
            this.tlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpLayout.Size = new System.Drawing.Size(384, 162);
            this.tlpLayout.TabIndex = 0;
            // 
            // grpOutput
            // 
            this.grpOutput.Controls.Add(this.txtOutput);
            this.grpOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOutput.Location = new System.Drawing.Point(195, 3);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new System.Drawing.Size(186, 127);
            this.grpOutput.TabIndex = 1;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "Output";
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(3, 16);
            this.txtOutput.MaxLength = 10000;
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(180, 108);
            this.txtOutput.TabIndex = 1;
            // 
            // grpInput
            // 
            this.grpInput.Controls.Add(this.txtInput);
            this.grpInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInput.Location = new System.Drawing.Point(3, 3);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(186, 127);
            this.grpInput.TabIndex = 0;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Input";
            // 
            // txtInput
            // 
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Location = new System.Drawing.Point(3, 16);
            this.txtInput.MaxLength = 20;
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(180, 108);
            this.txtInput.TabIndex = 0;
            // 
            // cmdProcess
            // 
            this.cmdProcess.Location = new System.Drawing.Point(3, 136);
            this.cmdProcess.Name = "cmdProcess";
            this.cmdProcess.Size = new System.Drawing.Size(75, 23);
            this.cmdProcess.TabIndex = 2;
            this.cmdProcess.Text = "Process!";
            this.cmdProcess.UseVisualStyleBackColor = true;
            this.cmdProcess.Click += new System.EventHandler(this.CmdProcess_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 162);
            this.Controls.Add(this.tlpLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capitalizer!";
            this.tlpLayout.ResumeLayout(false);
            this.grpOutput.ResumeLayout(false);
            this.grpOutput.PerformLayout();
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpLayout;
        private System.Windows.Forms.GroupBox grpOutput;
        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button cmdProcess;
    }
}