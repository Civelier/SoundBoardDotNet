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
    public partial class CurrentPlayHead : UserControl, IPlayHead
    {
        private AudioSoundInfo _soundInfo;

        public double Seconds
        {
            get
            {
                if (WaveViewerWidth == 0) return 0;
                return (double)PointingX / WaveViewerWidth * TotalSeconds;
            }
            set
            {
                if (Seconds != value)
                {
                    PointingX = (int)Math.Round(value / TotalSeconds * WaveViewerWidth);
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
        public int PointingX { get => ScrollX + 2; set => ScrollX = value - 2; }

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
                if (CursorPanel != null) CursorPanel.Left = PointingX;
            }
        }
        public SoundWaveViewer Viewer { private get; set; }

        private int WaveViewerWidth => Viewer?.WaveGraphWidth ?? 0;

        public AudioSoundInfo SoundInfo
        {
            private get => _soundInfo;
            set
            {
                if (_soundInfo != value)
                {
                    _soundInfo = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CurrentPlayHead()
        {
            InitializeComponent();
        }

        internal void CreateCursorPanel()
        {
            ParentPanel = (Panel)Parent;
            CursorPanel = new Panel();
            CursorPanel.Parent = Parent;
            CursorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            CursorPanel.BackColor = Color.FromArgb(3, 145, 255);
            CursorPanel.Size = new Size(2, cursorHeight);
            CursorPanel.Location = new Point(PointingX, 0);
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

        private void Head_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }
    }
}
