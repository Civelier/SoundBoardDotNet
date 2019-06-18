using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SoundBoardDotNet
{
    public partial class Form1 : Form
    {
        public static List<SoundButtonMaker> Buttons = new List<SoundButtonMaker>();
        public static List<AudioRecorder> Recorders = new List<AudioRecorder>();
        public static Form1 MyForm;

        private static string[] englishKeyboard = { "`1234567890-=", "qwertyuiop[]\\", "asdfghjkl;'", "zxcvbnm,./" };
        private static string[] frenchKeyboard = { "#1234567890-=", "qwertyuiop^¸<", "asdfghjkl;`", "zxcvbnm,.é" };
        public static string[] MyKeyboard = englishKeyboard;

        //ISound Sound;
        //ISoundEngine Engine;
        public string SavePath = "";

        private bool _hasChanged = false;
        
        public bool HasChanged
        {
            get { return _hasChanged; }
            set
            {
                if (value)
                {
                    Text = "*SoundBoradDotNet";
                }
                else
                {
                    Text = "SoundBoradDotNet";
                }
            }
        }

        void PlaySound()
        {
            
        }

        public Form1()
        {
            KeyPreview = true;
            SoundBoardData.LoadProperties();
            InitializeComponent();
            var r = new AudioRecorder(0);
            Recorders.Add(r);
            r.StartRecording();
            //SoundButtonMaker.Engine = Engine;
        }

        private void KeyboardLine(int xIncrement, int x, int y, string keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                var btn = new LetterKey(x, y, keys[i].ToString());
                btn.Data.Index = Buttons.Count;
                Buttons.Add(btn);
                x += xIncrement;
            }
        }

        public static void SetFocus()
        {
            MyForm.DeselectButton.Select();
        }

        private void KeyboardBuilder(string[] keyboard, Point origin, int xincrement = 4, int yincrement = 4)
        {
            SoundButtonMaker.Origin = origin;


            int x = 0, y = 0;
            foreach (var line in keyboard)
            {
                KeyboardLine(xincrement, x, y, line);
                if (x != 0) x += 1;
                else x += 8;
                y += yincrement;
            }

            Buttons.Add(new SpaceBarButton(15, 28, 6, 30));
            SaveRecordingButton.Select();

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
            SoundButtonMaker.Parent = this;
            KeyboardBuilder(MyKeyboard, new Point(10, 30), 7, 7);
            MyForm = this;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                SaveRecordingButton.Select();
            }
            System.Diagnostics.Debug.WriteLine($"Key {e.KeyChar}");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler((object tsender, System.Timers.ElapsedEventArgs te) => System.Diagnostics.Debug.WriteLine(te.SignalTime.ToString() + " Time ended"));
            timer.AutoReset = false;
            timer.Start();
        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PreferencesForm().Show();
        }

        private void DevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DevicesForm((int[] devices) =>
            {
                foreach (var recorder in Recorders)
                {
                    recorder.Reset();
                }
                Recorders.Clear();
                foreach (int i in devices)
                {
                    var r = new AudioRecorder(i);
                    Recorders.Add(r);
                    r.StartRecording();
                }
            }).Show();
        }

        private void SaveRecordingButton_Click(object sender, EventArgs e)
        {
            new SaveSound().Show();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                new SaveSound().Show();
            }
        }

        private bool ChooseSavePath()
        {
            var save = new SaveFileDialog() { AddExtension = true, DefaultExt = "sbdn", OverwritePrompt = true, InitialDirectory = SoundBoardData.GetDefaultSaveDirectory().FullName, Filter = "SoundBoard files (*.sbdn)|*.sbdn;*.SBDN"};
            if (save.ShowDialog() == DialogResult.Cancel) return false;
            SavePath = save.FileName;
            return true;
        }

        private bool Save()
        {
            if (SavePath == String.Empty)
            {
                if (!ChooseSavePath()) return false;
            }

            SoundBoardData.Save(SavePath);
            HasChanged = false;
            return true;
        }

        private void FileSaveAs_Click(object sender, EventArgs e)
        {
            ChooseSavePath();
            Save();
        }

        private void FileSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void FileOpen_Click(object sender, EventArgs e)
        {
            var file = new OpenFileDialog() { AddExtension = true, DefaultExt = "sbdn", CheckFileExists = true, InitialDirectory = SoundBoardData.GetDefaultSaveDirectory().FullName };
            if (file.ShowDialog() == DialogResult.Cancel) return;
            SavePath = file.FileName;
            SoundBoardData.Load(SavePath);
            foreach (var item in Buttons)
            {
                item.Data.Reset();
                item.Data = SoundBoardData.AllData.Data[item.Data.Index];
                item.Update();
            }
            HasChanged = false;
        }

        private void FileNew_Click(object sender, EventArgs e)
        {
            SavePath = String.Empty;
            foreach (var item in Buttons)
            {
                item.Data.Reset();
                item.Update();
            }
            HasChanged = true;
        }
    }
}
