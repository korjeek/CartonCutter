using System.Collections;

namespace CartonCutter.Application.Algorithm;

public class Pattern1380 : Pattern
{
    public const int MaxWidth = 1380;
    public int BigLength { get; }
    public int SmallLength { get; }
    
    public int ProductBigTimesAmount { get; set; }
    public int ProductSmallTimesAmount { get; set; }

    public Pattern1380(int bigLength, int smallLength) : base(1380)
    {
        if (bigLength < smallLength)
            throw new ArgumentException("The first length must be bigger than second.");
        BigLength = bigLength;
        SmallLength = smallLength;
    }
    
    public override string ToString()
    {
        var prodStr = string.Join(", ", Production.Select(kv => $"Order {kv.Key}: {kv.Value}"));
        return $"Pattern1380 (Lengths: {BigLength}, {SmallLength}) -> [{prodStr}], BigAmount = {ProductBigTimesAmount}, SmallAmount = {ProductSmallTimesAmount}, Waste = {Waste}, Waste percentage = {Waste / MaxWidth * 100}%";
    }
}