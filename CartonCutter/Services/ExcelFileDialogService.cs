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
        if (file.Count == 0) 
            return;

        var table = ExcelParser.GetOrders(await file[0].OpenReadAsync());
        // сортировка table.Orders = Sort(table.Orders);
        ExcelParser.WriteOrdersToXlsxFile(table);
    }
}