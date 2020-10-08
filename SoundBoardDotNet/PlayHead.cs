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
    public enum PlayHeadType
    {
        Start,
        Current,
        End
    }
    public partial class PlayHead : UserControl
    {
        PlayHeadType _headType = PlayHeadType.Start;
        [DisplayName("Playhead type")]
        [Description("Type of playhead.")]
        [DataObjectField(false)]
        public PlayHeadType HeadType
        {
            get => _headType;
            set
            {
                _headType = value;
                SuspendLayout();
                switch (_headType)
                {
                    case PlayHeadType.Start:
                        HeadGreen.Visible = true;
                        HeadRed.Visible = false;
                        HeadCurrent.Visible = false;
                        BarGreen.Visible = true;
                        BarRed.Visible = false;
                        BarCurrent.Visible = false;
                        break;
                    case PlayHeadType.Current:
                        HeadGreen.Visible = false;
                        HeadRed.Visible = false;
                        HeadCurrent.Visible = true;
                        BarGreen.Visible = false;
                        BarRed.Visible = false;
                        BarCurrent.Visible = true;
                        break;
                    case PlayHeadType.End:
                        HeadGreen.Visible = false;
                        HeadRed.Visible = true;
                        HeadCurrent.Visible = false;
                        BarGreen.Visible = false;
                        BarRed.Visible = true;
                        BarCurrent.Visible = false;
                        break;
                    default:
                        break;
                }
                ResumeLayout();
            }
        }

        //public PlayHead Other
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// From 0 to 1 how far into the the audio it is
        /// </summary>
        public double Progression
        {
            get => (double)(PointingX - ParentOffset) / (ParentWidth - 2 * ParentOffset);
            set
            {
                PointingX = (int)(value * (ParentWidth - 2 * ParentOffset)) + ParentOffset;
            }
        }

        public double Seconds
        {
            get => Progression * TotalSeconds;
            set => Progression = value / TotalSeconds;
        }

        public double TotalSeconds
        {
            get;
            set;
        }

        public int XLocation
        {
            get
            {
                switch (_headType)
                {
                    case PlayHeadType.Start:
                        return Left;
                    case PlayHeadType.Current:
                        return Left;
                    case PlayHeadType.End:
                        return Left;
                    default:
                        return 0;
                }
            }
            set
            {
                Left = value;
            }
        }

        public Panel ParentPanel { get; set; }

        public int ScrollX
        {
            get => (ParentPanel?.HorizontalScroll?.Value ?? 0) + XLocation;
            set => XLocation = value - (ParentPanel?.HorizontalScroll?.Value ?? 0);
        }

        public int PointingX
        {
            get
            {
                switch (HeadType)
                {
                    case PlayHeadType.Start:
                        return ScrollX + Width;
                    case PlayHeadType.Current:
                        return ScrollX + Width / 2;
                    case PlayHeadType.End:
                        return ScrollX;
                    default:
                        return 0;
                }
            }
            set
            {
                switch (HeadType)
                {
                    case PlayHeadType.Start:
                        ScrollX = value - Width;
                        break;
                    case PlayHeadType.Current:
                        ScrollX = value - Width / 2;
                        break;
                    case PlayHeadType.End:
                        ScrollX = value;
                        break;
                    default:
                        break;
                }
            }
        }

        public int ParentOffset
        {
            get;
            set;
        }

        public int ParentWidth
        {
            get;
            set;
        }

        public PlayHead()
        {
            InitializeComponent();
            HeadGreen.MouseDown += Head_MouseDown;
            HeadGreen.MouseMove += Head_MouseMove;
            HeadRed.MouseDown += Head_MouseDown;
            HeadRed.MouseMove += Head_MouseMove;
            HeadCurrent.MouseMove += Head_MouseMove;
            HeadCurrent.MouseDown += Head_MouseDown;
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
