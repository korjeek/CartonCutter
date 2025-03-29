using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace CartonCutter.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void OnCloseButtonClick(object sender, RoutedEventArgs e) => Close();
    
    private void OnChangeWindowStateButton(object sender, RoutedEventArgs e)
    {
        var image = ((Button)sender).Content as Image ?? 
                    throw new ArgumentException("Sender is not Button or content is not Image");

        if (WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
            image.Source = new Bitmap(AssetLoader.Open(new Uri("avares://CartonCutter/Assets/maximize-512.png")));
        }
        else
        {
            // var screen = Screens.ScreenFromWindow(this)!;
            // var workingArea = screen.WorkingArea;
            // Position = workingArea.Position;
            // Width = workingArea.Width;
            // Height = workingArea.Height;
            WindowState = WindowState.Maximized;
            image.Source =new Bitmap(AssetLoader.Open(new Uri("avares://CartonCutter/Assets/restore-down-512.png")));
        }
    }
    
    private void OnMinimizeButtonClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
}