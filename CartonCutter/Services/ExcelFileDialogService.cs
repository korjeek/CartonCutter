using Avalonia.Platform.Storage;
using CartonCutter.Application.ExcelParser;
using CartonCutter.Services.Interfaces;
using CartonCutter.Views;
using CartonCutter.Application.Algorithm;

namespace CartonCutter.Services;

public class ExcelFileDialogService(MainWindow window): IFileDialogService
{
    private readonly ExcelParserOrder _excelParser = new();
    
    public async Task<Stream?> OpenFileUploadDialogAsync()
    {
        var filePickerOptions = new FilePickerOpenOptions
        {
            Title = "Выберите Excel файл",
            AllowMultiple = false,
            FileTypeFilter =
            [
                new FilePickerFileType("Excel Files")
                {
                    Patterns = ["*.xlsx", "*.xls"]
                }
            ]
        };
            
        var file = await window.StorageProvider.OpenFilePickerAsync(filePickerOptions);
        if (file.Count == 0) 
            return null;
        
        return await file[0].OpenReadAsync();
    }

    public async Task OpenFileDownloadDialogAsync()
    {
        var saveFileDialog = new FilePickerSaveOptions
        {
            Title = "Сохранения Excel файл",
            SuggestedFileName = "file.xlsx",
            FileTypeChoices =
            [
                new FilePickerFileType("Excel Files")
                {
                    Patterns = ["*.xlsx", "*.xls"]
                }
            ]
        };

        var file = await window.StorageProvider.SaveFilePickerAsync(saveFileDialog);
        if (file is null)
            return;
        
        var stream = await file.OpenWriteAsync();
        _excelParser.Create().Fill().SaveInFile(stream);
    }

    public async Task ExecuteProgram(Stream stream)
    {
        var values = _excelParser.Open(stream).Parse().Values;

        if (values == null)
            return null;

        var algorithm = new Algorithm(values, 12);
        _excelParser.UpdateValuesBySorted(algorithm.Solve());
    }
}