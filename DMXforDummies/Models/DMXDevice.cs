using System;

namespace DMXforDummies.Models
{
    public class DMXDevice
    {
        public DMXDevice(byte startChannel, byte nChannels, string type, string name)
        {
            StartChannel = startChannel;
            NChannels = nChannels;
            Type = type;
            Name = name;
        }

        public byte StartChannel { get; }

        public byte NChannels { get; }

        public string Type { get; }

        public string Name { get; }
    }
}
