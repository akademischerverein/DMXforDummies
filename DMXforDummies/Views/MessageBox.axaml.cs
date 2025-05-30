using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;

namespace DMXforDummies.Views;

public partial class MessageBox : Window
{
    private readonly Action startup;

    public MessageBox()
    {
        InitializeComponent();
    }

    public MessageBox(string message, System.Action startup) : this()
    {
        ErrorMessage.Text = message;
        this.startup = startup;
    }

    private void Window_Closed(object? sender, System.EventArgs e)
    {
        startup?.Invoke();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }
}