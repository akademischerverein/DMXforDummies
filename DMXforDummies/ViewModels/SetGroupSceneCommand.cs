using DMXforDummies.Models;
using System;
using System.Windows.Input;

namespace DMXforDummies.ViewModels
{
    public class SetGroupSceneCommand : ICommand
    {
        private readonly DMXDeviceGroup _group;
        private readonly Action<DMXDeviceGroup> _scene;
        private readonly DMX _dmx;

        public SetGroupSceneCommand(DMXDeviceGroup group, Action<DMXDeviceGroup> scene, DMX dmx)
        {
            _group = group;
            _scene = scene;
            _dmx = dmx;
        }

        public void Execute(object parameter)
        {
            _scene(_group);

            if (_group.Name.Equals("gr Saal"))
            {
                _dmx.GrSaalLastCommand = this;
            } else if (_group.Name.Equals("kl Saal"))
            {
                _dmx.KlSaalLastCommand = this;
            }
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
