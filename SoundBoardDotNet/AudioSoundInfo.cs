using NAudio.Wave;
using System;
using SoundBoardDotNet.SoundEvents;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    public class AudioSoundInfo : INotifyPropertyChanged, IDisposable
    {
        private double _startPos;
        private double _endPos;
        private float _volume;

        public double StartPos 
        {
            get => _startPos;
            set
            {
                if (_startPos != value)
                {
                    _startPos = value;
                    OnPropertyChanged("StartPos");
                }
            }
        }
        public double EndPos 
        {
            get => _endPos;
            set
            {
                if (_endPos != value)
                {
                    _endPos = value;
                    OnPropertyChanged("EndPos");
                }
            }
        }
        public float Volume 
        {
            get => _volume;
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    OnPropertyChanged("Volume");
                }
            }
        }
        public readonly double TotalSeconds;
        public double SelectedLength => EndPos - StartPos;

        private readonly IWaveProvider _wave;
        private readonly WaveStream _waveStream;

        public event PropertyChangedEventHandler PropertyChanged;
        public event SoundStartedOnAnotherInstanceEventHandler SoundInstanceStarted;
        public event SoundInfoDisposedEventHandler Disposed;

        public WaveStream WaveStream => _waveStream;

        public bool CanHaveMultipleInstances => _canHaveMultipleInstances;

        private List<AudioSound> _instances;
        private bool _canHaveMultipleInstances;

        private delegate WaveStream RequestWaveStreamFunction();
        private RequestWaveStreamFunction _waveStreamRequest;

        public AudioSoundInfo(AudioRecorder recorder, double startPos, double endPos, float volume)
        {
            recorder.GetWaveStream();
            _wave = recorder.GetWaveProvider();
            _waveStream = recorder.GetWaveStream();
            
            _startPos = startPos;
            _endPos = endPos;
            _volume = volume;
            TotalSeconds = _waveStream.Length;
            _instances = new List<AudioSound>();
            _canHaveMultipleInstances = false;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ISampleProvider GetSample()
        {
            return _wave.ToSampleProvider().Skip(TimeSpan.FromSeconds(StartPos)).Take(TimeSpan.FromSeconds(SelectedLength));
        }

        private void OnSoundStarted(AudioSound newSound)
        {
            SoundStartedOnAnotherInstanceEventHandler handler = SoundInstanceStarted;
            handler?.Invoke(this, newSound);
        }

        private WaveStream RequestNewWaveStream()
        {
            if (!_canHaveMultipleInstances) throw new Exception("Only one instance of stream can be generated!\nUse the \"WaveStream\" property instead.");
            else
            {
                return _waveStreamRequest();
            }
        }

        public AudioSound GetAudioSound(bool disposeAtEnd = true)
        {
            if (!_canHaveMultipleInstances && _instances.Count > 0)
            {
                throw new Exception("Only one instance of stream can be generated! Use \"GetActiveAudioSound\" instead.");
            }
            var s = new AudioSound(GetSample(), RequestNewWaveStream(), StartPos, EndPos, Volume, disposeAtEnd);
            s.Started += S_Started;
            s.Disposed += S_Disposed;
            _instances.Add(s);
            return s;
        }

        public AudioSound GetActiveAudioSound(bool disposeAtEnd = true)
        {
            if (_canHaveMultipleInstances) throw new Exception("More than one instance can be created.\nUse this only when only one instance is possible.");
            if (_instances.Count == 1) return _instances[0];
            var s = new AudioSound(GetSample(), WaveStream, StartPos, EndPos, Volume, disposeAtEnd);
            s.Started += S_Started;
            s.Disposed += S_Disposed;
            _instances.Add(s);
            return s;
        }

        private void S_Disposed(AudioSound sender)
        {
            _instances.Remove(sender);
            sender.Started -= S_Started;
            sender.Disposed -= S_Disposed;
        }

        private void S_Started(AudioSound sender)
        {
            OnSoundStarted(sender);
        }

        private void OnDisposed()
        {
            SoundInfoDisposedEventHandler handler = Disposed;
            Disposed?.Invoke(this);
        }

        public void StopAllInstances()
        {
            foreach (var sound in _instances)
            {
                sound.Stop();
            }
        }

        public void Dispose()
        {
            foreach (var sound in _instances)
            {
                sound.Dispose();
            }
            _waveStream.Dispose();
            OnDisposed();
        }
    }
}
