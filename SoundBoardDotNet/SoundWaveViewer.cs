using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace SoundBoardDotNet
{
    public partial class SoundWaveViewer : UserControl
    {
        private AudioSound _sound;

        /// <summary>
        /// Seconds per pixels
        /// </summary>
        public double Zoom
        {
            get => (double)ZoomUpDown.Value;
            set
            {
                if ((decimal)value != ZoomUpDown.Value)
                {
                    ZoomUpDown.Value = (decimal)value;
                    _updateWidth();
                }
            }
        }

        public WaveStream WaveStream
        {
            get => WaveGraph.WaveStream;
            set
            {
                WaveGraph.WaveStream = value;
                _updateWidth();
            }
        }

        public double StartTime
        {
            get => (double)StartPositionUpDown.Value;
            set => StartPositionUpDown.Value = (decimal)value;
        }

        public double EndTime
        {
            get => (double)EndPositionUpDown.Value;
            set => EndPositionUpDown.Value = (decimal)value;
        }

        public AudioSound Sound
        {
            get => _sound;
            set
            {
                _sound = value;
                WaveStream = _sound?.FileReader;
            }
        }

        public NumericUpDown StartUpDown => StartPositionUpDown;
        public NumericUpDown EndUpDown => EndPositionUpDown;

        public double ScrollSpeed { get; set; }

        public SoundWaveViewer()
        {
            InitializeComponent();
            Zoom = 100;
            ZoomUpDown.ValueChanged += OnZoomChanged;
            HeadEnd.Other = HeadStart;
            HeadStart.Other = HeadEnd;
            HeadEnd.MouseDown += Head_MouseDown;
            HeadEnd.MouseMove += HeadEnd_MouseMove;
            HeadStart.MouseDown += Head_MouseDown;
            HeadStart.MouseMove += HeadStart_MouseMove;
            StartPositionUpDown.ValueChanged += StartPositionUpDown_ValueChanged;
            EndPositionUpDown.ValueChanged += EndPositionUpDown_ValueChanged;
            ScrollSpeed = 0.25;
            _updateWidth();
        }

        private void EndPositionUpDown_ValueChanged(object sender, EventArgs e)
        {
            HeadEnd.Seconds = (double)EndPositionUpDown.Value;
        }

        private void StartPositionUpDown_ValueChanged(object sender, EventArgs e)
        {
            HeadStart.Seconds = (double)StartPositionUpDown.Value;
        }

        private Point MouseDownLocation;

        private void Head_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
                PlayHead p = (PlayHead)(sender);
                GlidePanel(OutOfBoundDistance(p));
            }
        }

        private void HeadEnd_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                SuspendLayout();
                HeadEnd.PointingX = e.X + HeadEnd.PointingX - MouseDownLocation.X;
                var v = (decimal)HeadEnd.Seconds;
                if (v > EndPositionUpDown.Maximum || v < EndPositionUpDown.Minimum)
                {
                    v = Math.Max(EndPositionUpDown.Minimum, Math.Min(EndPositionUpDown.Maximum, v));
                    HeadEnd.Seconds = (double)v;
                }
                StartPositionUpDown.Maximum = v;
                EndPositionUpDown.Value = v;
                GlidePanel(OutOfBoundDistance(HeadEnd));
                ResumeLayout();
            }
        }

        private void HeadStart_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                SuspendLayout();
                HeadStart.PointingX = e.X + HeadStart.PointingX - MouseDownLocation.X;
                var v = (decimal)HeadStart.Seconds;
                if (v > StartPositionUpDown.Maximum || v < StartPositionUpDown.Minimum)
                {
                    v = Math.Max(StartPositionUpDown.Minimum, Math.Min(StartPositionUpDown.Maximum, v));
                    HeadStart.Seconds = (double)v;
                }
                if (v == EndPositionUpDown.Value) v = EndPositionUpDown.Value - 0.00001m;
                EndPositionUpDown.Minimum = v;
                StartPositionUpDown.Value = v;
                GlidePanel(OutOfBoundDistance(HeadStart));
                ResumeLayout();
            }
        }

        private bool IsOutOfBounds(PlayHead head)
        {
            return head.XLocation > WaveGraphPanel.Width + WaveGraphPanel.HorizontalScroll.Value || 
                head.XLocation < WaveGraphPanel.HorizontalScroll.Value;
        }

        private int OutOfBoundDistance(PlayHead head)
        {
            if (head.XLocation > WaveGraphPanel.Width)
            {
                return head.XLocation - WaveGraphPanel.Width;
            }
            if (head.XLocation < 0)
            {
                return head.XLocation;
            }
            return 0;
        }

        private void GlidePanel(int distance)
        {
            WaveGraphPanel.HorizontalScroll.Value += (int)Math.Round(ScrollSpeed * distance);
        }

        void OnZoomChanged(object sender, EventArgs e)
        {
            Zoom = (double)ZoomUpDown.Value;
            _updateWidth();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _updateWidth();
        }

        private void ResizeViewer(object sender, EventArgs e)
        {
            Width = Parent.Width - 20;
        }

        void _updateWidth()
        {
            if (Sound?.FileReader != null)
                WaveGraph.WaveStream = Sound?.FileReader;
            HeadEnd.Height = HeadStart.Height = WaveGraph.Height = SpacingPanel.Height = WaveGraphPanel.Height - 20;
            if (WaveStream == null)
            {
                WaveGraph.Width = WaveGraphPanel.Width - 2 * 40;
                return;
            }
            int bps = WaveStream.WaveFormat.AverageBytesPerSecond;
            long totalBytes = WaveStream.Length;
            WaveGraph.SamplesPerPixel = (int)Math.Round(Zoom * bps / 10000);
            WaveGraph.Left = 40;
            WaveGraph.Width = (int)Math.Round((double)totalBytes / (WaveGraph.SamplesPerPixel * 8));
            HeadEnd.ParentWidth = HeadStart.ParentWidth = WaveGraph.Width + 2 * WaveGraph.Left;
            SpacingPanel.Left = WaveGraph.Right;
            HeadEnd.ParentOffset = HeadStart.ParentOffset = WaveGraph.Left;
            HeadEnd.ParentPanel = HeadStart.ParentPanel = WaveGraphPanel;
            HeadEnd.TotalSeconds = HeadStart.TotalSeconds = WaveStream.TotalTime.TotalSeconds;
            HeadStart.Seconds = Sound?.StartPos ?? 0;
            HeadEnd.Seconds = Sound?.EndPos ?? WaveStream.TotalTime.TotalSeconds;
        }
    }
}
