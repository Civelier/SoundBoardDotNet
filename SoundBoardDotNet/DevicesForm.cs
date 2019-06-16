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
        private List<bool> _inDevices = new List<bool>();
        private Action<int[]> _callback;

        public DevicesForm(Action<int[]> cb)
        {
            _callback = cb;
            InitializeComponent();
            RefreshOut();
            RefreshIn();
            InputsCombo.SelectedItem = 0;
        }

        private void RefreshOut()
        {
            OutputsCombo.Items.Clear();

            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                OutputsCombo.Items.Add(WaveOut.GetCapabilities(i).ProductName);
            }
        }

        private void RefreshIn()
        {
            RecordInputCheck.Checked = true;
            InputsCombo.Items.Clear();
            _inDevices.Clear();

            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                InputsCombo.Items.Add(WaveIn.GetCapabilities(i).ProductName);
                _inDevices.Add(i == 0);
            }
            InputsCombo.SelectedItem = 0;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            List<int> devices = new List<int>();
            for(int i = 0; i < _inDevices.Count; i++)
            {
                if (_inDevices[i]) devices.Add(i); 
            }
            if (devices.Count == 0)
            {
                MessageBox.Show("At least one input is required!", "Insufficient inputs");
                return;
            }
            _callback(devices.ToArray());
            Close();
        }

        private void RefreshOutputsButton_Click(object sender, EventArgs e)
        {
            RefreshOut();
        }

        private void OutputsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            OutDevice = OutputsCombo.SelectedIndex;
        }

        private void InputsCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InputsCombo.SelectedIndex == -1) RecordInputCheck.Checked = false;
            RecordInputCheck.Checked = _inDevices[InputsCombo.SelectedIndex];
        }

        private void RecordInputCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (InputsCombo.SelectedIndex != -1) _inDevices[InputsCombo.SelectedIndex] = RecordInputCheck.Checked;
        }

        private void RefreshInputsButton_Click(object sender, EventArgs e)
        {
            RefreshIn();
            InputsCombo.SelectedItem = 0;
        }
    }
}
