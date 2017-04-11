using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DMXforDummies.Models;
using System.Threading.Tasks;

namespace DMXforDummies.ViewModels
{
    public class DMX : ObservableObject
    {
        private readonly DMXUniverse universe = new DMXUniverse("192.168.0.2", 5120);
        private readonly DMXKanalplan kanalplan = new DMXKanalplan();
        private readonly Task _universe_update_task;

        public DMX()
        {
            _universe_update_task = UpdateDMXUniverse();
        }

        public ICommand SetAVFarbenKlSaalCommand
        {
            get { return new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneAVFarben); }
        }

        public ICommand SetAVFarbenGrSaalCommand
        {
            get { return new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneAVFarben); }
        }

        private int SceneAVFarben(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar oben", "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.Set(dev.StartChannel + 0, 255);
                universe.Set(dev.StartChannel + 1, 0);
                universe.Set(dev.StartChannel + 2, 0);
            }
            dev = group.Device("Bar weiß");
            universe.Set(dev.StartChannel + 0, 128);
            universe.Set(dev.StartChannel + 1, 0);
            universe.Set(dev.StartChannel + 2, 0);

            return 0;
        }

        public ICommand SetWarmeFarbenKlSaalCommand
        {
            get { return new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneWarmeFarben); }
        }

        public ICommand SetWarmeFarbenGrSaalCommand
        {
            get { return new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneWarmeFarben); }
        }

        private int SceneWarmeFarben(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.Set(dev.StartChannel + 0, 220);
                universe.Set(dev.StartChannel + 1, 50);
                universe.Set(dev.StartChannel + 2, 0);
            }
            dev = group.Device("Bar oben");
            universe.Set(dev.StartChannel + 0, 200);
            universe.Set(dev.StartChannel + 1, 150);
            universe.Set(dev.StartChannel + 2, 0);

            dev = group.Device("Bar weiß");
            universe.Set(dev.StartChannel + 0, 0);
            universe.Set(dev.StartChannel + 1, 0);
            universe.Set(dev.StartChannel + 2, 0);

            return 0;
        }

        public ICommand SetKalteFarbenKlSaalCommand
        {
            get { return new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneKalteFarben); }
        }

        public ICommand SetKalteFarbenGrSaalCommand
        {
            get { return new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneKalteFarben); }
        }

        private int SceneKalteFarben(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.Set(dev.StartChannel + 0, 200);
                universe.Set(dev.StartChannel + 1, 200);
                universe.Set(dev.StartChannel + 2, 0);
            }
            dev = group.Device("Bar oben");
            universe.Set(dev.StartChannel + 0, 0);
            universe.Set(dev.StartChannel + 1, 200);
            universe.Set(dev.StartChannel + 2, 200);

            dev = group.Device("Bar weiß");
            universe.Set(dev.StartChannel + 0, 0);
            universe.Set(dev.StartChannel + 1, 0);
            universe.Set(dev.StartChannel + 2, 0);

            return 0;
        }


        public ICommand SetRotBlauKlSaalCommand
        {
            get { return new SetGroupSceneCommand(kanalplan.Group("kl Saal"), SceneRotBlau); }
        }

        public ICommand SetRotBlauGrSaalCommand
        {
            get { return new SetGroupSceneCommand(kanalplan.Group("gr Saal"), SceneRotBlau); }
        }

        private int SceneRotBlau(DMXDeviceGroup group)
        {
            DMXDevice dev;
            foreach (var name in new string[] { "Bar unten", "Schattenfuge" })
            {
                dev = group.Device(name);
                universe.Set(dev.StartChannel + 0, 255);
                universe.Set(dev.StartChannel + 1, 0);
                universe.Set(dev.StartChannel + 2, 0);
            }
            dev = group.Device("Bar oben");
            universe.Set(dev.StartChannel + 0, 0);
            universe.Set(dev.StartChannel + 1, 0);
            universe.Set(dev.StartChannel + 2, 255);

            dev = group.Device("Bar weiß");
            universe.Set(dev.StartChannel + 0, 0);
            universe.Set(dev.StartChannel + 1, 0);
            universe.Set(dev.StartChannel + 2, 0);

            return 0;
        }

        public ICommand SetAllesAusCommand
        {
            get { return new SetGlobalSceneCommand(kanalplan, SceneAllesAus); }
        }

        private int SceneAllesAus(DMXKanalplan kanalplan)
        {
            for (int i = 1; i <= 512; i++)
            {
                universe.Set(i, 0);
            }
            return 0;
        }

        private async Task UpdateDMXUniverse()
        {
            while (true)
            {
                universe.Commit();

                // don't run again for at least 100 milliseconds
                await Task.Delay(100).ConfigureAwait(false);
            }
        }
    }
}
