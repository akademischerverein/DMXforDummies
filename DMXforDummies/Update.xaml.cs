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
    /// Interaktionslogik für Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        public Update()
        {
            InitializeComponent();
        }

        public void SetStatus(string status)
        {
            Dispatcher.Invoke(() =>
            {
                this.Title = status;
            });
        }

        public void SetProgress(int progress)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateProgressbar.Value = progress;
            });
        }
    }
}
