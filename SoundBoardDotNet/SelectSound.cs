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
using NAudio;
using NAudio.Wave;
using System.IO;

namespace SoundBoardDotNet
{
    public partial class SelectSound : Form
    {
        public AudioSound Sound;
        public string FileName;

        public TextBox GetNameTextBox { get { return NameTextBox; } }
        public TextBox GetFileTextBox { get { return FileNameBox; } }
        //public WaveForm MyWaveForm { get { return WaveGraph; } }

        private OpenFileDialog _fileDialog = new OpenFileDialog();
        public SoundButtonData Data;

        public delegate void CallBack();

        private CallBack _cb;

        public SelectSound(CallBack cb, SoundButtonData data)
        {
            Data = data;
            _cb = cb;
            _fileDialog.Filter = "All playable files (*.wav)|*.wav;*.WAV;*.mp3;*.MP3";
            _fileDialog.FilterIndex = 0;
            InitializeComponent();
            FileNameBox.Text = data.FilePath;
            NameTextBox.Text = data.Name;
            StartTime.Minimum = 0;
            StartTime.Maximum = 0;
            EndTime.Minimum = 0;
            EndTime.Maximum = 0;
            //Sound = new AudioSound(data.FilePath, data.Slider1, data.Slider2, data.Volume);
        }

        private byte[] _soundWaves(string file)
        {
            //Engine.RemoveSoundSource(file);
            //var source = Engine.AddSoundSourceFromFile(file);
            //if (source != null)
            //{
            //    return source.SampleData;
            //}

            //get data
            return new byte[0];
        }

        private void _updateGraph(bool eraseIfEmpty = false)
        {
            if (FileNameBox.Text == "")
            {
                if (eraseIfEmpty)
                {
                    TotalTimeLabel.Text = "";
                    EndTime.Maximum = 0;
                }
                return;
            }

            Sound = new AudioSound(FileNameBox.Text, (double)StartTime.Value, (double)EndTime.Value, VolumeControl.Volume);

            WaveGraph.WaveStream = Sound.FileReader;
            var temp = EndTime.Maximum;
            EndTime.Maximum = (decimal)Sound.FileReader.TotalTime.TotalMilliseconds;
            if (temp == 0)
                EndTime.Value = EndTime.Maximum;
            TotalTimeLabel.Text = $"{EndTime.Maximum} ms";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //_updateGraph();
        }

        private void _updateData()
        {
            FileInfo file = new FileInfo(FileNameBox.Text);

            if (!file.Exists) throw new FileNotFoundException("File not found!", file.FullName);

            Data.FilePath = FileNameBox.Text;
            Data.Name = NameTextBox.Text;
            Data.Volume = VolumeControl.Volume;
            

            //Data.Volume = (float)VolumeTrack.Value / 100;
            //Data.Slider1 = WaveGraph.Slider1Value;
            //Data.Slider2 = WaveGraph.Slider2Value;
        }

        private uint _percentToTime(double percent, uint time)
        {
            var output = Convert.ToUInt32(Math.Round(percent * time / 100, 0));
            return output;
        }

        public void PlaySoundAsync()
        {
            //var sound = AudioSound.AddSound(FileNameBox.Text, WaveGraph.Slider1Value, WaveGraph.Slider2Value, (float)VolumeTrack.Value / 100);
            //return sound != null;// true if successfull
            if (Sound == null) return;
            AudioSound.PlaySound(Sound);
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            _fileDialog.ShowDialog();
            FileNameBox.Text = _fileDialog.FileName;
            var pathList = FileNameBox.Text.Split('\\');
            var extensionList = pathList[pathList.Length - 1].Split('.');
            NameTextBox.Text = extensionList[0];
            //WaveGraph.ResetCursors();
            _updateGraph();
        }

        private void FileNameBox_TextChanged(object sender, EventArgs e)
        {
            var pathList = FileNameBox.Text.Split('\\');
            var extensionList = pathList[pathList.Length - 1].Split('.');
            NameTextBox.Text = extensionList[0];
            //WaveGraph.ResetCursors();
            _updateGraph();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (FileNameBox.Text == "")
            {
                MessageBox.Show("Enter a valid File to play!");
                return;
            }
            AudioSound.PlaySound(Sound);
            //if (!PlaySoundAsync())
            //{
            //    MessageBox.Show("Enter a valid File to play!");
            //    return;
            //}
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            //Stop sounds
            //if (Sound != null) Engine.Init(Sound);
            AudioSound.StopAll();
        }

        private void SelectSound_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Stop sounds
            //if (Sound != null) Sound.Stop();
            AudioSound.StopAll();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                _updateData();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Invalid file name!");
                return;
            }

            _cb();
            Hide();
        }

        public void LoadFromData()
        {
            FileNameBox.Text = Data.FilePath;
            NameTextBox.Text = Data.Name;
            VolumeControl.Volume = Data.Volume;

            //WaveGraph.Slider1Value = Data.Slider1;
            //WaveGraph.Slider2Value = Data.Slider2;
            //VolumeTrack.Value = Convert.ToInt32(Data.Volume * 100);
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

            //Stop sound
            //if (Sound != null) Sound.Stop();
            e.Cancel = true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Hide();
            LoadFromData();
            _updateGraph(true);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            FileNameBox.Text = "";
            NameTextBox.Text = "";
            _updateGraph(true);
        }

        private void Volume_VolumeChanged(object sender, EventArgs e)
        {
            Sound.Volume = VolumeControl.Volume;
        }

        private void StartTime_ValueChanged(object sender, EventArgs e)
        {
            EndTime.Minimum = StartTime.Maximum;
            Sound.StartPos = (double)StartTime.Value;
        }

        private void EndTime_ValueChanged(object sender, EventArgs e)
        {
            StartTime.Maximum = EndTime.Value;
            Sound.EndPos = (double)EndTime.Value;
        }
    }
}