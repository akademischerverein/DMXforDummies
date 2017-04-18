using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DMXforDummies.Models
{
    public class DMXDeviceGroup
    {
        private readonly ObservableCollection<DMXDevice> _devices = new ObservableCollection<DMXDevice>();

        public DMXDeviceGroup(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public DMXDevice Device(string name) => _devices.First(d => d.Name == name);

        public void AddDevice (DMXDevice device)
        {
            if (_devices.Any(d => d.Name == device.Name)) throw new InvalidOperationException("One DeviceGroup can not contain more than one Device with the same name.");
            _devices.Add(device);
        }

    }
}
