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
        public static readonly string[] CaptionBuehne = new string[] {"Links", "Halblinks", "Halbrechts", "Rechts"};
        public static readonly string[] CaptionSaal = new string[] {"Hinten rechts", "Vorne rechts", "Vorne links", "Hinten links"};

        public ColorSaalDialog(string[] captions)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            lblOne.Content = captions[0];
            lblTwo.Content = captions[1];
            lblThree.Content = captions[2];
            lblFour.Content = captions[3];
        }

        public void SetAlphaDisplay(bool enable)
        {
            colorOne.UsingAlphaChannel = enable;
            colorTwo.UsingAlphaChannel = enable;
            colorThree.UsingAlphaChannel = enable;
            colorFour.UsingAlphaChannel = enable;
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
