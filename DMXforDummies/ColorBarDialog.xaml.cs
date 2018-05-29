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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
        /*public enum FieldType
        {
            ColorPicker,
            ColorPickerWithAlpha,
            Slider
        }*/

        public ColorBarDialog(DMXDeviceGroup group)
        {
            /*Dictionary<string, FieldType> fields = new Dictionary<string, FieldType>();
            fields.Add("Schattenfuge", FieldType.ColorPicker);
            fields.Add("Bar oben", FieldType.ColorPicker);
            fields.Add("Bar unten", FieldType.ColorPicker);
            fields.Add("Bar weiß", FieldType.Slider);

            InitializeComponent();

            var addedFields = new Dictionary<DMXDevice, Control>();

            foreach (var field in fields)
            {
                switch (field.Value)
                {
                    case FieldType.ColorPicker:
                        var color = new ColorPicker();
                        color.UsingAlphaChannel = false;
                        AddLogicalChild(color);
                        addedFields.Add(group.Device(field.Key), color);
                        break;
                    case FieldType.ColorPickerWithAlpha:
                        var colorAlpha = new ColorPicker();
                        colorAlpha.UsingAlphaChannel = true;
                        AddLogicalChild(colorAlpha);
                        addedFields.Add(group.Device(field.Key), colorAlpha);
                        break;
                    case FieldType.Slider:
                        var slider = new Slider();
                        slider.Maximum = 1.0;
                        slider.Minimum = 0.0;
                        AddLogicalChild(slider);
                        addedFields.Add(group.Device(field.Key), slider);
                        break;
                }
            }

            foreach (var kp in addedFields)
            {
                if (kp.Key.Type.Equals("Dimmer"))
                {
                    (kp.Value as Slider).Value = kp.Key.Value.R / 255.0;
                }
                else
                {
                    (kp.Value as ColorPicker).SelectedColor = kp.Key.Value;
                }
            }*/

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            colorSchattenfuge.UsingAlphaChannel = false;
            colorBarOben.UsingAlphaChannel = false;
            colorBarUnten.UsingAlphaChannel = false;
        }

        private void DialogFinish(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            if (btn == null)
            {
                return;
            }

            if ("OK".Equals(btn.Content as string))
            {
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }
    }
}
