using Avalonia.Controls;
using CartonCutter.Constants;
using CartonCutter.Services.Interfaces;

namespace CartonCutter.Services;

public class WindowTitleBarImageService(Window window): IImageService
{
    public string SetImage() => window.WindowState == WindowState.Maximized 
        ? AssetPaths.RestoreIcon 
        : AssetPaths.MaximizeIcon;
}