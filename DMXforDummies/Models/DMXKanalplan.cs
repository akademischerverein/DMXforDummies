using System;
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

            DMXDeviceGroup stromGrp = new DMXDeviceGroup("Feststrom");
            stromGrp.AddDevice(new DMXDevice(1, 1, "Relais", "Türseite 1"));
            stromGrp.AddDevice(new DMXDevice(5, 1, "Relais", "Kammerseite 1"));
            AddGroup(stromGrp);

            DMXDeviceGroup buehneGrp = new DMXDeviceGroup("Bühne");
            buehneGrp.AddDevice(new DMXDevice(81, 4, "DRGB", "links"));
            buehneGrp.AddDevice(new DMXDevice(85, 4, "DRGB", "halblinks"));
            buehneGrp.AddDevice(new DMXDevice(89, 4, "DRGB", "halbrechts"));
            buehneGrp.AddDevice(new DMXDevice(93, 4, "DRGB", "rechts"));
            AddGroup(buehneGrp);

            DMXDeviceGroup saalGrp = new DMXDeviceGroup("LED Kanne Saal");
            saalGrp.AddDevice(new DMXDevice(49, 4, "RGBW", "1"));
            saalGrp.AddDevice(new DMXDevice(53, 4, "RGBW", "2"));
            saalGrp.AddDevice(new DMXDevice(57, 4, "RGBW", "3"));
            saalGrp.AddDevice(new DMXDevice(61, 4, "RGBW", "4"));
            AddGroup(saalGrp);
        }

        public DMXDeviceGroup Group(string name) => _groups.First(g => g.Name == name);

        public void AddGroup(DMXDeviceGroup group)
        {
            if (_groups.Any(g => g.Name == group.Name)) throw new InvalidOperationException("One Kanalplan can not contain more than one group with the same name");
            _groups.Add(group);
        }
    }
}
