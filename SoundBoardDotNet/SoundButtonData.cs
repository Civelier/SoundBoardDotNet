using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrrKlang;
using NAudio;
using NAudio.Wave;

namespace SoundBoardDotNet
{
    public class SoundButtonData
    {
        public string Name = "";
        public WaveStream Sound;
        public string FilePath = "";
        public float Volume = 1;
        public double Slider1 = 0, Slider2 = 100;

        private static List<List<string>> _decode(string encodedString)
        {
            var output = new List<List<string>>();
            var decodedList = new List<string>(encodedString.Split(','));
            
            foreach(var v in decodedList)
            {
                output.Add(new List<string>(v.Split(':')));
            }
            return output;
        }

        public SoundButtonData(string encodedData)
        {
            Sound = null;
            var decodedData = _decode(encodedData);

            foreach(var v in decodedData)
            {
                switch (v[0])
                {
                    case "Name":
                        Name = v[1];
                        break;
                    case "FilePath":
                        FilePath = v[1];
                        break;
                    case "Volume":
                        Volume = float.Parse(v[1]);
                        break;
                    default:
                        break;
                }
            }
        }

        public SoundButtonData()
        {
            Name = "";
            Sound = null;
            FilePath = "";
            Volume = 1;
            Slider1 = 0;
            Slider2 = 100;
        }
    }
}
