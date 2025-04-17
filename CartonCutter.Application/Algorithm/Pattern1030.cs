using System.Collections;

namespace CartonCutter.Application.Algorithm;

public class Pattern1030(int patternLength) : Pattern(1030), IEnumerable<(int orderId, int count)>
{
    public int PatternLength { get; } = patternLength;
    
    public int ProductTimesCount { get; set; }
    
    public void AddOrderIdInProduction(int orderId)
    {
        if (!Production.TryAdd(orderId, 1))
            Production[orderId] += 1;
    }
    
    public bool GetOrderCountById(int orderId, out int orderInPatternCount)
    {
        var b = Production.TryGetValue(orderId, out var v);
        orderInPatternCount = v;
        return b;
    }
}