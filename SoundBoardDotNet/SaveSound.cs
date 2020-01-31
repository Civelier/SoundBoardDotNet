using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Utils;

namespace SoundBoardDotNet
{
    public partial class SaveSound : Form
    {
        private static string _defaultStartPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        public AudioRecorder Recorder;
        public AudioSound Sound;

        public SaveSound()
        {
            InitializeComponent();

            AddActionsForControlsOfTypes((Control c) => c.KeyDown += PlayStopOnKeys, typeof(Button), typeof(ComboBox), typeof(NumericUpDown));
            AddActionsForControlsOfTypes((Control c) => c.KeyDown += SelectNextOnEnterKey, typeof(ComboBox), typeof(NumericUpDown), typeof(TextBox));
            AddActionsForControlsOfTypes((Control c) => c.KeyDown += SpaceForNumUpDown, typeof(NumericUpDown));
            AddArrowSelectForControls(SoundWaveGraph.StartUpDown, SoundWaveGraph.EndUpDown, SaveButton, CancelButton);
            AddActionsForControlsOfTypes((Control c) => { c.KeyDown += CloseOnEsc; c.KeyDown += SupressKeys; }, typeof(Button), typeof(ComboBox), typeof(NumericUpDown), typeof(TextBox));

            InputDevice.StopRecorders();
            Recorder = InputDevice.GetRecordedDevices()[0].Recorder;
            foreach (var inputDevice in InputDevice.GetRecordedDevices())
            {
                InputCombo.Items.Add(inputDevice.DeviceName);
            }
            InputCombo.SelectedIndex = 0;
            var prov = Recorder.GetWaveProvider();
            Sound = new AudioSound(prov, 0, 0, VolumeControl.Volume);
            Sound.EndPos = Recorder.RecordedTime;

            SoundWaveGraph.StartUpDown.ValueChanged += StartTime_ValueChanged;
            SoundWaveGraph.EndUpDown.ValueChanged += EndTime_ValueChanged;
            SoundWaveGraph.StartUpDown.Minimum = 0;
            SoundWaveGraph.StartUpDown.Maximum = (decimal)Sound.EndPos;
            SoundWaveGraph.EndUpDown.Minimum = 0;
            SoundWaveGraph.EndUpDown.Maximum = SoundWaveGraph.StartUpDown.Maximum;
            SoundWaveGraph.EndUpDown.Value = SoundWaveGraph.StartUpDown.Maximum;
            TotalTimeLabel.Text = $"{SoundWaveGraph.EndUpDown.Maximum} s";
            KeyCombo.Items.Add("Select a key");
            foreach (var item in Form1.MyKeyboard)
            {
                foreach (var key in item)
                {
                    KeyCombo.Items.Add(key);
                }
            }
            KeyCombo.SelectedIndex = 0;
            //SaveButton.Enabled = false
            SoundWaveGraph.WaveStream = Recorder.GetWaveStream();
        }

        private void SupressKeys(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void AddActionsForControlsOfTypes(Action<Control> action, params Type[] types)
        {
            foreach (var control in Controls)
            {
                foreach (var type in types)
                {
                    if (control.GetType() == type)
                    {
                        action((Control)control);
                    }
                }
            }
        }

        private void AddArrowSelectForControls(params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.KeyDown += SelectWithArrows;
            }
        }

        private void SpaceForNumUpDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = e.SuppressKeyPress = true;
                ((NumericUpDown)sender).Refresh();
            }
        }

        private void SelectNextOnEnterKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, false, false, true);
                return;
            }
            e.Handled = false;
            e.SuppressKeyPress = false;
        }

        private void PlayStopOnKeys(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (e.Control)
                {
                    Stop();
                }
                else
                {
                    Play();
                }
                return;
            }
            e.Handled = true;
            e.SuppressKeyPress = false;
        }

        private void CloseOnEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                return;
            }

            e.SuppressKeyPress = false;
        }

        private void SelectWithArrows(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, true, false, false, true);
                return;
            }
            if (e.KeyCode == Keys.Left)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SelectNextControl(ActiveControl, false, false, false, true);
                return;
            }
            e.SuppressKeyPress = false;
        }

        private bool _isSaved = false;
        private bool SaveFile()
        {
            if (_isSaved) return true;
            if (NameTextBox.Text == "")
            {
                MessageBox.Show("File name is empty!", "File name empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string fileName = _defaultStartPath + "\\" + NameTextBox.Text + ".wav";

            

            if (File.Exists(fileName))
            {
                if (!VerifySoundOvewrite(fileName))
                {
                    MessageBox.Show("File already exists and is used in the soundboard.\nChoose another name or remove other sound from soundboard.", "Cannot replace existing file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                var result = MessageBox.Show("File already exists. Replace file?", "Replace file", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                switch (result)
                {
                    case DialogResult.None:
                        return false;
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        return false;
                    default:
                        return false;
                }
            }
            KeyCombo.SelectedItem = KeyCombo.Text;



            if (!KeyComboValidSelection())
            {
                Recorder.Save(fileName);
                _isSaved = true;
                return true;
            }

            var btn = GetButton();

            

            if (btn.HasSound)
            {
                var result = MessageBox.Show("This key already has a sound. Replace it?", "Replace sound", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    ResetKeyCombo();
                    return false;
                }
            }

            Recorder.Save(fileName);
            btn.Btn.Text = btn.Name + "\n" + NameTextBox.Text;
            btn.Data.FilePath = fileName;
            btn.Data.StartTime = (double)SoundWaveGraph.StartUpDown.Value;
            btn.Data.EndTime = (double)SoundWaveGraph.EndUpDown.Value;
            btn.Data.Name = NameTextBox.Text;
            btn.SoundForm.Sound = new AudioSound(btn.Data.FilePath, btn.Data.StartTime, btn.Data.EndTime, btn.Data.Volume);
            btn.SoundForm.Data.FilePath = btn.Data.FilePath = btn.SoundForm.Sound.FileName;
            _isSaved = true;
            Form1.MyForm.HasChanged = true;
            return true;
        }

        private bool KeyComboValidSelection()
        {
            foreach (var key in Form1.MyKeyboard)
            {
                foreach (var c in key)
                {
                    if (KeyCombo.Text == c.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (SaveFile())
            {
                Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Play()
        {
            AudioSound.StopAll();
            AudioSound.PlayRecordedSound(new AudioSound(Recorder, (double)SoundWaveGraph.StartUpDown.Value, (double)SoundWaveGraph.EndUpDown.Value, VolumeControl.Volume));
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void Stop()
        {
            AudioSound.StopAll();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void VolumeControl_VolumeChanged(object sender, EventArgs e)
        {
            Recorder.Sound.Volume = VolumeControl.Volume;
        }

        private void StartTime_ValueChanged(object sender, EventArgs e)
        {
            SoundWaveGraph.EndUpDown.Minimum = SoundWaveGraph.StartUpDown.Value;
            Sound.StartPos = Convert.ToDouble(SoundWaveGraph.StartUpDown.Value);
        }

        private void EndTime_ValueChanged(object sender, EventArgs e)
        {
            SoundWaveGraph.StartUpDown.Maximum = SoundWaveGraph.EndUpDown.Value;
            Sound.EndPos = Convert.ToDouble(SoundWaveGraph.EndUpDown.Value);
        }

        private SoundButtonMaker GetButton()
        {
            foreach (var button in Form1.Buttons)
            {
                if (KeyCombo.Text == "Select a key") return null;
                if (button.Name == KeyCombo.Text)
                {
                    return button;
                }
            }
            return null;
        }

        private bool VerifySoundOvewrite(string filePath)
        {
            foreach (var button in Form1.Buttons)
            {
                if (button != null)
                {
                    if (button.Data != null)
                    {
                        if (button.Data.FilePath == filePath) return false;
                    }
                }
            }
            return true;
        }

        private void ResetKeyCombo()
        {
            //SaveButton.Enabled = false;
            KeyCombo.SelectedIndex = 0;
        }

        private void KeyCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KeyCombo.SelectedIndex == 0)
            {
                ResetKeyCombo();
                return;
            }

            var btn = GetButton();
            if (btn == null)
            {
                ResetKeyCombo();
                return;
            }

            
            //SaveButton.Enabled = true;
        }

        private void InputCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recorder.CloseWaveStream();
            Recorder = InputDevice.GetRecordedDevices()[InputCombo.SelectedIndex].Recorder;
            var prov = Recorder.GetWaveProvider();
            VolumeControl.Volume = 1;
            Sound = new AudioSound(Recorder, 0, 0, VolumeControl.Volume);
            SoundWaveGraph.WaveStream = Recorder.GetWaveStream();
            Sound.EndPos = Recorder.RecordedTime;
            SoundWaveGraph.StartUpDown.Minimum = 0;
            SoundWaveGraph.StartUpDown.Maximum = (decimal)Sound.EndPos;
            SoundWaveGraph.EndUpDown.Minimum = 0;
            SoundWaveGraph.EndUpDown.Maximum = SoundWaveGraph.StartUpDown.Maximum;
            SoundWaveGraph.EndUpDown.Value = SoundWaveGraph.StartUpDown.Maximum;
            TotalTimeLabel.Text = $"{SoundWaveGraph.EndUpDown.Maximum} s";
        }

        private void SaveSound_FormClosed(object sender, FormClosedEventArgs e)
        {
            SoundWaveGraph.WaveStream = null;
            Recorder.CloseWaveStream();
            InputDevice.StartRecorders();
        }

        private void SaveSound_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isSaved)
            {
                e.Cancel = false;
                return;
            }
            var result = MessageBox.Show("Save changes?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            switch (result)
            {
                case DialogResult.None:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    e.Cancel = true;
                    if (SaveFile()) e.Cancel = false;
                    break;
                case DialogResult.No:
                    e.Cancel = false;
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }

        private void KeyCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    KeyCombo.SelectedItem = KeyCombo.Text;
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Not a valid key!", "Invalid key");
                    KeyCombo.SelectedIndex = 0;
                }
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Form1.MyKeyboard.Length; i++)
            {
                sb.Append(Form1.MyKeyboard[i]);
            }
            string s = sb.ToString();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].ToString() == SoundButtonMaker.KeyToChar(e.KeyCode).ToString().ToLower())
                {
                    KeyCombo.SelectedIndex = i + 1;
                    e.Handled = e.SuppressKeyPress = true;
                    KeyCombo.Text = "";
                    return;
                }
            }
            e.Handled = e.SuppressKeyPress = true;
        }
    }
}
