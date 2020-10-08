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
            this.HeadCurrent = new System.Windows.Forms.PictureBox();
            this.BarCurrent = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.HeadGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadCurrent)).BeginInit();
            this.SuspendLayout();
            // 
            // BarGreen
            // 
            this.BarGreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BarGreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(215)))), ((int)(((byte)(0)))));
            this.BarGreen.Location = new System.Drawing.Point(60, 0);
            this.BarGreen.Margin = new System.Windows.Forms.Padding(2);
            this.BarGreen.Name = "BarGreen";
            this.BarGreen.Size = new System.Drawing.Size(2, 59);
            this.BarGreen.TabIndex = 1;
            // 
            // HeadGreen
            // 
            this.HeadGreen.Image = ((System.Drawing.Image)(resources.GetObject("HeadGreen.Image")));
            this.HeadGreen.Location = new System.Drawing.Point(0, 0);
            this.HeadGreen.Margin = new System.Windows.Forms.Padding(2);
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
            this.HeadRed.Margin = new System.Windows.Forms.Padding(2);
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
            this.BarRed.Margin = new System.Windows.Forms.Padding(2);
            this.BarRed.Name = "BarRed";
            this.BarRed.Size = new System.Drawing.Size(2, 59);
            this.BarRed.TabIndex = 2;
            this.BarRed.Visible = false;
            // 
            // HeadCurrent
            // 
            this.HeadCurrent.Image = ((System.Drawing.Image)(resources.GetObject("HeadCurrent.Image")));
            this.HeadCurrent.Location = new System.Drawing.Point(0, 0);
            this.HeadCurrent.Name = "HeadCurrent";
            this.HeadCurrent.Size = new System.Drawing.Size(32, 16);
            this.HeadCurrent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.HeadCurrent.TabIndex = 3;
            this.HeadCurrent.TabStop = false;
            // 
            // BarCurrent
            // 
            this.BarCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.BarCurrent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.BarCurrent.Location = new System.Drawing.Point(15, 0);
            this.BarCurrent.Name = "BarCurrent";
            this.BarCurrent.Size = new System.Drawing.Size(2, 60);
            this.BarCurrent.TabIndex = 0;
            // 
            // PlayHead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.BarCurrent);
            this.Controls.Add(this.HeadCurrent);
            this.Controls.Add(this.BarRed);
            this.Controls.Add(this.BarGreen);
            this.Controls.Add(this.HeadGreen);
            this.Controls.Add(this.HeadRed);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PlayHead";
            ((System.ComponentModel.ISupportInitialize)(this.HeadGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeadCurrent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel BarGreen;
        private System.Windows.Forms.PictureBox HeadGreen;
        private System.Windows.Forms.PictureBox HeadRed;
        private System.Windows.Forms.Panel BarRed;
        private System.Windows.Forms.PictureBox HeadCurrent;
        private System.Windows.Forms.Panel BarCurrent;
    }
}
