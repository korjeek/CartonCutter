using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CartonCutter.Services.Interfaces;
using CartonCutter.Views;

namespace CartonCutter.Services;

public class ExcelFileDialogService(MainWindow window): IFileDialogService
{
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

    public async Task<Stream?> OpenFileDownloadDialogAsync()
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
            return null;
        
        var stream = await file.OpenWriteAsync();
        return stream;
    }
}