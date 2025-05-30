using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Avalonia.Media;
using DMXforDummies.Models;
using DmxLib;

namespace DMXforDummies
{
    internal static class Helpers
    {
        /// <summary>
        /// Sets field to value.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="field">The field which should be set.</param>
        /// <param name="value">The value which the field should have.</param>
        /// <param name="onPropertyChangedInvoker">The OnPropertyChanged Event of the field.</param>
        /// <param name="propertyName">The property name of the field.</param>
        /// <returns><c>true</c> if field was set to new value, otherwise <c>false</c>.</returns>
        public static bool SetField<T>(ref T field, T value, Action<string> onPropertyChangedInvoker, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            onPropertyChangedInvoker(propertyName);
            return true;
        }

        public static KeyValuePair<DeviceProperty, object[]> Change(this DeviceProperty property, params object[] values)
        {
            for (var i = 0; i < values.Length; i++)
            {
                values[i] = Convert.ChangeType(values[i], property.Type);
            }
            return new KeyValuePair<DeviceProperty, object[]>(property, values);
        }

        public static KeyValuePair<String, ColorDialog.FieldType> Option(string dev, ColorDialog.FieldType type)
        {
            return new KeyValuePair<string, ColorDialog.FieldType>(dev, type);
        }

        public static Color SystemColor(this IDevice device)
        {
            var color = (DmxLib.Util.Color) device.Get(DMXKanalplan.ColorProperty);
            var dimmer = (double) device.Get(DMXKanalplan.DimmerProperty);
            return Color.FromArgb((byte) (dimmer * 255), (byte) (color.R * 255), (byte) (color.G * 255), (byte) (color.B * 255));
        }
    }
}
