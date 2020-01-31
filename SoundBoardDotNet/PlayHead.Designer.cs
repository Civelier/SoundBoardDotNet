namespace SoundBoardDotNet
{
    partial class PlayHead
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayHead));
            this.BarGreen = new System.Windows.Forms.Panel();
            this.HeadGreen = new System.Windows.Forms.PictureBox();
            this.HeadRed = new System.Windows.Forms.PictureBox();
            this.BarRed = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.HeadGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadRed)).BeginInit();
            this.SuspendLayout();
            // 
            // BarGreen
            // 
            this.BarGreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BarGreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(215)))), ((int)(((byte)(0)))));
            this.BarGreen.Location = new System.Drawing.Point(30, 0);
            this.BarGreen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BarGreen.Name = "BarGreen";
            this.BarGreen.Size = new System.Drawing.Size(2, 38);
            this.BarGreen.TabIndex = 1;
            // 
            // HeadGreen
            // 
            this.HeadGreen.Image = ((System.Drawing.Image)(resources.GetObject("HeadGreen.Image")));
            this.HeadGreen.Location = new System.Drawing.Point(0, 0);
            this.HeadGreen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HeadGreen.Name = "HeadGreen";
            this.HeadGreen.Size = new System.Drawing.Size(32, 32);
            this.HeadGreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.HeadGreen.TabIndex = 0;
            this.HeadGreen.TabStop = false;
            // 
            // HeadRed
            // 
            this.HeadRed.Image = ((System.Drawing.Image)(resources.GetObject("HeadRed.Image")));
            this.HeadRed.Location = new System.Drawing.Point(0, 0);
            this.HeadRed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HeadRed.Name = "HeadRed";
            this.HeadRed.Size = new System.Drawing.Size(32, 32);
            this.HeadRed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.HeadRed.TabIndex = 2;
            this.HeadRed.TabStop = false;
            this.HeadRed.Visible = false;
            // 
            // BarRed
            // 
            this.BarRed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BarRed.BackColor = System.Drawing.Color.Red;
            this.BarRed.Location = new System.Drawing.Point(0, 0);
            this.BarRed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BarRed.Name = "BarRed";
            this.BarRed.Size = new System.Drawing.Size(3, 38);
            this.BarRed.TabIndex = 2;
            this.BarRed.Visible = false;
            // 
            // PlayHead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.BarRed);
            this.Controls.Add(this.BarGreen);
            this.Controls.Add(this.HeadGreen);
            this.Controls.Add(this.HeadRed);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PlayHead";
            this.Size = new System.Drawing.Size(51, 42);
            ((System.ComponentModel.ISupportInitialize)(this.HeadGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadRed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel BarGreen;
        private System.Windows.Forms.PictureBox HeadGreen;
        private System.Windows.Forms.PictureBox HeadRed;
        private System.Windows.Forms.Panel BarRed;
    }
}
