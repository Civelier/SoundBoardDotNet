using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundBoardDotNet
{
    public enum KeyboardType
    {
        QWERTY_FR,
        QWERTY_EN
    }

    public class SoundBoardProperties
    {
        private static SoundBoardProperties _props = new SoundBoardProperties();

        public static SoundBoardProperties Props
        {
            get
            {
                return _props;
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
    }
}
