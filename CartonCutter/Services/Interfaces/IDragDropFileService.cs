using Avalonia.Input;

namespace CartonCutter.Services.Interfaces;

public interface IDragDropFileService
{
    void DragOver(DragEventArgs e);
    
    void Drop(DragEventArgs e);
}