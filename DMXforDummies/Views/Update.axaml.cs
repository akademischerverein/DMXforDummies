using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;

namespace DMXforDummies.New;

public partial class Update : Window
{
    public Update()
    {
        InitializeComponent();
    }

    public void SetStatus(string status)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            UpdateStatus.Text = status;
        });
    }

    public void SetProgress(int progress)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            UpdateProgressbar.Value = progress;
        });
    }
}