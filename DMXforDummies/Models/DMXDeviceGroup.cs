using System;
using System.Collections.ObjectModel;

namespace DMXforDummies.Models
{
    public class DMXDeviceGroup
    {
        private string _name;
        private readonly ObservableCollection<DMXDevice> _devices = new ObservableCollection<DMXDevice>();

        public DMXDeviceGroup(string name)
        {
            _name = name;
        }

        public string name
        {
            get { return _name; }
        }

        public DMXDevice Device (string name)
        {
            foreach (var d in _devices)
            {
                if (d.Name == name)
                {
                    return d;
                }
            }
            throw new IndexOutOfRangeException();
        }

        public void AddDevice (DMXDevice device)
        {
            foreach (var d in _devices)
            {
                if (device.Name == d.Name)
                {
                    throw new IndexOutOfRangeException();
                }
            }
            _devices.Add(device);
        }

    }
}
