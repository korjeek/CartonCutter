using System.Collections;

namespace CartonCutter.Application.Algorithm;

public class Pattern1030 : Pattern
{
    public const int MaxWidth = 1030;
    public int Length { get; }

    public int ProductTimesAmount { get; set; }

    public Pattern1030(int length) : base(1030)
    {
        Length = length;
    }

    public override string ToString()
    {
        var prodStr = string.Join(", ", Production.Select(kv => $"Order {kv.Key}: {kv.Value}"));
        return
            $"Pattern1030 (Length: {Length}) -> [{prodStr}], Amount = {ProductTimesAmount}, Waste = {Waste}, Waste percentage = {Waste / MaxWidth * 100}%";
    }
}