using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundBoardDotNet
{
    
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();
            PreferenceProps.SelectedObject = SoundBoardProperties.Props;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            foreach (var recorder in InputDevice.GetRecordedDevices())
            {
                recorder.Recorder.Reset();
            }
            SoundBoardData.SavePreferences();
            Close();
        }

        private void ResetAllButton_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Are you sure you want to reset all parameters to default?\n(This will reset all preferences and devices values to default and CLOSE THE APP! You will still be able to save before closing.)", "Reset all", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                SoundBoardData.DeletePropertyFiles();
                Form1.MyForm.Close();
            }
        }
    }
}
