using System.IO;
using System.Threading.Tasks;
using ReactiveUI;
using System.Windows.Input;
using Avalonia.Input;
using Avalonia.Media;
using CartonCutter.Application.Algorithm;
using CartonCutter.Application.ExcelParser;
using CartonCutter.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CartonCutter.ViewModels;

public partial class MainViewModel(
    IWindowService windowService,
    IImageService imageService,
    IDragDropFileService dragDropFileService,
    IFileDialogService fileDialogService) : ViewModelBase
{
    private readonly ExcelParserOrder _excelParser = new();
    public ICommand CloseWindow { get; } = ReactiveCommand.Create(windowService.Close);
    public ICommand MinimizeWindow { get; } = ReactiveCommand.Create(windowService.Minimize);
    public ICommand ToggleWindowState { get; } = ReactiveCommand.Create(windowService.ToggleState);
    public ICommand MoveAndDragWindow { get; } = ReactiveCommand.Create<PointerPressedEventArgs>(windowService.MoveAndDrag);
    public IImage ToggleWindowStateIcon { get; } = imageService.SetImage();
    public ICommand DragOverFile { get; } = ReactiveCommand.Create<DragEventArgs>(dragDropFileService.DragOver);
    public string Threshold { get; set; } = "12";

    [ObservableProperty] 
    private bool _isLoading;

    [RelayCommand]
    public async Task DropFile(DragEventArgs e)
    {
        await using var uploadStream = await dragDropFileService.DropAsync(e);
        if (uploadStream == null) 
            return;
        
        await ExecuteAlgorithm(uploadStream);
    }

    [RelayCommand]
    public async Task OpenUploadDialog()
    {
        await using var uploadStream = await fileDialogService.OpenFileUploadDialogAsync();
        if (uploadStream == null) 
            return;
        
        await ExecuteAlgorithm(uploadStream);
    }
    
    private async Task ExecuteAlgorithm(Stream uploadStream)
    {
        var values = _excelParser.Open(uploadStream).Parse().Values;
        if (values == null) 
            return;

        IsLoading = true;
        var algorithm = new Algorithm(values, int.Parse(Threshold));
        var result = await algorithm.Solve();
        _excelParser.UpdateValuesBySorted(result);
        IsLoading = false;

        await using var downloadStream = await fileDialogService.OpenFileDownloadDialogAsync();
        if (downloadStream == null) 
            return;

        _excelParser.Create().Fill().SaveInFile(downloadStream);
    }
}