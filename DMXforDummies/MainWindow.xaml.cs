using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DMXforDummies.ViewModels;

namespace DMXforDummies
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            INSTANCE = this;
            InitializeComponent();
            Title = $"DMX for Dummies {ThisAssembly.AssemblyInformationalVersion}";

            SldrKlSaalDimm.Value = SldrKlSaalDimm.Maximum;
            SldrGrSaalDimm.Value = SldrGrSaalDimm.Maximum;
            SldrBühneDimm.Value = SldrBühneDimm.Maximum;
            SldrSaalDimm.Value = SldrSaalDimm.Maximum;
        }

        public static MainWindow INSTANCE;

        private HwndSource _hwndSource;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            _hwndSource = HwndSource.FromHwnd(new System.Windows.Interop.WindowInteropHelper(this).Handle);
            _hwndSource?.AddHook(HwndHook);
        }

        [DebuggerStepThrough]
        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == SingleInstance.WM_SHOWME)
            {
                BringWindowToFront();
                handled = true;
            }

            return IntPtr.Zero;
        }

        private void BringWindowToFront()
        {
            // Handle command line arguments of second instance

            Show();

            // Bring window to foreground
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }

            // Kind of hackish but makes MainWindow visible
            var topMost = Topmost;
            Topmost = true;
            Topmost = topMost;

            Activate();
        }
    }
}
