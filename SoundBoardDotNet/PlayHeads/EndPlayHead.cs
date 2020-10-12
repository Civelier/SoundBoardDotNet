using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundBoardDotNet.PlayHeads
{
    public partial class EndPlayHead : UserControl, IPlayHead
    {
        private AudioSoundInfo _soundInfo;

        public double Seconds
        {
            get
            {
                if (WaveViewerWidth == 0) return 0;
                return (double)PointingX / WaveViewerWidth;
            }
            set
            {
                if (Seconds != value)
                {
                    PointingX = (int)Math.Round(Progression * WaveViewerWidth);
                    OnPropertyChanged("Seconds");
                }
            }
        }

        public double Progression
        {
            get => TotalSeconds == 0 ? 0 : Seconds / TotalSeconds;
            set => Seconds = value * TotalSeconds;
        }

        public double TotalSeconds
        {
            get => Viewer?.SoundInfo?.TotalSeconds ?? 0;
        }

        public int ParentWidth => Parent?.Width ?? 0;

        public int ScrollX
        {
            get => (ParentPanel?.HorizontalScroll?.Value ?? 0) + XLocation;
            set => XLocation = value - (ParentPanel?.HorizontalScroll?.Value ?? 0);
        }
        public int PointingX { get => ScrollX; set => ScrollX = value; }

        public int ParentOffset { private get; set; }

        public Panel CursorPanel { get; private set; }

        private int totalHeight => ParentPanel?.Height ?? Height;

        private int cursorHeight => totalHeight - Height;

        public Panel ParentPanel { private get; set; }

        public int XLocation
        {
            get
            {
                return Left;
            }
            set
            {
                Left = value;
                if (SoundInfo != null) SoundInfo.EndPos = Seconds;
                if (CursorPanel != null) CursorPanel.Left = PointingX - ParentOffset - 1;
            }
        }
        public SoundWaveViewer Viewer { private get; set; }

        public int WaveViewerWidth { private get; set; }
        public AudioSoundInfo SoundInfo
        {
            private get => _soundInfo;
            set
            {
                if (_soundInfo != value)
                {
                    if (_soundInfo != null)
                    {
                        _soundInfo.PropertyChanged -= _soundInfo_PropertyChanged;
                    }
                    _soundInfo = value;
                    if (_soundInfo != null)
                    {
                        _soundInfo.PropertyChanged += _soundInfo_PropertyChanged;
                    }
                }
            }
        }

        private void _soundInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "EndPos":
                    Seconds = SoundInfo.EndPos;
                    break;
                default:
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public EndPlayHead()
        {
            InitializeComponent();
        }

        internal void CreateCursorPanel()
        {
            ParentPanel = (Panel)Parent;
            CursorPanel = new Panel();
            CursorPanel.Parent = Parent;
            CursorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            CursorPanel.BackColor = Color.FromArgb(255, 0, 0);
            CursorPanel.Size = new Size(1, cursorHeight);
            CursorPanel.Location = new Point(PointingX - ParentOffset, 0);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Head_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void Head_MouseMove(object sender, MouseEventArgs e)
        {
            OnMouseMove(e);
        }
    }
}
