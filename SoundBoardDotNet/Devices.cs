using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SoundBoardDotNet
{
    [Serializable]
    class Devices : ISerializable
    {
        public static Devices DevicesInfo = new Devices();

        public Devices() { }
        [NonSerialized]
        public List<InputDevice> RecordedDevices = null;
        [NonSerialized]
        public OutputDevice MainOutputDevice = null;

        public event EventHandler DevicesReloaded;

        private void OnDevicesReloaded(EventArgs e)
        {
            EventHandler handler = DevicesReloaded;
            handler?.Invoke(this, e);
        }

        public void DevicesRefreshed()
        {
            OnDevicesReloaded(new EventArgs());
        }

        public static void RefreshAll()
        {
            InputDevice.RefreshInputs(false);
            OutputDevice.RefreshOutputs(false);
            DevicesInfo.DevicesRefreshed();
        }

        public Devices(SerializationInfo info, StreamingContext context)
        {
            RecordedDevices = (List<InputDevice>)info.GetValue("RecordedInputs", typeof(List<InputDevice>));
            MainOutputDevice = (OutputDevice)info.GetValue("MainOutput", typeof(OutputDevice));
            InputDevice.RefreshInputs();
            foreach (var device in RecordedDevices)
            {
                foreach (var input in InputDevice.InputDevices)
                {
                    if (input.Index == device.Index)
                    {
                        input.IsRecorded = device.IsRecorded;
                    }
                }
            }
            if (InputDevice.GetRecordedDevices().Count == 0)
            {
                InputDevice.InputDevices[0].IsRecorded = true;
            }
            OutputDevice.MainOutput = MainOutputDevice;
            OnDevicesReloaded(new EventArgs());
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            RecordedDevices = InputDevice.GetRecordedDevices();
            MainOutputDevice = OutputDevice.MainOutput;
            info.AddValue("RecordedInputs", RecordedDevices);
            info.AddValue("MainOutput", MainOutputDevice);
        }
    }
}
