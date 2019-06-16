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
            foreach (var recorder in Form1.Recorders)
            {
                recorder.Reset();
            }
            Close();
        }
    }
}
