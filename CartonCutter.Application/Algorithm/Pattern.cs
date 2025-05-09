using System.Collections;
using CartonCutter.Domain.Models;

namespace CartonCutter.Application.Algorithm;

public class Pattern(int maxWidth) : IEnumerable<(int orderId, int orderIdCount)>
{
    protected readonly Dictionary<int, int> Production = new();

    public Dictionary<int, int> OrdersAmountById { get; } = new();
    public TimeSpan TimeToShip { get; set; }
    public int TotalLinesCount { get; private set; }
    public double Waste { get; private set; }
    public double WastePercentage { get; private set; }
    
    public void CalculateWaste(int totalWidth)
    {
        Waste = maxWidth - totalWidth;
        WastePercentage = Waste / maxWidth * 100;
    }

    public void CountLines()
    {
        TotalLinesCount = Production.Sum(kv => kv.Value);
    }

    public void AddOrderIdInProduction(int orderId)
    {
        if (!Production.TryAdd(orderId, 1))
            Production[orderId] += 1;
    }

    public bool TryGetOrderCountById(int orderId, out int orderInPatternCount)
    {
        var b = Production.TryGetValue(orderId, out var v);
        orderInPatternCount = v;
        return b;
    }

    public IEnumerator<(int orderId, int orderIdCount)> GetEnumerator()
    {
        foreach (var (k, v) in Production) 
            yield return (k, v);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}