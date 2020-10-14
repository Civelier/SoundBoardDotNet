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
        public bool IsDisposed { get; private set; }
        private double _startPos;
        private double _endPos;
        private float _volume;

        public double StartPos
        {
            get
            {
                CheckDisposed();
                return _startPos;
            }
            set
            {
                CheckDisposed();
                if (_startPos != value)
                {
                    _startPos = value;
                    OnPropertyChanged("StartPos");
                }
            }
        }
        public double EndPos
        {
            get
            {
                CheckDisposed();
                return _endPos;
            }
            set
            {
                CheckDisposed();
                if (_endPos != value)
                {
                    _endPos = value;
                    OnPropertyChanged("EndPos");
                }
            }
        }
        public float Volume
        {
            get
            {
                CheckDisposed();
                return _volume;
            }
            set
            {
                CheckDisposed();
                if (_volume != value)
                {
                    _volume = value;
                    foreach (var sound in _instances)
                    {
                        sound.Volume = value;
                    }
                    OnPropertyChanged("Volume");
                }
            }
        }
        public double TotalSeconds
        {
            get
            {
                CheckDisposed();
                return _totalSeconds;
            }
        }
        public double SelectedLength
        {
            get
            {
                CheckDisposed();
                return EndPos - StartPos;
            }
        }

        private readonly IWaveProvider _wave;
        private readonly WaveStream _waveStream;

        public event PropertyChangedEventHandler PropertyChanged;
        public event SoundStartedOnAnotherInstanceEventHandler SoundInstanceStarted;
        public event SoundInfoDisposedEventHandler Disposed;

        public WaveStream WaveStream
        {
            get
            {
                CheckDisposed();
                return _waveStream;
            }
        }

        public bool CanHaveMultipleInstances
        {
            get
            {
                CheckDisposed();
                return _canHaveMultipleInstances;
            }
        }

        private List<AudioSound> _instances;
        private bool _canHaveMultipleInstances;

        private delegate WaveStream RequestWaveStreamFunction();
        private RequestWaveStreamFunction _waveStreamRequest;
        private double _totalSeconds;
        private string _fileName;

        public string FileName
        {
            get
            {
                CheckDisposed();
                return _fileName;
            }
        }

        public AudioSoundInfo(AudioRecorder recorder, double startPos, double endPos, float volume)
        {
            IsDisposed = false;
            _wave = recorder.GetWaveProvider();
            _waveStream = recorder.GetWaveStream();
            
            _startPos = startPos;
            _endPos = endPos;
            _volume = volume;
            _totalSeconds = _waveStream.TotalTime.TotalSeconds;
            _instances = new List<AudioSound>();
            _canHaveMultipleInstances = false;
        }

        public AudioSoundInfo(string fileName, double startPos, double endPos, float volume)
        {
            IsDisposed = false;
            var afr = new AudioFileReader(fileName);
            _waveStream = afr;
            _wave = afr.ToWaveProvider();

            _startPos = startPos;
            _endPos = endPos;
            _volume = volume;
            _totalSeconds = _waveStream.TotalTime.TotalSeconds;
            _instances = new List<AudioSound>();
            _canHaveMultipleInstances = true;

            _waveStreamRequest = () => new AudioFileReader(fileName);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ISampleProvider GetSample()
        {
            CheckDisposed();
            return _wave.ToSampleProvider().Skip(TimeSpan.FromSeconds(StartPos)).Take(TimeSpan.FromSeconds(SelectedLength));
        }

        private void CheckDisposed()
        {
            if (IsDisposed) throw new ObjectDisposedException("AudioSoundInfo");
        }

        private void OnSoundStarted(AudioSound newSound)
        {
            SoundStartedOnAnotherInstanceEventHandler handler = SoundInstanceStarted;
            handler?.Invoke(this, newSound);
        }

        private WaveStream RequestNewWaveStream()
        {
            CheckDisposed();
            if (!_canHaveMultipleInstances) throw new Exception("Only one instance of stream can be generated!\nUse the \"WaveStream\" property instead.");
            else
            {
                return _waveStreamRequest();
            }
        }

        public AudioSound GetAudioSound(bool disposeAtEnd = true)
        {
            CheckDisposed();
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
            CheckDisposed();
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
            CheckDisposed();
            foreach (var sound in _instances)
            {
                sound.Stop();
            }
            if (_instances.Count > 0)
            {
                Console.WriteLine($"Stopped instances but {_instances.Count} haven\'t been disposed!");
            }
        }

        public void DisposeInstances()
        {
            CheckDisposed();
            while (_instances.Count > 0)
            {
                _instances.First().Dispose();
            }
            _instances.Clear();
        }

        public void Dispose()
        {
            CheckDisposed();
            while (_instances.Count > 0)
            {
                _instances.First().Dispose();
            }
            _waveStream.Dispose();
            _waveStreamRequest = null;
            _instances = null;
            IsDisposed = true;
            OnDisposed();
            Disposed = null;
            SoundInstanceStarted = null;
            PropertyChanged = null;
        }
    }
}
