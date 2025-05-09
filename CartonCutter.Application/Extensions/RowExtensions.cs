using CartonCutter.Domain.Extensions;
using CartonCutter.Domain.Models;
using NPOI.SS.UserModel;

namespace CartonCutter.Application.Extensions;

public static class RowExtensions
{
    private static readonly int[] NotBlankCells = [3, 7, 8, 9, 11];
    
    public static void FillRow(this IRow row, Order order)
    {
        if (order.Id == -1)
        {
            row.CreateCell(0).SetBlank();
            return;
        }
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

    public static void FillHeader(this IRow row, IRow? header)
    {
        if (header == null)
            return;
        
        for (var i = 0; i < header.Cells.Count; i++)
        {
            var j = i;
            while (j++ < header.Cells[i].ColumnIndex)
                row.CreateCell(j).SetBlank();

            row.CreateCell(header.Cells[i].ColumnIndex).SetCellValueFromAnother(header.Cells[i]);
        }
    }
    
    public static bool IsValid(this IRow row) =>
        NotBlankCells.All(index => row.GetCell(index) is { CellType: not CellType.Blank });
}