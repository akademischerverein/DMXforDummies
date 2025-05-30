using Avalonia.Media;
using DMXforDummies.Models;
using DMXforDummies.Views;
using DmxLib;
using Prism.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using static DMXforDummies.Models.DMXKanalplan;
using static DMXforDummies.Helpers;
using static DMXforDummies.ColorDialog.FieldType;
using Color = DmxLib.Util.Color;

namespace DMXforDummies.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly DMXKanalplan kanalplan = new DMXKanalplan();
        private bool _windowVisibility;

        public MainWindowViewModel()
        {
            SetAVFarbenKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl. Saal"), ColorProperty.Change(Color.FromRGB(1, 0, 0), null, Color.FromRGB(1, 0, 0), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1.0, 1, 1.0, 1.0));
            SetWarmeFarbenKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl. Saal"), ColorProperty.Change(Color.FromRGB(220.0 / 255, 50.0 / 255, 0), null, Color.FromRGB(200.0 / 255, 150.0 / 255, 0), Color.FromRGB(220.0 / 255, 50.0 / 255, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SetKalteFarbenKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl. Saal"), ColorProperty.Change(Color.FromRGB(200.0 / 255, 200.0 / 255, 0), null, Color.FromRGB(0, 200.0 / 255, 200.0 / 255), Color.FromRGB(200.0 / 255, 200.0 / 255, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SetRotBlauKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl. Saal"), ColorProperty.Change(Color.FromRGB(1, 0, 0), null, Color.FromRGB(0, 0, 1), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SelectRGBKlSaalCommand = new DelegateCommand(async () => await SelectFarben(kanalplan.Group("kl. Saal"), true, Option("Bar untere Hälfte", ColorPicker), Option("Bar Weiß", Slider), Option("Bar obere Hälfte", ColorPicker), Option("Schattenfuge", ColorPicker)));
            SetAusKlSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("kl. Saal"), ColorProperty.Change(Color.FromRGB(0, 0, 0), null, Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));

            SetAVFarbenGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr. Saal"), ColorProperty.Change(Color.FromRGB(1, 0, 0), null, Color.FromRGB(1, 0, 0), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1.0, 1, 1.0, 1.0));
            SetWarmeFarbenGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr. Saal"), ColorProperty.Change(Color.FromRGB(220.0 / 255, 50.0 / 255, 0), null, Color.FromRGB(200.0 / 255, 150.0 / 255, 0), Color.FromRGB(220.0 / 255, 50.0 / 255, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SetKalteFarbenGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr. Saal"), ColorProperty.Change(Color.FromRGB(200.0 / 255, 200.0 / 255, 0), null, Color.FromRGB(0, 200.0 / 255, 200.0 / 255), Color.FromRGB(200.0 / 255, 200.0 / 255, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SetRotBlauGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr. Saal"), ColorProperty.Change(Color.FromRGB(1, 0, 0), null, Color.FromRGB(0, 0, 1), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));
            SelectRGBGrSaalCommand = new DelegateCommand(async () => await SelectFarben(kanalplan.Group("gr. Saal"), true, Option("Bar untere Hälfte", ColorPicker), Option("Bar Weiß", Slider), Option("Bar obere Hälfte", ColorPicker), Option("Schattenfuge", ColorPicker)));
            SetAusGrSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("gr. Saal"), ColorProperty.Change(Color.FromRGB(0, 0, 0), null, Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0)), DimmerProperty.Change(1.0, 0, 1.0, 1.0));

            SetBuntBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Outdoor LED"), ColorProperty.Change(Color.FromRGB(1, 0, 0), Color.FromRGB(0, 1, 0), Color.FromRGB(0, 0, 1), Color.FromRGB(175.0 / 255, 175.0 / 255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetWarmeFarbenBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Outdoor LED"), ColorProperty.Change(Color.FromRGB(200.0 / 255, 150.0 / 255, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(200.0 / 255, 150.0 / 255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetKalteFarbenBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Outdoor LED"), ColorProperty.Change(Color.FromRGB(0, 143.0 / 255, 209.0 / 255), Color.FromRGB(124.0 / 255, 252.0 / 255, 0), Color.FromRGB(0, 143.0 / 255, 209.0 / 255), Color.FromRGB(124.0 / 255, 252.0 / 255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetRotGruenBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Outdoor LED"), ColorProperty.Change(Color.FromRGB(50.0 / 255, 1, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(50.0 / 255, 1, 0), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SelectRGBBuehneCommand = new DelegateCommand(async () => await SelectFarben(kanalplan.Group("Outdoor LED"), Option("Links", ColorPicker), Option("Halblinks", ColorPicker), Option("Halbrechts", ColorPicker), Option("Rechts", ColorPicker)));
            SetAusBuehneCommand = new SetDevicesSceneCommand(this, kanalplan.Group("Outdoor LED"), ColorProperty.Change(Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0)), DimmerProperty.Change(1, 1, 1, 1));

            SetBuntSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne gr. Saal"), ColorProperty.Change(Color.FromRGB(1, 0, 0), Color.FromRGB(0, 1, 0), Color.FromRGB(0, 0, 1), Color.FromRGB(175.0 / 255, 175.0 / 255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetWarmeFarbenSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne gr. Saal"), ColorProperty.Change(Color.FromRGB(200.0 / 255, 150.0 / 255, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(200.0 / 255, 150.0 / 255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetKalteFarbenSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne gr. Saal"), ColorProperty.Change(Color.FromRGB(0, 143.0 / 255, 209.0 / 255), Color.FromRGB(124.0 / 255, 252.0 / 255, 0), Color.FromRGB(0, 143.0 / 255, 209.0 / 255), Color.FromRGB(124.0 / 255, 252.0 / 255, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SetRotGruenSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne gr. Saal"), ColorProperty.Change(Color.FromRGB(50.0 / 255, 1, 0), Color.FromRGB(1, 0, 0), Color.FromRGB(50.0 / 255, 1, 0), Color.FromRGB(1, 0, 0)), DimmerProperty.Change(1, 1, 1, 1));
            SelectRGBSaalCommand = new DelegateCommand(async () => await SelectFarben(kanalplan.Group("LED Kanne gr. Saal"), Option("Hinten rechts", ColorPicker), Option("Vorne rechts", ColorPicker), Option("Vorne links", ColorPicker), Option("Hinten links", ColorPicker)));
            SetAusSaalCommand = new SetDevicesSceneCommand(this, kanalplan.Group("LED Kanne gr. Saal"), ColorProperty.Change(Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0), Color.FromRGB(0, 0, 0)), DimmerProperty.Change(1, 1, 1, 1));

            SetAllesAusCommand = new SetGlobalSceneCommand(kanalplan, SceneAllesAus, this);
            HideWindowCommand = new DelegateCommand(() => WindowVisibility = false);
            _universe_update_task = UpdateDMXUniverse();
        }

        public bool WindowVisibility
        {
            get { return _windowVisibility; }
            set { SetField(ref _windowVisibility, value, OnPropertyChanged); }
        }

        public ICommand HideWindowCommand { get; }

        private Task _universe_update_task;

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
            get => (double)kanalplan.Group("Outdoor LED").Get(DimmerProperty);
            set => kanalplan.Group("Outdoor LED").Set(DimmerProperty, value);
        }

        public double SetDimmLEDSaalCommand
        {
            get => (double)kanalplan.Group("LED Kanne gr. Saal").Get(DimmerProperty);
            set => kanalplan.Group("LED Kanne gr. Saal").Set(DimmerProperty, value);
        }
        private async Task SelectFarben(IDevice group, params KeyValuePair<string, ColorDialog.FieldType>[] fields)
        {
            await SelectFarben(group, false, fields);
        }

        private async Task SelectFarben(IDevice group, bool reverse, params KeyValuePair<string, ColorDialog.FieldType>[] fields)
        {
            ColorDialog dialog = new ColorDialog(fields, group, reverse);

            var result = await dialog.ShowDialog<bool>(MainWindow.INSTANCE);
            if(!result)
            {
                return;
            }
            UpdateBrushes();
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
            MainWindow.INSTANCE.ColorSaalVorneLinks.Background = new SolidColorBrush(kanalplan.Group("LED Kanne gr. Saal").Children[2].SystemColor());
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
                    }
                    else if (dev.SupportedProperties.Contains(DimmerProperty))
                    {
                        dev.Set(DimmerProperty, 0.0);
                    }
                }
            }

            UpdateBrushes();
        }

        private async Task UpdateDMXUniverse()
        {
#if DEBUG
            if (Avalonia.Controls.Design.IsDesignMode) return;
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
