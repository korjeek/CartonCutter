namespace CartonCutter.Application;

public class FileHandler
{
    public async void ReadFileFromPath(string[]? filePath)
    {
        if (filePath == null || filePath.Length == 0)
            // TODO: Какое ни будь исключение
            return;

        var a = await File.ReadAllBytesAsync(filePath[0]);
    }
}