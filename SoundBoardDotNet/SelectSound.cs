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
using System.Diagnostics;

namespace SoundBoardDotNet
{
    public partial class SelectSound : Form
    {
        public static ISoundEngine Engine;

        public ISound Sound;
        public string FileName;

        public TextBox GetNameTextBox { get { return NameTextBox; } }
        public TextBox GetFileTextBox { get { return FileNameBox; } }
        public WaveForm MyWaveForm { get { return WaveGraph; } }

        private OpenFileDialog _fileDialog = new OpenFileDialog();
        public SoundButtonData Data;

        public delegate void CallBack();

        private CallBack _cb;

        public SelectSound(CallBack cb, SoundButtonData data)
        {
            Data = data;
            _cb = cb;
            _fileDialog.Filter = "All playable files (*.wav)|*.wav";
            _fileDialog.FilterIndex = 0;
            InitializeComponent();
            FileNameBox.Text = data.FilePath;
            NameTextBox.Text = data.Name;
            Sound = data.Sound;
            WaveGraph.GradLabel = GradLabel;
        }

        private byte[] _soundWaves(string file)
        {
            Engine.RemoveSoundSource(file);
            var source = Engine.AddSoundSourceFromFile(file);
            if (source != null)
            {
                WaveGraph.SoundLength = source.PlayLength;
                return source.SampleData;
            }
            return new byte[0];
        }

        private void _updateGraph(bool eraseIfEmpty = false)
        {
            var bytes = _soundWaves(FileNameBox.Text);
            if (bytes == null)
            {
                Debug.WriteLine("File was invalid!");
                if (eraseIfEmpty)
                {
                    WaveGraph.ResetValues();
                    WaveGraph.EraseGraph();
                }
                return;
            }
            if (bytes.Length == 0)
            {
                Debug.WriteLine("No bytes");
                if (eraseIfEmpty)
                {
                    WaveGraph.ResetValues();
                    WaveGraph.EraseGraph();
                }
                return;
            }
            WaveGraph.Values = new List<byte>(bytes);
            //WaveGraph.DrawGraphOptimizedAvg(WaveGraph.CreateGraphics());
            Refresh();
            WaveGraph.Show();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //_updateGraph();
        }

        private void _updateData()
        {
            Data.FilePath = FileNameBox.Text;
            Data.Name = NameTextBox.Text;
            Data.Sound = Sound;
            Data.Volume = (float)VolumeTrack.Value / 100;
            Data.Slider1 = WaveGraph.Slider1Value;
            Data.Slider2 = WaveGraph.Slider2Value;
        }

        private uint _percentToTime(double percent, uint time)
        {
            var output = Convert.ToUInt32(Math.Round(percent * time / 100, 0));
            return output;
        }

        [Obsolete]
        public void PlaySound()
        {
            var sound = Engine.Play2D(FileNameBox.Text, false, true);
            if (sound == null) return;
            sound.Volume = (float)VolumeTrack.Value / 100;
            var pos = _percentToTime(WaveGraph.Slider1Value, sound.PlayLength);
            sound.PlayPosition = pos;
            sound.Paused = false;
        }

        public bool PlaySoundAsync()
        {
            var sound = AudioSound.AddSound(FileNameBox.Text, WaveGraph.Slider1Value, WaveGraph.Slider2Value, (float)VolumeTrack.Value / 100);
            return sound != null;// true if successfull
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            _fileDialog.ShowDialog();
            FileNameBox.Text = _fileDialog.FileName;
            var pathList = FileNameBox.Text.Split('\\');
            var extensionList = pathList[pathList.Length - 1].Split('.');
            NameTextBox.Text = extensionList[0];
            WaveGraph.ResetCursors();
            _updateGraph();
        }

        private void FileNameBox_TextChanged(object sender, EventArgs e)
        {
            var pathList = FileNameBox.Text.Split('\\');
            var extensionList = pathList[pathList.Length - 1].Split('.');
            NameTextBox.Text = extensionList[0];
            WaveGraph.ResetCursors();
            _updateGraph();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (FileNameBox.Text == "")
            {
                MessageBox.Show("Enter a valid File to play!");
                return;
            }
            if (!PlaySoundAsync())
            {
                MessageBox.Show("Enter a valid File to play!");
                return;
            }
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
            if (FileNameBox.Text != "")
            {
                if (Engine.Play2D(FileNameBox.Text, false, true) == null)
                {
                    MessageBox.Show("Invalid file name!");
                    return;
                }
            }

            _updateData();
            _cb();
            Hide();
        }

        public void LoadFromData()
        {
            FileNameBox.Text = Data.FilePath;
            NameTextBox.Text = Data.Name;
            Sound = Data.Sound;
            WaveGraph.Slider1Value = Data.Slider1;
            WaveGraph.Slider2Value = Data.Slider2;
            VolumeTrack.Value = Convert.ToInt32(Data.Volume * 100);
        }

        private void SelectSound_Shown(object sender, EventArgs e)
        {
            LoadFromData();
        }

        private void SelectSound_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            LoadFromData();
            _updateGraph(true);
            if (Sound != null) Sound.Stop();
            e.Cancel = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Hide();
            LoadFromData();
            _updateGraph(true);
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
            _updateGraph(true);
        }
    }
}