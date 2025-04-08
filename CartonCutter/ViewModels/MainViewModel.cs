using ReactiveUI;
using System.Windows.Input;
using CartonCutter.Interfaces;

namespace CartonCutter.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IWindowService _windowService;
    
    public ICommand CloseCommand { get; }
    public ICommand ToggleWindowStateCommand { get; }
    public ICommand MinimizeCommand { get; }

    public MainViewModel(IWindowService windowService)
    {
        _windowService = windowService;

        CloseCommand = ReactiveCommand.Create(_windowService.Close);
        MinimizeCommand = ReactiveCommand.Create(_windowService.Minimize);
        ToggleWindowStateCommand = ReactiveCommand.Create(_windowService.ToggleState);
    }
}