using Avalonia.Input;

namespace CartonCutter.Services.Interfaces;

public interface IWindowService
{
    void Close();

    void ToggleState();

    void Minimize();

    void MoveAndDrag(PointerPressedEventArgs pointerPressedEventArgs);
}