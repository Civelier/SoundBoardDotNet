using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using System.Diagnostics;
using NAudio.Utils;
using System.Timers;

namespace SoundBoardDotNet
{
    public class AudioRecorder
    {
        public WaveInProvider Provider;
        public WaveInEvent MyWaveIn;
        public double RecordTime;

        private WaveOutEvent _wav = new WaveOutEvent();
        private bool _isFull = false;
        private int _pos = 0;
        private byte[] _buffer;
        private bool _isRecording = false;

        /// <summary>
        /// Creates a new recorder with a buffer
        /// </summary>
        /// <param name="recordTime">Time to keep in buffer (in seconds)</param>
        public AudioRecorder(double recordTime)
        {
            RecordTime = recordTime;
            MyWaveIn = new WaveInEvent();
            MyWaveIn.DataAvailable += DataAvailable;
            _buffer = new byte[(int)(MyWaveIn.WaveFormat.AverageBytesPerSecond * RecordTime)];
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

        /// <summary>
        /// Stops recording
        /// </summary>
        public void StopRecording()
        {
            MyWaveIn.StopRecording();
            _isRecording = false;
        }

        /// <summary>
        /// Play currently recorded data
        /// </summary>
        public void PlayRecorded() 
        {
            if (_wav.PlaybackState == PlaybackState.Stopped)
            {
                var buff = new BufferedWaveProvider(MyWaveIn.WaveFormat);
                var bytes = GetBytesToSave();
                buff.AddSamples(bytes, 0, bytes.Length);
                _wav.Init(buff);
                _wav.Play();
            }
            
        }

        /// <summary>
        /// Stops replay
        /// </summary>
        public void StopReplay()
        {
            if (_wav != null) _wav.Stop();
        }

        /// <summary>
        /// Save to disk
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            var writer = new WaveFileWriter(fileName, MyWaveIn.WaveFormat);
            writer.Flush();
        }


        private void DataAvailable(object sender, WaveInEventArgs e)
        {

            for (int i = 0; i < e.BytesRecorded; ++i)
            {
                // save the data
                _buffer[_pos] = e.Buffer[i];
                // move the current position (advances by 1 OR resets to zero if the length of the buffer was reached)
                _pos = (_pos + 1) % _buffer.Length;
                // flag if the buffer is full (will only set it from false to true the first time that it reaches the full length of the buffer)
                _isFull |= (_pos == 0);
            }
            //var val = Provider.Read(buff, _end, e.BytesRecorded);

            //for (int i = 0; i < e.BytesRecorded; i++)
            //{
            //    if (_filled)
            //    {
            //        _start = _end + 1 > _bwp.BufferLength - 1 ? _end + 1 : 0;
            //    }
            //    if (_end > _buffer.Length - 1 && !_filled) _filled = true;
            //    _end = _end > _buffer.Length - 1 ? _end + 1 : 0;

            //    _buffer[_end] = e.Buffer[i];
            //}


            //var temp = new BufferedWaveProvider(MyWaveIn.WaveFormat);
            //temp.BufferDuration = TimeSpan.FromSeconds(temp.WaveFormat.AverageBytesPerSecond / e.BytesRecorded);
            //temp.AddSamples(e.Buffer, 0, e.BytesRecorded);
            //_sampleProviders.Enqueue(temp.ToSampleProvider());
            //if (_sampleProviders.Count > RecordTime / temp.BufferedDuration.TotalSeconds)
            //{
            //    _sampleProviders.Dequeue();
            //}
            //_datasRecieved++;
            //if (_datasRecieved / 5 > RecordTime / temp.BufferedDuration.TotalSeconds)
            //{
            //    _datasRecieved = 0;
            //    MyWaveIn.StopRecording();
            //}

            //int remainingSamples = e.BytesRecorded;
            //if (_bufferedWaveProvider1.BufferedBytes + e.BytesRecorded >= _bufferedWaveProvider1.BufferLength)
            //{
            //    _bufferedWaveProvider2.ClearBuffer();
            //    _bwp = _bufferedWaveProvider2;
            //    _bufferedWaveProvider1.AddSamples(buff, 0, _bufferedWaveProvider1.BufferLength - _bufferedWaveProvider1.BufferedBytes);
            //    remainingSamples -= _bufferedWaveProvider1.BufferLength - _bufferedWaveProvider1.BufferedBytes;
            //}
            //if (_bufferedWaveProvider2.BufferedBytes + e.BytesRecorded >= _bufferedWaveProvider2.BufferLength)
            //{
            //    _bufferedWaveProvider1.ClearBuffer();
            //    _bwp = _bufferedWaveProvider1;
            //    _bufferedWaveProvider2.AddSamples(buff, 0, _bufferedWaveProvider2.BufferLength - _bufferedWaveProvider2.BufferedBytes);
            //    remainingSamples -= _bufferedWaveProvider2.BufferLength - _bufferedWaveProvider2.BufferedBytes;
            //}

            //_bwp.AddSamples(buff, e.BytesRecorded - remainingSamples, remainingSamples);

            //_bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
            //_offset += e.BytesRecorded;
            //_offset = _offset > _buffer.Length - 1 ? _offset - (_buffer.Length - 1) : _offset;
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

        private void Stopped(object sender, StoppedEventArgs e)
        {
            Debug.WriteLine("Recording stopped!");
            if (e.Exception != null) Debug.WriteLine(e.Exception.Message);
            if (_isRecording)
            {
                MyWaveIn.StartRecording();
            }
        }
    }
}
