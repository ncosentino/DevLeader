namespace DevLeader.IronPython.WinForms
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
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.tlpInputLayout = new System.Windows.Forms.TableLayoutPanel();
            this.txtInputScriptPath = new System.Windows.Forms.TextBox();
            this.radInputFromFile = new System.Windows.Forms.RadioButton();
            this.radInputFromForm = new System.Windows.Forms.RadioButton();
            this.txtInputScript = new System.Windows.Forms.TextBox();
            this.cmdBrowseScript = new System.Windows.Forms.Button();
            this.grpOutput = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdClearOutput = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.spcInputOutput = new System.Windows.Forms.SplitContainer();
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.cmdRunScript = new System.Windows.Forms.Button();
            this.grpInput.SuspendLayout();
            this.tlpInputLayout.SuspendLayout();
            this.grpOutput.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spcInputOutput)).BeginInit();
            this.spcInputOutput.Panel1.SuspendLayout();
            this.spcInputOutput.Panel2.SuspendLayout();
            this.spcInputOutput.SuspendLayout();
            this.tlpMainLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInput
            // 
            this.grpInput.Controls.Add(this.tlpInputLayout);
            this.grpInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInput.Location = new System.Drawing.Point(0, 0);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(652, 186);
            this.grpInput.TabIndex = 0;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Input";
            // 
            // tlpInputLayout
            // 
            this.tlpInputLayout.ColumnCount = 3;
            this.tlpInputLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpInputLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInputLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpInputLayout.Controls.Add(this.txtInputScriptPath, 1, 0);
            this.tlpInputLayout.Controls.Add(this.radInputFromFile, 0, 0);
            this.tlpInputLayout.Controls.Add(this.radInputFromForm, 0, 1);
            this.tlpInputLayout.Controls.Add(this.txtInputScript, 1, 1);
            this.tlpInputLayout.Controls.Add(this.cmdBrowseScript, 2, 0);
            this.tlpInputLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpInputLayout.Location = new System.Drawing.Point(3, 16);
            this.tlpInputLayout.Name = "tlpInputLayout";
            this.tlpInputLayout.RowCount = 2;
            this.tlpInputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpInputLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpInputLayout.Size = new System.Drawing.Size(646, 167);
            this.tlpInputLayout.TabIndex = 0;
            // 
            // txtInputScriptPath
            // 
            this.txtInputScriptPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInputScriptPath.Enabled = false;
            this.txtInputScriptPath.Location = new System.Drawing.Point(99, 3);
            this.txtInputScriptPath.Name = "txtInputScriptPath";
            this.txtInputScriptPath.Size = new System.Drawing.Size(463, 20);
            this.txtInputScriptPath.TabIndex = 3;
            // 
            // radInputFromFile
            // 
            this.radInputFromFile.AutoSize = true;
            this.radInputFromFile.Location = new System.Drawing.Point(3, 3);
            this.radInputFromFile.Name = "radInputFromFile";
            this.radInputFromFile.Size = new System.Drawing.Size(67, 17);
            this.radInputFromFile.TabIndex = 0;
            this.radInputFromFile.Text = "From File";
            this.radInputFromFile.UseVisualStyleBackColor = true;
            this.radInputFromFile.CheckedChanged += new System.EventHandler(this.RadInputFromFile_CheckedChanged);
            // 
            // radInputFromForm
            // 
            this.radInputFromForm.AutoSize = true;
            this.radInputFromForm.Location = new System.Drawing.Point(3, 32);
            this.radInputFromForm.Name = "radInputFromForm";
            this.radInputFromForm.Size = new System.Drawing.Size(90, 17);
            this.radInputFromForm.TabIndex = 1;
            this.radInputFromForm.Text = "Custom Script";
            this.radInputFromForm.UseVisualStyleBackColor = true;
            this.radInputFromForm.CheckedChanged += new System.EventHandler(this.RadInputFromForm_CheckedChanged);
            // 
            // txtInputScript
            // 
            this.tlpInputLayout.SetColumnSpan(this.txtInputScript, 2);
            this.txtInputScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInputScript.Enabled = false;
            this.txtInputScript.Location = new System.Drawing.Point(99, 32);
            this.txtInputScript.Multiline = true;
            this.txtInputScript.Name = "txtInputScript";
            this.txtInputScript.Size = new System.Drawing.Size(544, 132);
            this.txtInputScript.TabIndex = 2;
            // 
            // cmdBrowseScript
            // 
            this.cmdBrowseScript.Enabled = false;
            this.cmdBrowseScript.Location = new System.Drawing.Point(568, 3);
            this.cmdBrowseScript.Name = "cmdBrowseScript";
            this.cmdBrowseScript.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseScript.TabIndex = 4;
            this.cmdBrowseScript.Text = "Browse...";
            this.cmdBrowseScript.UseVisualStyleBackColor = true;
            this.cmdBrowseScript.Click += new System.EventHandler(this.CmdBrowseScript_Click);
            // 
            // grpOutput
            // 
            this.grpOutput.Controls.Add(this.tableLayoutPanel1);
            this.grpOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOutput.Location = new System.Drawing.Point(0, 0);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new System.Drawing.Size(652, 218);
            this.grpOutput.TabIndex = 1;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "Output";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.cmdClearOutput, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtOutput, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(646, 199);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cmdClearOutput
            // 
            this.cmdClearOutput.Location = new System.Drawing.Point(3, 3);
            this.cmdClearOutput.Name = "cmdClearOutput";
            this.cmdClearOutput.Size = new System.Drawing.Size(75, 23);
            this.cmdClearOutput.TabIndex = 0;
            this.cmdClearOutput.Text = "Clear";
            this.cmdClearOutput.UseVisualStyleBackColor = true;
            this.cmdClearOutput.Click += new System.EventHandler(this.CmdClearOutput_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.Color.Black;
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.ForeColor = System.Drawing.Color.White;
            this.txtOutput.Location = new System.Drawing.Point(3, 32);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(640, 164);
            this.txtOutput.TabIndex = 2;
            // 
            // spcInputOutput
            // 
            this.spcInputOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcInputOutput.Location = new System.Drawing.Point(3, 3);
            this.spcInputOutput.Name = "spcInputOutput";
            this.spcInputOutput.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcInputOutput.Panel1
            // 
            this.spcInputOutput.Panel1.Controls.Add(this.grpInput);
            // 
            // spcInputOutput.Panel2
            // 
            this.spcInputOutput.Panel2.Controls.Add(this.grpOutput);
            this.spcInputOutput.Size = new System.Drawing.Size(652, 408);
            this.spcInputOutput.SplitterDistance = 186;
            this.spcInputOutput.TabIndex = 1;
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 1;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Controls.Add(this.spcInputOutput, 0, 0);
            this.tlpMainLayout.Controls.Add(this.cmdRunScript, 0, 1);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 2;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMainLayout.Size = new System.Drawing.Size(658, 443);
            this.tlpMainLayout.TabIndex = 2;
            // 
            // cmdRunScript
            // 
            this.cmdRunScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRunScript.Location = new System.Drawing.Point(580, 417);
            this.cmdRunScript.Name = "cmdRunScript";
            this.cmdRunScript.Size = new System.Drawing.Size(75, 23);
            this.cmdRunScript.TabIndex = 2;
            this.cmdRunScript.Text = "Run Script";
            this.cmdRunScript.UseVisualStyleBackColor = true;
            this.cmdRunScript.Click += new System.EventHandler(this.CmdRunScript_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 443);
            this.Controls.Add(this.tlpMainLayout);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Python Example";
            this.grpInput.ResumeLayout(false);
            this.tlpInputLayout.ResumeLayout(false);
            this.tlpInputLayout.PerformLayout();
            this.grpOutput.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.spcInputOutput.Panel1.ResumeLayout(false);
            this.spcInputOutput.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcInputOutput)).EndInit();
            this.spcInputOutput.ResumeLayout(false);
            this.tlpMainLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInput;
        private System.Windows.Forms.TableLayoutPanel tlpInputLayout;
        private System.Windows.Forms.TextBox txtInputScriptPath;
        private System.Windows.Forms.RadioButton radInputFromFile;
        private System.Windows.Forms.RadioButton radInputFromForm;
        private System.Windows.Forms.TextBox txtInputScript;
        private System.Windows.Forms.Button cmdBrowseScript;
        private System.Windows.Forms.GroupBox grpOutput;
        private System.Windows.Forms.SplitContainer spcInputOutput;
        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
        private System.Windows.Forms.Button cmdRunScript;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button cmdClearOutput;
        private System.Windows.Forms.TextBox txtOutput;

    }
}

