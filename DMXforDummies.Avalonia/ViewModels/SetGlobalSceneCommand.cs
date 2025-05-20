using DMXforDummies.Models;
using System;
using System.Windows.Input;

namespace DMXforDummies.Avalonia.ViewModels
{
    public class SetGlobalSceneCommand : ICommand
    {
        private readonly DMXKanalplan _kanalplan;
        private readonly Action<DMXKanalplan> _scene;
        private readonly MainWindowViewModel _dmx;

        public SetGlobalSceneCommand(DMXKanalplan kanalplan, Action<DMXKanalplan> scene, MainWindowViewModel dmx)
        {
            _kanalplan = kanalplan;
            _scene = scene;
            _dmx = dmx;
        }

        public void Execute(object parameter)
        {
            _scene(_kanalplan);
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
