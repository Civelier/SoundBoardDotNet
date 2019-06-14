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

namespace SoundBoardDotNet
{
    public class AudioSound
    {
        public static List<AudioSound> Sounds = new List<AudioSound>();
        private AudioFileReader _fileReader;
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
                    _timer.Interval = 0.001;
                }
            }
        }

        public float Volume;
        public AudioFileReader FileReader => _fileReader;

        private Timer _timer;

        public AudioSound(string fileName, double startPos, double endPos, float volume, bool loop = false)
        {
            FileName = fileName;
            _startPos = startPos;
            _endPos = endPos;
            Volume = volume;
            try { _timer = new Timer((_endPos - _startPos) * 1000); }
            catch (ArgumentException)
            {
                _timer = new Timer(0.001);
            }
            _timer.Elapsed += new ElapsedEventHandler(_stop);
            _timer.AutoReset = false;
            _fileReader = new AudioFileReader(fileName);
            _out = new WaveOut();
            _out.Init(_fileReader);
        }

        private AudioSound(AudioSound sound) : this(sound.FileName, sound._startPos, sound._endPos, sound.Volume) { }

        public void _stop(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine(e.SignalTime.ToString() + "  stop raised");
            Stop();
        }

        public static void PlaySound(AudioSound sound)
        {
            if (Sounds.Contains(sound)) new AudioSound(sound).Play();
            else sound.Play();
        }

        public void Play()
        {
            _fileReader.Volume = Volume;
            _fileReader.CurrentTime = TimeSpan.FromSeconds(_startPos);
            _out.Play();
            _timer.Start();
            Sounds.Add(this);
        }

        public void Stop()
        {
            _out.Stop();
            _timer.Stop();
            Sounds.Remove(this);
        }

        public static void StopAll()
        {
            foreach (var sound in Sounds)
            {
                sound._out.Stop();
                sound._timer.Stop();
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
