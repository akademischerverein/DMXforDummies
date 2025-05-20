using Avalonia.Controls;

namespace DMXforDummies.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            INSTANCE = this;
            InitializeComponent();
#if DEBUG
            Title = $"DMX for Dummies {ThisAssembly.AssemblyInformationalVersion}";
#else
            Title = $"DMX for Dummies {ThisAssembly.AssemblyInformationalVersion.Substring(0, ThisAssembly.AssemblyInformationalVersion.IndexOf("+"))}";
#endif

            SldrKlSaalDimm.Value = SldrKlSaalDimm.Maximum;
            SldrGrSaalDimm.Value = SldrGrSaalDimm.Maximum;
            SldrBühneDimm.Value = SldrBühneDimm.Maximum;
            SldrSaalDimm.Value = SldrSaalDimm.Maximum;

#if OS_MAC
            MenuHideWindow.IsVisible = false;
#endif
        }

        public static MainWindow INSTANCE;
    }
}