using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Shell;

namespace DMXforDummies
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        private const string Unique = "ac702c4fee8f4df395bcc09122a9aa4c";

        [STAThread]
        public static void Main()
        {
            if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
            {
                try
                {
                    var application = new App();
                    application.InitializeComponent();
                    application.Run();
                }
                finally
                {
                    // Allow single instance code to perform cleanup operations
                    SingleInstance<App>.Cleanup();
                }
            }
        }

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            // Handle command line arguments of second instance

            // Bring window to foreground
            if (MainWindow.WindowState == WindowState.Minimized)
            {
                MainWindow.WindowState = WindowState.Normal;
            }

            // Kind of hackish but makes MainWindow visible
            var topMost = MainWindow.Topmost;
            MainWindow.Topmost = true;
            MainWindow.Topmost = topMost;

            MainWindow.Activate();
            return true;
        }
    }
}
