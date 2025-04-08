using Avalonia.Controls;
using CartonCutter.Interfaces;

namespace CartonCutter.Services;

public class WindowService(Window window) : IWindowService
{
    public void Close() => window.Close();
    
    public void Minimize() => window.WindowState = WindowState.Minimized;
    
    private void NormalizeWindow(Image image)
    {
        WindowState = WindowState.Normal;
        image.Source = new Bitmap(AssetLoader.Open(new Uri("avares://CartonCutter/Assets/maximize-512.png")));
    }

    private void MaximizeWindow(Image image)
    {
        WindowState = WindowState.Maximized;
        image.Source = new Bitmap(AssetLoader.Open(new Uri("avares://CartonCutter/Assets/restore-down-512.png")));
    }
    
    public void ToggleState()
    {
        var image = ((Button)sender).Content as Image ?? 
                    throw new ArgumentException("Sender is not Button or content is not Image");

        if (WindowState == WindowState.Maximized)
            NormalizeWindow(image);
        else
            MaximizeWindow(image);
    }
}