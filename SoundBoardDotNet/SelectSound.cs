using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IrrKlang;
using System.Windows.Forms.DataVisualization.Charting;

namespace SoundBoardDotNet
{
    public partial class SelectSound : Form
    {
        public static ISoundEngine Engine;

        public ISound Sound;
        public string FileName;

        public TextBox GetNameTextBox { get { return NameTextBox; } }
        public TextBox GetFileTextBox { get { return FileNameBox; } }

        private OpenFileDialog _fileDialog = new OpenFileDialog();
        public SoundButtonData Data;

        public delegate void CallBack();

        private CallBack _cb;

        public SelectSound(CallBack cb, SoundButtonData data)
        {
            Data = data;
            _cb = cb;
            _fileDialog.Filter = "All playable files (*.mp3;*.ogg;*.wav;*.mod;*.it;*.xm;*.it;*.s3d)|*.mp3;*.ogg;*.wav;*.mod;*.it;*.xm;*.it;*.s3d";
            _fileDialog.FilterIndex = 0;
            InitializeComponent();
            FileNameBox.Text = data.FilePath;
            NameTextBox.Text = data.Name;
            Sound = data.Sound;
        }

        private byte[] _soundWaves(string file)
        {
            var source = Engine.AddSoundSourceFromFile(file);
            if (source != null) return source.SampleData;
            return new byte[0];
        }

        private void _updateGraph()
        {
            WaveGraph.Values = new List<byte>(_soundWaves(FileNameBox.Text));
            WaveGraph.Draw();
        }

        private void _updateData()
        {
            Data.FilePath = FileNameBox.Text;
            Data.Name = NameTextBox.Text;
            Data.Sound = Sound;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            _fileDialog.ShowDialog();
            FileNameBox.Text = _fileDialog.FileName;
            var pathList = FileNameBox.Text.Split('\\');
            var extensionList = pathList[pathList.Length - 1].Split('.');
            NameTextBox.Text = extensionList[0];
            _updateGraph();
        }

        private void FileNameBox_TextChanged(object sender, EventArgs e)
        {
            var pathList = FileNameBox.Text.Split('\\');
            var extensionList = pathList[pathList.Length - 1].Split('.');
            NameTextBox.Text = extensionList[0];
            _updateGraph();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (FileNameBox.Text == "")
            {
                MessageBox.Show("Enter a valid File to play!");
                return;
            }
            Sound = Engine.Play2D(FileNameBox.Text);
            if (Sound == null)
            {
                MessageBox.Show("Enter a valid File to play!");
                return;
            }
            Sound.Volume = (float)VolumeTrack.Value / 100;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (Sound != null) Sound.Stop();
        }

        private void SelectSound_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Sound != null) Sound.Stop();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            _updateData();
            _cb();
            Hide();
        }

        public void LoadFromData()
        {
            FileNameBox.Text = Data.FilePath;
            NameTextBox.Text = Data.Name;
            Sound = Data.Sound;
            Data.Volume = (float)VolumeTrack.Value / 100;
        }

        private void SelectSound_Shown(object sender, EventArgs e)
        {
            LoadFromData();
        }

        private void SelectSound_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            if (Sound != null) Sound.Stop();
            e.Cancel = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void VolumeTrack_Scroll(object sender, EventArgs e)
        {
            if (Sound != null)
            {
                Sound.Volume = (float)VolumeTrack.Value / 100;
            }
            LabelVolume.Text = VolumeTrack.Value.ToString();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            FileNameBox.Text = "";
            NameTextBox.Text = "";
        }
    }
}
