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
            this.VolumeTrack = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.LabelVolume = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.WaveGraph = new SoundBoardDotNet.WaveForm();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GradLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.VolumeTrack)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(174, 354);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // FileNameBox
            // 
            this.FileNameBox.Location = new System.Drawing.Point(15, 25);
            this.FileNameBox.Name = "FileNameBox";
            this.FileNameBox.Size = new System.Drawing.Size(414, 20);
            this.FileNameBox.TabIndex = 1;
            this.FileNameBox.TextChanged += new System.EventHandler(this.FileNameBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sound file:";
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(435, 25);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(58, 20);
            this.BrowseButton.TabIndex = 3;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(15, 51);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(75, 23);
            this.PlayButton.TabIndex = 4;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(96, 51);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 5;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Name:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(15, 93);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(478, 20);
            this.NameTextBox.TabIndex = 7;
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(255, 354);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 8;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // VolumeTrack
            // 
            this.VolumeTrack.Location = new System.Drawing.Point(15, 136);
            this.VolumeTrack.Maximum = 100;
            this.VolumeTrack.Name = "VolumeTrack";
            this.VolumeTrack.Size = new System.Drawing.Size(447, 45);
            this.VolumeTrack.TabIndex = 9;
            this.VolumeTrack.Value = 100;
            this.VolumeTrack.Scroll += new System.EventHandler(this.VolumeTrack_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Volume:";
            // 
            // LabelVolume
            // 
            this.LabelVolume.AutoSize = true;
            this.LabelVolume.Location = new System.Drawing.Point(468, 136);
            this.LabelVolume.Name = "LabelVolume";
            this.LabelVolume.Size = new System.Drawing.Size(25, 13);
            this.LabelVolume.TabIndex = 11;
            this.LabelVolume.Text = "100";
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(177, 51);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 12;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // WaveGraph
            // 
            this.WaveGraph.BackColor = System.Drawing.Color.DarkGray;
            this.WaveGraph.Location = new System.Drawing.Point(0, 0);
            this.WaveGraph.Name = "WaveGraph";
            this.WaveGraph.Size = new System.Drawing.Size(478, 160);
            this.WaveGraph.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.WaveGraph);
            this.panel1.Location = new System.Drawing.Point(15, 188);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 160);
            this.panel1.TabIndex = 14;
            // 
            // GradLabel
            // 
            this.GradLabel.AutoSize = true;
            this.GradLabel.Location = new System.Drawing.Point(15, 355);
            this.GradLabel.Name = "GradLabel";
            this.GradLabel.Size = new System.Drawing.Size(36, 13);
            this.GradLabel.TabIndex = 15;
            this.GradLabel.Text = "Grad: ";
            // 
            // SelectSound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 389);
            this.Controls.Add(this.GradLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.LabelVolume);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.VolumeTrack);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileNameBox);
            this.Controls.Add(this.SaveButton);
            this.Name = "SelectSound";
            this.Text = "SelectSound";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectSound_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelectSound_FormClosed);
            this.Shown += new System.EventHandler(this.SelectSound_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.VolumeTrack)).EndInit();
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.TrackBar VolumeTrack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LabelVolume;
        private System.Windows.Forms.Button ClearButton;
        private WaveForm WaveGraph;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label GradLabel;
    }
}