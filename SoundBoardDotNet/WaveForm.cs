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
        public uint SoundLength;
        public Rectangle MyMargin = new Rectangle();
        public Label GradLabel;

        public double Slider1Value
        {
            get { return Slider1.Value; }
            set { Slider1.Value = value; }
        }

        public double Slider2Value
        {
            get { return Slider2.Value; }
            set { Slider2.Value = value; }
        }

        static int _XMargin = 10, _YMargin = 10;

        private int _XAxis { get { return MyMargin.Height / 2; } }
        private int _RectWidth { get { return (int)Math.Ceiling((double)MyMargin.Width / Values.Count); } }
        private Point _Origin { get { return new Point(_XMargin, _XAxis); } }
        private Point _EndAxis { get { return new Point(Width - _XMargin, _XAxis); } }
        private Graphics _graph;
        private static int[] _grads = { 1, 2, 5 };
        
        //private Bitmap _bitmap;
        
        public WaveForm()
        {
            //Temporary Size******************
            MyMargin = new Rectangle(_XMargin, _YMargin, 478 - 2 * _XMargin, 160 - 2 * _YMargin);
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

        private void _drawAxes()
        {
            var p = new Pen(Color.Black);
            try
            {
                _graph.DrawLine(p, _Origin, _EndAxis);
            }
            catch (ArgumentException)
            {

            }
        }

        private int _getClosest(int iTarget, int i1, int i2)
        {
            if (Math.Abs(iTarget - i1) <= Math.Abs(iTarget - i2)) return i1;
            return i2;
        }

        private int _getGraduation(int size, int nGrads)
        {
            int target = size / nGrads;
            if (target == 0) return 0;
            int closest = 0;
            bool bFirst = true;
            for (int i = 0; i < 6; i++)
            {
                foreach (var v in _grads)
                {
                    if (bFirst)
                    {
                        closest = Convert.ToInt32(v * Math.Pow(10, i));
                        bFirst = false;
                    }
                    else
                    {
                        closest = _getClosest(target, closest, Convert.ToInt32(v * Math.Pow(10, i)));
                    }
                }
            }
            return closest;
        }

        private void _xGraduate()
        {
            var p = new Pen(Color.Black);
            List<Point> points = new List<Point>();
            int grad = _getGraduation(Convert.ToInt32(SoundLength), 30);
            if (SoundLength == 0)
            {
                try
                {
                    _graph.DrawLine(p, new Point(_Origin.X, _XAxis + 5), new Point(_Origin.X, _XAxis - 5));
                }
                catch (ArgumentException)
                {
                    _graph = CreateGraphics();
                    _graph.DrawLine(p, new Point(_Origin.X, _XAxis + 5), new Point(_Origin.X, _XAxis - 5));
                }
            }
            else
            {
                for (int i = 0; i < 40; i++)
                {
                    if (i * grad * MyMargin.Width / Convert.ToInt32(SoundLength) > MyMargin.Width) break;
                    try
                    {
                        _graph.DrawLine(p, new Point(_Origin.X + i * grad * MyMargin.Width / Convert.ToInt32(SoundLength), _XAxis + 5),
                            new Point(_Origin.X + i * grad * MyMargin.Width / Convert.ToInt32(SoundLength), _XAxis - 5));
                    }
                    catch (ArgumentException)
                    {
                        _graph = CreateGraphics();
                        _graph.DrawLine(p, new Point(_Origin.X + i * grad * MyMargin.Width / Convert.ToInt32(SoundLength), _XAxis + 5),
                            new Point(_Origin.X + i * grad * MyMargin.Width / Convert.ToInt32(SoundLength), _XAxis - 5));
                    }
                    
                }
            }
            
            if (GradLabel != null)
            {
                GradLabel.Text = "Grad: " + _timeToString(grad);
            }
        }

        private string _timeToString(int time)
        {
            if (time > 1000) return ((double)time).ToString() + "s";
            return time.ToString() + "ms";
        }

        public void Draw()
        {
            DrawGraph(_graph);
        }

        public void DrawGraph(Graphics graph)
        {
            double d = 0, increment = (double)MyMargin.Width / Values.Count;
            for (int i = 0; i < Values.Count; i++)
            {
                d += increment;
                _drawRectangle(new Rectangle(Convert.ToInt32(d), _XAxis - _scale(Values[i]) / 2, _RectWidth, _scale(Values[i])), graph);
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
                    _drawRectangle(new Rectangle(Convert.ToInt32(d), _XAxis - _scale(Values[i]) / 2, _RectWidth, _scale(Values[i])), graph);
                    diff2 = diff1;
                }
            }
        }

        public void DrawGraphOptimizedAvg(Graphics graph)
        {
            _graph = graph;
            if (Values.Count == 0) return;
            EraseGraph();
            double d = 0, increment = (double)(MyMargin.Width) / Values.Count;
            int diff1 = 0, diff2 = 0;
            int sum = Values[0], sumCount = 1;
            Func<byte> avg = () => (byte)(sum / sumCount);
            for (int i = 0; i < Values.Count; i++)
            {
                d += increment;
                diff1 = (int)d;
                if (diff1 != diff2)
                {
                    _drawRectangle(new Rectangle(Convert.ToInt32(d) + _Origin.X, _XAxis - _scale(avg()) / 2, _RectWidth, _scale(avg())), _graph);
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

            _drawAxes();
            _xGraduate();
        }

        public void ResetCursors()
        {
            Slider1.Value = 0;
            Slider2.Value = 100;
        }

        public void ResetValues()
        {
            ResetCursors();
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
            _graph = e.Graphics;
            //DrawGraph(e.Graphics);
            DrawGraphOptimizedAvg(e.Graphics);
            //e.Graphics.DrawImage(_bitmap, new Rectangle(0, 0, Width, Height));
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            OnPaint(e);
        }

        private void Slider1_Load(object sender, EventArgs e)
        {
            Slider1.Location = new Point(MyMargin.Left, Slider1.Location.Y);
            Slider1.MinBoundary = MyMargin.Left;
            Slider1.MaxBoundary = Slider2.Location.X;
        }

        private void Slider1_Paint(object sender, PaintEventArgs e)
        {
            Slider2.MinBoundary = Slider1.Location.X;
        }

        private void Slider2_Load(object sender, EventArgs e)
        {
            Slider2.Location = new Point(MyMargin.Right, Slider2.Location.Y);
            Slider2.MinBoundary = Slider1.Location.X;
            Slider2.MaxBoundary = MyMargin.Right;
        }

        private void Slider2_Paint(object sender, PaintEventArgs e)
        {
            Slider1.MaxBoundary = Slider2.Location.X;
        }

        private void WaveForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                DrawGraphOptimizedAvg(_graph);
                Refresh();
            }
            else
            {
                _graph.Dispose();
            }
        }
    }
}
