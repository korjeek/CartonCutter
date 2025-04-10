using System;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CartonCutter.Constants;
using CartonCutter.Services.Interfaces;
using CartonCutter.Views;

namespace CartonCutter.Services;

public class WindowTitleBarImageService(MainWindow window): IImageService
{
    public IImage SetImage()
    {
        var uri = new Uri(window.WindowState == WindowState.Maximized 
            ? AssetPaths.RestoreIcon 
            : AssetPaths.MaximizeIcon);
        
        return new Bitmap(AssetLoader.Open(uri));
    }
}