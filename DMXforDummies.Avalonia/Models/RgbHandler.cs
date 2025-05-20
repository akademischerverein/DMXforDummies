using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DmxLib;
using DmxLib.Util;

namespace DMXforDummies.Models
{
    class RgbHandler : IHandler
    {
        private readonly string _layout;

        public RgbHandler(string layout)
        {
            _layout = layout;
        }

        public void Update(Device device, ReadOnlyDictionary<DeviceProperty, object> properties,
            Dictionary<uint, byte> values)
        {
            var color = (Color)properties[DMXKanalplan.ColorProperty];
            var dimming = (double)properties[DMXKanalplan.DimmerProperty];

            if (!_layout.Contains('d'))
            {
                color = Color.FromRGB(color.R * dimming, color.G * dimming, color.B * dimming);
            }

            for (var i = 0; i < _layout.Length; i++)
            {
                switch (_layout[i])
                {
                    case 'r':
                        values[device.Channels[i]] = (byte)(color.R * 255);
                        break;
                    case 'g':
                        values[device.Channels[i]] = (byte)(color.G * 255);
                        break;
                    case 'b':
                        values[device.Channels[i]] = (byte)(color.B * 255);
                        break;
                    case 'w':
                        double gew_r = 0.21784 * color.R;
                        double gew_g = color.G;
                        double gew_b = 0.01308 * color.B;
                        double W = Math.Min(Math.Min(gew_r, gew_g), gew_b);
                        values[device.Channels[i]] = (byte)(W * 255);
                        break;
                    case 'd':
                        values[device.Channels[i]] = (byte)(dimming * 255);
                        break;
                }
            }
        }

        public bool IsValidValue(DeviceProperty property, object value)
        {
            return true;
        }

        public ReadOnlyCollection<object> ValidValues(DeviceProperty property)
        {
            throw new ArgumentException();
        }

        public ReadOnlyCollection<DeviceProperty> SupportedProperties => new ReadOnlyCollection<DeviceProperty>(new[] { DMXKanalplan.ColorProperty, DMXKanalplan.DimmerProperty});
    }
}
