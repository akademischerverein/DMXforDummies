using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.VisualTree;
using DMXforDummies.Models;
using DmxLib;
using Prism.Dialogs;
using System;
using System.Collections.Generic;

namespace DMXforDummies;

public partial class ColorDialog : Window
{
    private Dictionary<int, IDevice> deviceMap;
    private CheckBox useLive;
    private Dictionary<IDevice, Color> startMap;
    private bool? Result;

    public enum FieldType
    {
        ColorPicker,
        Slider
    }

    public ColorDialog() { }

    public ColorDialog(KeyValuePair<string, FieldType>[] fields, IDevice group, bool reverse)
    {
        InitializeComponent();

        deviceMap = new Dictionary<int, IDevice>();
        startMap = new Dictionary<IDevice, Color>();

        int i = 0;
        int j = 0;
        int jMod = 1;
        var grid = Content as Grid;

        if (reverse)
        {
            j = fields.Length - 1;
            jMod = -jMod;
        }

        for (; j < fields.Length && j > -1; j = j + jMod)
        {
            Control control = null;
            var field = fields[j];

            switch (field.Value)
            {
                case FieldType.ColorPicker:
                    control = new ColorPicker();
                    ((ColorPicker)control).IsAlphaEnabled = true;
                    ((ColorPicker)control).Color = group.Children[j].SystemColor();
                    ((ColorPicker)control).Width = 300;
                    ((ColorPicker)control).Height = 20;
                    ((ColorPicker)control).Palette = new MaterialColorPalette();
                    ((ColorPicker)control).ColorChanged += color_changed;
                    break;
                case FieldType.Slider:
                    control = new Slider();
                    ((Slider)control).Maximum = 1.0;
                    ((Slider)control).Minimum = 0.0;
                    ((Slider)control).Value = (double)group.Children[j].Get(DMXKanalplan.DimmerProperty);
                    ((Slider)control).ValueChanged += color_changed;
                    break;
            }

            var label = new Label();
            label.Content = field.Key;
            label.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            label.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            label.Margin = new Thickness(10, 10 + 31 * (i), 0, 0);

            control.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
            control.Margin = new Thickness(115, 14 + 31 * (i++), 10, 0);
            grid.Children.Add(control);
            grid.Children.Add(label);

            deviceMap.Add(control.GetHashCode(), group.Children[j]);

            startMap.Add(group.Children[j],
                field.Value == FieldType.ColorPicker
                    ? group.Children[j].SystemColor()
                    : Color.FromArgb((byte)(255.0 * (double)group.Children[j].Get(DMXKanalplan.DimmerProperty)),
                        0, 0, 0));
        }

        var liveLabel = new Label();
        liveLabel.Content = "Vorschau anzeigen";
        liveLabel.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
        liveLabel.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
        liveLabel.Margin = new Thickness(140, 14 + 31 * i + 1, 0, 0);
        liveLabel.PointerReleased += ToggleLive;

        useLive = new CheckBox();
        useLive.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
        useLive.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
        useLive.Margin = new Thickness(115, 14 + 31 * i + 4, 0, 0);
        useLive.IsChecked = true;

        var ok = new Button { Content = "OK" };
        ok.Click += DialogFinish;
        ok.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
        ok.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
        ok.Width = 200;
        ok.Margin = new Thickness(10, 14 + 31 * i + 30, 0, 0);

        var cancel = new Button { Content = "Cancel" };
        cancel.Click += DialogFinish;
        cancel.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
        cancel.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top;
        cancel.Width = 200;
        cancel.Margin = new Thickness(231, 14 + 31 * i + 30, 0, 10);

        grid.Children.Add(useLive);
        grid.Children.Add(liveLabel);
        grid.Children.Add(ok);
        grid.Children.Add(cancel);

        this.CanResize = false;
        this.SizeToContent = SizeToContent.Height;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        this.Closed += DialogFinish;
    }

    private void DialogFinish(object sender, EventArgs e)
    {
        if (((ColorDialog)sender).Result.HasValue && ((ColorDialog)sender).Result.Value) return;

        foreach (var field in startMap)
        {
            field.Key.Set(DMXKanalplan.DimmerProperty, field.Value.A / 255.0);
            if (field.Key.SupportedProperties.Contains(DMXKanalplan.ColorProperty))
            {
                field.Key.Set(DMXKanalplan.ColorProperty,
                    DmxLib.Util.Color.FromRGB(field.Value.R / 255.0, field.Value.G / 255.0, field.Value.B / 255.0));
            }
        }
    }

    private void ToggleLive(object sender, RoutedEventArgs e)
    {
        useLive.IsChecked = !useLive.IsChecked.Value;
    }

    private void color_changed(object sender, RoutedEventArgs e)
    {
        if (!useLive.IsChecked.Value) return;
        UpdateColors();
    }

    private void color_changed(object sender, ColorChangedEventArgs e)
    {
        if (!useLive.IsChecked.Value) return;
        UpdateColors();
    }

    private void UpdateColors()
    {
        var controls = ((Grid)Content).Children;

        foreach (var nativeControl in controls)
        {
            if (nativeControl.GetType() == typeof(ColorPicker))
            {
                var control = (ColorPicker)nativeControl;
                IDevice dev;
                if (deviceMap.TryGetValue(control.GetHashCode(), out dev))
                {
                    dev.Set(DMXKanalplan.ColorProperty, DmxLib.Util.Color.FromRGB(control.Color.R / 255.0, control.Color.G / 255.0, control.Color.B / 255.0));
                    dev.Set(DMXKanalplan.DimmerProperty, control.Color.A / 255.0);
                }
            }
            else if (nativeControl.GetType() == typeof(Slider))
            {
                var control = (Slider)nativeControl;
                IDevice dev;
                if (deviceMap.TryGetValue(control.GetHashCode(), out dev))
                {
                    dev.Set(DMXKanalplan.DimmerProperty, control.Value);
                }
            }
        }
    }

    private void DialogFinish(object sender, RoutedEventArgs e)
    {
        var btn = (Button)sender;
        Result = "OK".Equals(btn.Content as string);
        
        if(Result.Value)
        {
            UpdateColors();
        }
        Close(Result);
    }
}