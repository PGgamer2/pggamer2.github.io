namespace MC_Server_Manager
{
    partial class Main
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxVersions = new System.Windows.Forms.ComboBox();
            this.DLServerButton = new System.Windows.Forms.Button();
            this.DLProgressBar = new System.Windows.Forms.ProgressBar();
            this.formPanel = new System.Windows.Forms.Panel();
            this.checkBoxFUpgrade = new System.Windows.Forms.CheckBox();
            this.labelRAMamount = new System.Windows.Forms.Label();
            this.textBoxRAM = new System.Windows.Forms.TextBox();
            this.customPrompt = new System.Windows.Forms.TextBox();
            this.consolePanel = new System.Windows.Forms.Panel();
            this.labelOutputConsole = new System.Windows.Forms.Label();
            this.enterCmdButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.formPanel.SuspendLayout();
            this.consolePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxVersions
            // 
            this.comboBoxVersions.DropDownHeight = 68;
            this.comboBoxVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVersions.FormattingEnabled = true;
            this.comboBoxVersions.IntegralHeight = false;
            this.comboBoxVersions.Location = new System.Drawing.Point(3, 3);
            this.comboBoxVersions.Name = "comboBoxVersions";
            this.comboBoxVersions.Size = new System.Drawing.Size(121, 21);
            this.comboBoxVersions.TabIndex = 0;
            // 
            // DLServerButton
            // 
            this.DLServerButton.Location = new System.Drawing.Point(130, 3);
            this.DLServerButton.Name = "DLServerButton";
            this.DLServerButton.Size = new System.Drawing.Size(100, 23);
            this.DLServerButton.TabIndex = 1;
            this.DLServerButton.Text = "Download Server";
            this.DLServerButton.UseVisualStyleBackColor = true;
            this.DLServerButton.Click += new System.EventHandler(this.DLServerButton_Click);
            // 
            // DLProgressBar
            // 
            this.DLProgressBar.Location = new System.Drawing.Point(3, 32);
            this.DLProgressBar.Name = "DLProgressBar";
            this.DLProgressBar.Size = new System.Drawing.Size(227, 23);
            this.DLProgressBar.TabIndex = 2;
            // 
            // formPanel
            // 
            this.formPanel.Controls.Add(this.checkBoxFUpgrade);
            this.formPanel.Controls.Add(this.labelRAMamount);
            this.formPanel.Controls.Add(this.textBoxRAM);
            this.formPanel.Controls.Add(this.customPrompt);
            this.formPanel.Controls.Add(this.consolePanel);
            this.formPanel.Controls.Add(this.enterCmdButton);
            this.formPanel.Controls.Add(this.stopButton);
            this.formPanel.Controls.Add(this.startButton);
            this.formPanel.Controls.Add(this.comboBoxVersions);
            this.formPanel.Controls.Add(this.DLProgressBar);
            this.formPanel.Controls.Add(this.DLServerButton);
            this.formPanel.Location = new System.Drawing.Point(12, 12);
            this.formPanel.Name = "formPanel";
            this.formPanel.Size = new System.Drawing.Size(496, 288);
            this.formPanel.TabIndex = 3;
            // 
            // checkBoxFUpgrade
            // 
            this.checkBoxFUpgrade.AutoSize = true;
            this.checkBoxFUpgrade.Location = new System.Drawing.Point(236, 36);
            this.checkBoxFUpgrade.Name = "checkBoxFUpgrade";
            this.checkBoxFUpgrade.Size = new System.Drawing.Size(141, 17);
            this.checkBoxFUpgrade.TabIndex = 10;
            this.checkBoxFUpgrade.Text = "Force upgrade the world";
            this.checkBoxFUpgrade.UseVisualStyleBackColor = true;
            // 
            // labelRAMamount
            // 
            this.labelRAMamount.AutoSize = true;
            this.labelRAMamount.Location = new System.Drawing.Point(304, 8);
            this.labelRAMamount.Name = "labelRAMamount";
            this.labelRAMamount.Size = new System.Drawing.Size(93, 13);
            this.labelRAMamount.TabIndex = 9;
            this.labelRAMamount.Text = "RAM amount (Mb)";
            // 
            // textBoxRAM
            // 
            this.textBoxRAM.Location = new System.Drawing.Point(236, 5);
            this.textBoxRAM.Name = "textBoxRAM";
            this.textBoxRAM.Size = new System.Drawing.Size(62, 20);
            this.textBoxRAM.TabIndex = 8;
            this.textBoxRAM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxRAM_KeyPress);
            // 
            // customPrompt
            // 
            this.customPrompt.Location = new System.Drawing.Point(3, 63);
            this.customPrompt.Name = "customPrompt";
            this.customPrompt.Size = new System.Drawing.Size(394, 20);
            this.customPrompt.TabIndex = 7;
            this.customPrompt.Text = "say Welcome to my server!";
            // 
            // consolePanel
            // 
            this.consolePanel.AutoScroll = true;
            this.consolePanel.BackColor = System.Drawing.SystemColors.ControlText;
            this.consolePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.consolePanel.Controls.Add(this.labelOutputConsole);
            this.consolePanel.ForeColor = System.Drawing.SystemColors.Control;
            this.consolePanel.Location = new System.Drawing.Point(3, 90);
            this.consolePanel.Name = "consolePanel";
            this.consolePanel.Size = new System.Drawing.Size(490, 195);
            this.consolePanel.TabIndex = 6;
            // 
            // labelOutputConsole
            // 
            this.labelOutputConsole.AutoEllipsis = true;
            this.labelOutputConsole.AutoSize = true;
            this.labelOutputConsole.Location = new System.Drawing.Point(4, 4);
            this.labelOutputConsole.Name = "labelOutputConsole";
            this.labelOutputConsole.Size = new System.Drawing.Size(86, 13);
            this.labelOutputConsole.TabIndex = 0;
            this.labelOutputConsole.Text = "[Console Output]\r\n";
            this.labelOutputConsole.TextChanged += new System.EventHandler(this.labelOutputConsole_TextChanged);
            // 
            // enterCmdButton
            // 
            this.enterCmdButton.Location = new System.Drawing.Point(403, 61);
            this.enterCmdButton.Name = "enterCmdButton";
            this.enterCmdButton.Size = new System.Drawing.Size(90, 23);
            this.enterCmdButton.TabIndex = 5;
            this.enterCmdButton.Text = "Enter command";
            this.enterCmdButton.UseVisualStyleBackColor = true;
            this.enterCmdButton.Click += new System.EventHandler(this.enterCmdButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(403, 32);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(90, 23);
            this.stopButton.TabIndex = 4;
            this.stopButton.Text = "Stop Server";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(403, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(90, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Start Server";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 312);
            this.Controls.Add(this.formPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "MC Server Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.formPanel.ResumeLayout(false);
            this.formPanel.PerformLayout();
            this.consolePanel.ResumeLayout(false);
            this.consolePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxVersions;
        private System.Windows.Forms.Button DLServerButton;
        private System.Windows.Forms.ProgressBar DLProgressBar;
        private System.Windows.Forms.Panel formPanel;
        private System.Windows.Forms.Button enterCmdButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Panel consolePanel;
        private System.Windows.Forms.Label labelOutputConsole;
        private System.Windows.Forms.TextBox customPrompt;
        private System.Windows.Forms.Label labelRAMamount;
        private System.Windows.Forms.TextBox textBoxRAM;
        private System.Windows.Forms.CheckBox checkBoxFUpgrade;
    }
}

