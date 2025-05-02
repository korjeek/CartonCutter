using System.IO;
using System.Threading.Tasks;
using Avalonia.Input;

namespace CartonCutter.Services.Interfaces;

public interface IDragDropFileService
{
    void DragOver(DragEventArgs e);
    
    Task<Stream?> DropAsync(DragEventArgs e);
}