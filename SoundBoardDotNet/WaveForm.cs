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
    public partial class WaveForm : UserControl
    {
        public List<byte> Values = new List<byte>();

        private int XAxis { get { return Height / 2; } }
        private int RectWidth { get { return Width / Values.Count; } }
        
        public WaveForm()
        {
            InitializeComponent();
        }

        private void _drawRectangle(Point start, Point end)
        {
            var brush = new SolidBrush(Color.Cyan);
            var pen = new Pen(brush);
            pen.Width = RectWidth;
            pen.TranslateTransform(start.X, start.Y);
            pen.TranslateTransform(end.X, end.Y);
        }

        public void Draw()
        {
            for (int i = 0; i < 10; i++)
            {
                Values.Add(0);
            }

            for (int i = 0; i < Values.Count; i++)
            {
                _drawRectangle(new Point(i, 0), new Point(i, i));
            }
        }
    }
}
