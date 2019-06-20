using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace SoundBoardDotNet
{
    public partial class DevicesForm : Form
    {
        public static int OutDevice = 0;
        private bool _keepChanges = false;
        public DevicesForm()
        {
            InitializeComponent();
            RefreshOut();
            RefreshIn();
            InputsCombo.SelectedItem = 0;
        }

        private void RefreshOut()
        {
            OutputsCombo.Items.Clear();
            OutputDevice.RefreshOutputs();
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                OutputsCombo.Items.Add(WaveOut.GetCapabilities(i).ProductName);
            }
            OutputsCombo.SelectedIndex = OutputDevice.MainOutput.Index;
        }

        private void RefreshIn()
        {
            InputsCombo.Items.Clear();
            InputDevice.RefreshInputs();
            foreach (var input in InputDevice.InputDevices)
            {
                InputsCombo.Items.Add(input.DeviceName);
            }
            InputsCombo.SelectedIndex = 0;
            RecordInputCheck.Checked = InputDevice.InputDevices[0].IsRecorded;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var recordedDevices = InputDevice.GetRecordedDevices();
            if (recordedDevices.Count == 0)
            {
                MessageBox.Show("At least one input is required!", "Insufficient inputs");
                return;
            }
            SoundBoardData.SaveDevices();
            _keepChanges = true;
            Close();
        }

        private void RefreshOutputsButton_Click(object sender, EventArgs e)
        {
            RefreshOut();
        }

        private void OutputsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            OutputDevice.MainOutput = OutputDevice.OutputDevices[OutputsCombo.SelectedIndex];
        }

        private void InputsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InputsCombo.SelectedIndex == -1) RecordInputCheck.Checked = false;
            RecordInputCheck.Checked = InputDevice.InputDevices[InputsCombo.SelectedIndex].IsRecorded;
        }

        private void RecordInputCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (InputsCombo.SelectedIndex != -1) InputDevice.InputDevices[InputsCombo.SelectedIndex].IsRecorded = RecordInputCheck.Checked;
        }

        private void RefreshInputsButton_Click(object sender, EventArgs e)
        {
            RefreshIn();
            InputsCombo.SelectedIndex = 0;
        }

        private void DevicesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_keepChanges)
            {
                SoundBoardData.LoadProperties();
            }
            InputDevice.ResetRecorders();
            InputDevice.StartRecorders();
        }
    }
}
