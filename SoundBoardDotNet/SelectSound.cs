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
        public static WaveOut Device;

        public WaveStream Sound;
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
            _fileDialog.Filter = "All playable files (*.wav)|*.wav;*.WAV";
            _fileDialog.FilterIndex = 0;
            InitializeComponent();
            FileNameBox.Text = data.FilePath;
            NameTextBox.Text = data.Name;
            Sound = data.Sound;
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
            var bytes = _soundWaves(FileNameBox.Text);
            if (bytes == null)
            {
                Debug.WriteLine("File was invalid!");
                if (eraseIfEmpty)
                {
                    //Erase wave graph

                    //WaveGraph.ResetValues();
                    //WaveGraph.EraseGraph();
                }
                return;
            }
            if (bytes.Length == 0)
            {
                Debug.WriteLine("No bytes");
                if (eraseIfEmpty)
                {
                    //Erase graph

                    //WaveGraph.ResetValues();
                    //WaveGraph.EraseGraph();
                }
                return;
            }
            //Draw graph

            //WaveGraph.Values = new List<byte>(bytes);
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
            FileInfo file = new FileInfo(FileNameBox.Text);

            if (!file.Exists) throw new FileNotFoundException("File not found!", file.FullName);

            Data.FilePath = FileNameBox.Text;
            Data.Name = NameTextBox.Text;
            Data.Sound = Sound;
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

        public bool PlaySoundAsync()
        {
            //var sound = AudioSound.AddSound(FileNameBox.Text, WaveGraph.Slider1Value, WaveGraph.Slider2Value, (float)VolumeTrack.Value / 100);
            //return sound != null;// true if successfull
            return false;
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
            if (!PlaySoundAsync())
            {
                MessageBox.Show("Enter a valid File to play!");
                return;
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            //Stop sounds
            //if (Sound != null) Engine.Init(Sound);
        }

        private void SelectSound_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Stop sounds
            //if (Sound != null) Sound.Stop();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (FileNameBox.Text != "")
            {
                //Verify if file is playable
                //if (Engine.Play2D(FileNameBox.Text, false, true) == null)
                //{
                //    MessageBox.Show("Invalid file name!");
                //    return;
                //}
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
            //Change the sounds volume
        }
    }
}