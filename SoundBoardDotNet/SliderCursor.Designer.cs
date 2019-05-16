namespace SoundBoardDotNet
{
    partial class SliderCursor
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
            this.SuspendLayout();
            // 
            // SliderCursor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SliderCursor";
            this.Size = new System.Drawing.Size(5, 150);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SliderCursor_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SliderCursor_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SliderCursor_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
