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
        private int RectWidth { get { return (int)Math.Ceiling((double)Width / Values.Count); } }
        private Graphics _graph;
        //private Bitmap _bitmap;
        
        public WaveForm()
        {
            for (int i = 0; i < 100000; i++)
            {
                Values.Add((byte)(i * 255 / 100000));
            }
            _graph = CreateGraphics();
            //_bitmap = new Bitmap(Width, Height);
            InitializeComponent();
        }

        private void _drawRectangle(Rectangle rect, Graphics graph)
        {

            SolidBrush myBrush = new SolidBrush(Color.Cyan);

            graph = this.CreateGraphics();
            graph.FillRectangle(myBrush, rect);
            myBrush.Dispose();
            graph.Dispose();
        }

        private int _scale(byte b)
        {
            int output = b * Height / 2 / 255;
            return output;
        }

        public void Draw()
        {
            DrawGraph(_graph);
        }

        public void DrawGraph(Graphics graph)
        {
            double d = 0, increment = (double)Width / Values.Count;
            for (int i = 0; i < Values.Count; i++)
            {
                d += increment;
                _drawRectangle(new Rectangle(Convert.ToInt32(d), XAxis - _scale(Values[i]) / 2, RectWidth, _scale(Values[i])), graph);
            }
        }

        public void DrawGraphOptimized(Graphics graph)
        {
            double d = 0, increment = (double)Width / Values.Count;
            int diff1 = 0, diff2 = 0;
            for (int i = 0; i < Values.Count; i++)
            {
                d += increment;
                diff1 = (int)d;
                if (diff1 != diff2)
                {
                    _drawRectangle(new Rectangle(Convert.ToInt32(d), XAxis - _scale(Values[i]) / 2, RectWidth, _scale(Values[i])), graph);
                    diff2 = diff1;
                }
            }
        }

        public void DrawGraphOptimizedAvg(Graphics graph)
        {
            _graph = graph;
            if (Values.Count == 0) return;
            EraseGraph();
            double d = 0, increment = (double)Width / Values.Count;
            int diff1 = 0, diff2 = 0;
            int sum = Values[0], sumCount = 1;
            Func<byte> avg = () => (byte)(sum / sumCount);
            for (int i = 0; i < Values.Count; i++)
            {
                d += increment;
                diff1 = (int)d;
                if (diff1 != diff2)
                {
                    _drawRectangle(new Rectangle(Convert.ToInt32(d), XAxis - _scale(avg()) / 2, RectWidth, _scale(avg())), _graph);
                    diff2 = diff1;
                    sum = 0;
                    sumCount = 0;
                }
                else
                {
                    sum += Values[i];
                    sumCount++;
                }
            }
            //DrawToBitmap(_bitmap, new Rectangle(0, 0, Width, Height));
        }

        public void ResetValues()
        {
            Values = new List<byte>();
        }

        public void EraseGraph()
        {
            try
            {
                _graph.Clear(Color.DarkGray);
            }
            catch (ArgumentException)
            {

            }
            //_bitmap = new Bitmap(Width, Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            //_graph = e.Graphics;
            
            DrawGraphOptimizedAvg(e.Graphics);
            e.Graphics.Dispose();
            //e.Graphics.DrawImage(_bitmap, new Rectangle(0, 0, Width, Height));
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            OnPaint(e);
        }

        private void WaveForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                DrawGraphOptimizedAvg(_graph);
                Refresh();
            }
        }
    }
}
