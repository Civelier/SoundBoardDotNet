using System;
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
        public string FilePath
        {
            get => SoundInfo?.FileName ?? "";
            set
            {
                if (FilePath != (SoundInfo?.FileName ?? ""))
                {
                    SoundInfo.Dispose();
                    if ((value ?? "") == "") SoundInfo = null;
                    else SoundInfo = new AudioSoundInfo(value, 0, 0, 1);
                }
            }
        }
        public float Volume 
        { 
            get => SoundInfo?.Volume ?? 1;
            set
            {
                if (SoundInfo != null) SoundInfo.Volume = value;
            }
        }
        public double StartTime 
        { 
            get => SoundInfo?.StartPos ?? 0;
            set
            {
                if (SoundInfo != null) SoundInfo.StartPos = value;
            }
        }
        public double EndTime
        {
            get => SoundInfo?.EndPos ?? 0;
            set
            {
                if (SoundInfo != null) SoundInfo.EndPos = value;
            }
        }

        public AudioSoundInfo SoundInfo { get; private set; }

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
