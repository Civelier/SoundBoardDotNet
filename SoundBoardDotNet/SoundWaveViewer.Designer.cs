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
            this.components = new System.ComponentModel.Container();
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
            this.SpacingPanel = new System.Windows.Forms.Panel();
            this.HeadMove = new System.Windows.Forms.Timer(this.components);
            this.CurrentPositionLable = new System.Windows.Forms.Label();
            this.CurrentPositionValueLabel = new System.Windows.Forms.Label();
            this.HeadCurrent = new SoundBoardDotNet.PlayHead();
            this.HeadEnd = new SoundBoardDotNet.PlayHead();
            this.HeadStart = new SoundBoardDotNet.PlayHead();
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
            this.WaveGraph.Location = new System.Drawing.Point(28, 0);
            this.WaveGraph.Margin = new System.Windows.Forms.Padding(2);
            this.WaveGraph.Name = "WaveGraph";
            this.WaveGraph.SamplesPerPixel = 128;
            this.WaveGraph.Size = new System.Drawing.Size(409, 142);
            this.WaveGraph.StartPosition = ((long)(0));
            this.WaveGraph.TabIndex = 0;
            this.WaveGraph.WaveStream = null;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.CurrentPositionValueLabel);
            this.panel1.Controls.Add(this.CurrentPositionLable);
            this.panel1.Controls.Add(this.ZoomUpDown);
            this.panel1.Controls.Add(this.ZoomLabel);
            this.panel1.Controls.Add(this.EndPositionUpDown);
            this.panel1.Controls.Add(this.StartPositionUpDown);
            this.panel1.Controls.Add(this.EndPositionLabel);
            this.panel1.Controls.Add(this.StartPositionLabel);
            this.panel1.Location = new System.Drawing.Point(0, 147);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 44);
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
            this.ZoomUpDown.Location = new System.Drawing.Point(188, 19);
            this.ZoomUpDown.Margin = new System.Windows.Forms.Padding(2);
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
            this.ZoomUpDown.Size = new System.Drawing.Size(90, 20);
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
            this.ZoomLabel.Location = new System.Drawing.Point(185, 3);
            this.ZoomLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ZoomLabel.Name = "ZoomLabel";
            this.ZoomLabel.Size = new System.Drawing.Size(37, 13);
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
            this.EndPositionUpDown.Location = new System.Drawing.Point(95, 20);
            this.EndPositionUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.EndPositionUpDown.Name = "EndPositionUpDown";
            this.EndPositionUpDown.Size = new System.Drawing.Size(69, 20);
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
            this.StartPositionUpDown.Location = new System.Drawing.Point(5, 20);
            this.StartPositionUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.StartPositionUpDown.Name = "StartPositionUpDown";
            this.StartPositionUpDown.Size = new System.Drawing.Size(69, 20);
            this.StartPositionUpDown.TabIndex = 2;
            // 
            // EndPositionLabel
            // 
            this.EndPositionLabel.AutoSize = true;
            this.EndPositionLabel.Location = new System.Drawing.Point(92, 3);
            this.EndPositionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.EndPositionLabel.Name = "EndPositionLabel";
            this.EndPositionLabel.Size = new System.Drawing.Size(68, 13);
            this.EndPositionLabel.TabIndex = 1;
            this.EndPositionLabel.Text = "End position:";
            // 
            // StartPositionLabel
            // 
            this.StartPositionLabel.AutoSize = true;
            this.StartPositionLabel.Location = new System.Drawing.Point(3, 3);
            this.StartPositionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.StartPositionLabel.Name = "StartPositionLabel";
            this.StartPositionLabel.Size = new System.Drawing.Size(71, 13);
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
            this.MainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(467, 191);
            this.MainPanel.TabIndex = 1;
            // 
            // WaveGraphPanel
            // 
            this.WaveGraphPanel.AutoScroll = true;
            this.WaveGraphPanel.Controls.Add(this.HeadCurrent);
            this.WaveGraphPanel.Controls.Add(this.HeadEnd);
            this.WaveGraphPanel.Controls.Add(this.HeadStart);
            this.WaveGraphPanel.Controls.Add(this.WaveGraph);
            this.WaveGraphPanel.Controls.Add(this.SpacingPanel);
            this.WaveGraphPanel.Location = new System.Drawing.Point(0, 0);
            this.WaveGraphPanel.Margin = new System.Windows.Forms.Padding(2);
            this.WaveGraphPanel.Name = "WaveGraphPanel";
            this.WaveGraphPanel.Size = new System.Drawing.Size(467, 142);
            this.WaveGraphPanel.TabIndex = 2;
            // 
            // SpacingPanel
            // 
            this.SpacingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SpacingPanel.BackColor = System.Drawing.Color.Transparent;
            this.SpacingPanel.Location = new System.Drawing.Point(437, 0);
            this.SpacingPanel.Margin = new System.Windows.Forms.Padding(2);
            this.SpacingPanel.Name = "SpacingPanel";
            this.SpacingPanel.Size = new System.Drawing.Size(30, 142);
            this.SpacingPanel.TabIndex = 2;
            // 
            // HeadMove
            // 
            this.HeadMove.Tick += new System.EventHandler(this.HeadMove_Tick);
            // 
            // CurrentPositionLable
            // 
            this.CurrentPositionLable.AutoSize = true;
            this.CurrentPositionLable.Location = new System.Drawing.Point(301, 3);
            this.CurrentPositionLable.Name = "CurrentPositionLable";
            this.CurrentPositionLable.Size = new System.Drawing.Size(83, 13);
            this.CurrentPositionLable.TabIndex = 2;
            this.CurrentPositionLable.Text = "Current position:";
            // 
            // CurrentPositionValueLabel
            // 
            this.CurrentPositionValueLabel.AutoSize = true;
            this.CurrentPositionValueLabel.Location = new System.Drawing.Point(301, 22);
            this.CurrentPositionValueLabel.Name = "CurrentPositionValueLabel";
            this.CurrentPositionValueLabel.Size = new System.Drawing.Size(61, 13);
            this.CurrentPositionValueLabel.TabIndex = 6;
            this.CurrentPositionValueLabel.Text = "00:00.0000";
            // 
            // HeadCurrent
            // 
            this.HeadCurrent.AutoSize = true;
            this.HeadCurrent.BackColor = System.Drawing.Color.Transparent;
            this.HeadCurrent.HeadType = SoundBoardDotNet.PlayHeadType.Current;
            this.HeadCurrent.Location = new System.Drawing.Point(321, -2);
            this.HeadCurrent.Margin = new System.Windows.Forms.Padding(0);
            this.HeadCurrent.Name = "HeadCurrent";
            this.HeadCurrent.ParentOffset = 0;
            this.HeadCurrent.ParentPanel = this.WaveGraphPanel;
            this.HeadCurrent.ParentWidth = 0;
            this.HeadCurrent.PointingX = 340;
            this.HeadCurrent.Progression = double.PositiveInfinity;
            this.HeadCurrent.ScrollX = 321;
            this.HeadCurrent.Seconds = double.NaN;
            this.HeadCurrent.Size = new System.Drawing.Size(38, 142);
            this.HeadCurrent.TabIndex = 3;
            this.HeadCurrent.TotalSeconds = 0D;
            this.HeadCurrent.XLocation = 321;
            this.HeadCurrent.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HeadCurrent_MouseUp);
            // 
            // HeadEnd
            // 
            this.HeadEnd.AutoSize = true;
            this.HeadEnd.BackColor = System.Drawing.Color.Transparent;
            this.HeadEnd.HeadType = SoundBoardDotNet.PlayHeadType.End;
            this.HeadEnd.Location = new System.Drawing.Point(392, -2);
            this.HeadEnd.Margin = new System.Windows.Forms.Padding(2);
            this.HeadEnd.Name = "HeadEnd";
            this.HeadEnd.ParentOffset = 0;
            this.HeadEnd.ParentPanel = this.WaveGraphPanel;
            this.HeadEnd.ParentWidth = 0;
            this.HeadEnd.PointingX = 392;
            this.HeadEnd.Progression = double.PositiveInfinity;
            this.HeadEnd.ScrollX = 392;
            this.HeadEnd.Seconds = double.NaN;
            this.HeadEnd.Size = new System.Drawing.Size(34, 142);
            this.HeadEnd.TabIndex = 1;
            this.HeadEnd.TotalSeconds = 0D;
            this.HeadEnd.XLocation = 392;
            // 
            // HeadStart
            // 
            this.HeadStart.AutoSize = true;
            this.HeadStart.BackColor = System.Drawing.Color.Transparent;
            this.HeadStart.HeadType = SoundBoardDotNet.PlayHeadType.Start;
            this.HeadStart.Location = new System.Drawing.Point(0, -2);
            this.HeadStart.Margin = new System.Windows.Forms.Padding(2);
            this.HeadStart.Name = "HeadStart";
            this.HeadStart.ParentOffset = 0;
            this.HeadStart.ParentPanel = this.WaveGraphPanel;
            this.HeadStart.ParentWidth = 0;
            this.HeadStart.PointingX = 34;
            this.HeadStart.Progression = double.PositiveInfinity;
            this.HeadStart.ScrollX = 0;
            this.HeadStart.Seconds = double.NaN;
            this.HeadStart.Size = new System.Drawing.Size(34, 142);
            this.HeadStart.TabIndex = 2;
            this.HeadStart.TotalSeconds = 0D;
            this.HeadStart.XLocation = 0;
            // 
            // SoundWaveViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SoundWaveViewer";
            this.Size = new System.Drawing.Size(467, 250);
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
        private PlayHead HeadCurrent;
        private System.Windows.Forms.Timer HeadMove;
        private System.Windows.Forms.Label CurrentPositionValueLabel;
        private System.Windows.Forms.Label CurrentPositionLable;
    }
}
