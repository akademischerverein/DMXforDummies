using System;

namespace DMXforDummies.Models
{
    public class DMXDevice
    {
        private byte _start_channel;
        private byte _n_channels;
        private string _type;
        private string _name;

        public DMXDevice(byte start_channel, byte n_channels, string type, string name)
        {
            _start_channel = start_channel;
            _n_channels = n_channels;
            _type = type;
            _name = name;
        }

        public byte StartChannel { get => _start_channel; }

        public byte Nchannels { get => _n_channels; }

        public string Type { get => _type; }

        public string Name { get => _name; }
    }
}
