using DMXforDummies.Models;
using System;
using System.Windows.Input;

namespace DMXforDummies.ViewModels
{
    public class SetGroupSceneCommand : ICommand
    {
        private readonly DMXDeviceGroup _group;
        private readonly Action<DMXDeviceGroup> _scene;

        public SetGroupSceneCommand(DMXDeviceGroup group, Action<DMXDeviceGroup> scene)
        {
            _group = group;
            _scene = scene;
        }

        public void Execute(object parameter)
        {
            _scene(_group);
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
