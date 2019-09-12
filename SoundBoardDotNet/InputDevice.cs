using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using NAudio.Wave;
using System.Runtime.Serialization;

namespace SoundBoardDotNet
{
    [Serializable]
    class InputDevice
    {
        [NonSerialized]
        public static InputDevice Default = new InputDevice(0, true);
        [NonSerialized]
        public static List<InputDevice> InputDevices = new List<InputDevice>();

        public static void RefreshInputs(bool notify = true)
        {
            InputDevice[] devices = new InputDevice[InputDevices.Count];
            InputDevices.CopyTo(devices);
            InputDevices.Clear();
            InputDevices.Add(Default);
            for (int i = 1; i < WaveIn.DeviceCount; i++)
            {
                var device = new InputDevice(i);
                foreach (var oldDevice in devices)
                {
                    if (oldDevice.DeviceName == device.DeviceName)
                    {
                        device.IsRecorded = oldDevice.IsRecorded;
                        device.Recorder = oldDevice.Recorder;
                    }
                }
                InputDevices.Add(device);
            }
            if (notify) Devices.DevicesInfo.DevicesRefreshed();
        }

        private bool _isRecorded = false;
        public bool IsRecorded
        {
            get { return _isRecorded; }
            set
            {
                _isRecorded = value;
                if (value) Recorder = new AudioRecorder(Index);
                else Recorder = null;
            }
        }

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
                    _deviceName += WaveIn.GetCapabilities(Index).ProductName;
                    _deviceName += Index == 0 ? ")" : "";
                }
                return _deviceName;
            }
        }

        [NonSerialized]
        public AudioRecorder Recorder;

        public InputDevice(int index, bool isRecorded = false)
        {
            Index = index;
            IsRecorded = isRecorded;
        }

        public void StartRecording()
        {
            Recorder.StartRecording();
        }

        public void StopRecording()
        {
            Recorder.StopRecording();
        }

        public void Reset()
        {
            Recorder.Reset();
        }

        public static void StartRecorders()
        {
            foreach (var recorder in GetRecordedDevices())
            {
                recorder.StartRecording();
            }
        }

        public static void StopRecorders()
        {
            foreach (var recorder in GetRecordedDevices())
            {
                recorder.StopRecording();
            }
        }

        public static void ResetRecorders()
        {
            foreach (var recorder in GetRecordedDevices())
            {
                recorder.Reset();
            }
        }

        public static List<InputDevice> GetRecordedDevices()
        {
            var output = new List<InputDevice>();

            foreach (var device in InputDevices)
            {
                if (device.IsRecorded)
                {
                    output.Add(device);
                }
            }
            return output;
        }
    }
}
