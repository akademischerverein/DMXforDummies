using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DMXforDummies
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        private const string Unique = "ac702c4fee8f4df395bcc09122a9aa4c";
        private static System.Threading.Mutex singleInstanceMutex;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            singleInstanceMutex = new System.Threading.Mutex(true, Unique);
            var isOnlyInstance = singleInstanceMutex.WaitOne(TimeSpan.Zero, true);

            if (isOnlyInstance)
            {
                StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);

                singleInstanceMutex.ReleaseMutex();
            }
            else
            {
                SingleInstance.PostMessage((IntPtr)SingleInstance.HWND_BROADCAST, SingleInstance.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
                Shutdown();
            }
        }
    }
}
