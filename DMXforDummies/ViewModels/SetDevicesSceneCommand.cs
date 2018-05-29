using DMXforDummies.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;

namespace DMXforDummies.ViewModels
{
    public class SetDevicesSceneCommand : ICommand
    {
        private readonly DMX _dmx;
        private readonly Dictionary<DMXDevice, Color> _values;

        public SetDevicesSceneCommand(DMX dmx, DMXDeviceGroup group, params KeyValuePair<String, Color>[] colors)
        {
            _dmx = dmx;
            _values = new Dictionary<DMXDevice, Color>();
            foreach (var kp in colors)
            {
                _values.Add(group.Device(kp.Key), kp.Value);
            }
        }

        public SetDevicesSceneCommand(DMX dmx, params KeyValuePair<DMXDevice, Color>[] colors)
        {
            _dmx = dmx;
            _values = new Dictionary<DMXDevice, Color>();
            foreach (var kp in colors)
            {
                _values.Add(kp.Key, kp.Value);
            }
        }

        public void Execute(object parameter)
        {
            foreach (var kp in _values)
            {
                kp.Key.Value = kp.Value;
            }

            _dmx.UpdateSceneRGBFarben(_values.Keys);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged { add { } remove { } }
#pragma warning restore 67
    }
}
