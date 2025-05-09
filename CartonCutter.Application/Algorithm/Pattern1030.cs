namespace CartonCutter.Application.Algorithm;

public class Pattern1030(int length) : Pattern(1030)
{
    public const int MaxWidth = 1030;
    public int Length { get; } = length;

    public int ProductTimesAmount { get; set; }

    public void FillOrdersAmountById()
    {
        foreach (var (id, _) in Production)
            OrdersAmountById.Add(id, ProductTimesAmount);
    }

    public override string ToString()
    {
        var prodStr = string.Join(", ", Production.Select(kv => $"Order {kv.Key}: {kv.Value}"));
        return
            $"Pattern1030 (Length: {Length}) -> [{prodStr}], Amount = {ProductTimesAmount}, Waste = {Waste}, Waste percentage = {WastePercentage}%";
    }
}