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
            SaveButton.Enabled = false;
            WaveGraph.WaveStream = Sound.FileReader;
        }

        private bool SaveFile()
        {
            if (NameTextBox.Text == "")
            {
                MessageBox.Show("File name is empty!", "File name empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string fileName = _defaultStartPath + "\\" + NameTextBox.Text + ".wav";

            if (File.Exists(fileName))
            {
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

            Recorder.Save(fileName);
            System.Threading.Thread.Sleep(2000);
            var btn = GetButton();
            btn.Btn.Text = btn.Name + "\n" + NameTextBox.Text;
            btn.Data.FilePath = fileName;
            btn.HasSound = true;
            btn.Data.StartTime = (double)StartTime.Value;
            btn.Data.EndTime = (double)EndTime.Value;
            btn.Data.Name = NameTextBox.Text;
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
                if (button.Name == KeyCombo.SelectedItem.ToString())
                {
                    return button;
                }
            }
            return null;
        }

        private void ResetKeyCombo()
        {
            SaveButton.Enabled = false;
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

            if (btn.HasSound)
            {
                var result = MessageBox.Show("This key already has a sound. Replace it?", "Replace sound", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    ResetKeyCombo();
                    return;
                }
            }
            SaveButton.Enabled = true;
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
    }
}
