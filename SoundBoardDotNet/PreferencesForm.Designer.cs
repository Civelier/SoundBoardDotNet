namespace SoundBoardDotNet
{
    partial class PreferencesForm
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
            this.PreferenceProps = new System.Windows.Forms.PropertyGrid();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ResetAllButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PreferenceProps
            // 
            this.PreferenceProps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreferenceProps.Location = new System.Drawing.Point(12, 12);
            this.PreferenceProps.Name = "PreferenceProps";
            this.PreferenceProps.Size = new System.Drawing.Size(420, 424);
            this.PreferenceProps.TabIndex = 0;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(184, 442);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ResetAllButton
            // 
            this.ResetAllButton.Location = new System.Drawing.Point(103, 442);
            this.ResetAllButton.Name = "ResetAllButton";
            this.ResetAllButton.Size = new System.Drawing.Size(75, 23);
            this.ResetAllButton.TabIndex = 2;
            this.ResetAllButton.Text = "Reset all";
            this.ResetAllButton.UseVisualStyleBackColor = true;
            this.ResetAllButton.Click += new System.EventHandler(this.ResetAllButton_Click);
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 502);
            this.Controls.Add(this.ResetAllButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.PreferenceProps);
            this.Name = "PreferencesForm";
            this.Text = "PreferencesForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid PreferenceProps;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ResetAllButton;
    }
}