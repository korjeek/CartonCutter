using NPOI.SS.UserModel;

namespace CartonCutter.Application.Extensions;

public static class CellExtensions
{
    public static ICell SetCellValueFromAnother(this ICell source, ICell cell)
    {
        switch (cell.CellType)
        {
            case CellType.String:
                source.SetCellValue(cell.StringCellValue);
                break;
            case CellType.Numeric:
                if (DateUtil.IsCellDateFormatted(cell))
                {
                    source.SetCellValue(cell.DateOnlyCellValue.Value);
                }
                else
                {
                    source.SetCellValue(cell.NumericCellValue);
                }
                break;
            case CellType.Boolean:
                source.SetCellValue(cell.BooleanCellValue);
                break;
            case CellType.Formula:
                source.SetCellFormula(cell.CellFormula);
                break;
            case CellType.Blank:
                source.SetCellType(CellType.Blank);
                break;
            case CellType.Error:
                source.SetCellErrorValue(source.ErrorCellValue);
                break;
            default:
                source.SetCellType(CellType.Blank);
                break;
        }

        return source;
    }
}