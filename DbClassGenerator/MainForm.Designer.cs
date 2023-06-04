namespace DbClassGenerator
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
            this.userIdCheckBox = new System.Windows.Forms.CheckBox();
            this.dataSourceLabel = new System.Windows.Forms.Label();
            this.dataSourceTextBox = new System.Windows.Forms.TextBox();
            this.userIdTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.databaseComboBox = new System.Windows.Forms.ComboBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.generateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rootNamespaceTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // userIdCheckBox
            // 
            this.userIdCheckBox.AutoSize = true;
            this.userIdCheckBox.Location = new System.Drawing.Point(194, 14);
            this.userIdCheckBox.Name = "userIdCheckBox";
            this.userIdCheckBox.Size = new System.Drawing.Size(63, 17);
            this.userIdCheckBox.TabIndex = 0;
            this.userIdCheckBox.Text = "User Id:";
            this.userIdCheckBox.UseVisualStyleBackColor = true;
            this.userIdCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dataSourceLabel
            // 
            this.dataSourceLabel.AutoSize = true;
            this.dataSourceLabel.Location = new System.Drawing.Point(12, 15);
            this.dataSourceLabel.Name = "dataSourceLabel";
            this.dataSourceLabel.Size = new System.Drawing.Size(70, 13);
            this.dataSourceLabel.TabIndex = 1;
            this.dataSourceLabel.Text = "Data Source:";
            // 
            // dataSourceTextBox
            // 
            this.dataSourceTextBox.Location = new System.Drawing.Point(88, 12);
            this.dataSourceTextBox.Name = "dataSourceTextBox";
            this.dataSourceTextBox.Size = new System.Drawing.Size(100, 20);
            this.dataSourceTextBox.TabIndex = 2;
            // 
            // userIdTextBox
            // 
            this.userIdTextBox.Enabled = false;
            this.userIdTextBox.Location = new System.Drawing.Point(263, 12);
            this.userIdTextBox.Name = "userIdTextBox";
            this.userIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.userIdTextBox.TabIndex = 3;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(369, 15);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(56, 13);
            this.passwordLabel.TabIndex = 4;
            this.passwordLabel.Text = "Password:";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Enabled = false;
            this.passwordTextBox.Location = new System.Drawing.Point(431, 12);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(100, 20);
            this.passwordTextBox.TabIndex = 5;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(537, 10);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // databaseLabel
            // 
            this.databaseLabel.AutoSize = true;
            this.databaseLabel.Location = new System.Drawing.Point(12, 41);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(61, 13);
            this.databaseLabel.TabIndex = 8;
            this.databaseLabel.Text = "Databases:";
            // 
            // databaseComboBox
            // 
            this.databaseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.databaseComboBox.Enabled = false;
            this.databaseComboBox.FormattingEnabled = true;
            this.databaseComboBox.Location = new System.Drawing.Point(79, 38);
            this.databaseComboBox.Name = "databaseComboBox";
            this.databaseComboBox.Size = new System.Drawing.Size(533, 21);
            this.databaseComboBox.TabIndex = 9;
            this.databaseComboBox.SelectedIndexChanged += new System.EventHandler(this.databaseComboBox_SelectedIndexChanged);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(15, 71);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(597, 334);
            this.checkedListBox1.TabIndex = 10;
            // 
            // generateButton
            // 
            this.generateButton.Location = new System.Drawing.Point(537, 411);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(75, 23);
            this.generateButton.TabIndex = 11;
            this.generateButton.Text = "Generate ...";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 416);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = " Root Namespace";
            // 
            // rootNamespaceTextBox
            // 
            this.rootNamespaceTextBox.Location = new System.Drawing.Point(111, 413);
            this.rootNamespaceTextBox.Name = "rootNamespaceTextBox";
            this.rootNamespaceTextBox.Size = new System.Drawing.Size(420, 20);
            this.rootNamespaceTextBox.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 450);
            this.Controls.Add(this.rootNamespaceTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.databaseComboBox);
            this.Controls.Add(this.databaseLabel);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userIdTextBox);
            this.Controls.Add(this.dataSourceTextBox);
            this.Controls.Add(this.dataSourceLabel);
            this.Controls.Add(this.userIdCheckBox);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataBase .Net Classes Generatos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox userIdCheckBox;
        private System.Windows.Forms.Label dataSourceLabel;
        private System.Windows.Forms.TextBox dataSourceTextBox;
        private System.Windows.Forms.TextBox userIdTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.ComboBox databaseComboBox;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rootNamespaceTextBox;
    }
}

