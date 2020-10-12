namespace SoundBoardDotNet.PlayHeads
{
    partial class CurrentPlayHead
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrentPlayHead));
            this.Head = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Head)).BeginInit();
            this.SuspendLayout();
            // 
            // Head
            // 
            this.Head.Image = ((System.Drawing.Image)(resources.GetObject("Head.Image")));
            this.Head.Location = new System.Drawing.Point(0, 0);
            this.Head.Name = "Head";
            this.Head.Size = new System.Drawing.Size(64, 32);
            this.Head.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Head.TabIndex = 0;
            this.Head.TabStop = false;
            this.Head.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Head_MouseDown);
            this.Head.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Head_MouseMove);
            this.Head.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Head_MouseUp);
            // 
            // CurrentPlayHead
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Head);
            this.Name = "CurrentPlayHead";
            this.Size = new System.Drawing.Size(64, 32);
            ((System.ComponentModel.ISupportInitialize)(this.Head)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Head;
    }
}
