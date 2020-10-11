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
using NAudio.Gui;

namespace SoundBoardDotNet
{
    public partial class SoundWaveViewer : UserControl, INotifyPropertyChanged
    {
        private AudioSoundInfo _soundInfo;

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
        private AudioSound _playingSound;
        public AudioSound PlayingSound

        {
            get => _playingSound;
            set
            {
                if (_playingSound != value)
                {
                    if (_playingSound != null)
                    {
                        _playingSound.Stopped -= _playingSound_Stopped;
                        _playingSound.Disposed -= _playingSound_Disposed;
                    }
                    _playingSound = value;
                    if (_playingSound != null)
                    {
                        _playingSound.Stopped += _playingSound_Stopped;
                        _playingSound.Disposed += _playingSound_Disposed;
                    }
                }
            }
        }

        private void _playingSound_Disposed(AudioSound sender)
        {
            _playingSound = null;
        }

        private void _playingSound_Stopped(AudioSound sender, SoundEvents.SoundStoppedEventArgs args)
        {
            _playingSound = null;
        }

        public WaveStream WaveStream
        {
            get => SoundInfo?.WaveStream;
        }

        public double PlaySeconds
        {
            get => HeadCurrent.Seconds;
            set
            {
                CurrentPositionValueLabel.Text = TimeSpan.FromSeconds(value).ToString(@"mm\:ss\.ffff");
                HeadCurrent.Seconds = value;
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

        public AudioSoundInfo SoundInfo
        {
            get => _soundInfo;
            set
            {
                if (_soundInfo != value)
                {
                    if (_soundInfo != null)
                    {
                        _soundInfo.Dispose();
                    }
                    _soundInfo = value;
                    WaveGraph.WaveStream = _soundInfo?.WaveStream;
                    if (_soundInfo != null)
                    {
                        _soundInfo.SoundInstanceStarted += _soundInfo_SoundInstanceStarted;
                        _soundInfo.Disposed += _soundInfo_Disposed;
                    }
                }
                
            }
        }

        private void _soundInfo_SoundInstanceStarted(AudioSoundInfo sender, AudioSound newInstance)
        {
            PlayingSound = newInstance;
        }

        private void _soundInfo_Disposed(AudioSoundInfo sender)
        {
            _soundInfo = null;
            sender.Disposed -= _soundInfo_Disposed;
            sender.SoundInstanceStarted -= _soundInfo_SoundInstanceStarted;
        }

        public NumericUpDown StartUpDown => StartPositionUpDown;
        public NumericUpDown EndUpDown => EndPositionUpDown;

        public double ScrollSpeed { get; set; }

        public SoundWaveViewer()
        {
            InitializeComponent();
            Zoom = 100;
            ZoomUpDown.ValueChanged += OnZoomChanged;
            //HeadEnd.Other = HeadStart;
            //HeadStart.Other = HeadEnd;
            HeadEnd.MouseDown += Head_MouseDown;
            HeadEnd.MouseMove += HeadEnd_MouseMove;
            HeadStart.MouseDown += Head_MouseDown;
            HeadStart.MouseMove += HeadStart_MouseMove;
            HeadCurrent.MouseDown += Head_MouseDown;
            HeadCurrent.MouseMove += HeadCurrent_MouseMove;
            StartPositionUpDown.ValueChanged += StartPositionUpDown_ValueChanged;
            EndPositionUpDown.ValueChanged += EndPositionUpDown_ValueChanged;
            ScrollSpeed = 0.25;
            HeadMove.Enabled = true;
            _updateWidth();
        }

        private void HeadCurrent_MouseMove(object sender, MouseEventArgs e)
        {
            if (SoundInfo == null) return;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                SuspendLayout();
                //if (HeadCurrent.Seconds > HeadEnd.Seconds || HeadCurrent.Seconds < HeadStart.Seconds)
                //{
                    
                //}
                HeadCurrent.PointingX = Math.Max(HeadStart.PointingX, Math.Min(HeadEnd.PointingX, e.X + HeadCurrent.PointingX - MouseDownLocation.X));
                UpdateCurrentPositionTimeLabel();
                GlidePanel(OutOfBoundDistance(HeadCurrent));
                ResumeLayout();
            }
        }

        private void UpdateCurrentPositionTimeLabel()
        {
            CurrentPositionValueLabel.Text = TimeSpan.FromSeconds(PlaySeconds).ToString(@"mm\:ss\.ffff");
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void Head_MouseDown(object sender, MouseEventArgs e)
        {
            if (SoundInfo == null) return;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
                PlayHead p = (PlayHead)(sender);
                GlidePanel(OutOfBoundDistance(p));
            }
        }

        private void HeadEnd_MouseMove(object sender, MouseEventArgs e)
        {
            if (SoundInfo == null) return;
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
                HeadCurrent.Seconds = Math.Min(HeadCurrent.Seconds, HeadEnd.Seconds);
                GlidePanel(OutOfBoundDistance(HeadEnd));
                ResumeLayout();
            }
        }

        private void HeadStart_MouseMove(object sender, MouseEventArgs e)
        {
            if (SoundInfo == null) return;
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
                HeadCurrent.Seconds = Math.Max(HeadCurrent.Seconds, HeadStart.Seconds);
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
            int i = (int)Math.Round(ScrollSpeed * distance);
            if (WaveGraphPanel.HorizontalScroll.Value + i >= 0) WaveGraphPanel.HorizontalScroll.Value += (int)Math.Round(ScrollSpeed * distance);
        }

        void OnZoomChanged(object sender, EventArgs e)
        {
            Zoom = (double)ZoomUpDown.Value;
            _updateWidth();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _updateWidth(false);
        }

        private void ResizeViewer(object sender, EventArgs e)
        {
            Width = Parent.Width - 20;
        }

        void _updateWidth(bool resetCursors = true)
        {
            SuspendLayout();
            double startPos = SoundInfo?.StartPos ?? 0, currentPos = SoundInfo?.StartPos ?? 0;
            double endPos = SoundInfo?.EndPos ?? 0;
            if (!resetCursors)
            {
                startPos = HeadStart.Seconds;
                currentPos = HeadCurrent.Seconds;
                endPos = HeadEnd.Seconds;
            }

            HeadEnd.Height = HeadStart.Height = HeadCurrent.Height = WaveGraph.Height = SpacingPanel.Height = WaveGraphPanel.Height - 20;
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
            HeadEnd.ParentWidth = HeadStart.ParentWidth = HeadCurrent.ParentWidth = WaveGraph.Width + 2 * WaveGraph.Left;
            SpacingPanel.Left = WaveGraph.Right;
            HeadEnd.ParentOffset = HeadStart.ParentOffset = HeadCurrent.ParentOffset = WaveGraph.Left;
            HeadEnd.ParentPanel = HeadStart.ParentPanel = HeadCurrent.ParentPanel = WaveGraphPanel;
            HeadEnd.TotalSeconds = HeadStart.TotalSeconds = HeadCurrent.TotalSeconds = WaveStream.TotalTime.TotalSeconds;
            HeadStart.Seconds = startPos;
            HeadCurrent.Seconds = currentPos;
            HeadEnd.Seconds = endPos;
            ResumeLayout();
        }

        private void WaveGraph_MouseDown(object sender, MouseEventArgs e)
        {
            Head_MouseDown(HeadCurrent, e);
        }

        private void WaveGraph_MouseMove(object sender, MouseEventArgs e)
        {
            HeadCurrent_MouseMove(HeadCurrent, e);
        }

        private void HeadCurrent_MouseUp(object sender, MouseEventArgs e)
        {
            if (SoundInfo != null)
            {
                PlaySeconds = Math.Max(HeadStart.Seconds, Math.Min(HeadEnd.Seconds, HeadCurrent.Seconds));
                SoundInfo.GetAudioSound().Play();
            }
        }

        private void HeadMove_Tick(object sender, EventArgs e)
        {
            if (PlayingSound != null)
            {
                PlaySeconds = PlayingSound.Time;
            }
        }
    }
}
