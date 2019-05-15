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
        Dictionary<Keys, SoundButtonMaker> Buttons = new Dictionary<Keys, SoundButtonMaker>();
        ISound Sound;
        ISoundEngine Engine;

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

        private void KeyboardBuilder()
        {
            SoundButtonMaker.Origin = new Point(30, 30);
            Buttons.Add(Keys.D1, new SoundButtonMaker(0, 0, 3, 3, "1"));
            Buttons.Add(Keys.D2, new SoundButtonMaker(4, 0, 3, 3, "2"));
            Buttons.Add(Keys.D3, new SoundButtonMaker(8, 0, 3, 3, "3"));
            Buttons.Add(Keys.D4, new SoundButtonMaker(12, 0, 3, 3, "4"));
            Buttons.Add(Keys.D5, new SoundButtonMaker(16, 0, 3, 3, "5"));
            Buttons.Add(Keys.D6, new SoundButtonMaker(20, 0, 3, 3, "6"));
            Buttons.Add(Keys.D7, new SoundButtonMaker(24, 0, 3, 3, "7"));
            Buttons.Add(Keys.D8, new SoundButtonMaker(28, 0, 3, 3, "8"));
            Buttons.Add(Keys.D9, new SoundButtonMaker(32, 0, 3, 3, "9"));
            Buttons.Add(Keys.D0, new SoundButtonMaker(36, 0, 3, 3, "0"));
            Buttons.Add(Keys.OemMinus, new SoundButtonMaker(40, 0, 3, 3, "-"));
            Buttons.Add(Keys.Add, new SoundButtonMaker(44, 0, 3, 3, "+"));

            
            Buttons.Add(Keys.A, new SoundButtonMaker(0, 4, 3, 3, "A"));
            /*
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));

            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));

            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            Buttons.Add(Keys., new SoundButtonMaker(0, 0, 3, 3, Keys.));
            */
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
            SoundButtonMaker.Parent = this;
            KeyboardBuilder();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Key {e.KeyChar}");
        }
    }
}
