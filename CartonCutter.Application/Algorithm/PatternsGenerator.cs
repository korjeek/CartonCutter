using CartonCutter.Domain.Models;

namespace CartonCutter.Application.Algorithm;

public static class PatternsGenerator
{
    private static readonly Dictionary<int, List<Order>> OrdersByWidth = new();
    private static readonly Dictionary<int, List<Order>> OrdersByLength = new();

    public static void PrepareOrders(Order[] orders)
    {
        foreach (var order in orders)
        {
            if (!OrdersByWidth.TryAdd(order.WorkPieceWidth, [order]))
                OrdersByWidth[order.WorkPieceWidth].Add(order);
            if (!OrdersByLength.TryAdd(order.WorkPieceLength, [order]))
                OrdersByLength[order.WorkPieceLength].Add(order);
        }
        
        //... Что то ещё, пока непонятно
    }

    public static Pattern[] GeneratePatterns()
    {
        throw new NotImplementedException();
    }
}