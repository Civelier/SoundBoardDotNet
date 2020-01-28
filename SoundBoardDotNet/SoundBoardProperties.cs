using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.Design;

namespace SoundBoardDotNet
{
    public enum KeyboardType
    {
        QWERTY_FR,
        QWERTY_EN
    }

    [Serializable]
    public class SoundBoardProperties
    {
        private SemanticVersion _appVersion = new SemanticVersion(1, 6, 0);
        [ReadOnly(true)]
        [Description("App version number.")]
        [DisplayName("App version")]
        public SemanticVersion AppVersion
        {
            get { return _appVersion; }
        }

        private static SoundBoardProperties _props = new SoundBoardProperties();

        public static SoundBoardProperties Props
        {
            get
            {
                return _props;
            }
            set
            {
                _props = value;
            }
        }

        private double _recordingSampleBufferTime = 10;
        [Description("Time (in seconds) to keep in recording buffer.")]
        [DisplayName("Recording buffer time")]
        public double RecordingSampleTime
        {
            get { return _recordingSampleBufferTime; }
            set { _recordingSampleBufferTime = value; }
        }

        private KeyboardType _keyboard = KeyboardType.QWERTY_EN;
        [Description("Keyboard type to use.")]
        public KeyboardType Keyboard
        {
            get { return _keyboard; }
            set { _keyboard = value; }
        }

        private string _dir = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\SoundBoardRecordedSounds";
        [Editor(typeof(FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("Location where recorded files should be saved.")]
        [DisplayName("File save location.")]
        public string SoundSaveLocation
        {
            get { return _dir; }
            set { _dir = value; }
        }
    }
}
