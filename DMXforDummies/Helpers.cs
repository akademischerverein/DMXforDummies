using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        public static void SetRgb(this DMXUniverse universe, int firstChannel, byte r, byte g, byte b, DMXDeviceGroup group)
        {
            universe.Set(firstChannel, (byte) (r * group.Dimmer));
            universe.Set(firstChannel + 1, (byte) (g * group.Dimmer));
            universe.Set(firstChannel + 2, (byte) (b * group.Dimmer));
        }
    }
}
