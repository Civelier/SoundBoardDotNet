namespace SoundBoardDotNet
{
    partial class SaveSound
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
            this.TotalTimeLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.VolumeLabel = new System.Windows.Forms.Label();
            this.WaveGraph = new NAudio.Gui.WaveViewer();
            this.VolumeControl = new NAudio.Gui.VolumeSlider();
            this.CancelButton = new System.Windows.Forms.Button();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.KeyCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.InputCombo = new System.Windows.Forms.ComboBox();
            this.SoundWaveGraph = new SoundBoardDotNet.SoundWaveViewer();
            this.SuspendLayout();
            // 
            // TotalTimeLabel
            // 
            this.TotalTimeLabel.AutoSize = true;
            this.TotalTimeLabel.Location = new System.Drawing.Point(442, 140);
            this.TotalTimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TotalTimeLabel.Name = "TotalTimeLabel";
            this.TotalTimeLabel.Size = new System.Drawing.Size(0, 13);
            this.TotalTimeLabel.TabIndex = 44;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(386, 140);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "Total time:";
            // 
            // VolumeLabel
            // 
            this.VolumeLabel.AutoSize = true;
            this.VolumeLabel.Location = new System.Drawing.Point(9, 114);
            this.VolumeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.VolumeLabel.Name = "VolumeLabel";
            this.VolumeLabel.Size = new System.Drawing.Size(45, 13);
            this.VolumeLabel.TabIndex = 36;
            this.VolumeLabel.Text = "Volume:";
            // 
            // WaveGraph
            // 
            this.WaveGraph.AutoScroll = true;
            this.WaveGraph.Location = new System.Drawing.Point(7, 161);
            this.WaveGraph.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.WaveGraph.Name = "WaveGraph";
            this.WaveGraph.SamplesPerPixel = 128;
            this.WaveGraph.Size = new System.Drawing.Size(580, 210);
            this.WaveGraph.StartPosition = ((long)(0));
            this.WaveGraph.TabIndex = 35;
            this.WaveGraph.WaveStream = null;
            // 
            // VolumeControl
            // 
            this.VolumeControl.Location = new System.Drawing.Point(58, 106);
            this.VolumeControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.VolumeControl.Name = "VolumeControl";
            this.VolumeControl.Size = new System.Drawing.Size(529, 21);
            this.VolumeControl.TabIndex = 34;
            this.VolumeControl.VolumeChanged += new System.EventHandler(this.VolumeControl_VolumeChanged);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(88, 375);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(10, 53);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(577, 20);
            this.NameTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Name:";
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(88, 77);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 29;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(7, 78);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(75, 23);
            this.PlayButton.TabIndex = 28;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(7, 375);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // KeyCombo
            // 
            this.KeyCombo.FormattingEnabled = true;
            this.KeyCombo.Location = new System.Drawing.Point(226, 80);
            this.KeyCombo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.KeyCombo.Name = "KeyCombo";
            this.KeyCombo.Size = new System.Drawing.Size(92, 21);
            this.KeyCombo.TabIndex = 2;
            this.KeyCombo.SelectedIndexChanged += new System.EventHandler(this.KeyCombo_SelectedIndexChanged);
            this.KeyCombo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyCombo_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(165, 82);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "Set to key:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Input:";
            // 
            // InputCombo
            // 
            this.InputCombo.FormattingEnabled = true;
            this.InputCombo.Location = new System.Drawing.Point(46, 8);
            this.InputCombo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.InputCombo.Name = "InputCombo";
            this.InputCombo.Size = new System.Drawing.Size(159, 21);
            this.InputCombo.TabIndex = 0;
            this.InputCombo.SelectedIndexChanged += new System.EventHandler(this.InputCombo_SelectedIndexChanged);
            // 
            // SoundWaveGraph
            // 
            this.SoundWaveGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SoundWaveGraph.EndTime = 0D;
            this.SoundWaveGraph.Location = new System.Drawing.Point(7, 161);
            this.SoundWaveGraph.Margin = new System.Windows.Forms.Padding(2);
            this.SoundWaveGraph.Name = "SoundWaveGraph";
            this.SoundWaveGraph.PlayingSound = null;
            this.SoundWaveGraph.PlaySeconds = double.NaN;
            this.SoundWaveGraph.ScrollSpeed = 0.25D;
            this.SoundWaveGraph.Size = new System.Drawing.Size(580, 210);
            this.SoundWaveGraph.SoundInfo = null;
            this.SoundWaveGraph.StartTime = 0D;
            this.SoundWaveGraph.TabIndex = 48;
            this.SoundWaveGraph.Zoom = 100D;
            // 
            // SaveSound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 410);
            this.Controls.Add(this.SoundWaveGraph);
            this.Controls.Add(this.InputCombo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.KeyCombo);
            this.Controls.Add(this.TotalTimeLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.VolumeLabel);
            this.Controls.Add(this.WaveGraph);
            this.Controls.Add(this.VolumeControl);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.SaveButton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SaveSound";
            this.Text = "SaveSound";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveSound_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SaveSound_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TotalTimeLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label VolumeLabel;
        private NAudio.Gui.WaveViewer WaveGraph;
        private NAudio.Gui.VolumeSlider VolumeControl;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.ComboBox KeyCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox InputCombo;
        private SoundWaveViewer SoundWaveGraph;
    }
}