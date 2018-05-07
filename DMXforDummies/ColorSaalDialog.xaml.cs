using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DMXforDummies
{
    /// <summary>
    /// Interaction logic for ColorSaalDialog.xaml
    /// </summary>
    public partial class ColorSaalDialog : Window
    {
        public ColorSaalDialog()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void DialogFinish(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            if (btn == null)
            {
                return;
            }

            if ("OK".Equals(btn.Content as string))
            {
                DialogResult = true;
            }
            else
            {
                DialogResult = false;
            }
        }
    }
}
