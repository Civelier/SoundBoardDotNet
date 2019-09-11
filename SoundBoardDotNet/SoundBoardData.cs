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
        private static string _devicesFileName = "SoundBoardDevices.txt";
        private static string _preferencesFileName = "SoundBoardPreferences.txt";

        public static FileVersion DevicesFileVersion = new FileVersion(1, 1);
        public static FileVersion PreferencesFileVersion = new FileVersion(1, 1);
        public static FileVersion ProjectFileVersion = new FileVersion(1, 1);

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
            DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SoundBoardDotNet");
            if (!dir.Exists)
            {
                dir.Create();
            }
            dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SoundBoardDotNet\\Projects");
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
            FileInfo preferencesFile = null, devicesFile = null;
            foreach (var file in files)
            {
                if (file.Name == _preferencesFileName)
                {
                    preferencesFile = file;
                }
                if (file.Name == _devicesFileName)
                {
                    devicesFile = file;
                }
            }

            if (preferencesFile == null)
            {
                SavePreferences();
            }
            if (devicesFile == null)
            {
                SaveDevices();
            }

            FileStream stream = preferencesFile.Open(FileMode.Open);
            try
            {
                SoundBoardProperties.Props = (SoundBoardProperties)_formater.Deserialize(stream);
                stream.Close();
            }
            catch (SerializationException x)
            {
                stream.Close();
                preferencesFile.Delete();
                SavePreferences();
            }
            stream = devicesFile.Open(FileMode.Open);
            try
            {
                Devices.DevicesInfo = (Devices)_formater.Deserialize(stream);
                stream.Close();
            }
            catch (SerializationException x)
            {
                stream.Close();
                devicesFile.Delete();
                SaveDevices();
            }
        }

        public static void SaveDevices()
        {
            var dir = GetPropDirectory();
            var file = new FileInfo(dir.FullName + $"//{_devicesFileName}");
            FileStream stream = new FileStream(file.FullName, FileMode.Create);
            _formater.Serialize(stream, Devices.DevicesInfo);
            stream.Close();
        }

        public static void SavePreferences()
        {
            var dir = GetPropDirectory();
            var file = new FileInfo(dir.FullName + $"//{_preferencesFileName}");
            FileStream stream = new FileStream(file.FullName, FileMode.Create);
            _formater.Serialize(stream, SoundBoardProperties.Props);
            stream.Close();
        }

        public SoundBoardData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                Data.Clear();
                Data = (List<SoundButtonData>)info.GetValue("Data", typeof(List<SoundButtonData>));
                if (ProjectFileVersion.Upgrade((FileVersion)info.GetValue("Version", typeof(FileVersion))))
                {
                    System.Windows.Forms.MessageBox.Show("Wrong version!", "Version", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    throw new Exception("Wrong version!");
                }
            }
            catch (SerializationException)
            {
                throw new Exception("File corrupted or incompatible!");
            }
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Data.Clear();
            foreach (var btn in Form1.Buttons)
            {
                Data.Add(btn.Data);
            }
            info.AddValue("Data", Data);
            info.AddValue("Version", ProjectFileVersion);
        }
    }
}
