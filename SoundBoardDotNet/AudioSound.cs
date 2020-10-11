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
        private WaveOut _out;

        private double _startPos, _endPos;

        public double StartPos => _startPos;

        public double Time
        {
            get
            {
                if (WaveStream == null) return 0;
                return WaveStream.CurrentTime.TotalSeconds;
            }
            set
            {
                if (WaveStream != null)
                {
                    if (_endPos - value > 0)
                    {
                        if (IsPlaying)
                        {
                            Stop();
                        }
                        if (!IsPlaying)
                        {
                            Play(value);
                        }
                    }
                }
            }
        }

        public double EndPos => _endPos;

        public bool IsPlaying => _out.PlaybackState == PlaybackState.Playing;

        public float Volume
        {
            get { return _out.Volume; }
            set
            {
                _out.Volume = value;
            }
        }

        private bool _disposeAtEnd;
        private WaveStream _waveStream;

        public WaveStream WaveStream => _waveStream;

        public event SoundStoppedEventHandler Stopped;
        public event SoundDisposedEventHandler Disposed;
        public event SoundStartedEventHandler Started;

        //public AudioSound(string fileName, double startPos, double endPos, float volume, bool disposeAtEnd = true, bool loop = false)
        //{
        //    if (_out == null) _out = new WaveOut();
        //    _out.DeviceNumber = OutputDevice.MainOutput.Index;
        //    FileName = fileName;
        //    _startPos = startPos;
        //    _endPos = endPos;
        //    Volume = volume;

        //    if (FileName == null)
        //    {
        //        if (_buffWaveProvider == null) return;
        //        _out.Init(_buffWaveProvider);
        //        return;
        //    }

        //    while (true)
        //    {
        //        if (new FileInfo(FileName).Exists)
        //        {
        //            _fileReader = new AudioFileReader(FileName);
        //            break;
        //        }
        //        else
        //        {
        //            var result = Forms.MessageBox.Show($"File {FileName} does not exist.\nDo you want to try to find the file?", "Error", Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Error);

        //            if (result == Forms.DialogResult.Yes)
        //            {
        //                var file = new Forms.OpenFileDialog();
        //                var fileInfo = new FileInfo(fileName);
        //                file.InitialDirectory = fileInfo.DirectoryName;
        //                file.CheckFileExists = true;
        //                var result2 = file.ShowDialog();
        //                if (result2 != Forms.DialogResult.Cancel)
        //                {
        //                    using (var r = new AudioFileReader(file.FileName))
        //                    if (r.TotalTime.TotalSeconds >= EndPos)
        //                    {
        //                        FileName = file.FileName;
        //                    }
        //                }
        //            }
        //            if (result == Forms.DialogResult.No)
        //            {
        //                break;
        //            }
        //        }
        //    }

        //    if (_fileReader != null)
        //    {
        //        try
        //        {
        //            _out.Init(_fileReader);
        //        }
        //        catch (MmException e)
        //        {
        //            Debug.WriteLine(e.Message);
        //        }
        //    }
        //}

        private AudioSound(IWaveProvider wave, double startPos, double endPos, float volume, bool disposeAtEnd)
        {
            _out = new WaveOut();
            _out.DeviceNumber = OutputDevice.MainOutput.Index;
            _startPos = startPos;
            _endPos = endPos;
            Volume = volume;

            _out.Init(wave);

            _out.PlaybackStopped += _out_PlaybackStopped;
        }

        private void _out_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            OnEndReached();
            if (_disposeAtEnd) Dispose();
        }

        private void PauseDefaultStopEvent()
        {
            _out.PlaybackStopped -= _out_PlaybackStopped;
        }

        private void ResumeDefaultStopEvent()
        {
            _out.PlaybackStopped += _out_PlaybackStopped;
        }

        public AudioSound(ISampleProvider sample, WaveStream waveStream, double startPos, double endPos, float volume, bool disposeAtEnd) : this(sample.ToWaveProvider(), startPos, endPos, volume, disposeAtEnd)
        {
            _waveStream = waveStream;
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

        private void OnSoundStarted()
        {
            SoundStartedEventHandler handler = Started;
            handler?.Invoke(this);
        }

        public static void PlayRecordedSound(AudioSound sound)
        {
            sound.Play();
        }

        public void Play(double? time = null)
        {
            if (WaveStream != null)
            {
                WaveStream.CurrentTime = TimeSpan.FromSeconds(time.HasValue ? time.Value : _startPos);
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
                OnSoundStarted();
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
            PauseDefaultStopEvent();
            StopProcedure();
            ResumeDefaultStopEvent();
            OnStopMethodCalled();
        }

        private void _stop()
        {
            _out.Stop();
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
            if (WaveStream != null) WaveStream.Dispose();
            SoundDisposedEventHandler handler = Disposed;
            Disposed?.Invoke(this);
        }
    }
}
