using Avalonia.Input;
using Avalonia.Interactivity;

namespace CartonCutter.Interfaces;

public interface IWindowTitleBarHandler
{
    void OnCloseButtonClick(object sender, RoutedEventArgs e);

    void OnChangeWindowStateButtonClick(object sender, RoutedEventArgs e);

    void OnMoveAndDragWindow(object sender, PointerPressedEventArgs e);

    void OnMinimizeButtonClick(object sender, RoutedEventArgs e);
}