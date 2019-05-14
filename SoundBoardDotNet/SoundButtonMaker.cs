using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrrKlang;
using System.Windows.Forms;
using System.Drawing;

namespace SoundBoardDotNet
{
    class SoundButtonMaker
    {
        public ISoundSource MySoundSource;
        public SelectSound SoundForm;
        public bool HasSound = false;
        public Button Btn;
        public SoundButtonData Data;

        private string _name;

        public static Form Parent;
        public static ISoundEngine Engine;
        public static int Scale = 20;
        public static Point Origin;

        public static char KeyToChar(Keys k)
        {
            switch (k)
            {
                case Keys.D0:
                    return (char)48;
                case Keys.D1:
                    return (char)49;
                case Keys.D2:
                    return '2';
                case Keys.D3:
                    return '3';
                case Keys.D4:
                    return '4';
                case Keys.D5:
                    return '5';
                case Keys.D6:
                    return '6';
                case Keys.D7:
                    return '7';
                case Keys.D8:
                    return '8';
                case Keys.D9:
                    return '9';
                case Keys.A:
                    return 'A';
                case Keys.B:
                    return 'B';
                case Keys.C:
                    return 'C';
                case Keys.D:
                    return 'D';
                case Keys.E:
                    return 'E';
                case Keys.F:
                    return 'F';
                case Keys.G:
                    return 'G';
                case Keys.H:
                    return 'H';
                case Keys.I:
                    return 'I';
                case Keys.J:
                    return 'J';
                case Keys.K:
                    return 'K';
                case Keys.L:
                    return 'L';
                case Keys.M:
                    return 'M';
                case Keys.N:
                    return 'N';
                case Keys.O:
                    return 'O';
                case Keys.P:
                    return 'P';
                case Keys.Q:
                    return 'Q';
                case Keys.R:
                    return 'R';
                case Keys.S:
                    return 'S';
                case Keys.T:
                    return 'T';
                case Keys.U:
                    return 'U';
                case Keys.V:
                    return 'V';
                case Keys.W:
                    return 'W';
                case Keys.X:
                    return 'X';
                case Keys.Y:
                    return 'Y';
                case Keys.Z:
                    return 'Z';
                default:
                    return ' ';
            }
        }

        public SoundButtonMaker(int x, int y, int height, int width, string name)
        {
            _name = name;
            SelectSound.Engine = Engine;
            Data = new SoundButtonData();
            SoundForm = new SelectSound(_updateBtnText, Data);
            Btn = new Button();
            Btn.Click += Btn_Click;
            Btn.Size = new Size(width * Scale, height * Scale);
            Btn.Location = new Point(Origin.X + x * Scale, Origin.Y + y * Scale);
            Btn.Text = name;
            Btn.TextAlign = ContentAlignment.TopCenter;
            Parent.Controls.Add(Btn);
            Parent.KeyPress += Form_KeyPress;
        }

        private void Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == char.Parse(_name))
            {
                Data.Sound = Engine.Play2D(Data.FilePath);
                Data.Sound.Volume = Data.Volume;
            }
        }

        private void _updateBtnText()
        {
            Btn.Text = $"{_name}\n{SoundForm.GetNameTextBox.Text}";
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (SoundForm == null) SoundForm = new SelectSound(_updateBtnText, Data);
            try
            {
                SoundForm.LoadFromData();
                SoundForm.Show();
            }
            catch (ObjectDisposedException)
            {
                SoundForm = new SelectSound(_updateBtnText, Data);
                SoundForm.Show();
            }
        }
    }
}
