using DMXforDummies.Models;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace DMXforDummies.ViewModels
{
    public class SetGroupSceneCommand : ICommand
    {
        private readonly DMXDeviceGroup _group;
        private readonly DMX _dmx;
        private readonly GroupStatus _status;
        private readonly string[] _devices;

        public static readonly string[] BarDevices = new string[]{ "Schattenfuge", "Bar oben", "Bar unten", "Bar weiß" };
        public static readonly string[] BuehneDevices = new string[]{ "links", "halblinks", "halbrechts", "rechts" };
        public static readonly string[] SaalDevices = new string[]{ "1", "2", "3", "4"};

        public SetGroupSceneCommand(DMXDeviceGroup group, DMX dmx, string[] devices, params Color[] colors)
        {
            _group = group;
            _dmx = dmx;
            _status = GroupStatus.Create(devices.Length);
            _devices = devices;

            for (int i = 0; i < devices.Length; ++i)
            {
                _status.Identifiers[i] = colors[i];
            }
        }

        public void Execute(object parameter)
        {
            _dmx.SetSceneRGBFarben(_group, _status, _devices);
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
