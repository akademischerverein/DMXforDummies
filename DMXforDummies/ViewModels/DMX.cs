using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using DMXforDummies.Models;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DmxLib;
using Prism.Commands;
using static DMXforDummies.Helpers;
using static DMXforDummies.ColorBarDialog.FieldType;
using static DMXforDummies.Models.DMXKanalplan;
using Color = DmxLib.Util.Color;

namespace DMXforDummies.ViewModels
{
    public class DMX : ObservableObject
    {
        private readonly DMXKanalplan kanalplan = new DMXKanalplan();
        private readonly Task _universe_update_task;
        private Visibility _windowVisibility;

        public DMX()
        {
            SetRotBlauKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl. Saal"), ColorProperty.Change(Color.FromRGB(1, 0, 0), null, Color.FromRGB(0, 0, 1), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SetRotBlauGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr. Saal"), ColorProperty.Change(Color.FromRGB(1, 0, 0), null, Color.FromRGB(0, 0, 1), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SetAllesAusCommand = new SetGlobalSceneCommand(kanalplan, SceneAllesAus, this);
            SetKalteFarbenGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr. Saal"), ColorProperty.Change(Color.FromRGB(200.0/255, 200.0/255, 0), null, Color.FromRGB(0, 200.0/255, 200.0/255), Color.FromRGB(200.0/255, 200.0/255, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SetKalteFarbenKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl. Saal"), ColorProperty.Change(Color.FromRGB(200.0 / 255, 200.0 / 255, 0), null, Color.FromRGB(0, 200.0 / 255, 200.0 / 255), Color.FromRGB(200.0 / 255, 200.0 / 255, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SetWarmeFarbenGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr. Saal"), ColorProperty.Change(Color.FromRGB(220.0 / 255, 50.0 / 255, 0), null, Color.FromRGB(200.0/255, 150.0 / 255, 0), Color.FromRGB(220.0 / 255, 50.0 / 255, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SetWarmeFarbenKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl. Saal"), ColorProperty.Change(Color.FromRGB(220.0 / 255, 50.0 / 255, 0), null, Color.FromRGB(200.0 / 255, 150.0 / 255, 0), Color.FromRGB(220.0 / 255, 50.0 / 255, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SetAVFarbenGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr. Saal"), ColorProperty.Change(Color.FromRGB(1, 0, 0), null, Color.FromRGB(1, 0, 0), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1.0, 1, 1.0, 1.0));
            SetAVFarbenKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl. Saal"), ColorProperty.Change(Color.FromRGB(1, 0, 0), null, Color.FromRGB(1, 0, 0), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1.0, 1, 1.0, 1.0));
            SelectRGBKlSaalCommand = new DelegateCommand(() => SelectFarben(kanalplan.Group("kl. Saal"), Option("Schattenfuge", ColorPicker), Option("Bar oben", ColorPicker), Option("Bar unten", ColorPicker), Option("Bar weiß", Slider)));
            SelectRGBGrSaalCommand = new DelegateCommand(() => SelectFarben(kanalplan.Group("gr. Saal"), Option("Schattenfuge", ColorPicker), Option("Bar oben", ColorPicker), Option("Bar unten", ColorPicker), Option("Bar weiß", Slider)));
            SetAusGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr. Saal"), ColorProperty.Change(Color.FromRGB(0, 0, 0), null, Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SetAusKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl. Saal"), ColorProperty.Change(Color.FromRGB(0, 0, 0), null, Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            HideWindowCommand = new DelegateCommand(() => WindowVisibility = Visibility.Collapsed);
            _universe_update_task = UpdateDMXUniverse();

            SetBuntBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Outdoor LED"), ColorProperty.Change(Color.FromRGB(1, 0, 0), Color.FromRGB(0, 1, 0), Color.FromRGB(0, 0, 1), Color.FromRGB(175.0/255, 175.0/255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetWarmeFarbenBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Outdoor LED"), ColorProperty.Change(Color.FromRGB(200.0/255, 150.0/255, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(200.0 / 255, 150.0 / 255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetKalteFarbenBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Outdoor LED"), ColorProperty.Change(Color.FromRGB(0, 143.0 / 255, 209.0/255), Color.FromRGB(124.0/255, 252.0/255, 0), Color.FromRGB(0, 143.0 / 255, 209.0 / 255), Color.FromRGB(124.0 / 255, 252.0 / 255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetRotGruenBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Outdoor LED"), ColorProperty.Change(Color.FromRGB(50.0/255, 1, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(50.0/255, 1, 0), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SelectRGBBuehneCommand = new DelegateCommand(() => SelectFarben(kanalplan.Group("Bühne"), Option("links", ColorPicker), Option("halblinks", ColorPicker), Option("halbrechts", ColorPicker), Option("rechts", ColorPicker)));
            SetAusBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Outdoor LED"), ColorProperty.Change(Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0)), DimmerProperty.Change(1, 1, 1, 1));

            SetBuntSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne gr. Saal"), ColorProperty.Change(Color.FromRGB(1, 0, 0), Color.FromRGB(0, 1, 0), Color.FromRGB(0, 0, 1), Color.FromRGB(175.0 / 255, 175.0 / 255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetWarmeFarbenSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne gr. Saal"), ColorProperty.Change(Color.FromRGB(200.0 / 255, 150.0 / 255, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(200.0 / 255, 150.0 / 255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetKalteFarbenSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne gr. Saal"), ColorProperty.Change(Color.FromRGB(0, 143.0 / 255, 209.0 / 255), Color.FromRGB(124.0 / 255, 252.0 / 255, 0), Color.FromRGB(0, 143.0 / 255, 209.0 / 255), Color.FromRGB(124.0 / 255, 252.0 / 255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetRotGruenSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne gr. Saal"), ColorProperty.Change(Color.FromRGB(50.0 / 255, 1, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(50.0 / 255, 1, 0), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SelectRGBSaalCommand = new DelegateCommand(() => SelectFarben(kanalplan.Group("LED Kanne gr. Saal"), Option("1", ColorPicker), Option("2", ColorPicker), Option("3", ColorPicker), Option("4", ColorPicker)));
            SetAusSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne gr. Saal"), ColorProperty.Change(Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0)), DimmerProperty.Change(1, 1, 1, 1));
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

        public double SetDimmKlSaalCommand
        {
            get => (double)kanalplan.Group("kl. Saal").Get(DimmerProperty);
            set => kanalplan.Group("kl. Saal").Set(DimmerProperty, value);
        }

        public double SetDimmGrSaalCommand
        {
            get => (double)kanalplan.Group("gr. Saal").Get(DimmerProperty);
            set => kanalplan.Group("gr. Saal").Set(DimmerProperty, value);
        }

        public double SetDimmBuehneCommand
        {
            get => (double) kanalplan.Group("Outdoor LED").Get(DimmerProperty);
            set => kanalplan.Group("Outdoor LED").Set(DimmerProperty, value);
        }

        public double SetDimmLEDSaalCommand
        {
            get => (double) kanalplan.Group("LED Kanne gr. Saal").Get(DimmerProperty);
            set => kanalplan.Group("LED Kanne gr. Saal").Set(DimmerProperty, value);
        }

        private void SelectFarben(IDevice group, params KeyValuePair<string, ColorBarDialog.FieldType>[] fields)
        {
            var options = new KeyValuePair<IDevice, ColorBarDialog.FieldType>[fields.Length];

            for (int i = 0; i < options.Length; ++i)
            {
                options[i] = new KeyValuePair<IDevice, ColorBarDialog.FieldType>(group.Children.First(d => d.Name == fields[i].Key), fields[i].Value);
            }

            SelectFarben(options);
        }

        private void SelectFarben(params KeyValuePair<IDevice, ColorBarDialog.FieldType>[] fields)
        {
            ColorBarDialog dialog = new ColorBarDialog(fields, this);

            if (dialog.ShowDialog() == false) return;

            //UpdateSceneRGBFarben(fields.ToDictionary(kvp => kvp.Key, kvp => kvp.Value).Keys);
        }

        public void UpdateBrushes()
        {
            MainWindow.INSTANCE.ColorKlSaalSchattenfuge.Background = new SolidColorBrush(kanalplan.Group("kl. Saal").Children[3].SystemColor());
            MainWindow.INSTANCE.ColorKlSaalBarOben.Background = new SolidColorBrush(kanalplan.Group("kl. Saal").Children[2].SystemColor());
            MainWindow.INSTANCE.ColorKlSaalBarUnten.Background = new SolidColorBrush(kanalplan.Group("kl. Saal").Children[0].SystemColor());

            MainWindow.INSTANCE.ColorGrSaalSchattenfuge.Background = new SolidColorBrush(kanalplan.Group("gr. Saal").Children[3].SystemColor());
            MainWindow.INSTANCE.ColorGrSaalBarOben.Background = new SolidColorBrush(kanalplan.Group("gr. Saal").Children[2].SystemColor());
            MainWindow.INSTANCE.ColorGrSaalBarUnten.Background = new SolidColorBrush(kanalplan.Group("gr. Saal").Children[0].SystemColor());

            MainWindow.INSTANCE.ColorBühneLinks.Background = new SolidColorBrush(kanalplan.Group("Outdoor LED").Children[0].SystemColor());
            MainWindow.INSTANCE.ColorBühneHalblinks.Background = new SolidColorBrush(kanalplan.Group("Outdoor LED").Children[1].SystemColor());
            MainWindow.INSTANCE.ColorBühneHalbrechts.Background = new SolidColorBrush(kanalplan.Group("Outdoor LED").Children[2].SystemColor());
            MainWindow.INSTANCE.ColorBühneRechts.Background = new SolidColorBrush(kanalplan.Group("Outdoor LED").Children[3].SystemColor());

            MainWindow.INSTANCE.ColorSaalHintenRechts.Background = new SolidColorBrush(kanalplan.Group("LED Kanne gr. Saal").Children[0].SystemColor());
            MainWindow.INSTANCE.ColorSaalVorneRechts.Background = new SolidColorBrush(kanalplan.Group("LED Kanne gr. Saal").Children[1].SystemColor());
            MainWindow.INSTANCE.ColorSaalsVorneLinks.Background = new SolidColorBrush(kanalplan.Group("LED Kanne gr. Saal").Children[2].SystemColor());
            MainWindow.INSTANCE.ColorSaalHintenLinks.Background = new SolidColorBrush(kanalplan.Group("LED Kanne gr. Saal").Children[3].SystemColor());
        }

        private void SceneAllesAus(DMXKanalplan kanalplan)
        {
            foreach (var group in kanalplan.Universe.Devices)
            {
                foreach (var dev in group.Children)
                {
                    if (dev.SupportedProperties.Contains(ColorProperty))
                    {
                        dev.Set(ColorProperty, DmxLib.Util.Color.FromRGB(0, 0, 0));
                    } else if (dev.SupportedProperties.Contains(DimmerProperty))
                    {
                        dev.Set(DimmerProperty, 0);
                    }
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
                kanalplan.Sink.Commit();
                // don't run again for at least 100 milliseconds
                await Task.Delay(10).ConfigureAwait(false);
            }
        }
    }
}

