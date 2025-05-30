using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using DMXforDummies.New;
using DMXforDummies.ViewModels;
using DMXforDummies.Views;
using System;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Velopack;
using Velopack.Sources;

namespace DMXforDummies
{
    public partial class App : Application
    {
        private const string Unique = "ac702c4fee8f4df395bcc09122a9aa4c";
        private FileStream _stream;
        private NamedPipeServerStream _pipe;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override async void OnFrameworkInitializationCompleted()
        {
            var exception = false;

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();

                var lockFile = Path.Combine(Path.GetTempPath(), $"{Unique}.DMXforDummies.lock");
                try
                {
                    _stream = new FileStream(lockFile, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
                }
                catch (IOException)
                {
                    var pipe = new NamedPipeClientStream($"{Unique}.DMXforDummies.{Environment.UserName}");
                    pipe.Connect();
                    pipe.WriteByte(1);
                    pipe.Close();
                    Environment.Exit(0);
                    return;
                }

                _pipe = new NamedPipeServerStream($"{Unique}.DMXforDummies.{Environment.UserName}");

                var errorMsg = string.Empty;
                desktop.ShutdownMode = Avalonia.Controls.ShutdownMode.OnExplicitShutdown;

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
                                errorMsg = "Fehler beim Aktualisieren: " + ex.Message;
                                exception = true;

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMsg = "Fehler beim Überprüfen auf neue Updates: " + ex.Message;
                    exception = true;
                }

                if (exception)
                {
                    desktop.MainWindow = new MessageBox(errorMsg, Startup);
                }
                else
                {
                    Startup();
                }

                base.OnFrameworkInitializationCompleted();
            }
        }

        private void Startup() {
            CheckPipe();

            var desktop = ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
                IsVisible = true,
            };

            desktop.MainWindow.Closed += CloseApp;
            desktop.MainWindow.Show();
            desktop.ShutdownMode = Avalonia.Controls.ShutdownMode.OnMainWindowClose;
        }

        private async Task CheckPipe()
        {
            while (true)
            {
                await _pipe.WaitForConnectionAsync();
                if (_pipe.ReadByte() == 1)
                {
                    var mw = ((IClassicDesktopStyleApplicationLifetime)ApplicationLifetime).MainWindow as MainWindow;
                    mw.IsVisible = true;
                    mw.Activate();
                }
                _pipe.Disconnect();
            }
        }

        private void CloseApp(object? sender, EventArgs e)
        {
            _stream.Close();
            _pipe.Close();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}