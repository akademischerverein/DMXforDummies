using DMXforDummies.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using DmxLib;
using DmxLib.Util;

namespace DMXforDummies.ViewModels
{
    public class SetDevicesSceneCommand : ICommand
    {
        private readonly DMX _dmx;
        private readonly KeyValuePair<DeviceProperty, object[]>[] _values;
        private IDevice _group;

        public SetDevicesSceneCommand(DMX dmx, IDevice group, params KeyValuePair<DeviceProperty, object[]>[] values)
        {
            _dmx = dmx;
            _values = values;
            _group = group;
        }

        public void Execute(object parameter)
        {
            for (var i = 0; i < _group.Children.Count; i++)
            {
                foreach(var value in _values)
                {
                    if (!_group.Children[i].SupportedProperties.Contains(value.Key)) continue;
                    _group.Children[i].Set(value.Key, value.Value[i]);
                }
            }

            _dmx.UpdateBrushes();
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
