using System;
using System.Windows.Media;

namespace DMXforDummies.Models
{
    public class DMXDevice
    {
        public DMXDevice(byte startChannel, byte nChannels, string type, string name, string friendlyName)
        {
            StartChannel = startChannel;
            NChannels = nChannels;
            Type = type;
            Name = name;
            Value = Color.FromRgb(0, 0, 0);
            FriendlyName = friendlyName;
        }

        public byte StartChannel { get; }

        public byte NChannels { get; }

        public string Type { get; }

        public string Name { get; }

        public string FriendlyName { get; }

        public Color Value;
    }
}
