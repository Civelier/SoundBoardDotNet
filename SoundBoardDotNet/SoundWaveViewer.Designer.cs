namespace SoundBoardDotNet
{
    partial class SoundWaveViewer
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
            this.WaveGraph = new NAudio.Gui.WaveViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ZoomUpDown = new System.Windows.Forms.NumericUpDown();
            this.ZoomLabel = new System.Windows.Forms.Label();
            this.EndPositionUpDown = new System.Windows.Forms.NumericUpDown();
            this.StartPositionUpDown = new System.Windows.Forms.NumericUpDown();
            this.EndPositionLabel = new System.Windows.Forms.Label();
            this.StartPositionLabel = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.WaveGraphPanel = new System.Windows.Forms.Panel();
            this.HeadEnd = new SoundBoardDotNet.PlayHead();
            this.HeadStart = new SoundBoardDotNet.PlayHead();
            this.SpacingPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndPositionUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartPositionUpDown)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.WaveGraphPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WaveGraph
            // 
            this.WaveGraph.BackColor = System.Drawing.SystemColors.ControlLight;
            this.WaveGraph.Location = new System.Drawing.Point(37, 0);
            this.WaveGraph.Name = "WaveGraph";
            this.WaveGraph.SamplesPerPixel = 128;
            this.WaveGraph.Size = new System.Drawing.Size(545, 175);
            this.WaveGraph.StartPosition = ((long)(0));
            this.WaveGraph.TabIndex = 0;
            this.WaveGraph.WaveStream = null;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.ZoomUpDown);
            this.panel1.Controls.Add(this.ZoomLabel);
            this.panel1.Controls.Add(this.EndPositionUpDown);
            this.panel1.Controls.Add(this.StartPositionUpDown);
            this.panel1.Controls.Add(this.EndPositionLabel);
            this.panel1.Controls.Add(this.StartPositionLabel);
            this.panel1.Location = new System.Drawing.Point(0, 181);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(623, 54);
            this.panel1.TabIndex = 1;
            // 
            // ZoomUpDown
            // 
            this.ZoomUpDown.DecimalPlaces = 2;
            this.ZoomUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ZoomUpDown.Location = new System.Drawing.Point(250, 23);
            this.ZoomUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.ZoomUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ZoomUpDown.Name = "ZoomUpDown";
            this.ZoomUpDown.Size = new System.Drawing.Size(120, 22);
            this.ZoomUpDown.TabIndex = 5;
            this.ZoomUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // ZoomLabel
            // 
            this.ZoomLabel.AutoSize = true;
            this.ZoomLabel.Location = new System.Drawing.Point(247, 4);
            this.ZoomLabel.Name = "ZoomLabel";
            this.ZoomLabel.Size = new System.Drawing.Size(46, 16);
            this.ZoomLabel.TabIndex = 4;
            this.ZoomLabel.Text = "Zoom:";
            // 
            // EndPositionUpDown
            // 
            this.EndPositionUpDown.DecimalPlaces = 4;
            this.EndPositionUpDown.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.EndPositionUpDown.Location = new System.Drawing.Point(127, 24);
            this.EndPositionUpDown.Name = "EndPositionUpDown";
            this.EndPositionUpDown.Size = new System.Drawing.Size(92, 22);
            this.EndPositionUpDown.TabIndex = 3;
            // 
            // StartPositionUpDown
            // 
            this.StartPositionUpDown.DecimalPlaces = 4;
            this.StartPositionUpDown.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.StartPositionUpDown.Location = new System.Drawing.Point(7, 24);
            this.StartPositionUpDown.Name = "StartPositionUpDown";
            this.StartPositionUpDown.Size = new System.Drawing.Size(92, 22);
            this.StartPositionUpDown.TabIndex = 2;
            // 
            // EndPositionLabel
            // 
            this.EndPositionLabel.AutoSize = true;
            this.EndPositionLabel.Location = new System.Drawing.Point(129, 4);
            this.EndPositionLabel.Name = "EndPositionLabel";
            this.EndPositionLabel.Size = new System.Drawing.Size(85, 16);
            this.EndPositionLabel.TabIndex = 1;
            this.EndPositionLabel.Text = "End position:";
            // 
            // StartPositionLabel
            // 
            this.StartPositionLabel.AutoSize = true;
            this.StartPositionLabel.Location = new System.Drawing.Point(4, 4);
            this.StartPositionLabel.Name = "StartPositionLabel";
            this.StartPositionLabel.Size = new System.Drawing.Size(88, 16);
            this.StartPositionLabel.TabIndex = 0;
            this.StartPositionLabel.Text = "Start position:";
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.Controls.Add(this.WaveGraphPanel);
            this.MainPanel.Controls.Add(this.panel1);
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(623, 235);
            this.MainPanel.TabIndex = 1;
            // 
            // WaveGraphPanel
            // 
            this.WaveGraphPanel.AutoScroll = true;
            this.WaveGraphPanel.Controls.Add(this.HeadEnd);
            this.WaveGraphPanel.Controls.Add(this.HeadStart);
            this.WaveGraphPanel.Controls.Add(this.WaveGraph);
            this.WaveGraphPanel.Controls.Add(this.SpacingPanel);
            this.WaveGraphPanel.Location = new System.Drawing.Point(0, 0);
            this.WaveGraphPanel.Name = "WaveGraphPanel";
            this.WaveGraphPanel.Size = new System.Drawing.Size(623, 175);
            this.WaveGraphPanel.TabIndex = 2;
            // 
            // HeadEnd
            // 
            this.HeadEnd.AutoSize = true;
            this.HeadEnd.BackColor = System.Drawing.Color.Transparent;
            this.HeadEnd.HeadType = SoundBoardDotNet.PlayHeadType.End;
            this.HeadEnd.Location = new System.Drawing.Point(522, -2);
            this.HeadEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HeadEnd.Name = "HeadEnd";
            this.HeadEnd.Other = null;
            this.HeadEnd.ParentOffset = 0;
            this.HeadEnd.ParentPanel = null;
            this.HeadEnd.PointingX = 522;
            this.HeadEnd.Progression = double.PositiveInfinity;
            this.HeadEnd.ScrollX = 522;
            this.HeadEnd.Seconds = double.NaN;
            this.HeadEnd.Size = new System.Drawing.Size(35, 175);
            this.HeadEnd.TabIndex = 1;
            this.HeadEnd.TotalSeconds = 0D;
            this.HeadEnd.XLocation = 522;
            // 
            // HeadStart
            // 
            this.HeadStart.AutoSize = true;
            this.HeadStart.BackColor = System.Drawing.Color.Transparent;
            this.HeadStart.HeadType = SoundBoardDotNet.PlayHeadType.Start;
            this.HeadStart.Location = new System.Drawing.Point(0, 0);
            this.HeadStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HeadStart.Name = "HeadStart";
            this.HeadStart.Other = null;
            this.HeadStart.ParentOffset = 0;
            this.HeadStart.ParentPanel = null;
            this.HeadStart.PointingX = 46;
            this.HeadStart.Progression = double.PositiveInfinity;
            this.HeadStart.ScrollX = 46;
            this.HeadStart.Seconds = double.NaN;
            this.HeadStart.Size = new System.Drawing.Size(46, 175);
            this.HeadStart.TabIndex = 2;
            this.HeadStart.TotalSeconds = 0D;
            this.HeadStart.XLocation = 46;
            // 
            // SpacingPanel
            // 
            this.SpacingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SpacingPanel.BackColor = System.Drawing.Color.Transparent;
            this.SpacingPanel.Location = new System.Drawing.Point(583, 0);
            this.SpacingPanel.Name = "SpacingPanel";
            this.SpacingPanel.Size = new System.Drawing.Size(40, 175);
            this.SpacingPanel.TabIndex = 2;
            // 
            // SoundWaveViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPanel);
            this.Name = "SoundWaveViewer";
            this.Size = new System.Drawing.Size(623, 308);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndPositionUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartPositionUpDown)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.WaveGraphPanel.ResumeLayout(false);
            this.WaveGraphPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private NAudio.Gui.WaveViewer WaveGraph;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label EndPositionLabel;
        private System.Windows.Forms.Label StartPositionLabel;
        private System.Windows.Forms.NumericUpDown EndPositionUpDown;
        private System.Windows.Forms.NumericUpDown StartPositionUpDown;
        private System.Windows.Forms.NumericUpDown ZoomUpDown;
        private System.Windows.Forms.Label ZoomLabel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel WaveGraphPanel;
        private PlayHead HeadEnd;
        private PlayHead HeadStart;
        private System.Windows.Forms.Panel SpacingPanel;
    }
}
