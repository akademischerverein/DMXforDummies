using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DMXforDummies.Models;

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

        public static void SetValues(this DMXUniverse universe, int firstChannel, DMXDeviceGroup group, params byte[] values)
        {
            for (int i = 0; i < values.Length; ++i)
            {
                universe.Set(firstChannel+i, (byte) (values[i] * group.Dimmer));
            }
        }

        public static KeyValuePair<DMXDevice, Color> Change(this DMXDevice dev, Color color)
        {
            return new KeyValuePair<DMXDevice, Color>(dev, color);
        }

        public static KeyValuePair<String, Color> Change(string dev, Color color)
        {
            return new KeyValuePair<string, Color>(dev, color);
        }

        public static KeyValuePair<DMXDevice, ColorBarDialog.FieldType> Option(this DMXDevice dev,
            ColorBarDialog.FieldType type)
        {
            return new KeyValuePair<DMXDevice, ColorBarDialog.FieldType>(dev, type);
        }

        public static KeyValuePair<String, ColorBarDialog.FieldType> Option(string dev, ColorBarDialog.FieldType type)
        {
            return new KeyValuePair<string, ColorBarDialog.FieldType>(dev, type);
        }
    }
}
