using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using IrrKlang;
using System.Diagnostics;

namespace SoundBoardDotNet
{
    class AudioSound
    {
        public static ISoundEngine Engine;
        public static List<AudioSound> Sounds = new List<AudioSound>();

        private static double _time = 0;
        private static Thread _main = new Thread(_run);
        private static bool _canWrite;
        private static bool _interupt = false;

        public string FileName;
        public uint StartPos, EndPos;
        public float Volume
        {
            get
            {
                if (Sound == null) return -1;
                return Sound.Volume;
            }
            set
            {
                if (Sound == null) return;
                Sound.Volume = value;
            }
        }
        public ISound Sound;

        private uint _stopPos = uint.MaxValue;

        public AudioSound(string fileName, uint startPos, uint endPos, float volume, bool loop = false)
        {
            FileName = fileName;
            StartPos = startPos;
            EndPos = endPos;
            Volume = volume;
            Sound = Engine.Play2D(fileName, loop, true);
        }

        private AudioSound(string fileName, float volume, bool loop = false)
        {
            FileName = fileName;
            Volume = volume;
            Sound = Engine.Play2D(fileName, loop, true);
        }

        public static void Run()
        {
            _main.Start();
        }

        public static void StopMain()
        {
            _interupt = true;
        }

        private static void _run()
        {
            List<AudioSound> sounds;
            DateTime time;
            while (!_interupt)
            {
                time = DateTime.Now;
                _canWrite = false;
                sounds = new List<AudioSound>(Sounds);
                _canWrite = true;
                foreach (var sound in sounds)
                {
                    if (sound._stopPos <= (uint)_time)
                    {
                        _canWrite = false;
                        sound.Stop();
                        _canWrite = true;
                    }
                }
                _time += (DateTime.Now - time).TotalMilliseconds;
            }
        }

        public static AudioSound AddSound(string fileName, uint startPos, uint endPos, float volume, bool startPaused = false, bool loop = false)
        {
            Engine.RemoveSoundSource(fileName);
            var source = Engine.AddSoundSourceFromFile(fileName, StreamMode.NoStreaming, false);
            var sound = new AudioSound(fileName, startPos, endPos, volume, loop);
            if (sound.Sound == null) return null;
            while (!_canWrite) ;
            Sounds.Add(sound);
            sound.Sound.PlayPosition = startPos;
            if (!startPaused)
            {
                sound.Play();
            }

            return sound;
        }

        public static AudioSound AddSound(string fileName, double startPercent, double endPercent, float volume, bool startPaused = false, bool loop = false)
        {
            Engine.RemoveSoundSource(fileName);
            var source = Engine.AddSoundSourceFromFile(fileName, StreamMode.NoStreaming, false);
            var sound = new AudioSound(fileName, volume, loop);
            sound.StartPos = sound._percentToTime(startPercent);
            sound.EndPos = sound._percentToTime(endPercent);
            if (sound.Sound == null) return null;
            while (!_canWrite) ;
            Sounds.Add(sound);
            sound.Sound.PlayPosition = sound.StartPos;
            if (!startPaused)
            {
                sound.Play();
            }

            return sound;
        }


        public void Play()
        {
            Sound.Paused = false;
            _stopPos = EndPos + (uint)_time;
        }

        public void Pause()
        {
            EndPos = (uint)_time - _stopPos;
            _stopPos = uint.MaxValue;
            Sound.Paused = true;
        }

        public void Stop()
        {
            Sound.Stop();
            Sounds.Remove(this);
        }

        private uint _percentToTime(double percent)
        {
            if (Sound == null) return uint.MaxValue;
            var output = Convert.ToUInt32(Math.Round(percent * Sound.PlayLength / 100, 0));
            return output;
        }
    }
}
