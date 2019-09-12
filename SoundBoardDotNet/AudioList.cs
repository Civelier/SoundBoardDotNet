using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundBoardDotNet
{
    public partial class AudioList : UserControl
    {
        private int _height = 20;
        private int _width = 80;
        public AudioList()
        {
            InitializeComponent();
            Devices.DevicesInfo.DevicesReloaded += new EventHandler((object o, EventArgs e) => UpdateDevices());
            Devices.RefreshAll();
        }

        public void UpdateDevices()
        {
            Content.Controls.Clear();
            var devices = Devices.DevicesInfo.RecordedDevices;
            for (int i = 0; i < devices.Count; i++)
            {
                var t = new TextBox();
                t.Anchor = AnchorStyles.Left;
                t.Text = devices[i].DeviceName ?? "Default";
                Content.Controls.Add(t);
                t.Enabled = false;
                t.Location = new Point(0, -i * _height);
            }
        }

        private void Content_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Content_ControlAdded(object sender, ControlEventArgs e)
        {
            Content.Size = new Size(e.Control.Right, e.Control.Bottom);
        }
    }
}
