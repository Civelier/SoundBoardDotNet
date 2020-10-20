using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    [Serializable()]
    class SoundBoardData : ISerializable
    {
        private static IFormatter _formater = new BinaryFormatter();
        private static string _devicesFileName = "SoundBoardDevices.txt";
        private static string _preferencesFileName = "SoundBoardPreferences.txt";

        /// <summary>
        /// Version of the Devices file encoding.
        /// </summary>
        public static FileVersion DevicesFileVersion = new FileVersion(1, 1);
        /// <summary>
        /// Version of the preferences file encoding.
        /// </summary>
        public static FileVersion PreferencesFileVersion = new FileVersion(1, 1);
        /// <summary>
        /// Version of the project file encoding.
        /// </summary>
        public static FileVersion ProjectFileVersion = new FileVersion(1, 1);

        /// <summary>
        /// Contains the buttons data.
        /// </summary>
        public List<SoundButtonData> Data = new List<SoundButtonData>();
        public SoundBoardData()
        {

        }

        private static SoundBoardData _allData;

        /// <summary>
        /// Currently used soundboard data.
        /// </summary>
        public static SoundBoardData AllData
        {
            get
            {
                if (_allData == null) _allData = new SoundBoardData();
                return _allData;
            }
        }

        /// <summary>
        /// Saves the soundboard data to the specific file path.
        /// </summary>
        /// <param name="filePath">The path to save the .sbdn file.</param>
        public static void Save(string filePath)
        {
            while (true)
            {
                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        _formater.Serialize(stream, AllData);
                    }
                    break;
                }
                catch (IOException)
                {

                }
            }
        }

        /// <summary>
        /// Load the soundboard data from a specific file.
        /// </summary>
        /// <param name="filePath">The .sbdn path to load the project from.</param>
        public static void Load(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                _allData = (SoundBoardData)_formater.Deserialize(stream);
            }
        }

        /// <summary>
        /// Gets the default save directory for projects.
        /// If it doesn't exist, it creates it.
        /// It is located in MyDocuments\SoundBoardDotNet
        /// </summary>
        /// <returns>The default save directory for projects.</returns>
        public static DirectoryInfo GetDefaultSaveDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SoundBoardDotNet");
            if (!dir.Exists)
            {
                dir.Create();
            }
            dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\SoundBoardDotNet\Projects");
            if (!dir.Exists)
            {
                dir.Create();
            }
            return dir;
        }

        /// <summary>
        /// Gets the property directory.
        /// If it doesn't exist, it creates it.
        /// It is located in AppData\SoundBoardDotNet
        /// </summary>
        /// <returns>The property directory.</returns>
        private static DirectoryInfo GetPropDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SoundBoardDotNet");
            if (!dir.Exists)
            {
                dir.Create();
            }
            return dir;
        }

        /// <summary>
        /// Loads the properties for the soundboard or creates them if they don't exixt yet.
        /// </summary>
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
                files = dir.GetFiles();
                foreach (var file in files)
                {
                    if (file.Name == _preferencesFileName)
                    {
                        preferencesFile = file;
                    }
                }
            }
            if (devicesFile == null)
            {
                SaveDevices();
                files = dir.GetFiles();
                Thread.Sleep(100);
                foreach (var file in files)
                {
                    if (file.Name == _devicesFileName)
                    {
                        devicesFile = file;
                    }
                }
            }
            

            using (FileStream stream = preferencesFile.Open(FileMode.Open))
            {
                try
                {
                    SoundBoardProperties.Props = (SoundBoardProperties)_formater.Deserialize(stream);
                }
                catch (SerializationException x)
                {
                    preferencesFile.Delete();
                    SavePreferences();
                }
            }
            using (FileStream stream = devicesFile.Open(FileMode.Open))
            {
                try
                {
                    Devices.DevicesInfo = (Devices)_formater.Deserialize(stream);
                }
                catch (SerializationException x)
                {
                    devicesFile.Delete();
                    SaveDevices();
                }
            }
        }

        /// <summary>
        /// Deletes the soundboard properties.
        /// </summary>
        public static void DeletePropertyFiles()
        {
            var dir = GetPropDirectory();
            foreach (var file in dir.GetFiles())
            {
                if (file.Name == _devicesFileName)
                {
                    file.Delete();
                }
                if (file.Name == _preferencesFileName)
                {
                    file.Delete();
                }
            }
        }

        /// <summary>
        /// Saves the Devices properties.
        /// </summary>
        public static void SaveDevices()
        {
            var dir = GetPropDirectory();
            var file = new FileInfo(dir.FullName + $"//{_devicesFileName}");
            using (FileStream stream = new FileStream(file.FullName, FileMode.Create))
            {
                _formater.Serialize(stream, Devices.DevicesInfo);
            }
        }


        /// <summary>
        /// Saves the preferences properties.
        /// </summary>
        public static void SavePreferences()
        {
            var dir = GetPropDirectory();
            var file = new FileInfo(dir.FullName + $"//{_preferencesFileName}");
            using (FileStream stream = new FileStream(file.FullName, FileMode.Create))
            {
                _formater.Serialize(stream, SoundBoardProperties.Props);
            }
        }

        /// <summary>
        /// Used for deserialization of the .sbdn (Sound Board Dot Net) files
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public SoundBoardData(SerializationInfo info, StreamingContext context)
        {
            try
            {
                try
                {
                    if (ProjectFileVersion.Upgrade((FileVersion)info.GetValue("Version", typeof(FileVersion))))
                    {
                        System.Windows.Forms.MessageBox.Show("Wrong version!", "Version", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        throw new Exception("Wrong version!");
                    }
                }
                catch (InvalidCastException)
                {
                    System.Windows.Forms.MessageBox.Show("File corrupted or invalid!", "Invalid file", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    throw new Exception("File corrupted!");
                }
                Data.Clear();
                Data = (List<SoundButtonData>)info.GetValue("Data", typeof(List<SoundButtonData>));
            }
            catch (SerializationException)
            {
                System.Windows.Forms.MessageBox.Show("File corrupted or incompatible!", "Loading failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                throw new Exception("File corrupted or incompatible!");
            }
        }

        /// <summary>
        /// Used for serialization of the .sbdn (Sound Board Dot Net) files
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
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
