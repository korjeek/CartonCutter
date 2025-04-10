using CartonCutter.Domain.Extensions;
using CartonCutter.Domain.Models;
using NPOI.XSSF.UserModel;

namespace CartonCutter.Application.ExcelParser;

// TODO: async / fluentAPI / 
public static class ExcelParser
{
    public static TableView GetOrders(Stream fileStream)
    {
        // TODO: если файл открыт, выбрасывать исключение, сообщать пользователю, что нужно закрыть файл
        
        var workbook = new XSSFWorkbook(fileStream);
        var sheet = workbook.GetSheetAt(workbook.NumberOfSheets - 1);
        var tableView = new TableView(sheet.First())
        {
            Orders = sheet.Skip(1)
                .Where(x => x.IsValid())
                .Select(row => row.Cells.GetOrder()).ToArray()
        };

        return tableView;
    }

    // TODO: сюда так же будет передаваться путь, где надо сохранить файл
    public static void WriteOrdersToXlsxFile(TableView table)
    {
        var workbook = new XSSFWorkbook();

        var sheet = workbook.CreateSheet("Sheet1");
        var curRow = 0;
        sheet.CreateRow(curRow++).FillRow(table.Header);
        
        foreach (var order in table.Orders)
            sheet.CreateRow(curRow++).FillRow(order);

        using var fileStream = new FileStream("file.xlsx", FileMode.Create);
        workbook.Write(fileStream);
    }
}
