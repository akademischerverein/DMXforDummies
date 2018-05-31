using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DMXforDummies.Models
{
    public class DMXKanalplan
    {
        private readonly ObservableCollection<DMXDeviceGroup> _groups = new ObservableCollection<DMXDeviceGroup>();

        public DMXKanalplan()
        {
            // FIXME: aus kanalbelegung.txt parsen
            DMXDeviceGroup klSaalGrp = new DMXDeviceGroup("kl Saal");
            klSaalGrp.AddDevice(new DMXDevice(29, 3, "RGB", "Schattenfuge", "Schattenfuge"));
            klSaalGrp.AddDevice(new  DMXDevice(17, 3, "RGB", "Bar unten", "Bar untere Hälfte"));
            klSaalGrp.AddDevice(new DMXDevice(25, 3, "RGB", "Bar oben", "Bar obere Hälfte"));
            klSaalGrp.AddDevice(new DMXDevice(21, 1, "Dimmer", "Bar weiß", "Bar Weiß"));
            AddGroup(klSaalGrp);

            DMXDeviceGroup grSaalGrp = new DMXDeviceGroup("gr Saal");
            grSaalGrp.AddDevice(new DMXDevice(45, 3, "RGB", "Schattenfuge", "Schattenfuge"));
            grSaalGrp.AddDevice(new DMXDevice(33, 3, "RGB", "Bar unten", "Bar untere Hälfte"));
            grSaalGrp.AddDevice(new DMXDevice(41, 3, "RGB", "Bar oben", "Bar obere Hälfte"));
            grSaalGrp.AddDevice(new DMXDevice(37, 1, "Dimmer", "Bar weiß", "Bar Weiß"));
            AddGroup(grSaalGrp);

            DMXDeviceGroup stromGrp = new DMXDeviceGroup("Feststrom");
            stromGrp.AddDevice(new DMXDevice(1, 1, "Relais", "Türseite 1", "Relais Saallichter 1"));
            stromGrp.AddDevice(new DMXDevice(5, 1, "Relais", "Kammerseite 1", "Relais Saallichter 2"));
            AddGroup(stromGrp);

            DMXDeviceGroup buehneGrp = new DMXDeviceGroup("Bühne");
            buehneGrp.AddDevice(new DMXDevice(81, 4, "DRGB", "links", "Links"));
            buehneGrp.AddDevice(new DMXDevice(85, 4, "DRGB", "halblinks", "Halblinks"));
            buehneGrp.AddDevice(new DMXDevice(89, 4, "DRGB", "halbrechts", "Halbrechts"));
            buehneGrp.AddDevice(new DMXDevice(93, 4, "DRGB", "rechts", "Rechts"));
            AddGroup(buehneGrp);

            DMXDeviceGroup saalGrp = new DMXDeviceGroup("LED Kanne Saal");
            saalGrp.AddDevice(new DMXDevice(49, 4, "RGBW", "1", "Hinten rechts"));
            saalGrp.AddDevice(new DMXDevice(53, 4, "RGBW", "2", "Vorne rechts"));
            saalGrp.AddDevice(new DMXDevice(57, 4, "RGBW", "3", "Vorne links"));
            saalGrp.AddDevice(new DMXDevice(61, 4, "RGBW", "4", "Hinten links"));
            AddGroup(saalGrp);
        }

        public DMXDeviceGroup Group(string name) => _groups.First(g => g.Name == name);

        public DMXDeviceGroup GroupByDevice(DMXDevice dev) => _groups.First(g => g.Devices.Contains(dev));

        public IReadOnlyCollection<DMXDeviceGroup> Groups => _groups;

        public void AddGroup(DMXDeviceGroup group)
        {
            if (_groups.Any(g => g.Name == group.Name)) throw new InvalidOperationException("One Kanalplan can not contain more than one group with the same name");
            _groups.Add(group);
        }
    }
}
