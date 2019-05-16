using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IrrKlang;

namespace SoundBoardDotNet
{
    public partial class Form1 : Form
    {
        List<SoundButtonMaker> Buttons = new List<SoundButtonMaker>();
        ISound Sound;
        ISoundEngine Engine;

        string[] englishKeyboard = { "`1234567890-=", "qwertyuiop[]\\", "asdfghjkl;'", "zxcvbnm,./" };
        string[] frenchKeyboard = { "#1234567890-=", "qwertyuiop^¸<", "asdfghjkl;`", "zxcvbnm,.é" };


        void PlaySound()
        {
            
            if (Sound != null) Sound.Stop();
        }

        public Form1()
        {
            KeyPreview = true;
            Engine = new ISoundEngine();
            InitializeComponent();
            SoundButtonMaker.Engine = Engine;
        }

        private void KeyboardLine(int xIncrement, int x, int y, string keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                Buttons.Add(new LetterKey(x, y, keys[i].ToString()));
                x += xIncrement;
            }
        }

        private void KeyboardBuilder(string[] keyboard, Point origin, int xincrement = 4, int yincrement = 4)
        {
            SoundButtonMaker.Origin = origin;


            int x = 0, y = 0;
            foreach (var line in keyboard)
            {
                KeyboardLine(xincrement, x, y, line);
                x += 1;
                y += yincrement;
            }

            Buttons.Add(new SpaceBarButton(15, 28, 6, 30));

            //Buttons.Add(new LetterKey(0, 0, "`"));
            //Buttons.Add(new LetterKey(4, 0, "1"));
            //Buttons.Add(new LetterKey(8, 0, "2"));
            //Buttons.Add(new LetterKey(12, 0, "3"));
            //Buttons.Add(new LetterKey(16, 0, "4"));
            //Buttons.Add(new LetterKey(20, 0, "5"));
            //Buttons.Add(new LetterKey(24, 0, "6"));
            //Buttons.Add(new LetterKey(28, 0, "7"));
            //Buttons.Add(new LetterKey(32, 0, "8"));
            //Buttons.Add(new LetterKey(36, 0, "9"));
            //Buttons.Add(new LetterKey(40, 0, "0"));
            //Buttons.Add(new LetterKey(44, 0, "-"));
            //Buttons.Add(new LetterKey(48, 0, "="));

            //Buttons.Add(new LetterKey(1, 4, "q"));
            //Buttons.Add(new LetterKey(5, 4, "w"));
            //Buttons.Add(new LetterKey(9, 4, "e"));
            //Buttons.Add(new LetterKey(13, 4, "r"));
            //Buttons.Add(new LetterKey(17, 4, "t"));
            //Buttons.Add(new LetterKey(21, 4, "y"));
            //Buttons.Add(new LetterKey(25, 4, "u"));
            //Buttons.Add(new LetterKey(29, 4, "i"));
            //Buttons.Add(new LetterKey(33, 4, "o"));
            //Buttons.Add(new LetterKey(37, 4, "p"));
            //Buttons.Add(new LetterKey(41, 4, "["));
            //Buttons.Add(new LetterKey(45, 4, "]"));

            //Buttons.Add(new LetterKey(2, 8, "a"));
            //Buttons.Add(new LetterKey(6, 8, "s"));
            //Buttons.Add(new LetterKey(10, 8, "d"));
            //Buttons.Add(new LetterKey(14, 8, "f"));
            //Buttons.Add(new LetterKey(18, 8, "g"));
            //Buttons.Add(new LetterKey(22, 8, "h"));
            //Buttons.Add(new LetterKey(26, 8, "j"));
            //Buttons.Add(new LetterKey(30, 8, "k"));
            //Buttons.Add(new LetterKey(34, 8, "l"));
            //Buttons.Add(new LetterKey(38, 8, ";"));
            //Buttons.Add(new LetterKey(42, 8, "'"));

            //Buttons.Add(new LetterKey(4, 12, "z"));
            //Buttons.Add(new LetterKey(8, 12, "x"));
            //Buttons.Add(new LetterKey(12, 12, "c"));
            //Buttons.Add(new LetterKey(16, 12, "v"));
            //Buttons.Add(new LetterKey(20, 12, "b"));
            //Buttons.Add(new LetterKey(24, 12, "n"));
            //Buttons.Add(new LetterKey(28, 12, "m"));
            //Buttons.Add(new LetterKey(32, 12, ","));
            //Buttons.Add(new LetterKey(36, 12, "."));
            //Buttons.Add(new LetterKey(40, 12, "/"));

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //MyEngine = new ISoundEngine((SoundOutputDriver)(Enum.GetValues(typeof(SoundOutputDriver)).GetValue(DriverCombo.SelectedIndex)), 
            //    SoundEngineOptionFlag.DefaultOptions, 
            //    DevicesCombo.SelectedText);
            //var file = new OpenFileDialog();
            //file.ShowDialog();
            //SoundButton = new SoundButtonMaker(file.FileName);
            //SoundButton.MySoundSource.ForceReloadAtNextUse();
            //var sound = MyEngine.Play2D(SoundButton.MySoundSource, false, false, false);
            PlaySound();
            //MyEngine.Update();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AudioSound.Engine = Engine;
            AudioSound.Run();
            SoundButtonMaker.Parent = this;
            KeyboardBuilder(englishKeyboard, new Point(10, 30), 7, 7);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') DeselectButton.Select();
            System.Diagnostics.Debug.WriteLine($"Key {e.KeyChar}");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            AudioSound.StopMain();
        }
    }
}
