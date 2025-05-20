using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmxLib;

namespace DMXforDummies.Models
{
    class DimmerHandler : IHandler
    {
        public void Update(Device device, ReadOnlyDictionary<DeviceProperty, object> properties, Dictionary<uint, byte> values)
        {
            values[device.Channels[0]] = (byte) ((double)properties[DMXKanalplan.DimmerProperty] * 255);
        }

        public bool IsValidValue(DeviceProperty property, object o)
        {
            return true;
        }

        public ReadOnlyCollection<object> ValidValues(DeviceProperty property)
        {
            throw new ArgumentException();
        }

        public ReadOnlyCollection<DeviceProperty> SupportedProperties => new ReadOnlyCollection<DeviceProperty>(new[] { DMXKanalplan.DimmerProperty });
    }
}
