namespace SoundBoardDotNet
{
    partial class DevicesForm
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
            this.Tabs = new System.Windows.Forms.TabControl();
            this.InputTab = new System.Windows.Forms.TabPage();
            this.OutputTab = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.RecordInputCheck = new System.Windows.Forms.CheckBox();
            this.InputsCombo = new System.Windows.Forms.ComboBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.RefreshInputsButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.OutputsCombo = new System.Windows.Forms.ComboBox();
            this.RefreshOutputsButton = new System.Windows.Forms.Button();
            this.Tabs.SuspendLayout();
            this.InputTab.SuspendLayout();
            this.OutputTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tabs.Controls.Add(this.InputTab);
            this.Tabs.Controls.Add(this.OutputTab);
            this.Tabs.Location = new System.Drawing.Point(12, 12);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(366, 212);
            this.Tabs.TabIndex = 0;
            // 
            // InputTab
            // 
            this.InputTab.Controls.Add(this.RefreshInputsButton);
            this.InputTab.Controls.Add(this.InputsCombo);
            this.InputTab.Controls.Add(this.RecordInputCheck);
            this.InputTab.Controls.Add(this.label1);
            this.InputTab.Location = new System.Drawing.Point(4, 25);
            this.InputTab.Name = "InputTab";
            this.InputTab.Padding = new System.Windows.Forms.Padding(3);
            this.InputTab.Size = new System.Drawing.Size(358, 183);
            this.InputTab.TabIndex = 0;
            this.InputTab.Text = "Inputs";
            this.InputTab.UseVisualStyleBackColor = true;
            // 
            // OutputTab
            // 
            this.OutputTab.Controls.Add(this.RefreshOutputsButton);
            this.OutputTab.Controls.Add(this.OutputsCombo);
            this.OutputTab.Controls.Add(this.label2);
            this.OutputTab.Location = new System.Drawing.Point(4, 25);
            this.OutputTab.Name = "OutputTab";
            this.OutputTab.Padding = new System.Windows.Forms.Padding(3);
            this.OutputTab.Size = new System.Drawing.Size(358, 183);
            this.OutputTab.TabIndex = 1;
            this.OutputTab.Text = "Outputs";
            this.OutputTab.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input:";
            // 
            // RecordInputCheck
            // 
            this.RecordInputCheck.AutoSize = true;
            this.RecordInputCheck.Location = new System.Drawing.Point(246, 6);
            this.RecordInputCheck.Name = "RecordInputCheck";
            this.RecordInputCheck.Size = new System.Drawing.Size(76, 21);
            this.RecordInputCheck.TabIndex = 1;
            this.RecordInputCheck.Text = "Record";
            this.RecordInputCheck.UseVisualStyleBackColor = true;
            this.RecordInputCheck.CheckedChanged += new System.EventHandler(this.RecordInputCheck_CheckedChanged);
            // 
            // InputsCombo
            // 
            this.InputsCombo.FormattingEnabled = true;
            this.InputsCombo.Location = new System.Drawing.Point(55, 6);
            this.InputsCombo.Name = "InputsCombo";
            this.InputsCombo.Size = new System.Drawing.Size(185, 24);
            this.InputsCombo.TabIndex = 2;
            this.InputsCombo.SelectedIndexChanged += new System.EventHandler(this.InputsCombo_SelectedIndexChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SaveButton.Location = new System.Drawing.Point(163, 230);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // RefreshInputsButton
            // 
            this.RefreshInputsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RefreshInputsButton.Location = new System.Drawing.Point(6, 154);
            this.RefreshInputsButton.Name = "RefreshInputsButton";
            this.RefreshInputsButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshInputsButton.TabIndex = 5;
            this.RefreshInputsButton.Text = "Refresh";
            this.RefreshInputsButton.UseVisualStyleBackColor = true;
            this.RefreshInputsButton.Click += new System.EventHandler(this.RefreshInputsButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Output:";
            // 
            // OutputsCombo
            // 
            this.OutputsCombo.FormattingEnabled = true;
            this.OutputsCombo.Location = new System.Drawing.Point(69, 7);
            this.OutputsCombo.Name = "OutputsCombo";
            this.OutputsCombo.Size = new System.Drawing.Size(176, 24);
            this.OutputsCombo.TabIndex = 1;
            this.OutputsCombo.SelectedIndexChanged += new System.EventHandler(this.OutputsCombo_SelectedIndexChanged);
            // 
            // RefreshOutputsButton
            // 
            this.RefreshOutputsButton.Location = new System.Drawing.Point(7, 154);
            this.RefreshOutputsButton.Name = "RefreshOutputsButton";
            this.RefreshOutputsButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshOutputsButton.TabIndex = 2;
            this.RefreshOutputsButton.Text = "Refresh";
            this.RefreshOutputsButton.UseVisualStyleBackColor = true;
            this.RefreshOutputsButton.Click += new System.EventHandler(this.RefreshOutputsButton_Click);
            // 
            // DevicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 265);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Tabs);
            this.Name = "DevicesForm";
            this.Text = "DevicesForm";
            this.Tabs.ResumeLayout(false);
            this.InputTab.ResumeLayout(false);
            this.InputTab.PerformLayout();
            this.OutputTab.ResumeLayout(false);
            this.OutputTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage InputTab;
        private System.Windows.Forms.TabPage OutputTab;
        private System.Windows.Forms.ComboBox InputsCombo;
        private System.Windows.Forms.CheckBox RecordInputCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button RefreshInputsButton;
        private System.Windows.Forms.Button RefreshOutputsButton;
        private System.Windows.Forms.ComboBox OutputsCombo;
        private System.Windows.Forms.Label label2;
    }
}