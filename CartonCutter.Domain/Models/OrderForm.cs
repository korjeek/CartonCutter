using NPOI.SS.UserModel;

namespace CartonCutter.Domain.Models;

public class OrderForm(int fieldsCount)
{
    private ICell[] OrderFields { get; } = new ICell[fieldsCount];

    public void AddFieldAt(ICell field, int index) => OrderFields[index] = field;
    
    //TODO: переделай
    public Order GetOrder() => 
        new(
            OrderFields[0].StringCellValue,
            OrderFields[1].StringCellValue,
            OrderFields[2].CellType is CellType.Numeric ? 
                ((int)OrderFields[2].NumericCellValue).ToString() : OrderFields[2].StringCellValue,
            OrderFields[3].StringCellValue,
            OrderFields[4].CellType is CellType.Blank ? null : (int)OrderFields[4].NumericCellValue,
            OrderFields[5].CellType is CellType.Blank ? null : (int)OrderFields[5].NumericCellValue,
            OrderFields[6].CellType is CellType.Blank ? null : (int)OrderFields[6].NumericCellValue,
            DateOnly.Parse(OrderFields[7].StringCellValue),
            (int)OrderFields[8].NumericCellValue,
            (int)OrderFields[9].NumericCellValue,
            OrderFields[10].CellType is CellType.Blank ? null : (int)OrderFields[10].NumericCellValue,
            (int)OrderFields[11].NumericCellValue
        );
}
