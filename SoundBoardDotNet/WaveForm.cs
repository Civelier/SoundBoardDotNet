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

        private void _drawRectangle(Rectangle rect, Graphics graph)
        {

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

            graph = this.CreateGraphics();
            graph.FillRectangle(myBrush, rect);
            myBrush.Dispose();
            graph.Dispose();
        }

        public void Draw()
        {
            for (int i = 0; i < Values.Count; i++)
            {
                _drawRectangle(new Rectangle(Values[i] * RectWidth, XAxis - Values[i] / 2, RectWidth, Values[i]), CreateGraphics());
            }
        }

        public void DrawGraph(Graphics graph)
        {
            for (int i = 0; i < Values.Count; i++)
            {
                _drawRectangle(new Rectangle(Values[i] * RectWidth, XAxis - Values[i] / 2, RectWidth, Values[i]), graph);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGraph(e.Graphics);
        }
    }
}
