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
using SoundBoardDotNet.SoundEvents;

namespace SoundBoardDotNet
{
    public class AudioSound : IDisposable
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
                if (_endPos - _startPos != 0) _timer = new Timer((_endPos - _startPos) * 1000);
                else _timer = new Timer(0.001);
            }
        }

        public double Time
        {
            get
            {
                if (_fileReader == null) return 0;
                return _fileReader.CurrentTime.TotalSeconds;
            }
            set
            {
                if (_fileReader != null)
                {
                    if (_endPos - value > 0)
                    {
                        if (IsPlaying)
                        {
                            Stop();
                        }
                        _timer.Interval = (_endPos - value) * 1000;
                        if (!IsPlaying)
                        {
                            Play(value);
                        }
                    }
                }
            }
        }

        public double EndPos
        {
            get { return _endPos; }
            set
            {
                _endPos = value;
                if (_endPos - _startPos != 0) _timer = new Timer((_endPos - _startPos) * 1000);
                else _timer = new Timer(0.001);
            }
        }

        public bool IsPlaying => _out.PlaybackState == PlaybackState.Playing;

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

        public event SoundStoppedEventHandler Stopped;
        public event SoundDisposedEventHandler Disposed;
        public event SoundStartedOnAnotherInstanceEventHandler StartedOnAnotherInstance;

        public AudioSound(string fileName, double startPos, double endPos, float volume, bool loop = false)
        {
            if (_out == null) _out = new WaveOut();
            _out.DeviceNumber = OutputDevice.MainOutput.Index;
            FileName = fileName;
            _startPos = startPos;
            _endPos = endPos;
            Volume = volume;
            if (_endPos - _startPos != 0) _timer = new Timer((_endPos - _startPos) * 1000);
            else _timer = new Timer(0.001);
            _timer.Elapsed += new ElapsedEventHandler(OnStop);
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
                            using (var r = new AudioFileReader(file.FileName))
                            if (r.TotalTime.TotalSeconds >= EndPos)
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
                try
                {
                    _out.Init(_fileReader);
                }
                catch (MmException e)
                {
                    Debug.WriteLine(e.Message);
                }
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
            if (_endPos - _startPos != 0) _timer = new Timer((_endPos - _startPos) * 1000);
            else _timer = new Timer(0.001);
            _timer.Elapsed += new ElapsedEventHandler(OnStop);
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
            if (_endPos - _startPos != 0) _timer = new Timer((_endPos - _startPos) * 1000);
            else _timer = new Timer(0.001);
            _timer.Elapsed += new ElapsedEventHandler(OnStop);
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

        private void OnStopMethodCalled()
        {
            SoundStoppedEventHandler handler = Stopped;
            handler?.Invoke(this, SoundStoppedEventArgs.StoppedManually());
        }

        private void OnEndReached()
        {
            SoundStoppedEventHandler handler = Stopped;
            handler?.Invoke(this, SoundStoppedEventArgs.ReachedEnd());
        }

        public void OnStop(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine(e.SignalTime.ToString() + " stop raised");
            StopProcedure();
            OnEndReached();
        }

        private void OnSoundStarted(AudioSound newInstance)
        {
            SoundStartedOnAnotherInstanceEventHandler handler = StartedOnAnotherInstance;
            handler?.Invoke(this, newInstance);
        }

        public static AudioSound PlaySound(AudioSound sound)
        {
            var s = new AudioSound(sound);
            s.Play();
            sound.OnSoundStarted(s);
            return s;
        }

        public static void PlayRecordedSound(AudioSound sound)
        {
            sound.Play();
        }

        public void Play(double? time = null)
        {
            if (_fileReader != null)
            {
                _fileReader.CurrentTime = TimeSpan.FromSeconds(time.HasValue ? time.Value : _startPos);
            }
            try
            {
                _out.Volume = Volume;

            }
            catch (MmException e)
            {
                Forms.MessageBox.Show($"An error occured trying to set the output volume.\n" +
                    $"This might be a problem with the audio driver and could have occured if the audio configuration changed.\n" +
                    $"Try restarting the app.\n" +
                    $"Exception message: {e}", "Error setting volume");
            }
            try
            {
                _out.Play();
                _timer.Start();
                Sounds.Add(this);
            }
            catch (NullReferenceException)
            {
                Forms.MessageBox.Show("A problem occured with Windows waveOut api. You may need to restart your device if this issue persists.", "WaveOut api error");
            }
        }

        private void StopProcedure()
        {
            _stop();
            Sounds.Remove(this);
        }

        public void Stop()
        {
            StopProcedure();
            OnStopMethodCalled();
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
                sound.Dispose();
            }
            Sounds.Clear();
        }

        public void Dispose()
        {
            _stop();
            _out.Dispose();
            if (_fileReader != null) _fileReader.Dispose();
            _timer.Dispose();
            SoundDisposedEventHandler handler = Disposed;
            Disposed?.Invoke(this);
        }
    }
}
