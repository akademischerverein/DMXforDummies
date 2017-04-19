using DMXforDummies.Models;
using System;
using System.Windows.Input;

namespace DMXforDummies.ViewModels
{
    public class SetGlobalSceneCommand : ICommand
    {
        private readonly DMXKanalplan _kanalplan;
        private readonly Action<DMXKanalplan> _scene;

        public SetGlobalSceneCommand(DMXKanalplan kanalplan, Action<DMXKanalplan> scene)
        {
            _kanalplan = kanalplan;
            _scene = scene;
        }

        public void Execute(object parameter)
        {
            _scene(_kanalplan);
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
