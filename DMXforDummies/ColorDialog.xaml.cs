using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using DMXforDummies.Models;
using DMXforDummies.ViewModels;
using DmxLib;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.Toolkit;
using static DMXforDummies.Helpers;
using WindowStartupLocation = System.Windows.WindowStartupLocation;

namespace DMXforDummies
{
    /// <summary>
    /// Interaction logic for ColorBarDialog.xaml
    /// </summary>
    public partial class ColorBarDialog : Window
    {
        private Dictionary<int, IDevice> deviceMap;
        private CheckBox useLive;
        private DMX dmx;
        private Dictionary<IDevice, Color> startMap;

        public enum FieldType
        {
            ColorPicker,
            ColorPickerWithAlpha,
            Slider
        }

        public ColorBarDialog(KeyValuePair<IDevice, FieldType>[] fields, DMX dmx)
        {
            InitializeComponent();

            deviceMap = new Dictionary<int, IDevice>();
            startMap = new Dictionary<IDevice, Color>();
            this.dmx = dmx;

            int i = 0;

            foreach (var field in fields)
            {
                Control control = null;

                switch (field.Value)
                {
                    case FieldType.ColorPicker:
                    case FieldType.ColorPickerWithAlpha:
                        control = new ColorPicker();
                        ((ColorPicker) control).UsingAlphaChannel = true;
                        ((ColorPicker) control).SelectedColor =
                            ((DmxLib.Util.Color) field.Key.Get(DMXKanalplan.ColorProperty)).SystemColor();
                        ((ColorPicker) control).SelectedColorChanged += color_changed;
                        break;
                    case FieldType.Slider:
                        control = new Slider();
                        ((Slider) control).Maximum = 1.0;
                        ((Slider) control).Minimum = 0.0;
                        ((Slider) control).Value = ((DmxLib.Util.Color)field.Key.Get(DMXKanalplan.ColorProperty)).R;
                        ((Slider) control).ValueChanged += color_changed;
                        break;
                }

                var label = new Label();
                label.Content = field.Key.Name;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.VerticalAlignment = VerticalAlignment.Top;
                label.Margin = new Thickness(10, 10 + 31*(i), 0, 0);

                control.VerticalAlignment = VerticalAlignment.Top;
                control.Margin = new Thickness(115, 14 + 31*(i++), 10, 0);
                this.FindLogicalChildren<Grid>().First().Children.Add(control);
                this.FindLogicalChildren<Grid>().First().Children.Add(label);

                deviceMap.Add(control.GetHashCode(), field.Key);
                startMap.Add(field.Key, ((DmxLib.Util.Color)field.Key.Get(DMXKanalplan.ColorProperty)).SystemColor());
            }

            var liveLabel = new Label();
            liveLabel.Content = "Vorschau anzeigen";
            liveLabel.HorizontalAlignment = HorizontalAlignment.Left;
            liveLabel.VerticalAlignment = VerticalAlignment.Top;
            liveLabel.Margin = new Thickness(130, 14 + 31 * i - 4, 0, 0);
            liveLabel.MouseLeftButtonUp += ToggleLive;

            useLive = new CheckBox();
            useLive.HorizontalAlignment = HorizontalAlignment.Left;
            useLive.VerticalAlignment = VerticalAlignment.Top;
            useLive.Margin = new Thickness(115, 14 + 31 * i + 4, 0, 0);
            useLive.IsChecked = true;

            var ok = new Button {Content = "OK"};
            ok.Click += DialogFinish;
            ok.HorizontalAlignment = HorizontalAlignment.Left;
            ok.VerticalAlignment = VerticalAlignment.Top;
            ok.Width = 200;
            ok.Margin = new Thickness(10, 14+31*i+30, 0, 0);

            var cancel = new Button {Content = "Cancel"};
            cancel.Click += DialogFinish;
            cancel.HorizontalAlignment = HorizontalAlignment.Left;
            cancel.VerticalAlignment = VerticalAlignment.Top;
            cancel.Width = 200;
            cancel.Margin = new Thickness(231, 14 + 31 * i + 30, 0, 10);

            this.FindLogicalChildren<Grid>().First().Children.Add(useLive);
            this.FindLogicalChildren<Grid>().First().Children.Add(liveLabel);
            this.FindLogicalChildren<Grid>().First().Children.Add(ok);
            this.FindLogicalChildren<Grid>().First().Children.Add(cancel);

            this.ResizeMode = ResizeMode.NoResize;
            this.SizeToContent = SizeToContent.Height;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            this.Closed += DialogFinish;
        }

        private void DialogFinish(object sender, EventArgs e)
        {
            if (((ColorBarDialog) sender).DialogResult.HasValue && ((ColorBarDialog) sender).DialogResult.Value) return;

            foreach (var field in startMap)
            {
            //    field.Key.Value = field.Value;
            }
            List<DMXDevice> devices = new List<DMXDevice>();
            foreach (DMXDevice dev in deviceMap.Values)
            {
                devices.Add(dev);
            }
            //dmx.UpdateSceneRGBFarben(devices);
        }

        private void ToggleLive(object sender, RoutedEventArgs e)
        {
            useLive.IsChecked = !useLive.IsChecked.Value;
        }

        private void color_changed(object sender, RoutedEventArgs e)
        {
            if (!useLive.IsChecked.Value) return;
            UpdateColors();

            List<DMXDevice> devices = new List<DMXDevice>();
            foreach (DMXDevice dev in deviceMap.Values)
            {
                devices.Add(dev);
            }
        }

        private void UpdateColors()
        {
            var controls = this.FindLogicalChildren<Grid>().First().Children;

            foreach (var nativeControl in controls)
            {
                if (nativeControl.GetType() == typeof(ColorPicker))
                {
                    var control = (ColorPicker)nativeControl;
                    DMXDevice dev;
                    //if (control.SelectedColor.HasValue && deviceMap.TryGetValue(control.GetHashCode(), out dev))
                    {
                        //dev.Value = control.SelectedColor.Value;
                    }
                }
                else if (nativeControl.GetType() == typeof(Slider))
                {
                    var control = (Slider)nativeControl;
                    DMXDevice dev;
                    //if (deviceMap.TryGetValue(control.GetHashCode(), out dev))
                    {
                        //dev.Value = Color.FromRgb((byte)(control.Value * 255.0), 0, 0);
                    }
                }
            }
        }

        private void DialogFinish(object sender, RoutedEventArgs e)
        {
            var btn = (Button) sender;
            DialogResult = "OK".Equals(btn.Content as string);

            if (DialogResult != true) return;

            UpdateColors();
        }
    }
}
