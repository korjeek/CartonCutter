using NPOI.SS.UserModel;

namespace CartonCutter.Domain.Extensions;

public static class CellExtensions
{
    public static ICell SetCellValue(this ICell cell, int? value)
        => value is null ? cell.SetBlank() : cell.SetCellValue(int.Parse(value.ToString()!));
}