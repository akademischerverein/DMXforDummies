using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Velopack;
using Velopack.Sources;
using Velopack.Windows;

namespace DMXforDummies
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private const string Unique = "ac702c4fee8f4df395bcc09122a9aa4c";
        private static System.Threading.Mutex singleInstanceMutex;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            singleInstanceMutex = new System.Threading.Mutex(true, Unique);
            var isOnlyInstance = singleInstanceMutex.WaitOne(TimeSpan.Zero, true);

            if(!isOnlyInstance)
            {
                SingleInstance.PostMessage((IntPtr)SingleInstance.HWND_BROADCAST, SingleInstance.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
                Shutdown();
            }

            VelopackApp.Build().Run();
            try
            {
                var mgr = new UpdateManager(new GithubSource("https://github.com/akademischerverein/DMXforDummies", null, false));

                if (mgr?.CurrentVersion != null)
                {
                    var updateInfo = await mgr.CheckForUpdatesAsync();

                    if (updateInfo != null)
                    {
                        try
                        {
                            var updateWindow = new Update();
                            updateWindow.Show();

                            updateWindow.SetStatus("Lade Update herunter...");
                            await mgr.DownloadUpdatesAsync(updateInfo, updateWindow.SetProgress);

                            updateWindow.SetStatus("Installiere Updates...");
                            mgr.ApplyUpdatesAndRestart(updateInfo);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Fehler beim Aktualisieren: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Überprüfen auf neue Updates: " + ex.Message);
            }

            var mw = new MainWindow();
            mw.Show();
            singleInstanceMutex.ReleaseMutex();
        }
    }
}
