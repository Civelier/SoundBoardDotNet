using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    public struct AudioSoundInfo
    {
        public double StartPos { get; set; }
        public double EndPos { get; set; }
        public double Volume { get; set; }
        public readonly double TotalSeconds;
        public double SelectedLength => EndPos - StartPos;

        private readonly IWaveProvider _wave;
        private readonly WaveStream _waveStream;
        public WaveStream WaveStream => _waveStream;

        public AudioSoundInfo(AudioRecorder recorder, double startPos, double endPos, double volume)
        {
            _wave = recorder.GetWaveProvider();
            _waveStream = recorder.GetWaveStream();
            
            StartPos = startPos;
            EndPos = endPos;
            Volume = volume;
            TotalSeconds = _waveStream.Length;
        }

        public ISampleProvider GetSample()
        {
            return _wave.ToSampleProvider().Skip(TimeSpan.FromSeconds(StartPos)).Take(TimeSpan.FromSeconds(SelectedLength));
        }
    }
}
