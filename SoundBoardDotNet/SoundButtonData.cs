﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrrKlang;
using NAudio;
using NAudio.Wave;
using System.Runtime.Serialization;

namespace SoundBoardDotNet
{
    [Serializable()]
    public class SoundButtonData : ISerializable
    {
        public int Index = 0;
        public string Name = "";
        public string FilePath = "";
        public float Volume = 1;
        public double StartTime = 0, EndTime = 0;

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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("FilePath", FilePath);
            info.AddValue("Volume", Volume);
            info.AddValue("StartTime", StartTime);
            info.AddValue("EndTime", EndTime);
            info.AddValue("Index", Index);
        }

        public SoundButtonData(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            FilePath = info.GetString("FilePath");
            Volume = (float)info.GetDouble("Volume");
            StartTime = info.GetDouble("StartTime");
            EndTime = info.GetDouble("EndTime");
            Index = info.GetInt32("Index");
        }

        public SoundButtonData(string encodedData)
        {
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

        public void Reset()
        {
            Name = "";
            FilePath = "";
            Volume = 1;
            StartTime = 0;
            EndTime = 0;
        }

        public SoundButtonData()
        {
            Index = 0;
            Name = "";
            FilePath = "";
            Volume = 1;
            StartTime = 0;
            EndTime = 0;
        }
    }
}
