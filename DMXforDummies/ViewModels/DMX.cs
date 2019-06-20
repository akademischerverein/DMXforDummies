using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using DMXforDummies.Models;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using static DMXforDummies.Helpers;
using static DMXforDummies.ColorBarDialog.FieldType;

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
            SetRotBlauKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl Saal"), Change("Schattenfuge", Color.FromRgb(255, 0, 0)), Change("Bar oben", Color.FromRgb(0, 0, 255)), Change("Bar unten", Color.FromRgb(255, 0, 0)), Change("Bar weiß", Color.FromRgb(0, 0, 0)));
            SetRotBlauGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr Saal"), Change("Schattenfuge", Color.FromRgb(255, 0, 0)), Change("Bar oben", Color.FromRgb(0, 0, 255)), Change("Bar unten", Color.FromRgb(255, 0, 0)), Change("Bar weiß", Color.FromRgb(0, 0, 0)));
            SetAllesAusCommand = new SetGlobalSceneCommand(kanalplan, SceneAllesAus, this);
            SetKalteFarbenGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr Saal"), Change("Schattenfuge", Color.FromRgb(200, 200, 0)), Change("Bar oben", Color.FromRgb(0, 200, 200)), Change("Bar unten", Color.FromRgb(200, 200, 0)), Change("Bar weiß", Color.FromRgb(0, 0, 0)));
            SetKalteFarbenKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl Saal"), Change("Schattenfuge", Color.FromRgb(200, 200, 0)), Change("Bar oben", Color.FromRgb(0, 200, 200)), Change("Bar unten", Color.FromRgb(200, 200, 0)), Change("Bar weiß", Color.FromRgb(0, 0, 0)));
            SetWarmeFarbenGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr Saal"), Change("Schattenfuge", Color.FromRgb(220, 50, 0)), Change("Bar oben", Color.FromRgb(200, 150, 0)), Change("Bar unten", Color.FromRgb(220, 50, 0)), Change("Bar weiß", Color.FromRgb(0, 0, 0)));
            SetWarmeFarbenKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl Saal"), Change("Schattenfuge", Color.FromRgb(220, 50, 0)), Change("Bar oben", Color.FromRgb(200, 150, 0)), Change("Bar unten", Color.FromRgb(220, 50, 0)), Change("Bar weiß", Color.FromRgb(0, 0, 0)));
            SetAVFarbenGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr Saal"), Change("Schattenfuge", Color.FromRgb(255, 0, 0)), Change("Bar oben", Color.FromRgb(255, 0, 0)), Change("Bar unten", Color.FromRgb(255, 0, 0)), Change("Bar weiß", Color.FromRgb(192, 0, 0)));
            SetAVFarbenKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl Saal"), Change("Schattenfuge", Color.FromRgb(255, 0, 0)), Change("Bar oben", Color.FromRgb(255, 0, 0)), Change("Bar unten", Color.FromRgb(255, 0, 0)), Change("Bar weiß", Color.FromRgb(192, 0, 0)));
            SelectRGBKlSaalCommand = new DelegateCommand(() => SelectFarben(kanalplan.Group("kl Saal"), Option("Schattenfuge", ColorPicker), Option("Bar oben", ColorPicker), Option("Bar unten", ColorPicker), Option("Bar weiß", Slider)));
            SelectRGBGrSaalCommand = new DelegateCommand(() => SelectFarben(kanalplan.Group("gr Saal"), Option("Schattenfuge", ColorPicker), Option("Bar oben", ColorPicker), Option("Bar unten", ColorPicker), Option("Bar weiß", Slider)));
            SetAusGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr Saal"), Change("Schattenfuge", Color.FromRgb(0, 0, 0)), Change("Bar oben", Color.FromRgb(0, 0, 0)), Change("Bar unten", Color.FromRgb(0, 0, 0)), Change("Bar weiß", Color.FromRgb(0, 0, 0)));
            SetAusKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl Saal"), Change("Schattenfuge", Color.FromRgb(0, 0, 0)), Change("Bar oben", Color.FromRgb(0, 0, 0)), Change("Bar unten", Color.FromRgb(0, 0, 0)), Change("Bar weiß", Color.FromRgb(0, 0, 0)));
            HideWindowCommand = new DelegateCommand(() => WindowVisibility = Visibility.Collapsed);
            _universe_update_task = UpdateDMXUniverse();

            SetBuntBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Bühne"), Change("links", Color.FromRgb(255, 0, 0)), Change("halblinks", Color.FromRgb(0, 255, 0)), Change("halbrechts", Color.FromRgb(0, 0, 255)), Change("rechts", Color.FromRgb(175, 175, 0)));
            SetWarmeFarbenBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Bühne"), Change("links", Color.FromRgb(200, 150, 0)), Change("halblinks", Color.FromRgb(255, 0, 0)), Change("halbrechts", Color.FromRgb(255, 0, 0)), Change("rechts", Color.FromRgb(200, 150, 0)));
            SetKalteFarbenBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Bühne"), Change("links", Color.FromRgb(0, 143, 209)), Change("halblinks", Color.FromRgb(124, 252, 0)), Change("halbrechts", Color.FromRgb(0, 143, 209)), Change("rechts", Color.FromRgb(124, 252, 0)));
            SetRotGruenBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Bühne"), Change("links", Color.FromRgb(50, 255, 0)), Change("halblinks", Color.FromRgb(255, 0, 0)), Change("halbrechts", Color.FromRgb(50, 255, 0)), Change("rechts", Color.FromRgb(255, 0, 0)));
            SelectRGBBuehneCommand = new DelegateCommand(() => SelectFarben(kanalplan.Group("Bühne"), Option("links", ColorPickerWithAlpha), Option("halblinks", ColorPickerWithAlpha), Option("halbrechts", ColorPickerWithAlpha), Option("rechts", ColorPickerWithAlpha)));
            SetAusBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Bühne"), Change("links", Color.FromRgb(0, 0, 0)), Change("halblinks", Color.FromRgb(0, 0, 0)), Change("halbrechts", Color.FromRgb(0, 0, 0)), Change("rechts", Color.FromRgb(0, 0, 0)));

            SetBuntSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne Saal"), Change("1", Color.FromRgb(255, 0, 0)), Change("2", Color.FromRgb(0, 255, 0)), Change("3", Color.FromRgb(0, 0, 255)), Change("4", Color.FromRgb(175, 175, 0)));
            SetWarmeFarbenSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne Saal"), Change("1", Color.FromRgb(200, 150, 0)), Change("2", Color.FromRgb(255, 0, 0)), Change("3", Color.FromRgb(255, 0, 0)), Change("4", Color.FromRgb(200, 150, 0)));
            SetKalteFarbenSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne Saal"), Change("1", Color.FromRgb(0, 143, 209)), Change("2", Color.FromRgb(124, 252, 0)), Change("3", Color.FromRgb(0, 143, 209)), Change("4", Color.FromRgb(124, 252, 0)));
            SetRotGruenSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne Saal"), Change("1", Color.FromRgb(50, 255, 0)), Change("2", Color.FromRgb(255, 0, 0)), Change("3", Color.FromRgb(50, 255, 0)), Change("4", Color.FromRgb(255, 0, 0)));
            SelectRGBSaalCommand = new DelegateCommand(() => SelectFarben(kanalplan.Group("LED Kanne Saal"), Option("1", ColorPicker), Option("2", ColorPicker), Option("3", ColorPicker), Option("4", ColorPicker)));
            SetAusSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne Saal"), Change("1", Color.FromRgb(0, 0, 0)), Change("2", Color.FromRgb(0, 0, 0)), Change("3", Color.FromRgb(0, 0, 0)), Change("4", Color.FromRgb(0, 0, 0)));
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
            get => kanalplan.Group("kl Saal").Dimmer;
            set
            {
                kanalplan.Group("kl Saal").Dimmer = value;
                UpdateSceneRGBFarben(kanalplan.Group("kl Saal").Devices);
            }
        }

        public float SetDimmGrSaalCommand
        {
            get => kanalplan.Group("gr Saal").Dimmer;
            set
            {
                kanalplan.Group("gr Saal").Dimmer = value;
                UpdateSceneRGBFarben(kanalplan.Group("gr Saal").Devices);
            }
        }

        public float SetDimmBuehneCommand
        {
            get => kanalplan.Group("Bühne").Dimmer;
            set
            {
                kanalplan.Group("Bühne").Dimmer = value;
                UpdateSceneRGBFarben(kanalplan.Group("Bühne").Devices);
            }
        }

        public float SetDimmLEDSaalCommand
        {
            get => kanalplan.Group("LED Kanne Saal").Dimmer;
            set
            {
                kanalplan.Group("LED Kanne Saal").Dimmer = value;
                UpdateSceneRGBFarben(kanalplan.Group("LED Kanne Saal").Devices);
            }
        }

        private void SelectFarben(DMXDeviceGroup group, params KeyValuePair<string, ColorBarDialog.FieldType>[] fields)
        {
            KeyValuePair<DMXDevice, ColorBarDialog.FieldType>[] options = new KeyValuePair<DMXDevice, ColorBarDialog.FieldType>[fields.Length];

            for (int i = 0; i < options.Length; ++i)
            {
                options[i] = new KeyValuePair<DMXDevice, ColorBarDialog.FieldType>(group.Device(fields[i].Key), fields[i].Value);
            }

            SelectFarben(options);
        }

        private void SelectFarben(params KeyValuePair<DMXDevice, ColorBarDialog.FieldType>[] fields)
        {
            ColorBarDialog dialog = new ColorBarDialog(fields, this);

            if (dialog.ShowDialog() == false) return;

            UpdateSceneRGBFarben(fields.ToDictionary(kvp => kvp.Key, kvp => kvp.Value).Keys);
        }

        public void UpdateSceneRGBFarben(IReadOnlyCollection<DMXDevice> devices)
        {
            //Requirements work-around:
            if (devices.Any((dev) => kanalplan.GroupByDevice(dev).Name.Equals("Bühne")))
            {
               //TODO: Which relais are necessary?

            } else if (devices.Any((dev) => kanalplan.GroupByDevice(dev).Name.Equals("LED Kanne Saal")))
            {
                if (kanalplan.Group("LED Kanne Saal").Devices.Any((dev) => !dev.Value.Equals(Color.FromRgb(0, 0, 0))))
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

            foreach (var dev in devices)
            {
                switch (dev.Type)
                {
                    case "RGB":
                    case "Dimmer":
                        universe.SetValues(dev.StartChannel, kanalplan.GroupByDevice(dev), dev.Value.R, dev.Value.G, dev.Value.B);
                        break;

                    case "DRGB":
                        universe.SetValues(dev.StartChannel, kanalplan.GroupByDevice(dev), dev.Value.A, dev.Value.R, dev.Value.G, dev.Value.B);
                        break;
                    case "RGBW":
                        double xn = 1/3.0;
                        double yn = 1/3.0;
                        double Yn = (1 / 0.17697) * (0.17697 * 255 + 0.8124 * 255 + 0.01063 * 255);
                        double X = (1 / 0.17697) * (0.49 * dev.Value.R + 0.31 * dev.Value.G + 0.2 * dev.Value.B);
                        double Y = (1 / 0.17697) * (0.17697 * dev.Value.R + 0.8124 * dev.Value.G + 0.01063 * dev.Value.B);
                        double Z = (1 / 0.17697) * (0.01 * dev.Value.G + 0.99 * dev.Value.B);
                        double x = X / (X + Y + Z);
                        double y = Y / (X + Y + Z);
                        double W = Y + 800 * (xn - x) + 1700 * (yn - y);
                        W = W / Yn;
                        W = W * 255;

                        universe.SetValues(dev.StartChannel, kanalplan.GroupByDevice(dev), dev.Value.R, dev.Value.G, dev.Value.B, (byte)W);
                        break;
                }
            }

            UpdateBrushes();
        }

        //TODO: Use new functionality
        private void UpdateBrushes()
        {
            MainWindow.INSTANCE.ColorKlSaalSchattenfuge.Background = new SolidColorBrush(kanalplan.Group("kl Saal").Device("Schattenfuge").Value);
            MainWindow.INSTANCE.ColorKlSaalBarOben.Background = new SolidColorBrush(kanalplan.Group("kl Saal").Device("Bar oben").Value);
            MainWindow.INSTANCE.ColorKlSaalBarUnten.Background = new SolidColorBrush(kanalplan.Group("kl Saal").Device("Bar unten").Value);

            MainWindow.INSTANCE.ColorGrSaalSchattenfuge.Background = new SolidColorBrush(kanalplan.Group("gr Saal").Device("Schattenfuge").Value);
            MainWindow.INSTANCE.ColorGrSaalBarOben.Background = new SolidColorBrush(kanalplan.Group("gr Saal").Device("Bar oben").Value);
            MainWindow.INSTANCE.ColorGrSaalBarUnten.Background = new SolidColorBrush(kanalplan.Group("gr Saal").Device("Bar unten").Value);

            MainWindow.INSTANCE.ColorBühneLinks.Background = new SolidColorBrush(kanalplan.Group("Bühne").Device("links").Value);
            MainWindow.INSTANCE.ColorBühneHalblinks.Background = new SolidColorBrush(kanalplan.Group("Bühne").Device("halblinks").Value);
            MainWindow.INSTANCE.ColorBühneHalbrechts.Background = new SolidColorBrush(kanalplan.Group("Bühne").Device("halbrechts").Value);
            MainWindow.INSTANCE.ColorBühneRechts.Background = new SolidColorBrush(kanalplan.Group("Bühne").Device("rechts").Value);

            MainWindow.INSTANCE.ColorSaalHintenRechts.Background = new SolidColorBrush(kanalplan.Group("LED Kanne Saal").Device("1").Value);
            MainWindow.INSTANCE.ColorSaalVorneRechts.Background = new SolidColorBrush(kanalplan.Group("LED Kanne Saal").Device("2").Value);
            MainWindow.INSTANCE.ColorSaalsVorneLinks.Background = new SolidColorBrush(kanalplan.Group("LED Kanne Saal").Device("3").Value);
            MainWindow.INSTANCE.ColorSaalHintenLinks.Background = new SolidColorBrush(kanalplan.Group("LED Kanne Saal").Device("4").Value);
        }

        private void SceneAllesAus(DMXKanalplan kanalplan)
        {
            for (int i = 1; i <= 512; i++)
            {
                universe.Set(i, 0);
            }

            foreach (var group in kanalplan.Groups)
            {
                foreach (var dev in group.Devices)
                {
                    dev.Value = Color.FromRgb(0, 0, 0);
                }
            }

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

