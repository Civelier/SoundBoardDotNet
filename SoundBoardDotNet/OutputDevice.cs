using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    [Serializable]
    class OutputDevice
    {
        [NonSerialized]
        public static OutputDevice Default = new OutputDevice(0);
        [NonSerialized]
        public static List<OutputDevice> OutputDevices = new List<OutputDevice>();

        private static OutputDevice _mainOutput = Default;
        public static OutputDevice MainOutput
        {
            get
            {
                if (Devices.DevicesInfo.MainOutputDevice != null)
                {
                    _mainOutput = Devices.DevicesInfo.MainOutputDevice;
                }
                return _mainOutput;
            }
            set
            {
                _mainOutput = value;
                Devices.DevicesInfo.MainOutputDevice = _mainOutput;
            }
        }

        public static void RefreshOutputs()
        {
            OutputDevice[] devices = new OutputDevice[OutputDevices.Count];
            OutputDevices.CopyTo(devices);
            OutputDevices.Clear();
            OutputDevices.Add(Default);
            string mainOutputName = MainOutput.DeviceName;
            MainOutput = Default;
            for (int i = 1; i < WaveOut.DeviceCount; i++)
            {
                var device = new OutputDevice(i);
                if (mainOutputName == device.DeviceName)
                {
                    MainOutput = device;
                    break;
                }
                
                OutputDevices.Add(device);
            }
        }

        public OutputDevice() { }

        public int Index;

        [NonSerialized]
        private string _deviceName = "";
        public string DeviceName
        {
            get
            {
                if (_deviceName == "")
                {
                    _deviceName = Index == 0 ? "Default (" : "";
                    _deviceName += WaveOut.GetCapabilities(Index).ProductName;
                    _deviceName += Index == 0 ? ")" : "";
                }
                return _deviceName;
            }
        }

        public OutputDevice(int index)
        {
            Index = index;
        }
    }
}
