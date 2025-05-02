using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using CartonCutter.Services.Interfaces;

namespace CartonCutter.Services;

public class DragDropFileService: IDragDropFileService
{
    public void DragOver(DragEventArgs eventArgs) => 
        eventArgs.DragEffects = eventArgs.Data.Contains(DataFormats.Files) 
        ? DragDropEffects.Copy 
        : DragDropEffects.None;

    public async Task<Stream?> DropAsync(DragEventArgs eventArgs)
    {
        var files = eventArgs.Data.GetFiles();

        var item = files?.FirstOrDefault();
        if (item is not IStorageFile file)
            return null;

        return await file.OpenReadAsync();
    }
}