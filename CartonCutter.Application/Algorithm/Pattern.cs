using System.Collections;

namespace CartonCutter.Application.Algorithm;

public class Pattern(int maxWidth) : IEnumerable<(int orderId, int count)>
{
    public readonly int MaxWidth = maxWidth;
    protected readonly Dictionary<int, int> Production = new();
    public double Waste { get; private set; }
    public double WastePercentage { get; private set; }

    public void CalculateWaste(int totalWidth)
    {
        Waste = MaxWidth - totalWidth;
        WastePercentage = Waste / totalWidth * 100;
    }

    public IEnumerator<(int orderId, int count)> GetEnumerator()
    {
        foreach (var (k ,v) in Production)
            for (var i = 0; i < v; i++)
                yield return (k, v);
    }

    public override string ToString()
    {
        var prodStr = string.Join(", ", Production.Select(kv => $"Order {kv.Key}: {kv.Value}"));
        return $"Pattern (Length: {Production}) -> [{prodStr}], Waste = {Waste}, Waste percentage = {Waste / MaxWidth * 100}%";
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}