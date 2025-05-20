using Avalonia.Controls;

namespace DMXforDummies.Avalonia.Views
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
        }

        public static MainWindow INSTANCE;
    }
}