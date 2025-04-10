using CartonCutter.Domain.Extensions;
using CartonCutter.Domain.Models;
using NPOI.XSSF.UserModel;

namespace CartonCutter.Application.ExcelParser;

// TODO: async / fluentAPI / 
public static class ExcelParser
{
    public static Order[] GetOrders(Stream fileStream)
    {
        // using var fileStream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read);
        // TODO: если файл открыт, выбрасывать исключение, сообщать пользователю, что нужно закрыть файл
        
        var workbook = new XSSFWorkbook(fileStream);
        var sheet = workbook.GetSheetAt(workbook.NumberOfSheets - 1);

        return sheet.Skip(1).Select(row => row.Cells.GetOrder()).ToArray();
    }

    // TODO: сюда так же будет передаваться путь, где надо сохранить файл
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
