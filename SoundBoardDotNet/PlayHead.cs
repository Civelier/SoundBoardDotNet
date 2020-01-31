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
                if (_headType == PlayHeadType.End)
                {
                    HeadGreen.Visible = false;
                    HeadRed.Visible = true;
                    BarGreen.Visible = false;
                    BarRed.Visible = true;
                }
                else
                {
                    HeadGreen.Visible = true;
                    HeadRed.Visible = false;
                    BarGreen.Visible = true;
                    BarRed.Visible = false;
                }
                ResumeLayout();
            }
        }

        public PlayHead Other
        {
            get;
            set;
        }

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
                if (HeadType == PlayHeadType.End) return Left;
                else return Left;
            }
            set
            {
                if (Other == null) return;
                if (HeadType == PlayHeadType.End)
                {
                    //Left = Math.Max(Other.Right, Math.Min(ParentWidth + ParentOffset, value));
                    Left = value;
                }
                else
                {
                    //Left = Math.Max(ParentOffset - Width, Math.Min(value, Other.Left - Width));
                    Left = value;
                }
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
                if (HeadType == PlayHeadType.End) return ScrollX;
                return ScrollX + Width;
            }
            set
            {
                if (HeadType == PlayHeadType.End)
                {
                    ScrollX = value;
                }
                else
                {
                    ScrollX = value - Width;
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
