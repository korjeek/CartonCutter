namespace CartonCutter.Services.Interfaces;

public interface IWindowService
{
    void Close();

    void ToggleState();

    void Minimize();
}