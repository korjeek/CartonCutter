using ReactiveUI;
using System.Windows.Input;
using Avalonia.Input;
using Avalonia.Media;
using CartonCutter.Services.Interfaces;

namespace CartonCutter.ViewModels;

public class MainViewModel(
    IWindowService windowService, 
    IImageService imageService,
    IDragDropFileService dragDropFileService,
    IFileDialogService fileDialogService) : ViewModelBase
{
    public ICommand CloseWindow { get; } = ReactiveCommand.Create(windowService.Close);
    public ICommand MinimizeWindow { get; } = ReactiveCommand.Create(windowService.Minimize);
    public ICommand ToggleWindowState { get; } = ReactiveCommand.Create(windowService.ToggleState);
    public ICommand MoveAndDragWindow { get; } = ReactiveCommand.Create<PointerPressedEventArgs>(windowService.MoveAndDrag);
    public IImage ToggleWindowStateIcon { get; } = imageService.SetImage();
    public ICommand DragOverFile { get; } = ReactiveCommand.Create<DragEventArgs>(dragDropFileService.DragOver);
    public ICommand DropFile { get; } = ReactiveCommand.Create<DragEventArgs>(dragDropFileService.Drop);
    public ICommand OpenFileDialog { get; } = ReactiveCommand.Create(() =>
    {
        fileDialogService.OpenFileUploadDialog();
        fileDialogService.OpenFileDownloadDialog();
    });
}