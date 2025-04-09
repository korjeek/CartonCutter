using System;
using Avalonia.Platform.Storage;
using CartonCutter.Application.ExcelParser;
using CartonCutter.Services.Interfaces;
using CartonCutter.Views;

namespace CartonCutter.Services;

public class ExcelFileDialogService(MainWindow window): IFileDialogService
{
    public async void OpenFileDownloadDialog()
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

        if (file.Count != 0)
        {
            var orders = ExcelParser.GetOrders(await file[0].OpenReadAsync());
            ExcelParser.WriteOrdersToXlsxFile(orders);
        }
    }
}