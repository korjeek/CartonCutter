using Avalonia.Input;
using Avalonia.Interactivity;

namespace CartonCutter.Interfaces;

public interface IWindowService
{
    void Close();

    void ToggleState();

    void Minimize();
}