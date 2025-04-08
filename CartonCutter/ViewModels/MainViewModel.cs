using ReactiveUI;
using System.Windows.Input;
using CartonCutter.Services.Interfaces;

namespace CartonCutter.ViewModels;

public class MainViewModel(IWindowService windowService, IImageService imageService) : ViewModelBase
{
    public ICommand CloseCommand { get; } = ReactiveCommand.Create(windowService.Close);
    public ICommand MinimizeCommand { get; } = ReactiveCommand.Create(windowService.Minimize);
    public ICommand ToggleWindowStateCommand { get; } = ReactiveCommand.Create(windowService.ToggleState);
    public ICommand ToggleWindowStateIconCommand { get; } = ReactiveCommand.Create(imageService.SetImage);
}