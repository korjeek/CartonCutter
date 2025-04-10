using NPOI.SS.UserModel;

namespace CartonCutter.Domain.Models;

public class TableView(IRow header)
{
    public IRow Header { get; } = header;

    public Order[] Orders { get; set; }
}