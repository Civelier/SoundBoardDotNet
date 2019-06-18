using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    [Serializable()]
    class SoundBoardData : ISerializable
    {
        private static IFormatter _formater = new BinaryFormatter();

        public List<SoundButtonData> Data = new List<SoundButtonData>();
        public SoundBoardData()
        {

        }

        private static SoundBoardData _allData;
        public static SoundBoardData AllData
        {
            get
            {
                if (_allData == null) _allData = new SoundBoardData();
                return _allData;
            }
        }

        public static void Save(string filePath)
        {
            FileStream stream;
            while (true)
            {
                try
                {
                    stream = new FileStream(filePath, FileMode.Create);
                    break;
                }
                catch (IOException)
                {

                }
            }
            _formater.Serialize(stream, AllData);
            stream.Close();
        }

        public static void Load(string filePath)
        {
            FileStream stream = new FileStream(filePath, FileMode.Open);
            _allData = (SoundBoardData)_formater.Deserialize(stream);
        }

        public static DirectoryInfo GetDefaultSaveDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SoundBoardDotNet Projects");
            if (!dir.Exists)
            {
                dir.Create();
            }
            return dir;
        }

        private static DirectoryInfo GetPropDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SoundBoardDotNet");
            if (!dir.Exists)
            {
                dir.Create();
            }
            return dir;
        }

        public static void LoadProperties()
        {
            var dir = GetPropDirectory();
            var files = dir.GetFiles();
            FileInfo propertyFile = null;
            foreach (var file in files)
            {
                if (file.Name == "SoundBoardProperties.txt")
                {
                    propertyFile = file;
                }
            }

            if (propertyFile == null)
            {
                SaveProperties();
                return;
            }
            FileStream stream = new FileStream(propertyFile.FullName, FileMode.Open);
            SoundBoardProperties.Props = (SoundBoardProperties)_formater.Deserialize(stream);
        }

        public static void SaveProperties()
        {
            var dir = GetPropDirectory();
            var file = new FileInfo(dir.FullName + "//SoundBoardProperties.txt");
            FileStream stream = new FileStream(file.FullName, FileMode.Create);
            _formater.Serialize(stream, SoundBoardProperties.Props);
        }

        public SoundBoardData(SerializationInfo info, StreamingContext context)
        {
            Data.Clear();
            Data = (List<SoundButtonData>)info.GetValue("Data", typeof(List<SoundButtonData>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Data.Clear();
            foreach (var btn in Form1.Buttons)
            {
                Data.Add(btn.Data);
            }
            info.AddValue("Data", Data);
        }
    }
}
