using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
            Title = $"DMX for Dummies {Assembly.GetExecutingAssembly().GetName().Version}";
            
            foreach (var slider in new Slider[] {SldrKlSaalDimm, SldrGrSaalDimm, SldrBühneDimm, SldrSaalDimm})
            {
                slider.Value = slider.Maximum;
            }
        }

        public static MainWindow INSTANCE;
    }
}
