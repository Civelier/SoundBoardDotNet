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
        public static Form1 MyForm;

        private static string[] englishKeyboard = { "`1234567890-=", "qwertyuiop[]\\", "asdfghjkl;'", "zxcvbnm,./" };
        private static string[] frenchKeyboard = { "#1234567890-=", "qwertyuiop^¸<", "asdfghjkl;`", "zxcvbnm,.é" };
        public static string[] MyKeyboard = englishKeyboard;

        public string SavePath = "";

        private bool _hasChanged = false;
        
        /// <summary>
        /// Represents wether the soundboard has unsaved changes.
        /// </summary>
        public bool HasChanged
        {
            get { return _hasChanged; }
            set
            {
                _hasChanged = value;
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

        public Form1()
        {
            KeyPreview = true;

            SoundBoardData.LoadProperties();
            InitializeComponent();
            // Refreshes the dvices list
            Devices.RefreshAll();
            // Starts the recorders
            InputDevice.StartRecorders();
        }

        /// <summary>
        /// Acts as the listener for key inputs and also prevents selcetion of unwanted key inputs like Enter and Space.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeselectButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                AudioSound.StopAll();
                e.Handled = e.SuppressKeyPress = true;
                //Stop sounds
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                new SaveSound().Show();
            }
        }

        /// <summary>
        /// Generates a keyboard line.
        /// </summary>
        /// <param name="xIncrement">Increment for the x location between each key</param>
        /// <param name="x">Start x location</param>
        /// <param name="y">Start y location</param>
        /// <param name="keys">Keys to generate</param>
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
            MyForm.SaveRecordingButton.Select();
        }

        /// <summary>
        /// Generates the whole keyboard
        /// </summary>
        /// <param name="keyboard">Keyboard lines</param>
        /// <param name="origin">Start point</param>
        /// <param name="xincrement">Increment of the x location between each key</param>
        /// <param name="yincrement">Increment of the y location between each line</param>
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

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PreferencesForm().Show();
        }

        private void DevicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DevicesForm().Show();
        }

        private void SaveRecordingButton_Click(object sender, EventArgs e)
        {
            new SaveSound().Show();
        }

        //private void Form1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        new SaveSound().Show();
        //    }
        //}

        public static SoundButtonMaker GetButton(char key)
        {
            foreach (var button in Form1.Buttons)
            {
                if (key == (char)0) return null;
                if (button.Name.ToUpper() == key.ToString())
                {
                    return button;
                }
            }
            return null;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.Shift)
                {
                    if (e.KeyCode == Keys.S)
                    {
                        FileSaveAs_Click(null, null);
                        e.Handled = e.SuppressKeyPress = true;
                    }
                    return;
                }
                else
                {
                    if (e.KeyCode == Keys.S)
                    {
                        FileSave_Click(null, null);
                        e.Handled = e.SuppressKeyPress = true;
                    }
                    if (e.KeyCode == Keys.O)
                    {
                        FileOpen_Click(null, null);
                        e.Handled = e.SuppressKeyPress = true;
                    }
                    if (e.KeyCode == Keys.N)
                    {
                        FileNew_Click(null, null);
                        e.Handled = e.SuppressKeyPress = true;
                    }
                    if (e.KeyCode == Keys.K)
                    {
                        PreferencesToolStripMenuItem_Click(null, null);
                        e.Handled = e.SuppressKeyPress = true;
                    }
                }
                return;
            }
            var btn = GetButton(SoundButtonMaker.KeyToChar(e.KeyCode));
            if (btn != null)
            {
                btn.SoundForm.PlaySoundAsync();
                e.Handled = e.SuppressKeyPress = true;
                return;
            }
            if (e.KeyData == Keys.Space)
            {
                AudioSound.StopAll();
                e.Handled = e.SuppressKeyPress = true;
                return;
                //Stop sounds
            }
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                new SaveSound().Show();
                return;
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
            if (!CanOverwrite()) return;
            var file = new OpenFileDialog() { AddExtension = true, DefaultExt = "sbdn", CheckFileExists = true, InitialDirectory = SoundBoardData.GetDefaultSaveDirectory().FullName };
            if (file.ShowDialog() == DialogResult.Cancel) return;
            SavePath = file.FileName;
            try
            {
                SoundBoardData.Load(SavePath);
            }
            catch (Exception x)
            {
                //MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //message boxes are now being shown from the root of the exception
                SavePath = "";
                return;
            }
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
            if (!CanOverwrite()) return;
            SavePath = String.Empty;
            foreach (var item in Buttons)
            {
                item.Data.Reset();
                item.Update();
            }
            HasChanged = false;
        }

        private bool CanOverwrite()
        {
            if (HasChanged)
            {
                var result = MessageBox.Show("Unsaved changes.\nSave before continuing?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);

                switch (result)
                {
                    case DialogResult.None:
                        return false;
                    case DialogResult.Cancel:
                        return false;
                    case DialogResult.Yes:
                        return Save();
                    case DialogResult.No:
                        return true;
                    default:
                        return false;
                }
            }
            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CanOverwrite();
        }
    }
}
