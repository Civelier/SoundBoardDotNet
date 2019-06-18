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
            AddArrowSelectForControls(StartTime, EndTime);
            AddActionsForControlsOfTypes((Control c) => { c.KeyDown += CloseOnEsc; c.KeyDown += SupressKeys; }, typeof(Button), typeof(ComboBox), typeof(NumericUpDown), typeof(TextBox));

            Recorder = Form1.Recorders[0];
            foreach (var recorder in Form1.Recorders)
            {
                recorder.StopRecording();
                InputCombo.Items.Add(WaveIn.GetCapabilities(recorder.Device).ProductName);
            }
            InputCombo.SelectedIndex = 0;
            var prov = Recorder.GetWaveProvider();
            Sound = new AudioSound(prov, 0, 0, VolumeControl.Volume);
            Sound.EndPos = Recorder.RecordedTime;
            StartTime.Minimum = 0;
            StartTime.Maximum = (decimal)Sound.EndPos;
            EndTime.Minimum = 0;
            EndTime.Maximum = StartTime.Maximum;
            EndTime.Value = StartTime.Maximum;
            TotalTimeLabel.Text = $"{EndTime.Maximum} s";
            KeyCombo.Items.Add("Select a key");
            foreach (var item in Form1.MyKeyboard)
            {
                foreach (var key in item)
                {
                    KeyCombo.Items.Add(key);
                }
            }
            KeyCombo.SelectedIndex = 0;
            //SaveButton.Enabled = false;
            WaveGraph.WaveStream = Sound.FileReader;
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
            System.Threading.Thread.Sleep(2000);
            btn.Btn.Text = btn.Name + "\n" + NameTextBox.Text;
            btn.Data.FilePath = fileName;
            btn.HasSound = true;
            btn.Data.StartTime = (double)StartTime.Value;
            btn.Data.EndTime = (double)EndTime.Value;
            btn.Data.Name = NameTextBox.Text;
            _isSaved = true;
            Form1.MyForm.HasChanged = true;
            return true;
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

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Recorder.PlayRecorded();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Recorder.StopReplay();
        }

        private void VolumeControl_VolumeChanged(object sender, EventArgs e)
        {
            Recorder.Sound.Volume = VolumeControl.Volume;
        }

        private void StartTime_ValueChanged(object sender, EventArgs e)
        {
            EndTime.Minimum = StartTime.Value;
            Sound.StartPos = Convert.ToDouble(StartTime.Value);
        }

        private void EndTime_ValueChanged(object sender, EventArgs e)
        {
            StartTime.Maximum = EndTime.Value;
            Sound.EndPos = Convert.ToDouble(EndTime.Value);
        }

        private SoundButtonMaker GetButton()
        {
            foreach (var button in Form1.Buttons)
            {
                if (KeyCombo.SelectedItem == null) return null;
                if (button.Name == KeyCombo.SelectedItem.ToString())
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
            var prov = Recorder.GetWaveProvider();
            VolumeControl.Volume = 1;
            Sound = new AudioSound(prov, 0, 0, VolumeControl.Volume);
            Sound.EndPos = Recorder.RecordedTime;
            StartTime.Minimum = 0;
            StartTime.Maximum = (decimal)Sound.EndPos;
            EndTime.Minimum = 0;
            EndTime.Maximum = StartTime.Maximum;
            EndTime.Value = StartTime.Maximum;
            TotalTimeLabel.Text = $"{EndTime.Maximum} s";
            WaveGraph.WaveStream = Sound.FileReader;
        }

        private void SaveSound_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var recorder in Form1.Recorders)
            {
                recorder.Reset();
                recorder.StartRecording();
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
                    Recorder.StopReplay();
                }
                else
                {
                    Recorder.PlayRecorded();
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
        }
    }
}
