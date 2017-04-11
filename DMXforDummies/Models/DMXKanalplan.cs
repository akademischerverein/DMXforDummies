using System;
using System.Collections.ObjectModel;

namespace DMXforDummies.Models
{
    public class DMXKanalplan
    {
        private readonly ObservableCollection<DMXDeviceGroup> _groups = new ObservableCollection<DMXDeviceGroup>();

        public DMXKanalplan()
        {
            // FIXME: aus kanalbelegung.txt parsen
            DMXDeviceGroup klSaalGrp = new DMXDeviceGroup("kl Saal");
            klSaalGrp.AddDevice(new DMXDevice(17, 3, "RGB", "Bar unten"));
            klSaalGrp.AddDevice(new DMXDevice(21, 1, "Dimmer", "Bar weiß"));
            klSaalGrp.AddDevice(new DMXDevice(25, 3, "RGB", "Bar oben"));
            klSaalGrp.AddDevice(new DMXDevice(29, 3, "RGB", "Schattenfuge"));
            AddGroup(klSaalGrp);

            DMXDeviceGroup grSaalGrp = new DMXDeviceGroup("gr Saal");
            grSaalGrp.AddDevice(new DMXDevice(33, 3, "RGB", "Bar unten"));
            grSaalGrp.AddDevice(new DMXDevice(37, 1, "Dimmer", "Bar weiß"));
            grSaalGrp.AddDevice(new DMXDevice(41, 3, "RGB", "Bar oben"));
            grSaalGrp.AddDevice(new DMXDevice(45, 3, "RGB", "Schattenfuge"));
            AddGroup(grSaalGrp);
        }

        public DMXDeviceGroup Group(string name)
        {
            foreach(var g in _groups)
            {
                if (g.name == name) {
                    return g;
                }
            }
            throw new IndexOutOfRangeException();
        }

        public void AddGroup(DMXDeviceGroup group)
        {
            foreach (var g in _groups)
            {
                if (group.name == g.name)
                {
                    throw new IndexOutOfRangeException();
                }
            }
            _groups.Add(group);
        }
    }
}
