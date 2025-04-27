using Avalonia.Platform.Storage;
using CartonCutter.Application.ExcelParser;
using CartonCutter.Services.Interfaces;
using CartonCutter.Views;
using CartonCutter.Application.Algorithm;

namespace CartonCutter.Services;

public class ExcelFileDialogService(MainWindow window): IFileDialogService
{
    private readonly ExcelParserOrder _excelParser = new();
    
    public async void OpenFileUploadDialog()
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
            return;

        var fileStream = await file[0].OpenReadAsync();
        var values = _excelParser.Open(fileStream).Parse().Values;

        var algorithm = new Algorithm(values, 15);
        _excelParser.UpdateValuesBySorted(algorithm.Solve());
    }

    public async void OpenFileDownloadDialog()
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
}