using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public double StartTime
        {
            get;
            set;
        }

        public double EndTime
        {
            get;
            set;
        }

        public AudioSound Sound
        {
            get => _sound;
            set
            {
                _sound = value;
                WaveGraph.WaveStream = _sound?.FileReader;
                _updateWidth();
            }
        }

        private bool _outOfboundDragging = false;

        public NumericUpDown StartUpDown => StartPositionUpDown;
        public NumericUpDown EndUpDown => EndPositionUpDown;

        public SoundWaveViewer()
        {
            InitializeComponent();
            Zoom = 100;
            ZoomUpDown.ValueChanged += OnZoomChanged;
            HeadEnd.Other = HeadStart;
            HeadStart.Other = HeadEnd;
            HeadEnd.ParentOffset = HeadStart.ParentOffset = 40;
            HeadEnd.ParentWidth = HeadStart.ParentWidth = Width;
            HeadEnd.MouseDown += Head_MouseDown;
            HeadEnd.MouseMove += HeadEnd_MouseMove;
            HeadStart.MouseDown += Head_MouseDown;
            HeadStart.MouseMove += HeadStart_MouseMove;
            StartPositionUpDown.ValueChanged += StartPositionUpDown_ValueChanged;
            EndPositionUpDown.ValueChanged += EndPositionUpDown_ValueChanged;
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
            }
        }

        private void HeadEnd_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                HeadEnd.XLocation = e.X + HeadEnd.Left - MouseDownLocation.X;
                var v = (decimal)HeadEnd.Seconds;
                if (v > EndPositionUpDown.Maximum || v < EndPositionUpDown.Minimum)
                {
                    v = Math.Max(EndPositionUpDown.Minimum, Math.Min(EndPositionUpDown.Maximum, v));
                    HeadEnd.Seconds = (double)v;
                }
                EndPositionUpDown.Value = v;
            }
        }

        private void HeadStart_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                HeadStart.XLocation = e.X + HeadStart.Left - MouseDownLocation.X;
                var v = (decimal)HeadStart.Seconds;
                if (v > StartPositionUpDown.Maximum || v < StartPositionUpDown.Minimum)
                {
                    v = Math.Max(StartPositionUpDown.Minimum, Math.Min(StartPositionUpDown.Maximum, v));
                    HeadStart.Seconds = (double)v;
                }
                StartPositionUpDown.Value = v;
            }
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
            HeadEnd.Height = HeadStart.Height = WaveGraph.Height = WaveGraphPanel.Height - 20;
            if (Sound?.FileReader == null)
            {
                WaveGraph.Width = WaveGraphPanel.Width;
                return;
            }
            int bps = Sound.FileReader.WaveFormat.AverageBytesPerSecond;
            long totalBytes = Sound.FileReader.Length;
            WaveGraph.SamplesPerPixel = (int)Math.Round(Zoom * bps / 10000);
            WaveGraph.Width = (int)Math.Round((double)totalBytes / (WaveGraph.SamplesPerPixel * 8));
            HeadEnd.ParentOffset = HeadStart.ParentOffset = 40;
            HeadEnd.ParentWidth = HeadStart.ParentWidth = Width;
            HeadEnd.TotalSeconds = HeadStart.TotalSeconds = Sound.FileReader.TotalTime.TotalSeconds;
            HeadStart.Seconds = Sound.StartPos;
            HeadEnd.Seconds = Sound.EndPos;
        }
    }
}
