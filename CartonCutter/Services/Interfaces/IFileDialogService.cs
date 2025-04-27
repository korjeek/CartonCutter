using System.IO;
using System.Threading.Tasks;

namespace CartonCutter.Services.Interfaces;

public interface IFileDialogService
{
    Task<Stream?> OpenFileUploadDialogAsync();
    
    Task OpenFileDownloadDialogAsync();
}