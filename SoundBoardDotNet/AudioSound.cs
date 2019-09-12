using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IrrKlang;
using System.Diagnostics;
using NAudio;
using NAudio.Wave;
using System.Timers;
using System.IO;
using Forms = System.Windows.Forms;

namespace SoundBoardDotNet
{
    public class AudioSound
    {
        public static List<AudioSound> Sounds = new List<AudioSound>();
        private AudioFileReader _fileReader;
        private BufferedWaveProvider _buffWaveProvider;
        private WaveOut _out;

        public string FileName;
        private double _startPos, _endPos;

        public double StartPos
        {
            get { return _startPos; }
            set
            {
                _startPos = value;
                try { _timer.Interval = (_endPos - _startPos) * 1000; }
                catch (ArgumentException)
                {
                    Debug.Print("Timer interval was less or equal to 0");
                    _timer.Interval = 0.001;
                }
            }
        }

        public double EndPos
        {
            get { return _endPos; }
            set
            {
                _endPos = value;
                try { _timer.Interval = (_endPos - _startPos) * 1000; }
                catch (ArgumentException)
                {
                    Debug.Print("Timer interval was less or equal to 0");
                    _timer.Interval = 0.001;
                }
            }
        }

        public float Volume
        {
            get { return _out.Volume; }
            set
            {
                _out.Volume = value;
            }
        }
        public AudioFileReader FileReader => _fileReader;

        private Timer _timer;

        public AudioSound(string fileName, double startPos, double endPos, float volume, bool loop = false)
        {
            _out = new WaveOut();
            _out.DeviceNumber = OutputDevice.MainOutput.Index;
            FileName = fileName;
            _startPos = startPos;
            _endPos = endPos;
            Volume = volume;
            try { _timer = new Timer((_endPos - _startPos) * 1000); }
            catch (ArgumentException)
            {
                Debug.Print("Timer interval was less or equal to 0");
                _timer = new Timer(0.001);
            }
            _timer.Elapsed += new ElapsedEventHandler(_stop);
            _timer.AutoReset = false;
            
            if (FileName == null)
            {
                if (_buffWaveProvider == null) return;
                _out.Init(_buffWaveProvider);
                return;
            }

            while (true)
            {
                if (new FileInfo(FileName).Exists)
                {
                    _fileReader = new AudioFileReader(FileName);
                    break;
                }
                else
                {
                    var result = Forms.MessageBox.Show($"File {FileName} does not exist.\nDo you want to try to find the file?", "Error", Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Error);

                    if (result == Forms.DialogResult.Yes)
                    {
                        var file = new Forms.OpenFileDialog();
                        var fileInfo = new FileInfo(fileName);
                        file.InitialDirectory = fileInfo.DirectoryName;
                        file.CheckFileExists = true;
                        var result2 = file.ShowDialog();
                        if (result2 != Forms.DialogResult.Cancel)
                        {
                            if (new AudioFileReader(file.FileName).TotalTime.TotalSeconds >= EndPos)
                            {
                                FileName = file.FileName;
                            }
                        }
                    }
                    if (result == Forms.DialogResult.No)
                    {
                        break;
                    }
                }
            }
            
            if (_fileReader != null)
            {
                _out.Init(_fileReader);
            }
        }

        public AudioSound(AudioRecorder recorder, double startPos, double endPos, float volume)
        {
            _buffWaveProvider = (BufferedWaveProvider)recorder.GetWaveProvider();
            _out = new WaveOut();
            _out.DeviceNumber = OutputDevice.MainOutput.Index;
            _startPos = startPos;
            _endPos = endPos;
            Volume = volume;
            try { _timer = new Timer((_endPos - _startPos) * 1000); }
            catch (ArgumentException)
            {
                Debug.Print("Timer interval was less or equal to 0");
                _timer = new Timer(0.001);
            }
            _timer.Elapsed += new ElapsedEventHandler(_stop);
            _timer.AutoReset = false;

            _out.Init(_buffWaveProvider.ToSampleProvider().Skip(TimeSpan.FromSeconds(_startPos)));
        }

        public AudioSound(IWaveProvider wave, double startPos, double endPos, float volume)
        {
            _out = new WaveOut();
            _out.DeviceNumber = OutputDevice.MainOutput.Index;
            _startPos = startPos;
            _endPos = endPos;
            Volume = volume;
            try { _timer = new Timer((_endPos - _startPos) * 1000); }
            catch (ArgumentException)
            {
                Debug.Print("Timer interval was less or equal to 0");
                _timer = new Timer(0.001);
            }
            _timer.Elapsed += new ElapsedEventHandler(_stop);
            _timer.AutoReset = false;

            _out.Init(wave);
        }

        private AudioSound(AudioSound sound) : this(sound.FileName, sound._startPos, sound._endPos, sound.Volume)
        {
            if (sound._buffWaveProvider != null)
            {
                _buffWaveProvider = sound._buffWaveProvider;
            }
        }

        public void _stop(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine(e.SignalTime.ToString() + " stop raised");
            Stop();
        }

        public static void PlaySound(AudioSound sound)
        {
            new AudioSound(sound).Play();
        }

        public static void PlayRecordedSound(AudioSound sound)
        {
            sound.Play();
        }

        public void Play()
        {
            if (_fileReader != null)
            {
                _fileReader.CurrentTime = TimeSpan.FromSeconds(_startPos);
            }

            _out.Volume = Volume;
            _out.Play();
            _timer.Start();
            Sounds.Add(this);
        }

        public void Stop()
        {
            _stop();
            Sounds.Remove(this);
        }

        private void _stop()
        {
            _out.Stop();
            _timer.Stop();
        }

        public static void StopAll()
        {
            foreach (var sound in Sounds)
            {
                sound._stop();
                sound._out.Dispose();
                if (sound._fileReader != null) sound._fileReader.Dispose();
                sound._timer.Dispose();
            }
            Sounds.Clear();
        }

        private uint _percentToTime(double percent)
        {
            //if (Sound == null) return uint.MaxValue;
            //var output = Convert.ToUInt32(Math.Round(percent * Sound.PlayLength / 100, 0));

            return 0;
        }
    }
}
