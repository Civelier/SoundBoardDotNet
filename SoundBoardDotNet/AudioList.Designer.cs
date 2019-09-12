namespace SoundBoardDotNet
{
    partial class AudioList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ListContainer = new System.Windows.Forms.Panel();
            this.Content = new System.Windows.Forms.Panel();
            this.ListContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListContainer
            // 
            this.ListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListContainer.AutoScroll = true;
            this.ListContainer.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ListContainer.Controls.Add(this.Content);
            this.ListContainer.Location = new System.Drawing.Point(0, 0);
            this.ListContainer.Name = "ListContainer";
            this.ListContainer.Size = new System.Drawing.Size(434, 188);
            this.ListContainer.TabIndex = 0;
            // 
            // Content
            // 
            this.Content.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Content.BackColor = System.Drawing.SystemColors.Control;
            this.Content.Location = new System.Drawing.Point(3, 3);
            this.Content.Name = "Content";
            this.Content.Size = new System.Drawing.Size(470, 182);
            this.Content.TabIndex = 0;
            this.Content.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.Content_ControlAdded);
            this.Content.Paint += new System.Windows.Forms.PaintEventHandler(this.Content_Paint);
            // 
            // AudioList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ListContainer);
            this.Name = "AudioList";
            this.Size = new System.Drawing.Size(434, 188);
            this.ListContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ListContainer;
        private System.Windows.Forms.Panel Content;
    }
}
