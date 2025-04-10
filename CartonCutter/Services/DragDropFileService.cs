using System;
using System.Linq;
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

    public void Drop(DragEventArgs eventArgs)
    {
        var files = eventArgs.Data.GetFiles();

        if (files == null)
            return;
        
        var filePath = files.FirstOrDefault()?.TryGetLocalPath();
        if (!string.IsNullOrEmpty(filePath))
            Console.WriteLine("Good!");
    }
}