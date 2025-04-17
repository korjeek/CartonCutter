using NPOI.SS.UserModel;

namespace CartonCutter.Domain.Models;

public class OrderForm(int fieldsCount)
{
    private ICell[] OrderFields { get; } = new ICell[fieldsCount];
    
    public void AddFieldAt(ICell field, int index) => OrderFields[index] = field;
    
    public Order GetOrder()
    {
        var formatter = new DataFormatter();
        return new Order(
            OrderFields[0].RowIndex,
            formatter.FormatCellValue(OrderFields[0]),
            formatter.FormatCellValue(OrderFields[0]),
            formatter.FormatCellValue(OrderFields[2]),
            formatter.FormatCellValue(OrderFields[3]),
            formatter.FormatCellValue(OrderFields[4]),
            formatter.FormatCellValue(OrderFields[5]),
            formatter.FormatCellValue(OrderFields[6]),
            DateOnly.Parse(OrderFields[7].StringCellValue),
            (int)OrderFields[8].NumericCellValue,
            (int)OrderFields[9].NumericCellValue,
            formatter.FormatCellValue(OrderFields[10]),
            (int)OrderFields[11].NumericCellValue
        );
    }
}
