using DMXforDummies.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using DmxLib;
using DmxLib.Util;

namespace DMXforDummies.Avalonia.ViewModels
{
    public class SetDevicesSceneCommand : ICommand
    {
        private readonly KeyValuePair<DeviceProperty, object[]>[] _values;
        private IDevice _group;
        private MainWindowViewModel _dmx;

        public SetDevicesSceneCommand(MainWindowViewModel dmx, IDevice @group, params KeyValuePair<DeviceProperty, object[]>[] values)
        {
            _values = values;
            _group = group;
            _dmx = dmx;
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
