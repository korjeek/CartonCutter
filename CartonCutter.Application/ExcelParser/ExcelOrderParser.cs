using CartonCutter.Domain.Extensions;
using CartonCutter.Domain.Interfaces;
using CartonCutter.Domain.Models;
using NPOI.XSSF.UserModel;

namespace CartonCutter.Application.ExcelParser;

public class ExcelOrderParser: IExcelParser<Order>
{
    private XSSFWorkbook workbook;
    public Order[]? Values { get; set; }
    
    public IExcelParser<Order> Create()
    {
        workbook = new XSSFWorkbook();
        return this;
    }

    public IExcelParser<Order> Fill()
    {
        if (Values is null)
            return this;
        
        var curRow = 0;
        var sheet = workbook.CreateSheet("Sheet1");
        foreach (var value in Values)
        {
            var row = sheet.CreateRow(curRow++);
            row.CreateCell(0).SetCellValue(order.CustomerName);
            row.CreateCell(1).SetCellValue(order.Nomenclature);
            row.CreateCell(2).SetCellValue(order.Characteristic);
            row.CreateCell(3).SetCellValue(order.Mark);
            row.CreateCell(4).SetCellValue(order.Length.GetValueOrDefault());
            row.CreateCell(5).SetCellValue(order.Width.GetValueOrDefault());
            row.CreateCell(6).SetCellValue(order.Height.GetValueOrDefault());
            row.CreateCell(7).SetCellValue(order.ShippingDate.ToString());
            row.CreateCell(8).SetCellValue(order.WorkPieceLength);
            row.CreateCell(9).SetCellValue(order.WorkPieceWidth);
            row.CreateCell(10).SetCellValue(order.AmountProductsOnStamp.GetValueOrDefault());
            row.CreateCell(11).SetCellValue(order.Amount);
        }

        return this;
    }

    public IExcelParser<TValue> Open(Stream fileStream)
    {
        workbook = new XSSFWorkbook(fileStream, true);
        return this;
    }

    public IExcelParser<TValue> Parse()
    {
        if (workbook.NumberOfSheets == 0)
            return this;
            
        Values = workbook
            .GetSheetAt(0)
            .Skip(1)
            .Select(row => row.Cells.GetOrder<TValue>())
            .ToArray();
        return this;
    }

    public TValue[] GetValues() => Values ?? [];
    
    public static Order[] GetOrders(Stream fileStream)
    {
        // using var fileStream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read);
        // TODO: если файл открыт, выбрасывать исключение, сообщать пользователю, что нужно закрыть файл
        
        var workbook = new XSSFWorkbook(fileStream);
        var sheet = workbook.GetSheetAt(workbook.NumberOfSheets - 1);

        return sheet.Skip(1).Select(row => row.Cells.GetOrder()).ToArray();
    }

    public static void WriteOrdersToXlsxFile(Order[] orders)
    {
        var workbook = new XSSFWorkbook();

        var sheet = workbook.CreateSheet("Sheet1");
        var curRow = 0;

        foreach (var order in orders)
        {
            var row = sheet.CreateRow(curRow++);
            row.CreateCell(0).SetCellValue(order.CustomerName);
            row.CreateCell(1).SetCellValue(order.Nomenclature);
            row.CreateCell(2).SetCellValue(order.Characteristic);
            row.CreateCell(3).SetCellValue(order.Mark);
            row.CreateCell(4).SetCellValue(order.Length.GetValueOrDefault());
            row.CreateCell(5).SetCellValue(order.Width.GetValueOrDefault());
            row.CreateCell(6).SetCellValue(order.Height.GetValueOrDefault());
            row.CreateCell(7).SetCellValue(order.ShippingDate.ToString());
            row.CreateCell(8).SetCellValue(order.WorkPieceLength);
            row.CreateCell(9).SetCellValue(order.WorkPieceWidth);
            row.CreateCell(10).SetCellValue(order.AmountProductsOnStamp.GetValueOrDefault());
            row.CreateCell(11).SetCellValue(order.Amount);
        }

        using var fileStream = new FileStream("file.xlsx", FileMode.Create);
        workbook.Write(fileStream);
    }
}
