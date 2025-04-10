using NPOI.SS.UserModel;

namespace CartonCutter.Domain.Models;

public static class ListCellsExtensions
{
    public static Order GetOrder(this List<ICell> cells)
    {
        var orderForm = new OrderForm(cells.Count);
        foreach (var cell in cells)
            orderForm.AddFieldAt(cell, cell.ColumnIndex);

        return orderForm.GetOrder();
    }
    // TODO: обработать пустые ячейки с числами
    private class OrderForm(int fieldsCount)
    {
        private ICell[] OrderFields { get; } = new ICell[fieldsCount];

        public void AddFieldAt(ICell field, int index) => OrderFields[index] = field;

        public Order GetOrder()
        {
            return new Order(
                OrderFields[0].StringCellValue,
                OrderFields[1].StringCellValue,
                OrderFields[2].CellType is CellType.Numeric ? 
                    ((int)OrderFields[2].NumericCellValue).ToString() : OrderFields[2].StringCellValue,
                OrderFields[3].StringCellValue,
                OrderFields[4].NumericCellValue is 0 ? null : (int)OrderFields[4].NumericCellValue,
                OrderFields[5].NumericCellValue is 0 ? null : (int)OrderFields[5].NumericCellValue,
                OrderFields[6].NumericCellValue is 0 ? null : (int)OrderFields[6].NumericCellValue,
                DateOnly.Parse(OrderFields[7].StringCellValue),
                (int)OrderFields[8].NumericCellValue,
                (int)OrderFields[9].NumericCellValue,
                OrderFields[10].NumericCellValue is 0 ? null : (int)OrderFields[10].NumericCellValue,
                (int)OrderFields[11].NumericCellValue
            );
        }
    }
}