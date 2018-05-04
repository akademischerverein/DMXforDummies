using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
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

        private Color[] customKlSaal = new Color[]
            {Color.FromRgb(255, 0, 0), Color.FromRgb(255, 255, 255), Color.FromRgb(255, 0, 0)};
        private Color[] customGrSaal = new Color[]
            {Color.FromRgb(255, 0, 0), Color.FromRgb(255, 255, 255), Color.FromRgb(255, 0, 0)};

        public DMX()
        {
            SetRotBlauKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneRotBlau, this);
            SetRotBlauGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneRotBlau, this);
            SetAllesAusCommand = new SetGlobalSceneCommand(kanalplan, SceneAllesAus, this);
            SetKalteFarbenGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneKalteFarben, this);
            SetKalteFarbenKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneKalteFarben, this);
            SetWarmeFarbenGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneWarmeFarben, this);
            SetWarmeFarbenKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneWarmeFarben, this);
            SetAVFarbenGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneAVFarben, this);
            SetAVFarbenKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneAVFarben, this);
            SelectRGBKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SelectRGBFarben, this);
            SelectRGBGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SelectRGBFarben, this);
            SetRGBKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneRGBFarben, this);
            SetRGBGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneRGBFarben, this);
            SetAusGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneAus, this);
            SetAusKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneAus, this);
            HideWindowCommand = new DelegateCommand(() => WindowVisibility = Visibility.Collapsed);
            _universe_update_task = UpdateDMXUniverse();

            KlSaalLastCommand = SetAusKlSaalCommand;
            GrSaalLastCommand = SetAusGrSaalCommand;
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

        public ICommand SelectRGBKlSaalCommand { get; }

        public ICommand SelectRGBGrSaalCommand { get; }

        public ICommand SetRGBKlSaalCommand { get; }

        public ICommand SetRGBGrSaalCommand { get; }

        public ICommand SetAusKlSaalCommand { get; }

        public ICommand SetAusGrSaalCommand { get; }

        public ICommand SetAllesAusCommand { get; }

        public ICommand KlSaalLastCommand { get; set; }

        public ICommand GrSaalLastCommand { get; set; }

        public float SetDimmKlSaalCommand
        {
            get { return kanalplan.Group("kl Saal").Dimmer; }
            set
            {
                kanalplan.Group("kl Saal").Dimmer = value;
                KlSaalLastCommand.Execute(null);
            }
        }

        public float SetDimmGrSaalCommand
        {
            get { return kanalplan.Group("gr Saal").Dimmer; }
            set
            {
                kanalplan.Group("gr Saal").Dimmer = value;
                GrSaalLastCommand.Execute(null);
            }
        }

        private void SceneAus(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new[] { "Bar oben", "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.SetRgb(dev.StartChannel, 0, 0, 0, group);
            }
            dev = group.Device("Bar weiß");
            universe.SetRgb(dev.StartChannel, 0, 0, 0, group);
        }

        private void SceneAVFarben(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar oben", "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.SetRgb(dev.StartChannel, 255, 0, 0, group);
            }
            dev = group.Device("Bar weiß");
            universe.SetRgb(dev.StartChannel, 192, 0, 0, group);
        }

        private void SceneWarmeFarben(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.SetRgb(dev.StartChannel, 220, 50, 0, group);
            }
            dev = group.Device("Bar oben");
            universe.SetRgb(dev.StartChannel, 200, 150, 0, group);

            dev = group.Device("Bar weiß");
            universe.SetRgb(dev.StartChannel, 0, 0, 0, group);
        }

        private void SceneKalteFarben(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.SetRgb(dev.StartChannel, 200, 200, 0, group);
            }
            dev = group.Device("Bar oben");
            universe.SetRgb(dev.StartChannel, 0, 200, 200, group);

            dev = group.Device("Bar weiß");
            universe.SetRgb(dev.StartChannel, 0, 0, 0, group);
        }

        private void SceneRotBlau(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.SetRgb(dev.StartChannel, 255, 0, 0, group);
            }
            dev = group.Device("Bar oben");
            universe.SetRgb(dev.StartChannel, 0, 0, 255, group);

            dev = group.Device("Bar weiß");
            universe.SetRgb(dev.StartChannel, 0, 0, 0, group);
        }

        private void SelectRGBFarben(DMXDeviceGroup group)
        {
            ColorDialog dialog = new ColorDialog();

            if (group.Name.Equals("kl Saal"))
            {
                dialog.colorSchattenfuge.SelectedColor = customKlSaal[0];
                dialog.colorBarOben.SelectedColor = customKlSaal[1];
                dialog.colorBarUnten.SelectedColor = customKlSaal[2];
            }
            else if (group.Name.Equals("gr Saal"))
            {
                dialog.colorSchattenfuge.SelectedColor = customGrSaal[0];
                dialog.colorBarOben.SelectedColor = customGrSaal[1];
                dialog.colorBarUnten.SelectedColor = customGrSaal[2];
            }

            if (dialog.ShowDialog() == false)
            {
                if (group.Name.Equals("kl Saal"))
                {
                    KlSaalLastCommand = SetAusKlSaalCommand;
                } else if (group.Name.Equals("gr Saal"))
                {
                    GrSaalLastCommand = SetAusGrSaalCommand;
                }
                return;
            }

            if (!dialog.colorSchattenfuge.SelectedColor.HasValue ||
                !dialog.colorBarOben.SelectedColor.HasValue ||
                !dialog.colorBarUnten.SelectedColor.HasValue)
            {
                return;
            }

            Color[] newColors = new Color[]
            {
                dialog.colorSchattenfuge.SelectedColor.Value,
                dialog.colorBarOben.SelectedColor.Value,
                dialog.colorBarUnten.SelectedColor.Value
            };

            if (group.Name.Equals("kl Saal"))
            {
                customKlSaal = newColors;
                SetRGBKlSaalCommand.Execute(null);
            } else if (group.Name.Equals("gr Saal"))
            {
                customGrSaal = newColors;
                SetRGBGrSaalCommand.Execute(null);
            }
        }

        private void SceneRGBFarben(DMXDeviceGroup group)
        {
            DMXDevice dev;
            Color[] colors;

            if (group.Name.Equals("kl Saal"))
            {
                colors = customKlSaal;
            } else if (group.Name.Equals("gr Saal"))
            {
                colors = customGrSaal;
            }
            else
            {
                colors = new Color[]{
                    Color.FromRgb(0, 0, 0),
                    Color.FromRgb(0, 0, 0),
                    Color.FromRgb(0, 0, 0)
                };
            }

            dev = group.Device("Schattenfuge");
            universe.SetRgb(dev.StartChannel, colors[0].R, colors[0].G, colors[0].B, group);

            dev = group.Device("Bar oben");
            //c = dialog.colorBarOben.SelectedColor.Value;
            universe.SetRgb(dev.StartChannel, colors[1].R, colors[1].G, colors[1].B, group);

            dev = group.Device("Bar unten");
            //c = dialog.colorBarUnten.SelectedColor.Value;
            universe.SetRgb(dev.StartChannel, colors[2].R, colors[2].G, colors[2].B, group);

            dev = group.Device("Bar weiß");
            universe.SetRgb(dev.StartChannel, 0, 0, 0, group);
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
#if DEBUG
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;
#endif
            while (true)
            {
                universe.Commit();

                // don't run again for at least 100 milliseconds
                await Task.Delay(100).ConfigureAwait(false);
            }
        }
    }
}

