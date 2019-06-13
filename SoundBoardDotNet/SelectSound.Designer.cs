namespace SoundBoardDotNet
{
    partial class SelectSound
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.FileNameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.VolumeControl = new NAudio.Gui.VolumeSlider();
            this.WaveGraph = new NAudio.Gui.WaveViewer();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(232, 436);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(4);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(100, 28);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // FileNameBox
            // 
            this.FileNameBox.Location = new System.Drawing.Point(20, 31);
            this.FileNameBox.Margin = new System.Windows.Forms.Padding(4);
            this.FileNameBox.Name = "FileNameBox";
            this.FileNameBox.Size = new System.Drawing.Size(570, 22);
            this.FileNameBox.TabIndex = 1;
            this.FileNameBox.TextChanged += new System.EventHandler(this.FileNameBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sound file:";
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(598, 31);
            this.BrowseButton.Margin = new System.Windows.Forms.Padding(4);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(77, 25);
            this.BrowseButton.TabIndex = 3;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(20, 63);
            this.PlayButton.Margin = new System.Windows.Forms.Padding(4);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(100, 28);
            this.PlayButton.TabIndex = 4;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(128, 63);
            this.StopButton.Margin = new System.Windows.Forms.Padding(4);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(100, 28);
            this.StopButton.TabIndex = 5;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Name:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(20, 114);
            this.NameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(655, 22);
            this.NameTextBox.TabIndex = 7;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(340, 436);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(100, 28);
            this.CancelButton.TabIndex = 8;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(236, 63);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(4);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(100, 28);
            this.ClearButton.TabIndex = 12;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // Volume
            // 
            this.VolumeControl.Location = new System.Drawing.Point(19, 143);
            this.VolumeControl.Name = "Volume";
            this.VolumeControl.Size = new System.Drawing.Size(657, 26);
            this.VolumeControl.TabIndex = 13;
            this.VolumeControl.VolumeChanged += new System.EventHandler(this.Volume_VolumeChanged);
            // 
            // WaveGraph
            // 
            this.WaveGraph.Location = new System.Drawing.Point(12, 175);
            this.WaveGraph.Name = "WaveGraph";
            this.WaveGraph.SamplesPerPixel = 128;
            this.WaveGraph.Size = new System.Drawing.Size(664, 254);
            this.WaveGraph.StartPosition = ((long)(0));
            this.WaveGraph.TabIndex = 14;
            this.WaveGraph.WaveStream = null;
            // 
            // SelectSound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 479);
            this.Controls.Add(this.WaveGraph);
            this.Controls.Add(this.VolumeControl);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileNameBox);
            this.Controls.Add(this.SaveButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SelectSound";
            this.Text = "SelectSound";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectSound_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelectSound_FormClosed);
            this.Shown += new System.EventHandler(this.SelectSound_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox FileNameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ClearButton;
        private NAudio.Gui.VolumeSlider VolumeControl;
        private NAudio.Gui.WaveViewer WaveGraph;
    }
}