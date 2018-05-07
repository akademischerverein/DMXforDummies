using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using DMXforDummies.Models;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using static DMXforDummies.Helpers;
using static DMXforDummies.ViewModels.SetGroupSceneCommand;

namespace DMXforDummies.ViewModels
{
    public class DMX : ObservableObject
    {
        private readonly DMXUniverse universe = new DMXUniverse("192.168.0.2", 5120);
        private readonly DMXKanalplan kanalplan = new DMXKanalplan();
        private readonly Task _universe_update_task;
        private Visibility _windowVisibility;
        private SaalStatus klSaal;
        private SaalStatus grSaal;
        
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
            SelectRGBKlSaalCommand = new DelegateCommand(() => SelectRGBFarben(kanalplan.Group("kl Saal")));
            SelectRGBGrSaalCommand = new DelegateCommand(() => SelectRGBFarben(kanalplan.Group("gr Saal")));
            SetAusGrSaalCommand = new SetGroupSceneCommand(kanalplan.Group("gr Saal"), this, BarDevices, Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0));
            SetAusKlSaalCommand = new SetGroupSceneCommand(kanalplan.Group("kl Saal"), this, BarDevices, Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0), Color.FromRgb(0, 0, 0));
            HideWindowCommand = new DelegateCommand(() => WindowVisibility = Visibility.Collapsed);
            _universe_update_task = UpdateDMXUniverse();

            SetLicht1AnCommand = new DelegateCommand(() => { universe.Set(49, 255); });
            SetLicht2AnCommand = new DelegateCommand(() => { universe.Set(53, 255); });
            SetLicht3AnCommand = new DelegateCommand(() => { universe.Set(57, 255); });
            SetLicht4AnCommand = new DelegateCommand(() => { universe.Set(61, 255); });
            SetLicht5AnCommand = new DelegateCommand(() => { universe.Set(81, 255); universe.Set(82, 255); });
            SetLicht6AnCommand = new DelegateCommand(() => { universe.Set(85, 255); universe.Set(86, 255); });
            SetLicht7AnCommand = new DelegateCommand(() => { universe.Set(89, 255); universe.Set(90, 255); });
            SetLicht8AnCommand = new DelegateCommand(() => { universe.Set(93, 255); universe.Set(94, 255); });

            SetRelaisAnCommand = new DelegateCommand(() => {universe.Set(1, 255);universe.Set(5, 255);});

            klSaal = SaalStatus.Create(4);
            grSaal = SaalStatus.Create(4);
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
        
        public ICommand SetLicht1AnCommand { get; }
        public ICommand SetLicht2AnCommand { get; }
        public ICommand SetLicht3AnCommand { get; }
        public ICommand SetLicht4AnCommand { get; }
        public ICommand SetLicht5AnCommand { get; }
        public ICommand SetLicht6AnCommand { get; }
        public ICommand SetLicht7AnCommand { get; }
        public ICommand SetLicht8AnCommand { get; }
        public ICommand SetRelaisAnCommand { get; }

        public float SetDimmKlSaalCommand
        {
            get { return kanalplan.Group("kl Saal").Dimmer; }
            set
            {
                kanalplan.Group("kl Saal").Dimmer = value;
                SetSceneRGBFarben(kanalplan.Group("kl Saal"), klSaal, BarDevices);
            }
        }

        public float SetDimmGrSaalCommand
        {
            get { return kanalplan.Group("gr Saal").Dimmer; }
            set
            {
                kanalplan.Group("gr Saal").Dimmer = value;
                SetSceneRGBFarben(kanalplan.Group("gr Saal"), grSaal, BarDevices);
            }
        }

        private void SelectRGBFarben(DMXDeviceGroup group)
        {
            ColorDialog dialog = new ColorDialog();

            if (group.Name.Equals("kl Saal"))
            {
                dialog.colorSchattenfuge.SelectedColor = klSaal.Identifiers[0];
                dialog.colorBarOben.SelectedColor = klSaal.Identifiers[1];
                dialog.colorBarUnten.SelectedColor = klSaal.Identifiers[2];
                dialog.barWeiß.Value = (double)klSaal.Identifiers[3].R / 255d;
            }
            else if (group.Name.Equals("gr Saal"))
            {
                dialog.colorSchattenfuge.SelectedColor = grSaal.Identifiers[0];
                dialog.colorBarOben.SelectedColor = grSaal.Identifiers[1];
                dialog.colorBarUnten.SelectedColor = grSaal.Identifiers[2];
                dialog.barWeiß.Value = (double)grSaal.Identifiers[3].R / 255d;
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

            SaalStatus saal = SaalStatus.Create(4);
            saal.Identifiers[0] = dialog.colorSchattenfuge.SelectedColor.Value;
            saal.Identifiers[1] = dialog.colorBarOben.SelectedColor.Value;
            saal.Identifiers[2] = dialog.colorBarUnten.SelectedColor.Value;
            saal.Identifiers[3].R = (byte) (dialog.barWeiß.Value * 255);

            SetSceneRGBFarben(group, saal, BarDevices);
        }

        public void SetSceneRGBFarben(DMXDeviceGroup group, SaalStatus saal, string[] devices)
        {
            DMXDevice dev;
            
            if (group.Name.Equals("kl Saal"))
            {
                klSaal = saal;
            } else if (group.Name.Equals("gr Saal"))
            {
                grSaal = saal;
            }

            for(int i=0; i<devices.Length; ++i)
            {
                dev = group.Device(devices[i]);
                universe.SetValues(dev.StartChannel, group, saal.Identifiers[i].R, saal.Identifiers[i].G, saal.Identifiers[i].B);
            }

            UpdateBrushes();
        }

        private void UpdateBrushes()
        {
            MainWindow.INSTANCE.ColorKlSaalSchattenfuge.Background = new SolidColorBrush(klSaal.Identifiers[0]);
            MainWindow.INSTANCE.ColorKlSaalBarOben.Background = new SolidColorBrush(klSaal.Identifiers[1]);
            MainWindow.INSTANCE.ColorKlSaalBarUnten.Background = new SolidColorBrush(klSaal.Identifiers[2]);

            MainWindow.INSTANCE.ColorGrSaalSchattenfuge.Background = new SolidColorBrush(grSaal.Identifiers[0]);
            MainWindow.INSTANCE.ColorGrSaalBarOben.Background = new SolidColorBrush(grSaal.Identifiers[1]);
            MainWindow.INSTANCE.ColorGrSaalBarUnten.Background = new SolidColorBrush(grSaal.Identifiers[2]);
        }

        private void SceneAllesAus(DMXKanalplan kanalplan)
        {
            for (int i = 1; i <= 512; i++)
            {
                universe.Set(i, 0);
            }

            klSaal = SaalStatus.Create(4);
            grSaal = SaalStatus.Create(4);
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

