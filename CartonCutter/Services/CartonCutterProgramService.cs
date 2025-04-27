using System.IO;
using System.Threading.Tasks;
using CartonCutter.Application.Algorithm;
using CartonCutter.Application.ExcelParser;
using CartonCutter.Services.Interfaces;

namespace CartonCutter.Services;

public class CartonCutterProgramService: IProgramService<ExcelParserOrder>
{
    private readonly ExcelParserOrder _excelParser = new();
    
    public Task<ExcelParserOrder>? ExecuteProgramWithFile(Stream stream)
    {
        var values = _excelParser.Open(stream).Parse().Values;

        if (values == null)
            return null;

        var algorithm = new Algorithm(values, 12);
        _excelParser.UpdateValuesBySorted(algorithm.Solve());
        return _excelParser;
    }
}