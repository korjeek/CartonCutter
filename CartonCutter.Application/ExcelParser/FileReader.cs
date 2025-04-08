using System.Globalization;
using CartonCutter.Domain.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CartonCutter.Application.ExcelParser;

public static class FileReader
{
    public static Order[] GetOrderForms(string fileLocation)
    {
        var orderForms = new OrderForms();
        using var fileStream = new FileStream(fileLocation, FileMode.Open, FileAccess.Read);
        // TODO: если файл открыт, выбрасывать исключение, сообщать пользователю, что нужно закрыть файл
        
        var workbook = new XSSFWorkbook(fileStream);
        var sheet = workbook.GetSheetAt(0);
        foreach (IRow row in sheet)
        {
            var cells = row.Cells;
            var orderForm = new OrderForm();
            
            foreach (var cell in cells)
            {
                if (cell.CellType is CellType.String)
                    orderForm.OrderFields[cell.ColumnIndex] = cell.StringCellValue;
                else
                    orderForm.OrderFields[cell.ColumnIndex] = 
                        cell.NumericCellValue.ToString(CultureInfo.InvariantCulture);
            }

            orderForms.Add(orderForm);
        }

        return orderForms.GetOrders();
    }

    public static void WriteOrdersToXlsxFile(Order[] orders)
    {
        var workbook = new XSSFWorkbook();

        var sheet = workbook.CreateSheet("Sheet1");

        foreach (var order in orders)
        {
            var row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue(order.CustomerName);
            row.CreateCell(1).SetCellValue(order.Nomenclature);
            row.CreateCell(2).SetCellValue(order.Characteristic);
            row.CreateCell(3).SetCellValue(order.Mark);
            row.CreateCell(4).SetCellValue(order.Length);
            row.CreateCell(5).SetCellValue(order.Width);
            row.CreateCell(6).SetCellValue(order.Height);
            row.CreateCell(7).SetCellValue(order.ShippingDate);
            row.CreateCell(8).SetCellValue(order.WorkPieceLength);
            row.CreateCell(9).SetCellValue(order.WorkPieceWidth);
            row.CreateCell(10).SetCellValue(order.AmountProductsOnStamp);
            row.CreateCell(11).SetCellValue(order.Amount);
        }

        using var fileStream = new FileStream("./file.xlsx", FileMode.Create);
        workbook.Write(fileStream);
    }
}
