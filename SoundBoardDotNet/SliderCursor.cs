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
    public partial class SliderCursor : UserControl
    {
        public int MinBoundary, MaxBoundary;
        public double Value
        {
            get { return ((double)(Location.X - MinBoundary) * 100 / _range); }
            set { Location = new Point(((int)value * _range / 100) + MinBoundary, Location.Y); }
        }

        private int _range { get { return MaxBoundary - MinBoundary; } }
        private Point LocationFromParent
        {
            get { return Parent.Controls[Parent.Controls.IndexOf(this)].Location; }
            set { Parent.Controls[Parent.Controls.IndexOf(this)].Location = value; }
        }
        private Point _mouseStartPos;
        private bool _isMouseClicked = false;

        public SliderCursor()
        {
            InitializeComponent();
        }

        private int _applyBoundaries(int x)
        {
            if (Location.X + x > MaxBoundary)
                return MaxBoundary;
            if (Location.X + x < MinBoundary)
                return MinBoundary;
            return Location.X + x;

        }

        private void _move(int x)
        {
            Location = new Point(_applyBoundaries(x), Location.Y);
        }

        private void SliderCursor_MouseDown(object sender, MouseEventArgs e)
        {
            _isMouseClicked = true;
            _mouseStartPos = e.Location;
        }

        private void SliderCursor_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseClicked = false;
            Parent.Refresh();
        }

        private void SliderCursor_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseClicked)
            {
                _move(e.Location.X);
            }
        }
    }
}
