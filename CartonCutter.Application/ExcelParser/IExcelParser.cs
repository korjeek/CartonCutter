namespace CartonCutter.Application.ExcelParser;

public interface IExcelParser<TValue>
{
    TValue[] Values { get; set; }
    
    IExcelParser<TValue> Create();
    
    IExcelParser<TValue> Open(Stream fileStream);
}