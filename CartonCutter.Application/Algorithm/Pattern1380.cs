using System.Collections;

namespace CartonCutter.Application.Algorithm;

public class Pattern1380 : Pattern
{
    public const int MaxWidth = 1380;

    public int PatternBigLength { get; }
    public int PatternSmallLength { get; }
    public int ProductBigTimesCount { get; set; }
    public int ProductSmallTimesCount { get; set; }

    public Pattern1380(int patternBigLength, int patternSmallLength) : base(1380)
    {
        if (patternBigLength < patternSmallLength)
            throw new ArgumentException("The first length must be bigger than second.");
        PatternBigLength = patternBigLength;
        PatternSmallLength = patternSmallLength;
    }
}