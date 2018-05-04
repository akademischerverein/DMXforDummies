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
        private readonly SaalStatus _saal;

        public SetGroupSceneCommand(DMXDeviceGroup @group, DMX dmx, Color schattenfuge, Color barOben,
            Color barUnten, byte barWeiß)
        {
            _group = group;
            _dmx = dmx;
            _saal = SaalStatus.Create();
            _saal.Schattenfuge = schattenfuge;
            _saal.BarOben = barOben;
            _saal.BarUnten = barUnten;
            _saal.BarWeiß = barWeiß;
        }

        public void Execute(object parameter)
        {
            _dmx.SetSceneRGBFarben(_group, _saal);
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
