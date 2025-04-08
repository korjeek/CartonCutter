using Avalonia.Controls;
using Avalonia.Input;
using CartonCutter.Services.Interfaces;

namespace CartonCutter.Services;

public class WindowService(Window window) : IWindowService
{
    public void Close() => window.Close();
    
    public void Minimize() => window.WindowState = WindowState.Minimized;

    public void ToggleState() => window.WindowState = window.WindowState == WindowState.Maximized
        ? WindowState.Normal
        : WindowState.Maximized;
    
    public void MoveAndDrag(PointerPressedEventArgs pointerPressedEventArgs)
    {
        if (pointerPressedEventArgs.GetCurrentPoint(window).Properties.IsLeftButtonPressed)
            window.BeginMoveDrag(pointerPressedEventArgs);
    }
}