using CartonCutter.Application.Algorithm;
using CartonCutter.Application.Extensions;
using CartonCutter.Domain.Extensions;
using CartonCutter.Domain.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CartonCutter.Application.ExcelParser;

public class ExcelParserOrder
{
    public Order[]? Values { get; private set; } = [];
    private XSSFWorkbook? Workbook { get; set; }
    private IRow? Header { get; set; }

    public ExcelParserOrder Create()
    {
        Workbook = new XSSFWorkbook();
        return this;
    }

    public ExcelParserOrder Fill()
    {
        if (Values is null || Workbook is null)
            return this;

        var curRow = 0;
        var sheet = Workbook.CreateSheet("Sheet1");

        sheet.CreateRow(curRow++).FillHeader(Header);
        foreach (var order in Values)
            sheet.CreateRow(curRow++).FillRow(order);

        return this;
    }

    public ExcelParserOrder Open(Stream fileStream)
    {
        Workbook = new XSSFWorkbook(fileStream, true);
        return this;
    }

    public ExcelParserOrder Parse()
    {
        if (Workbook is null || Workbook.NumberOfSheets == 0)
            return this;

        Values = Workbook
            .GetSheetAt(0)
            .Skip(1)
            .Where(row => row.IsValid())
            .Select((row, i) => row.Cells.GetOrder(i, row.LastCellNum))
            .ToArray();

        Header = Workbook.GetSheetAt(0).FirstOrDefault();

        return this;
    }

    public void UpdateValuesBySorted(List<Pattern> patterns)
    {
        var newValues = new List<Order>();
        foreach (var pattern in patterns)
        {
            foreach (var (orderId, amount) in pattern) 
                for (var i = 0; i < amount; i++)
                    newValues.AddOrderWithNewAmount(Values!.GetOrderById(orderId), orderId, pattern.OrdersAmountById[orderId]);

            newValues.AddEmptyOrder();
        }

        Values = newValues.ToArray();
    }

    public void SaveInFile(Stream fileStream) => Workbook?.Write(fileStream);
}