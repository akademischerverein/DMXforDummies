using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DMXforDummies.Models;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using static DMXforDummies.Helpers;

namespace DMXforDummies.ViewModels
{
    public class DMX : ObservableObject
    {
        private readonly DMXUniverse universe = new DMXUniverse("192.168.0.2", 5120);
        private readonly DMXKanalplan kanalplan = new DMXKanalplan();
        private readonly Task _universe_update_task;
        private Visibility _windowVisibility;

        public DMX()
        {
            SetRotBlauKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneRotBlau);
            SetRotBlauGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneRotBlau);
            SetAllesAusCommand = new SetGlobalSceneCommand(kanalplan, SceneAllesAus);
            SetKalteFarbenGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneKalteFarben);
            SetKalteFarbenKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneKalteFarben);
            SetWarmeFarbenGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneWarmeFarben);
            SetWarmeFarbenKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneWarmeFarben);
            SetAVFarbenGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneAVFarben);
            SetAVFarbenKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneAVFarben);
            HideWindowCommand = new DelegateCommand(() => WindowVisibility = Visibility.Collapsed);
            _universe_update_task = UpdateDMXUniverse();
        }

        public Visibility WindowVisibility
        {
            get { return _windowVisibility; }
            set { SetField(ref _windowVisibility, value, OnPropertyChanged); }
        }

        public ICommand HideWindowCommand { get; }

        public ICommand SetAVFarbenKlSaalCommand { get; }

        public ICommand SetAVFarbenGrSaalCommand { get; }

        public ICommand SetWarmeFarbenKlSaalCommand { get; }

        public ICommand SetWarmeFarbenGrSaalCommand { get; }

        public ICommand SetKalteFarbenKlSaalCommand { get; }

        public ICommand SetKalteFarbenGrSaalCommand { get; }

        public ICommand SetRotBlauKlSaalCommand { get; }

        public ICommand SetRotBlauGrSaalCommand { get; }

        public ICommand SetAllesAusCommand { get; }

        private void SceneAVFarben(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar oben", "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.SetRgb(dev.StartChannel, 255, 0, 0);
            }
            dev = group.Device("Bar weiß");
            universe.SetRgb(dev.StartChannel, 192, 0, 0);
        }

        private void SceneWarmeFarben(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.SetRgb(dev.StartChannel, 220, 50, 0);
            }
            dev = group.Device("Bar oben");
            universe.SetRgb(dev.StartChannel, 200, 150, 0);

            dev = group.Device("Bar weiß");
            universe.SetRgb(dev.StartChannel, 0, 0, 0);
        }

        private void SceneKalteFarben(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.SetRgb(dev.StartChannel, 200, 200, 0);
            }
            dev = group.Device("Bar oben");
            universe.SetRgb(dev.StartChannel, 0, 200, 200);

            dev = group.Device("Bar weiß");
            universe.SetRgb(dev.StartChannel, 0, 0, 0);
        }

        private void SceneRotBlau(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.SetRgb(dev.StartChannel, 255, 0, 0);
            }
            dev = group.Device("Bar oben");
            universe.SetRgb(dev.StartChannel, 0, 0, 255);

            dev = group.Device("Bar weiß");
            universe.SetRgb(dev.StartChannel, 0, 0, 0);
        }

        private void SceneAllesAus(DMXKanalplan kanalplan)
        {
            for (int i = 1; i <= 512; i++)
            {
                universe.Set(i, 0);
            }
        }

        private async Task UpdateDMXUniverse()
        {
            while (true)
            {
                universe.Commit();

                // don't run again for at least 100 milliseconds
                await Task.Delay(100).ConfigureAwait(false);
            }
        }
    }
}
