using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using DMXforDummies.Models;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using static DMXforDummies.Helpers;
using static DMXforDummies.ViewModels.SetGroupSceneCommand;
using static DMXforDummies.ColorSaalDialog;

namespace DMXforDummies.ViewModels
{
    public class DMX : ObservableObject
    {
        private readonly DMXUniverse universe = new DMXUniverse("192.168.0.2", 5120);
        private readonly DMXKanalplan kanalplan = new DMXKanalplan();
        private readonly Task _universe_update_task;
        private Visibility _windowVisibility;
        private GroupStatus klSaalBar;
        private GroupStatus grSaalBar;
        private GroupStatus buehne;
        private GroupStatus ledSaal;
        
        public DMX()
        {
            SetRotBlauKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), this, BarDevices, Color.FromRgb(255, 0, 0), Color.FromRgb(0, 0, 255), Color.FromRgb(255, 0, 0), Color.FromRgb(0, 0, 0));
            SetRotBlauGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), this, BarDevices, Color.FromRgb(255, 0, 0), Color.FromRgb(0, 0, 255), Color.FromRgb(255, 0, 0), Color.FromRgb(0, 0, 0));
            SetAllesAusCommand = new SetGlobalSceneCommand(kanalplan, SceneAllesAus, this);
            SetKalteFarbenGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), this, BarDevices, Color.FromRgb(200, 200, 0), Color.FromRgb(0, 200, 200), Color.FromRgb(200, 200, 0), Color.FromRgb(0, 0, 0));
            SetKalteFarbenKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), this, BarDevices, Color.FromRgb(200, 200, 0), Color.FromRgb(0, 200, 200), Color.FromRgb(200, 200, 0), Color.FromRgb(0, 0, 0));
            SetWarmeFarbenGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), this, BarDevices, Color.FromRgb(220, 50, 0), Color.FromRgb(200, 150, 0), Color.FromRgb(220, 50, 0), Color.FromRgb(0, 0, 0));
            SetWarmeFarbenKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), this, BarDevices, Color.FromRgb(220, 50, 0), Color.FromRgb(200, 150, 0), Color.FromRgb(220, 50, 0), Color.FromRgb(0, 0, 0));
            SetAVFarbenGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), this, BarDevices, Color.FromRgb(255, 0, 0), Color.FromRgb(255, 0, 0), Color.FromRgb(255, 0, 0), Color.FromRgb(192, 0, 0));
            SetAVFarbenKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), this, BarDevices, Color.FromRgb(255, 0, 0), Color.FromRgb(255, 0, 0), Color.FromRgb(255, 0, 0), Color.FromRgb(192, 0, 0));
            SelectRGBKlSaalCommand = new DelegateCommand(() => SelectBarFarben(kanalplan.Group("kl Saal")));
            SelectRGBGrSaalCommand = new DelegateCommand(() => SelectBarFarben(kanalplan.Group("gr Saal")));
            SetAusGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), this, BarDevices, Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0));
            SetAusKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), this, BarDevices, Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0));
            HideWindowCommand = new DelegateCommand(() => WindowVisibility = Visibility.Collapsed);
            _universe_update_task = UpdateDMXUniverse();

            SetBuntBuehneCommand = new SetGroupSceneCommand(kanalplan.Group("Bühne"), this, BuehneDevices, Color.FromRgb(255, 0, 0), Color.FromRgb(0, 255, 0), Color.FromRgb(0, 0, 255), Color.FromRgb(175, 175, 0));
            SetWarmeFarbenBuehneCommand = new SetGroupSceneCommand(kanalplan.Group("Bühne"), this, BuehneDevices, Color.FromRgb(200, 150, 0), Color.FromRgb(255, 0, 0), Color.FromRgb(255, 0, 0), Color.FromRgb(200, 150, 0));
            SetKalteFarbenBuehneCommand = new SetGroupSceneCommand(kanalplan.Group("Bühne"), this, BuehneDevices, Color.FromRgb(0, 143, 209), Color.FromRgb(124, 252, 0), Color.FromRgb(0, 143, 209), Color.FromRgb(124, 252, 0));
            SetRotGruenBuehneCommand = new SetGroupSceneCommand(kanalplan.Group("Bühne"), this, BuehneDevices, Color.FromRgb(50, 255, 0), Color.FromRgb(255, 0, 0), Color.FromRgb(50, 255, 0), Color.FromRgb(255, 0, 0));
            SelectRGBBuehneCommand = new DelegateCommand(() => SelectSaalFarben(kanalplan.Group("Bühne")));
            SetAusBuehneCommand = new SetGroupSceneCommand(kanalplan.Group("Bühne"), this, BuehneDevices, Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0));

            SetBuntSaalCommand = new SetGroupSceneCommand(kanalplan.Group("LED Kanne Saal"), this, SaalDevices, Color.FromRgb(255, 0, 0), Color.FromRgb(0, 255, 0), Color.FromRgb(0, 0, 255), Color.FromRgb(175, 175, 0));
            SetWarmeFarbenSaalCommand = new SetGroupSceneCommand(kanalplan.Group("LED Kanne Saal"), this, SaalDevices, Color.FromRgb(200, 150, 0), Color.FromRgb(255, 0, 0), Color.FromRgb(255, 0, 0), Color.FromRgb(200, 150, 0));
            SetKalteFarbenSaalCommand = new SetGroupSceneCommand(kanalplan.Group("LED Kanne Saal"), this, SaalDevices, Color.FromRgb(0, 143, 209), Color.FromRgb(124, 252, 0), Color.FromRgb(0, 143, 209), Color.FromRgb(124, 252, 0));
            SetRotGruenSaalCommand = new SetGroupSceneCommand(kanalplan.Group("LED Kanne Saal"), this, SaalDevices, Color.FromRgb(50, 255, 0), Color.FromRgb(255, 0, 0), Color.FromRgb(50, 255, 0), Color.FromRgb(255, 0, 0));
            SelectRGBSaalCommand = new DelegateCommand(() => SelectSaalFarben(kanalplan.Group("LED Kanne Saal")));
            SetAusSaalCommand = new SetGroupSceneCommand(kanalplan.Group("LED Kanne Saal"), this, SaalDevices, Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0));

            klSaalBar = GroupStatus.Create(4);
            grSaalBar = GroupStatus.Create(4);
            buehne = GroupStatus.Create(4);
            ledSaal = GroupStatus.Create(4);
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

        public ICommand SetAusKlSaalCommand { get; }

        public ICommand SetAusGrSaalCommand { get; }

        public ICommand SetAllesAusCommand { get; }

        public ICommand SetBuntBuehneCommand { get; }

        public ICommand SetWarmeFarbenBuehneCommand { get; }

        public ICommand SetKalteFarbenBuehneCommand { get; }

        public ICommand SetRotGruenBuehneCommand { get; }

        public ICommand SelectRGBBuehneCommand { get; }

        public ICommand SetAusBuehneCommand { get; }

        public ICommand SetBuntSaalCommand { get; }

        public ICommand SetWarmeFarbenSaalCommand { get; }

        public ICommand SetKalteFarbenSaalCommand { get; }

        public ICommand SetRotGruenSaalCommand { get; }

        public ICommand SelectRGBSaalCommand { get; }

        public ICommand SetAusSaalCommand { get; }

        public float SetDimmKlSaalCommand
        {
            get { return kanalplan.Group("kl Saal").Dimmer; }
            set
            {
                kanalplan.Group("kl Saal").Dimmer = value;
                SetSceneRGBFarben(kanalplan.Group("kl Saal"), klSaalBar, BarDevices);
            }
        }

        public float SetDimmGrSaalCommand
        {
            get { return kanalplan.Group("gr Saal").Dimmer; }
            set
            {
                kanalplan.Group("gr Saal").Dimmer = value;
                SetSceneRGBFarben(kanalplan.Group("gr Saal"), grSaalBar, BarDevices);
            }
        }

        public float SetDimmBuehneCommand
        {
            get { return kanalplan.Group("Bühne").Dimmer; }
            set
            {
                kanalplan.Group("Bühne").Dimmer = value;
                SetSceneRGBFarben(kanalplan.Group("Bühne"), buehne, BuehneDevices);
            }
        }

        public float SetDimmLEDSaalCommand
        {
            get { return kanalplan.Group("LED Kanne Saal").Dimmer; }
            set
            {
                kanalplan.Group("LED Kanne Saal").Dimmer = value;
                SetSceneRGBFarben(kanalplan.Group("LED Kanne Saal"), ledSaal, SaalDevices);
            }
        }

        private void SelectBarFarben(DMXDeviceGroup group)
        {
            ColorBarDialog dialog = new ColorBarDialog();

            if (group.Name.Equals("kl Saal"))
            {
                dialog.colorSchattenfuge.SelectedColor = klSaalBar.Identifiers[0];
                dialog.colorBarOben.SelectedColor = klSaalBar.Identifiers[1];
                dialog.colorBarUnten.SelectedColor = klSaalBar.Identifiers[2];
                dialog.barWeiß.Value = (double)klSaalBar.Identifiers[3].R / 255d;
            }
            else if (group.Name.Equals("gr Saal"))
            {
                dialog.colorSchattenfuge.SelectedColor = grSaalBar.Identifiers[0];
                dialog.colorBarOben.SelectedColor = grSaalBar.Identifiers[1];
                dialog.colorBarUnten.SelectedColor = grSaalBar.Identifiers[2];
                dialog.barWeiß.Value = (double)grSaalBar.Identifiers[3].R / 255d;
            }

            if (dialog.ShowDialog() == false)
            {
                return;
            }

            if (!dialog.colorSchattenfuge.SelectedColor.HasValue ||
                !dialog.colorBarOben.SelectedColor.HasValue ||
                !dialog.colorBarUnten.SelectedColor.HasValue)
            {
                return;
            }

            GroupStatus groupStatus = GroupStatus.Create(4);
            groupStatus.Identifiers[0] = dialog.colorSchattenfuge.SelectedColor.Value;
            groupStatus.Identifiers[1] = dialog.colorBarOben.SelectedColor.Value;
            groupStatus.Identifiers[2] = dialog.colorBarUnten.SelectedColor.Value;
            groupStatus.Identifiers[3].R = (byte) (dialog.barWeiß.Value * 255);

            SetSceneRGBFarben(group, groupStatus, BarDevices);
        }

        private void SelectSaalFarben(DMXDeviceGroup group)
        {
            ColorSaalDialog dialog = null;
            string[] devices = null;

            if (group.Name.Equals("Bühne"))
            {
                dialog = new ColorSaalDialog(CaptionBuehne);
                dialog.colorOne.SelectedColor = buehne.Identifiers[0];
                dialog.colorTwo.SelectedColor = buehne.Identifiers[1];
                dialog.colorThree.SelectedColor = buehne.Identifiers[2];
                dialog.colorFour.SelectedColor = buehne.Identifiers[3];
                dialog.SetAlphaDisplay(true);

                devices = BuehneDevices;
            }
            else if (group.Name.Equals("LED Kanne Saal"))
            {
                dialog = new ColorSaalDialog(CaptionSaal);
                dialog.colorOne.SelectedColor = ledSaal.Identifiers[0];
                dialog.colorTwo.SelectedColor = ledSaal.Identifiers[1];
                dialog.colorThree.SelectedColor = ledSaal.Identifiers[2];
                dialog.colorFour.SelectedColor = ledSaal.Identifiers[3];
                dialog.SetAlphaDisplay(false);

                devices = SaalDevices;
            }

            if (devices == null || dialog.ShowDialog() == false)
            {
                return;
            }

            if (!dialog.colorOne.SelectedColor.HasValue ||
                !dialog.colorTwo.SelectedColor.HasValue ||
                !dialog.colorThree.SelectedColor.HasValue ||
                !dialog.colorFour.SelectedColor.HasValue)
            {
                return;
            }

            GroupStatus groupStatus = GroupStatus.Create(4);
            groupStatus.Identifiers[0] = dialog.colorOne.SelectedColor.Value;
            groupStatus.Identifiers[1] = dialog.colorTwo.SelectedColor.Value;
            groupStatus.Identifiers[2] = dialog.colorThree.SelectedColor.Value;
            groupStatus.Identifiers[3] = dialog.colorFour.SelectedColor.Value;

            SetSceneRGBFarben(group, groupStatus, devices);
        }

        public void SetSceneRGBFarben(DMXDeviceGroup group, GroupStatus groupStatus, string[] devices)
        {
            DMXDevice dev;
            
            if (group.Name.Equals("kl Saal"))
            {
                klSaalBar = groupStatus;
            } else if (group.Name.Equals("gr Saal"))
            {
                grSaalBar = groupStatus;
            } else if (group.Name.Equals("Bühne"))
            {
                buehne = groupStatus;
               /* if (groupStatus.Identifiers.Any((color) => !color.Equals(Color.FromRgb(0, 0, 0))))
                {
                    universe.Set(kanalplan.Group("Feststrom").Device("Türseite 1").StartChannel, 255);
                    universe.Set(kanalplan.Group("Feststrom").Device("Kammerseite 1").StartChannel, 255);
                }
                else
                {
                    universe.Set(kanalplan.Group("Feststrom").Device("Türseite 1").StartChannel, 0);
                    universe.Set(kanalplan.Group("Feststrom").Device("Kammerseite 1").StartChannel, 0);
                }*/
            } else if (group.Name.Equals("LED Kanne Saal"))
            {
                ledSaal = groupStatus;
                if (groupStatus.Identifiers.Any((color) => !color.Equals(Color.FromRgb(0, 0, 0))))
                 {
                     universe.Set(kanalplan.Group("Feststrom").Device("Türseite 1").StartChannel, 255);
                     universe.Set(kanalplan.Group("Feststrom").Device("Kammerseite 1").StartChannel, 255);
                 }
                 else
                 {
                     universe.Set(kanalplan.Group("Feststrom").Device("Türseite 1").StartChannel, 0);
                     universe.Set(kanalplan.Group("Feststrom").Device("Kammerseite 1").StartChannel, 0);
                 }
            }

            for (int i=0; i<devices.Length; ++i)
            {
                dev = group.Device(devices[i]);

                switch (dev.Type)
                {
                    case "RGB":
                    case "Dimmer":
                        universe.SetValues(dev.StartChannel, group, groupStatus.Identifiers[i].R, groupStatus.Identifiers[i].G, groupStatus.Identifiers[i].B);
                        break;

                    case "DRGB":
                        universe.SetValues(dev.StartChannel, group, groupStatus.Identifiers[i].A, groupStatus.Identifiers[i].R, groupStatus.Identifiers[i].G, groupStatus.Identifiers[i].B);
                        break;
                    case "RGBW":
                        universe.SetValues(dev.StartChannel, group, groupStatus.Identifiers[i].R, groupStatus.Identifiers[i].G, groupStatus.Identifiers[i].B, 0);
                        break;
                }
            }

            UpdateBrushes();
        }

        private void UpdateBrushes()
        {
            MainWindow.INSTANCE.ColorKlSaalSchattenfuge.Background = new SolidColorBrush(klSaalBar.Identifiers[0]);
            MainWindow.INSTANCE.ColorKlSaalBarOben.Background = new SolidColorBrush(klSaalBar.Identifiers[1]);
            MainWindow.INSTANCE.ColorKlSaalBarUnten.Background = new SolidColorBrush(klSaalBar.Identifiers[2]);

            MainWindow.INSTANCE.ColorGrSaalSchattenfuge.Background = new SolidColorBrush(grSaalBar.Identifiers[0]);
            MainWindow.INSTANCE.ColorGrSaalBarOben.Background = new SolidColorBrush(grSaalBar.Identifiers[1]);
            MainWindow.INSTANCE.ColorGrSaalBarUnten.Background = new SolidColorBrush(grSaalBar.Identifiers[2]);

            MainWindow.INSTANCE.ColorBühneLinks.Background = new SolidColorBrush(buehne.Identifiers[0]);
            MainWindow.INSTANCE.ColorBühneHalblinks.Background = new SolidColorBrush(buehne.Identifiers[1]);
            MainWindow.INSTANCE.ColorBühneHalbrechts.Background = new SolidColorBrush(buehne.Identifiers[2]);
            MainWindow.INSTANCE.ColorBühneRechts.Background = new SolidColorBrush(buehne.Identifiers[3]);

            MainWindow.INSTANCE.ColorSaalHintenRechts.Background = new SolidColorBrush(ledSaal.Identifiers[0]);
            MainWindow.INSTANCE.ColorSaalVorneRechts.Background = new SolidColorBrush(ledSaal.Identifiers[1]);
            MainWindow.INSTANCE.ColorSaalsVorneLinks.Background = new SolidColorBrush(ledSaal.Identifiers[2]);
            MainWindow.INSTANCE.ColorSaalHintenLinks.Background = new SolidColorBrush(ledSaal.Identifiers[3]);
        }

        private void SceneAllesAus(DMXKanalplan kanalplan)
        {
            for (int i = 1; i <= 512; i++)
            {
                universe.Set(i, 0);
            }

            klSaalBar = GroupStatus.Create(4);
            grSaalBar = GroupStatus.Create(4);
            buehne = GroupStatus.Create(4);
            ledSaal = GroupStatus.Create(4);
            UpdateBrushes();
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

