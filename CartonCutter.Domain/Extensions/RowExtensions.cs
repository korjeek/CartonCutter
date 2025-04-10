using CartonCutter.Domain.Models;
using NPOI.SS.UserModel;

namespace CartonCutter.Domain.Extensions;

public static class RowExtensions
{
    public static void FillRow(this IRow row, IRow header)
    {
        if (header.FirstCellNum != 0)
            row.CreateCell(0).SetBlank();
        foreach (var cell in header.Cells)
            row.CreateCell(cell.ColumnIndex).SetCellValue(cell.StringCellValue);
    }

    public static void FillRow(this IRow row, Order order)
    {
        row.CreateCell(0).SetCellValue(order.CustomerName);
        row.CreateCell(1).SetCellValue(order.Nomenclature);
        row.CreateCell(2).SetCellValue(order.Characteristic);
        row.CreateCell(3).SetCellValue(order.Mark);
        row.CreateCell(4).SetCellValue(order.Length);
        row.CreateCell(5).SetCellValue(order.Width);
        row.CreateCell(6).SetCellValue(order.Height);
        row.CreateCell(7).SetCellValue(order.ShippingDate.ToString());
        row.CreateCell(8).SetCellValue(order.WorkPieceLength);
        row.CreateCell(9).SetCellValue(order.WorkPieceWidth);
        row.CreateCell(10).SetCellValue(order.AmountProductsOnStamp);
        row.CreateCell(11).SetCellValue(order.Amount);
    }

    public static bool IsValid(this IRow row)
    {
        if (row.GetCell(3).CellType is CellType.Blank)
            return false;
        if (row.GetCell(7).CellType is CellType.Blank)
            return false;
        if (row.GetCell(8).CellType is CellType.Blank)
            return false;
        if (row.GetCell(9).CellType is CellType.Blank)
            return false;
        if (row.GetCell(11).CellType is CellType.Blank)
            return false; // Переделать на Any???
        return true;
    }
}