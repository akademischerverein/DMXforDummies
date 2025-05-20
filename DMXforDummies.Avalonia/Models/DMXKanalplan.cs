using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Platform;
using DMXforDummies.Avalonia;
using DmxLib;
using Color = DmxLib.Util.Color;

namespace DMXforDummies.Models
{
    public class DMXKanalplan
    {
        public DMXKanalplan()
        {
            Sink = new AvSink("192.168.0.6", 5120);
            Universe = new Universe(512, Sink);
            Universe.Hooks += _relayChannelsApply;

            var rgb = new RgbHandler("rgb");
            var drgb = new RgbHandler("drgb");
            var rgbw = new RgbHandler("rgbw");
            var dimmer = new DimmerHandler();

            var groups = new Dictionary<string, List<IDevice>>();

            var stream = new StreamReader(AssetLoader.Open(new Uri("avares://DMXforDummies.Avalonia/Assets/kanalplan.txt")));
            var kanalplan = stream.ReadToEnd();

            foreach (var l in kanalplan.Split('\n'))
            {
                if (l.StartsWith("#") || l.Trim().Length == 0) continue;

                var line = l.Trim().Split(';');

                for (var i = 0; i < line.Length; i++)
                {
                    line[i] = line[i].Trim();
                }

                var deviceType = line[1].Split(':');

                if (!groups.ContainsKey(line[2]))
                {
                    groups[line[2]] = new List<IDevice>();
                }

                switch (deviceType[1])
                {
                    case "RGB":
                        groups[line[2]].Add(new Device(line[3], uint.Parse(deviceType[0]), uint.Parse(line[0]), new []{rgb}));
                        break;
                    case "DRGB":
                        groups[line[2]].Add(new Device(line[3], uint.Parse(deviceType[0]), uint.Parse(line[0]), new[] { drgb }));
                        break;
                    case "RGBW":
                        groups[line[2]].Add(new Device(line[3], uint.Parse(deviceType[0]), uint.Parse(line[0]), new[] { rgbw }));
                        break;
                    case "Dimmer":
                        groups[line[2]].Add(new Device(line[3], uint.Parse(deviceType[0]), uint.Parse(line[0]), new[] { dimmer }));
                        groups[line[2]][groups[line[2]].Count - 1].Set(DimmerProperty, 0.0);
                        break;
                }
            }

            foreach (var g in groups)
            {
                if (g.Value.Count == 0) continue;
                Universe.AddDevice(new DeviceGroup(g.Key, g.Value));
            }
        }

        private DateTime? _lastRelayUse;
        private List<uint> _relayChannels = new List<uint>(new uint[]{0, 4});
        private List<uint> _relayRequiredFor = new List<uint>(new uint[]{48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63});

        private void _relayChannelsApply(Universe universe, ref byte[] values)
        {
            var localRef = values;
            if (_relayRequiredFor.Any(ch => localRef[ch] > 0))
            {
                _lastRelayUse = DateTime.Now;
            }

            byte chValue = 0;

            if (_lastRelayUse.HasValue && (DateTime.Now - _lastRelayUse.Value).TotalSeconds < 300)
            {
                chValue = 255;
            }

            foreach (var ch in _relayChannels)
            {
                values[ch] = chValue;
            }
        }

        public IDevice Group(string name) => Universe.Devices.First(g => g.Name == name);

        public IDevice GroupByDevice(IDevice dev) => Universe.Devices.First(g => g.Children.Contains(dev));
        public Universe Universe { get; }
        public AvSink Sink { get; }

        public static readonly DeviceProperty ColorProperty =
            DeviceProperty.RegisterProperty("color", typeof(Color), Color.FromRGB(0, 0, 0), (g, d) => ((Color)g).R != 0 || ((Color)g).G != 0 || ((Color)g).B != 0 ? g : d);
        public static readonly DeviceProperty DimmerProperty =
            DeviceProperty.RegisterProperty("dimmer", typeof(double), 1.0, (g, d) => (double)g * (double)d);
    }
}
