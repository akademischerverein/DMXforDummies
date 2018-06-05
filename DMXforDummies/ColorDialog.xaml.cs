using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using DMXforDummies.Models;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.Toolkit;
using WindowStartupLocation = System.Windows.WindowStartupLocation;

namespace DMXforDummies
{
    /// <summary>
    /// Interaction logic for ColorBarDialog.xaml
    /// </summary>
    public partial class ColorBarDialog : Window
    {
        private Dictionary<int, DMXDevice> deviceMap;

        public enum FieldType
        {
            ColorPicker,
            ColorPickerWithAlpha,
            Slider
        }

        public ColorBarDialog(KeyValuePair<DMXDevice, FieldType>[] fields)
        {
            InitializeComponent();

            deviceMap = new Dictionary<int, DMXDevice>();

            int i = 0;

            foreach (var field in fields)
            {
                Control control = null;

                switch (field.Value)
                {
                    case FieldType.ColorPicker:
                        control = new ColorPicker();
                        ((ColorPicker) control).UsingAlphaChannel = false;
                        ((ColorPicker) control).SelectedColor = field.Key.Value;
                        break;
                    case FieldType.ColorPickerWithAlpha:
                        control = new ColorPicker();
                        ((ColorPicker) control).UsingAlphaChannel = true;
                        ((ColorPicker) control).SelectedColor = field.Key.Value;
                        break;
                    case FieldType.Slider:
                        control = new Slider();
                        ((Slider) control).Maximum = 1.0;
                        ((Slider) control).Minimum = 0.0;
                        ((Slider) control).Value = field.Key.Value.R / 255.0;
                        break;
                }

                var label = new Label();
                label.Content = field.Key.FriendlyName;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.VerticalAlignment = VerticalAlignment.Top;
                label.Margin = new Thickness(10, 10 + 31*(i), 0, 0);

                control.VerticalAlignment = VerticalAlignment.Top;
                control.Margin = new Thickness(115, 14 + 31*(i++), 10, 0);
                this.FindLogicalChildren<Grid>().First().Children.Add(control);
                this.FindLogicalChildren<Grid>().First().Children.Add(label);

                deviceMap.Add(control.GetHashCode(), field.Key);
            }

            var ok = new Button {Content = "OK"};
            ok.Click += DialogFinish;
            ok.HorizontalAlignment = HorizontalAlignment.Left;
            ok.VerticalAlignment = VerticalAlignment.Top;
            ok.Width = 200;
            ok.Margin = new Thickness(10, 14+31*i+8, 0, 0);

            var cancel = new Button {Content = "Cancel"};
            cancel.Click += DialogFinish;
            cancel.HorizontalAlignment = HorizontalAlignment.Left;
            cancel.VerticalAlignment = VerticalAlignment.Top;
            cancel.Width = 200;
            cancel.Margin = new Thickness(231, 14 + 31 * i + 8, 0, 10);

            this.FindLogicalChildren<Grid>().First().Children.Add(ok);
            this.FindLogicalChildren<Grid>().First().Children.Add(cancel);

            this.ResizeMode = ResizeMode.NoResize;
            this.SizeToContent = SizeToContent.Height;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void DialogFinish(object sender, RoutedEventArgs e)
        {
            var btn = (Button) sender;
            DialogResult = "OK".Equals(btn.Content as string);

            if (DialogResult != true) return;

            var controls = this.FindLogicalChildren<Grid>().First().Children;

            foreach (var nativeControl in controls)
            {
                if (nativeControl.GetType() == typeof(ColorPicker))
                {
                    var control = (ColorPicker) nativeControl;
                    DMXDevice dev;
                    if (control.SelectedColor.HasValue && deviceMap.TryGetValue(control.GetHashCode(), out dev))
                    {
                        dev.Value = control.SelectedColor.Value;
                    }
                } else if (nativeControl.GetType() == typeof(Slider))
                {
                    var control = (Slider)nativeControl;
                    DMXDevice dev;
                    if (deviceMap.TryGetValue(control.GetHashCode(), out dev))
                    {
                        dev.Value = Color.FromRgb((byte) (control.Value * 255.0), 0, 0);
                    }
                }
            }
        }
    }
}
