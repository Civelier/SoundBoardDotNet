using System;
using NAudio.Wave;
using System.Diagnostics;
using System.IO;

namespace SoundBoardDotNet
{
    public class AudioRecorder
    {
        public WaveIn MyWaveIn;
        public double RecordTime => SoundBoardProperties.Props.RecordingSampleTime;
        public double RecordedTime => _isFull ? RecordTime : _pos == 0 ? 0 : 1.0 / MyWaveIn.WaveFormat.AverageBytesPerSecond * _pos;
        public int Device = 0;

        public WaveOutEvent Sound = new WaveOutEvent();
        private bool _isFull = false;
        private int _pos = 0;
        private byte[] _buffer;
        private bool _isRecording = false;
        private int _deviceIndex;

        private WaveStream _waveStream;

        /// <summary>
        /// Creates a new recorder with a buffer
        /// </summary>
        /// <param name="recordTime">Time to keep in buffer (in seconds)</param>
        public AudioRecorder(int deviceIndex = 0)
        {
            Device = deviceIndex;
            _deviceIndex = deviceIndex;
            SetupWaveIn();
            //MyWaveIn = new WaveInEvent();
            MyWaveIn.DataAvailable += DataAvailable;
            MyWaveIn.RecordingStopped += Stopped;
            _buffer = new byte[(int)(MyWaveIn.WaveFormat.AverageBytesPerSecond * RecordTime)];
        }

        private void SetupWaveIn()
        {
            //var cb = WaveCallbackInfo.ExistingWindow(Form1.MyForm.Handle);
            var cb = WaveCallbackInfo.FunctionCallback();
            MyWaveIn = new WaveIn(cb);
        }

        /// <summary>
        /// Starts recording
        /// </summary>
        public void StartRecording()
        {
            if (!_isRecording)
            {
                try
                {
                    MyWaveIn.StartRecording();
                }
                catch (InvalidOperationException)
                {
                    Debug.WriteLine("Already recording!");
                }
            }
            
            _isRecording = true;
        }

        public WaveStream GetWaveStream()
        {
            if (_waveStream == null)
            {
                var b = GetBytesToSave();
                _waveStream = new RawSourceWaveStream(b, 0, b.Length, MyWaveIn.WaveFormat);
            }
            return _waveStream;
        }

        public void CloseWaveStream()
        {
            _waveStream?.Close();
            _waveStream?.Dispose();
            _waveStream = null;
        }
        
        public void Reset()
        {
            MyWaveIn.StopRecording();
            StopReplay();

            _buffer = _buffer = new byte[(int)(MyWaveIn.WaveFormat.AverageBytesPerSecond * RecordTime)];
            _isFull = false;
            _isRecording = false;
            _pos = 0;
            MyWaveIn.Dispose();
            SetupWaveIn();
            //MyWaveIn = new WaveInEvent();
            //MyWaveIn.BufferMilliseconds = 200;
            MyWaveIn.DeviceNumber = Device;
            MyWaveIn.DataAvailable += DataAvailable;
            MyWaveIn.RecordingStopped += Stopped;
            MyWaveIn.DeviceNumber = _deviceIndex;
        }

        public IWaveProvider GetWaveProvider()
        {
            var buff = new BufferedWaveProvider(MyWaveIn.WaveFormat);
            buff.BufferDuration = TimeSpan.FromSeconds(RecordTime);
            var bytes = GetBytesToSave();
            buff.AddSamples(bytes, 0, bytes.Length);
            return buff;
        }

        /// <summary>
        /// Stops recording
        /// </summary>
        public void StopRecording()
        {
            _isRecording = false;
            MyWaveIn.StopRecording();
        }

        /// <summary>
        /// Play currently recorded data
        /// </summary>
        public void PlayRecorded() 
        {
            if (Sound.PlaybackState == PlaybackState.Stopped)
            {
                Sound.Init(GetWaveProvider());
                Sound.Play();
            }
        }

        /// <summary>
        /// Stops replay
        /// </summary>
        public void StopReplay()
        {
            if (Sound != null) Sound.Stop();
        }

        /// <summary>
        /// Save to disk
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            var writer = new WaveFileWriter(fileName, MyWaveIn.WaveFormat);
            var buff = GetBytesToSave();
            writer.Write(buff, 0, buff.Length);
            writer.Close();
        }


        private void DataAvailable(object sender, WaveInEventArgs e)
        {
            Console.WriteLine($"Buffer length : {e.BytesRecorded}");
            for (int i = 0; i < e.BytesRecorded; ++i)
            {
                // save the data
                _buffer[_pos] = e.Buffer[i];
                // move the current position (advances by 1 OR resets to zero if the length of the buffer was reached)
                _pos = (_pos + 1) % _buffer.Length;
                // flag if the buffer is full (will only set it from false to true the first time that it reaches the full length of the buffer)
                _isFull |= (_pos == 0);
            }
        }

        public byte[] GetBytesToSave()
        {
            int length = _isFull ? _buffer.Length : _pos;
            var bytesToSave = new byte[length];
            int byteCountToEnd = _isFull ? (_buffer.Length - _pos) : 0;
            if (byteCountToEnd > 0)
            {
                // bytes from the current position to the end
                Array.Copy(_buffer, _pos, bytesToSave, 0, byteCountToEnd);
            }
            if (_pos > 0)
            {
                // bytes from the start to the current position
                Array.Copy(_buffer, 0, bytesToSave, byteCountToEnd, _pos);
            }
            return bytesToSave;
        }

        /// <summary>
        /// Starts recording if WaveIn stopped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stopped(object sender, StoppedEventArgs e)
        {
            Debug.WriteLine("Recording stopped!");
            if (e.Exception != null) Debug.WriteLine(e.Exception.Message);
            if (_isRecording)
            {
                try
                {
                    MyWaveIn.StartRecording();
                }
                catch (InvalidOperationException)
                {

                }
            }
        }
    }
}
