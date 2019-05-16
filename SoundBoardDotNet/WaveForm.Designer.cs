namespace SoundBoardDotNet
{
    partial class WaveForm
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
            this.Slider1 = new SoundBoardDotNet.SliderCursor();
            this.Slider2 = new SoundBoardDotNet.SliderCursor();
            this.SuspendLayout();
            // 
            // Slider1
            // 
            this.Slider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Slider1.Location = new System.Drawing.Point(0, 91);
            this.Slider1.Margin = new System.Windows.Forms.Padding(0);
            this.Slider1.Name = "Slider1";
            this.Slider1.Size = new System.Drawing.Size(5, 87);
            this.Slider1.TabIndex = 0;
            this.Slider1.Load += new System.EventHandler(this.Slider1_Load);
            this.Slider1.Paint += new System.Windows.Forms.PaintEventHandler(this.Slider1_Paint);
            // 
            // Slider2
            // 
            this.Slider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Slider2.Location = new System.Drawing.Point(369, 0);
            this.Slider2.Margin = new System.Windows.Forms.Padding(0);
            this.Slider2.Name = "Slider2";
            this.Slider2.Size = new System.Drawing.Size(5, 91);
            this.Slider2.TabIndex = 1;
            this.Slider2.Load += new System.EventHandler(this.Slider2_Load);
            this.Slider2.Paint += new System.Windows.Forms.PaintEventHandler(this.Slider2_Paint);
            // 
            // WaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.Controls.Add(this.Slider2);
            this.Controls.Add(this.Slider1);
            this.Name = "WaveForm";
            this.Size = new System.Drawing.Size(374, 178);
            this.VisibleChanged += new System.EventHandler(this.WaveForm_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private SliderCursor Slider1;
        private SliderCursor Slider2;
    }
}
