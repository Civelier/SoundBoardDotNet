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
    public partial class StartPlayHead : UserControl, IPlayHead
    {
        private AudioSoundInfo _soundInfo;

        public double Seconds 
        {
            get => (double)PointingX / useableWidth;
            set
            {
                if (Seconds != value)
                {
                    PointingX = (int)Math.Round(Progression * useableWidth);
                    OnPropertyChanged("Seconds");
                }
            }
        }

        private double _progression;
        public double Progression 
        {
            get => TotalSeconds == 0 ? 0 : Seconds / TotalSeconds; 
            set => Seconds = value * TotalSeconds; 
        }

        public double TotalSeconds
        {
            get => Viewer.SoundInfo?.TotalSeconds ?? 0;
        }

        public int ParentWidth => Parent.Width;

        public int ScrollX 
        { 
            get => (ParentPanel?.HorizontalScroll?.Value ?? 0) + XLocation;
            set => XLocation = value - (ParentPanel?.HorizontalScroll?.Value ?? 0);
        }
        public int PointingX { get => ScrollX + Width; set => ScrollX = value - Width; }

        public int ParentOffset { private get; set; }

        private int useableWidth => ParentWidth - 2 * ParentOffset;

        public Panel CursorPanel { get; private set; }

        private int totalHeight => ParentPanel.Height;

        public Panel ParentPanel { private get; set; }

        public int XLocation { get => Left; set => Left = value; }
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
                }
            }
        }

        private void _soundInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "StartPos":
                    
                    break;
                default:
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public StartPlayHead()
        {
            InitializeComponent();
            ParentPanel.SuspendLayout();
            CursorPanel = new Panel();
            CursorPanel.Parent = ParentPanel;
            CursorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            CursorPanel.BackColor = Color.FromArgb(25, 215, 0);
            CursorPanel.Size = new Size(2, totalHeight);
            CursorPanel.Location = new Point(PointingX - 1, 0);
            ParentPanel.ResumeLayout();
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
