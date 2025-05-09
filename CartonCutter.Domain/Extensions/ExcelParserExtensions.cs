using CartonCutter.Domain.Models;
using NPOI.SS.UserModel;

namespace CartonCutter.Domain.Extensions;

public static class ExcelParserExtensions
{
    /// <summary>
    /// Extension method created for working with Excel tables. Creates an <see cref="Order"/> object from a row in the
    /// table.
    /// </summary>
    /// <param name="cells">List of Excel table cells from which the <see cref="Order"/> will be created</param>
    /// <param name="rowNumber">This variable need to correct indexing Orders</param>
    /// <param name="rowCapacity">This variable need to correct indexing cells</param>
    /// <returns>Object of type <see cref="Order"/></returns>
    public static Order GetOrder(this List<ICell> cells, int rowNumber, int rowCapacity)
    {
        var orderForm = new OrderForm(rowCapacity);
        foreach (var cell in cells)
            orderForm.AddFieldAt(cell, cell.ColumnIndex);

        return orderForm.GetOrder(rowNumber + 1);
    }
}